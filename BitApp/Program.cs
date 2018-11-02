using System;
using NBitcoin;
using QBitNinja.Client;

namespace BitApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var  privateKey = new Key();
            var testNetPrivateKey = privateKey.GetBitcoinSecret(Network.TestNet);

            Console.WriteLine(testNetPrivateKey);
            var publicKey = privateKey.PubKey; // compute public key
            var address = publicKey.GetAddress(Network.TestNet);

            Console.WriteLine("pubkey: \t" + publicKey);
            Console.WriteLine("address: \t" + address);
        }
    }
}
