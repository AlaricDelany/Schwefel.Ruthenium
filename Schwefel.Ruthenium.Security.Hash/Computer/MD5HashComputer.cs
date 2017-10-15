using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Logging;

namespace Schwefel.Ruthenium.Security.Hash.Computer
{
    public class Md5HashComputer : HashCalculatorBase<MD5>
    {
        public Md5HashComputer(
            ILoggerFactory loggerFactory,
            Encoding encoding,
            bool removeSeperators,
            ushort? maxLenght = null,
            string startSalt = null,
            string endSalt = null
        )
            : base(loggerFactory, MD5.Create(), encoding, removeSeperators, maxLenght, startSalt, endSalt) {

        }
    }
}