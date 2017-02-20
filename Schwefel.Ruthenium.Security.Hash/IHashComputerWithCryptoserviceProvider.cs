using System.Security.Cryptography;

namespace Schwefel.Ruthenium.Security.Hash {
    public interface IHashComputerWithCryptoserviceProvider<in TCryptoserviceProvider>
        : IHashComputer
        where TCryptoserviceProvider : HashAlgorithm
    {
        string ComputeHash(TCryptoserviceProvider cryptoProvider, string input);
    }
}