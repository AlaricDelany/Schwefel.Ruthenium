using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Schwefel.Ruthenium.Security.Hash.Computer
{
    public class HashCalculatorBase<THashAlgorithm>
        : IHashComputerWithHahAlgorithm<THashAlgorithm> where THashAlgorithm : HashAlgorithm
    {
        #region constrcutors

        protected internal HashCalculatorBase(
            ILoggerFactory loggerFactory
            ,
            THashAlgorithm cryptoserviceProvider
            ,
            Encoding encoding
            ,
            bool removeSeperators
            ,
            int? maxLenght
            ,
            string startSalt
            ,
            string endSalt
            )
        {
            HashAlgorithm = cryptoserviceProvider;
            DefaultEncoding = encoding ?? throw new ArgumentNullException(nameof(encoding));
            RemoveSeperators = removeSeperators;
            StartSalt = startSalt;
            EndSalt = endSalt;
            MaxLenght = maxLenght ?? CalculateDefaultMaxLenght();

            _Logger = loggerFactory.CreateLogger<HashCalculatorBase<THashAlgorithm>>();
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

        public int MaxLenght
        {
            get;
        }

        #endregion properties

        #region fields

        protected Func<string, string> AfterCalculateHashDelegate = null;
        protected Func<string, string> BeforeCalculateHashDelegate = null;
        protected readonly THashAlgorithm HashAlgorithm;
        private readonly ILogger _Logger = null;

        #endregion fields

        #region methods

        protected static string ReverseString(string input)
        {
            return new string(input.Reverse().ToArray());
        }

        protected string RemoveSeperator(string input, string seperator = "-")
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

        public string ComputeHash(string input)
        {
            _Logger.LogDebug($"{nameof(ComputeHash)} - {nameof(THashAlgorithm)}: {typeof(THashAlgorithm).FullName} {nameof(input)}: \"{input}\"");

            if(input == null)
                throw new ArgumentNullException(nameof(input));

            string lHash = ComputeHashInternal(
                cryptoProvider: HashAlgorithm
                , input: input
                , startSalt: StartSalt
                , endSalt: EndSalt
                , maxLenght: MaxLenght
                , beforeCalculateHashDelegate: BeforeCalculateHashDelegate
                , afterCalculateHashDelegate: AfterCalculateHashDelegate
                , removeSeperators: RemoveSeperators
            , encoding: DefaultEncoding
            );

            return lHash;
        }

        /// <inheritdoc />
        protected int CalculateDefaultMaxLenght()
        {
            return CalculateDefaultMaxLenghtInternal(HashAlgorithm, DefaultEncoding, RemoveSeperators);
        }

        private int CalculateDefaultMaxLenghtInternal(THashAlgorithm cryptoProvider, Encoding defaultEncoding, bool removeSeperators)
        {
            string lHash = ComputeHashInternal(cryptoProvider, "DEFAULT", defaultEncoding, removeSeperators);

            return lHash.Length;
        }

        protected virtual string CombineInputWithSalt(string input)
        {
            return CombineInputWithSaltInternal(input, StartSalt, EndSalt);
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

        public string ComputeHash(THashAlgorithm cryptoProvider, string input)
        {
            _Logger.LogDebug($"{nameof(ComputeHash)} - {nameof(THashAlgorithm)}: {typeof(THashAlgorithm).FullName} {nameof(input)}: \"{input}\"");

            if(input == null)
                throw new ArgumentNullException(nameof(input));
            if(cryptoProvider == null)
                throw new ArgumentNullException(nameof(cryptoProvider));

            string lHash = ComputeHashInternal(
                cryptoProvider: cryptoProvider
                , input: input
                , startSalt: StartSalt
                , endSalt: EndSalt
                , maxLenght: MaxLenght
                , beforeCalculateHashDelegate: BeforeCalculateHashDelegate
                , afterCalculateHashDelegate: AfterCalculateHashDelegate
                , removeSeperators: RemoveSeperators
                , encoding: DefaultEncoding
                );

            return lHash;
        }

        private string ComputeHashInternal(
            THashAlgorithm cryptoProvider,
            string input,
            Encoding encoding,
            bool removeSeperators,
            int maxLenght = int.MaxValue,
            Func<string, string> afterCalculateHashDelegate = null,
            Func<string, string> beforeCalculateHashDelegate = null,
            string startSalt = null,
            string endSalt = null

            )
        {
            _Logger.LogDebug($"{nameof(ComputeHashInternal)} - {nameof(THashAlgorithm)}: {typeof(THashAlgorithm).FullName} {nameof(input)}: \"{input}\"");

            string lInputAfterBeforeCalculateDelegateExecution = beforeCalculateHashDelegate != null
                ? beforeCalculateHashDelegate(input)
                : input;

            if(string.IsNullOrEmpty(lInputAfterBeforeCalculateDelegateExecution))
            {
                _Logger.LogWarning($"The {nameof(input)}: \"{input}\" results after the {beforeCalculateHashDelegate} execution to a null or empty string.");
                throw new ArgumentNullException(nameof(lInputAfterBeforeCalculateDelegateExecution));
            }
            string lInputWithSalt = CombineInputWithSaltInternal(lInputAfterBeforeCalculateDelegateExecution, startSalt, endSalt);
            byte[] lInputData = encoding.GetBytes(lInputWithSalt);
            byte[] lInputDataHashed = cryptoProvider.ComputeHash(lInputData);
            string lHashRaw = BitConverter.ToString(lInputDataHashed);

            if(string.IsNullOrEmpty(lHashRaw))
                _Logger.LogWarning($"{nameof(ComputeHash)} - {nameof(input)}: \"{input}\" {nameof(lInputAfterBeforeCalculateDelegateExecution)}: \"{lInputAfterBeforeCalculateDelegateExecution}\" results a null or empty string");

            string lHash = lHashRaw;

            if(removeSeperators)
                lHash = RemoveSeperator(lHash);

            if(afterCalculateHashDelegate != null)
                lHash = afterCalculateHashDelegate(lHash);

            if(string.IsNullOrWhiteSpace(lHash))
            {
                _Logger.LogWarning($"{nameof(ComputeHash)} - {nameof(lHashRaw)}: \"{lHashRaw}\" {nameof(AfterCalculateHashDelegate)}: \"{lHash}\" results a null or empty string");
                return null;
            }
            lHash = TrimToMaxLengh(lHash, maxLenght);

            return lHash;
        }

        protected virtual string TrimToMaxLengh(string hash, int maxLenght)
        {
            if(hash.Length > maxLenght + 1)
                return hash.Substring(0, maxLenght);
            else
                return hash;
        }

        #endregion methods
    }
}
