using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Logging;

namespace Schwefel.Ruthenium.Security.Hash.Computer
{
    public class Sha256HashComputer : HashCalculatorBase<SHA256> {
        public Sha256HashComputer(
            ILoggerFactory loggerFactory,
            Encoding encoding,
            bool     removeSeperators,
            ushort?  maxLenght = null,
            string   startSalt = null,
            string   endSalt   = null
            ) 
            : base(loggerFactory, SHA256.Create(), encoding, removeSeperators, maxLenght, startSalt, endSalt) {

        }
    }
}