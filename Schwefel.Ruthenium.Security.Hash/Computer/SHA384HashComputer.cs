using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Logging;

namespace Schwefel.Ruthenium.Security.Hash.Computer
{
    public class Sha384HashComputer : HashCalculatorBase<SHA384>
    {
        public Sha384HashComputer(
            ILoggerFactory loggerFactory,
            Encoding encoding,
            bool     removeSeperators,
            ushort?  maxLenght = null,
            string   startSalt = null,
            string   endSalt   = null
            )
            : base(loggerFactory, SHA384.Create(), encoding, removeSeperators, maxLenght, startSalt, endSalt)
        {

        }
    }
}
