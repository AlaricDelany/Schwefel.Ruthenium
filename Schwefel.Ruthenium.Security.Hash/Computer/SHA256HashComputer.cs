using System.Security.Cryptography;
using System.Text;

namespace Schwefel.Ruthenium.Security.Hash.Computer {
    public class Sha256HashComputer : HashCalculatorBase<SHA256> {
        public Sha256HashComputer(
            Encoding encoding,
            bool     removeSeperators,
            ushort?  maxLenght = null,
            string   startSalt = null,
            string   endSalt   = null
            ) 
            : base(SHA256.Create(), encoding, removeSeperators, maxLenght, startSalt, endSalt) {

        }
    }
}