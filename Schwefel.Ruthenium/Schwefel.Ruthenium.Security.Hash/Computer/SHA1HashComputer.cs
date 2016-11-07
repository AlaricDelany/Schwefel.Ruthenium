using System.Security.Cryptography;
using System.Text;

namespace Schwefel.Ruthenium.Security.Hash.Computer
{
    public class Sha1HashComputer : HashCalculatorBase<SHA1>
    {
        public Sha1HashComputer(
            Encoding encoding,
            bool     removeSeperators,
            ushort?  maxLenght = null,
            string   startSalt = null,
            string   endSalt   = null
            )
            : base(SHA1.Create(), encoding, removeSeperators, maxLenght, startSalt, endSalt)
        {

        }
    }
}