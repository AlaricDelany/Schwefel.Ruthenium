using System.Security.Cryptography;
using System.Text;

namespace Schwefel.Ruthenium.Security.Hash.Computer {
    public class Md5HashComputer : HashCalculatorBase<MD5>
    {
        public Md5HashComputer(
            Encoding encoding,
            bool removeSeperators,
            ushort? maxLenght = null,
            string startSalt = null,
            string endSalt = null
        )
            : base(MD5.Create(), encoding, removeSeperators, maxLenght, startSalt, endSalt) {

        }
    }
}