using System;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Logging;
using Schwefel.Ruthenium.Logging;

namespace Schwefel.Ruthenium.Security.Hash.Computer {
    public class HashCalculatorBase<TCryptoserviceProvider>
        : IHashComputerWithCryptoserviceProvider<TCryptoserviceProvider> where TCryptoserviceProvider : HashAlgorithm {

        #region constants

        private static readonly ILogger _Logger =
            LoggingHelper.CreateLogger<HashCalculatorBase<TCryptoserviceProvider>>();

        #endregion constants

        #region constrcutors

        protected internal HashCalculatorBase(
            TCryptoserviceProvider cryptoserviceProvider
            ,
            Encoding               encoding
            ,                      
            bool                   removeSeperators
            ,                      
            ushort?                maxLenght
            ,                      
            string                 startSalt
            ,                      
            string                 endSalt
            )
        {
            CryptoserviceProvider = cryptoserviceProvider;

            if(encoding == null)
                throw new ArgumentNullException(nameof(encoding));

            DefaultEncoding = encoding;
            RemoveSeperators = removeSeperators;
            StartSalt = startSalt;
            EndSalt = endSalt;
            MaxLenght = maxLenght ?? CalculateDefaultMaxLenght();
        }

        #endregion

        #region properties

        public Encoding DefaultEncoding
        {
            get;
        }

        public bool RemoveSeperators
        {
            get;
        }

        /// <inheritdoc />
        public string StartSalt { get; }

        /// <inheritdoc />
        public string EndSalt { get; }

        public ushort MaxLenght
        {
            get;
        }

        #endregion properties

        #region fields

        protected Func<string, string> AfterCalculateHashDelegate = null;
        protected Func<string, string> BeforeCalculateHashDelegate = null;
        protected readonly TCryptoserviceProvider CryptoserviceProvider;

        #endregion fields

        #region methods

        protected static string Invert(string input)
        {
            string result = string.Empty;

            for(int index = input.Length - 1; index >= 0; index--)
            {
                result += input[index];
            }
            return result;
        }

        protected static string RemoveSeperator(string input, string seperator = "-")
        {
            _Logger.LogDebug($"{nameof(RemoveSeperator)} - {nameof(input)}: \"{input}\" {nameof(seperator)}: \"{seperator}\"");

            if(string.IsNullOrEmpty(seperator))
                throw new ArgumentNullException(nameof(seperator));
            if(input == null)
            {
                _Logger.LogInformation($"{nameof(RemoveSeperator)} - {nameof(input)} is null");
                return null;
            }
            if(!input.Contains(seperator))
            {
                _Logger.LogInformation($"{nameof(RemoveSeperator)} - {nameof(seperator)}: {seperator} not found at {nameof(input)}: \"{input}\"");
                return input;
            }
            return input.Replace(seperator, string.Empty);
        }

        public string ComputeHash(string input) {
            _Logger.LogDebug($"{nameof(ComputeHash)} - {nameof(TCryptoserviceProvider)}: {typeof(TCryptoserviceProvider).FullName} {nameof(input)}: \"{input}\"");

            if(input == null)
                throw new ArgumentNullException(nameof(input));

            string lHash             = ComputeHashInternal(
                cryptoProvider:                 CryptoserviceProvider
                , input:                        input
                , startSalt:                    StartSalt
                , endSalt:                      EndSalt
                , maxLenght:                    MaxLenght
                , beforeCalculateHashDelegate:  BeforeCalculateHashDelegate
                , afterCalculateHashDelegate:   AfterCalculateHashDelegate
                , removeSeperators:             RemoveSeperators
            , encoding:                     DefaultEncoding
            );

            return lHash;
        }

        /// <inheritdoc />
        protected ushort CalculateDefaultMaxLenght()
        {
            return CalculateDefaultMaxLenghtInternal(CryptoserviceProvider, DefaultEncoding, RemoveSeperators);
        }

        private static ushort CalculateDefaultMaxLenghtInternal(TCryptoserviceProvider cryptoProvider, Encoding defaultEncoding, bool removeSeperators)
        {
            string lHash = ComputeHashInternal(cryptoProvider, "DEFAULT", defaultEncoding, removeSeperators);

            return (ushort)lHash.Length;
        }

        protected virtual string CombineInputWithSalt(string input)
        {
            string hashInputWithSalt = input;

            if(!String.IsNullOrWhiteSpace(StartSalt))
                hashInputWithSalt = $"{StartSalt}_{hashInputWithSalt}";
            if(!String.IsNullOrWhiteSpace(EndSalt))
                hashInputWithSalt = $"{hashInputWithSalt}_{EndSalt}";

            return hashInputWithSalt;
        }

        private static string CombineInputWithSaltInternal(string input, string startSalt, string endSalt)
        {
            string hashInputWithSalt = input;

            if(!String.IsNullOrWhiteSpace(startSalt))
                hashInputWithSalt = $"{startSalt}_{hashInputWithSalt}";
            if(!String.IsNullOrWhiteSpace(endSalt))
                hashInputWithSalt = $"{hashInputWithSalt}_{endSalt}";

            return hashInputWithSalt;
        }

        public string ComputeHash(TCryptoserviceProvider cryptoProvider, string input) {
            _Logger.LogDebug($"{nameof(ComputeHash)} - {nameof(TCryptoserviceProvider)}: {typeof(TCryptoserviceProvider).FullName} {nameof(input)}: \"{input}\"");

            if(input == null)
                throw new ArgumentNullException(nameof(input));
            if(cryptoProvider == null)
                throw new ArgumentNullException(nameof(cryptoProvider));

            string lHash = ComputeHashInternal(
                cryptoProvider:                 cryptoProvider
                , input:                        input
                , startSalt:                    StartSalt
                , endSalt:                      EndSalt
                , maxLenght:                    MaxLenght
                , beforeCalculateHashDelegate:  BeforeCalculateHashDelegate
                , afterCalculateHashDelegate:   AfterCalculateHashDelegate
                , removeSeperators:             RemoveSeperators
                , encoding:                     DefaultEncoding
                );

            return lHash;
        }

        private static string ComputeHashInternal(
            TCryptoserviceProvider  cryptoProvider,
            string                  input,
            Encoding                encoding,
            bool                    removeSeperators,
            ushort                  maxLenght                   = ushort.MaxValue,
            Func<string, string>    afterCalculateHashDelegate  = null,
            Func<string, string>    beforeCalculateHashDelegate = null,
            string                  startSalt                   = null,
            string                  endSalt                     = null
                                 
            ) {
            _Logger.LogDebug($"{nameof(ComputeHashInternal)} - {nameof(TCryptoserviceProvider)}: {typeof(TCryptoserviceProvider).FullName} {nameof(input)}: \"{input}\"");

            string lInputAfterBeforeCalculateDelegateExecution = beforeCalculateHashDelegate != null
                ? beforeCalculateHashDelegate(input)
                : input;

            if (string.IsNullOrEmpty(lInputAfterBeforeCalculateDelegateExecution))
            {
                _Logger.LogWarning($"The {nameof(input)}: \"{input}\" results after the {beforeCalculateHashDelegate} execution to a null or empty string.");
                throw new ArgumentNullException(nameof(lInputAfterBeforeCalculateDelegateExecution));
            }
            string lInputWithSalt   = CombineInputWithSaltInternal(lInputAfterBeforeCalculateDelegateExecution, startSalt, endSalt);
            byte[] lInputData       = encoding.GetBytes(lInputWithSalt);
            byte[] lInputDataHashed = cryptoProvider.ComputeHash(lInputData);
            string lHashRaw         = BitConverter.ToString(lInputDataHashed);

            if(string.IsNullOrEmpty(lHashRaw))
                _Logger.LogWarning($"{nameof(ComputeHash)} - {nameof(input)}: \"{input}\" {nameof(lInputAfterBeforeCalculateDelegateExecution)}: \"{lInputAfterBeforeCalculateDelegateExecution}\" results a null or empty string");

            string  lHash           = lHashRaw;

            if(removeSeperators)
                lHash = RemoveSeperator(lHash);
            
            if(afterCalculateHashDelegate != null)
                lHash = afterCalculateHashDelegate(lHash);

            if (string.IsNullOrWhiteSpace(lHash))
            {
                _Logger.LogWarning($"{nameof(ComputeHash)} - {nameof(lHashRaw)}: \"{lHashRaw}\" {nameof(AfterCalculateHashDelegate)}: \"{lHash}\" results a null or empty string");
                return null;
            }
            if (lHash.Length > maxLenght + 1)
                lHash = lHash.Substring(0, maxLenght);

            return lHash;
        }

        #endregion methods
    }
}
