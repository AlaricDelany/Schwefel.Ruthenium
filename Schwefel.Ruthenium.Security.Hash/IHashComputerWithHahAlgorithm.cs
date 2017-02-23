using System.Security.Cryptography;

namespace Schwefel.Ruthenium.Security.Hash {
    public interface IHashComputerWithHahAlgorithm<in THashAlgorithm>
        : IHashComputer
        where THashAlgorithm : HashAlgorithm
    {
        string ComputeHash(THashAlgorithm cryptoProvider, string input);
    }
}