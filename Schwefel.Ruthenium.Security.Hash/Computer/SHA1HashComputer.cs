using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Logging;

namespace Schwefel.Ruthenium.Security.Hash.Computer
{
    public class Sha1HashComputer : HashCalculatorBase<SHA1>
    {
        public Sha1HashComputer(
            ILoggerFactory loggerFactory,
            Encoding encoding,
            bool     removeSeperators,
            ushort?  maxLenght = null,
            string   startSalt = null,
            string   endSalt   = null
            )
            : base(loggerFactory, SHA1.Create(), encoding, removeSeperators, maxLenght, startSalt, endSalt)
        {

        }
    }
}