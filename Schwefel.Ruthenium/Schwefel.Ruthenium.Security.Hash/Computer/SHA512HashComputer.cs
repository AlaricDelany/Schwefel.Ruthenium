using System.Security.Cryptography;
using System.Text;

namespace Schwefel.Ruthenium.Security.Hash.Computer
{
    public class Sha512HashComputer : HashCalculatorBase<SHA512>
    {
        public Sha512HashComputer(
            Encoding encoding,
            bool     removeSeperators,
            ushort?  maxLenght = null,
            string   startSalt = null,
            string   endSalt   = null
            )
            : base(SHA512.Create(), encoding, removeSeperators, maxLenght, startSalt, endSalt)
        {

        }
    }
}
