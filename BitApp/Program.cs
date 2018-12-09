using System;
using NBitcoin;
using QBitNinja.Client;

namespace BitApp
{
    class Program
    {
        static void Main(string[] args)
        {

            // Test net
            Console.WriteLine("\n\n--------------------------------------------\n\n");
            Console.WriteLine("Test net");
            // BitcoinSecret old_bitcoinSecretKey = new BitcoinSecret("cS2U4Nz7tjn2SeUJaqQ3VnUrVXjVWPZG4KJmb3ecPa7YWuJwoh37");
            // byte[] byteArray = old_bitcoinSecretKey.PrivateKey.ToBytes();
            // byteArray[0] = 0;
            // byteArray[1] = 223;
            // NBitcoin.DataEncoders.Base58Encoder encoder = new NBitcoin.DataEncoders.Base58Encoder();
            // String str_base58 = encoder.EncodeData(byteArray, 0, byteArray.Length);
            // Console.WriteLine("Base58 string: " + str_base58);
            // Console.WriteLine("Byte array len: " + byteArray.Length);
            // Key computedPrivKey = new Key(byteArray);
            // BitcoinSecret computedSecret = computedPrivKey.GetBitcoinSecret(Network.TestNet);
            // Console.WriteLine("Computed bitcoin sectret: " +computedSecret );


            
            
            BitcoinSecret bitcoinSecretKey = new BitcoinSecret("cS2U4Nz7tjn2SeUJaqQ3VnUrVXjVWPZG4KJmb3ecPa7YWuJwoh37");
            Key privateKey = bitcoinSecretKey.PrivateKey; // generate a random private key
            PubKey publicKey = privateKey.PubKey;
            var publicKeyHash = publicKey.Hash;  // https://programmingblockchain.gitbook.io/programmingblockchain/bitcoin_transfer/bitcoin_address
            BitcoinPubKeyAddress bitcoinPublicKey = publicKey.GetAddress(Network.TestNet); 
            var address = publicKey.GetAddress(Network.TestNet);
            var paymentScript = publicKeyHash.ScriptPubKey;  // https://programmingblockchain.gitbook.io/programmingblockchain/bitcoin_transfer/payment_script

            Console.WriteLine("Public Key: " + publicKey);
            Console.WriteLine("Address: " + address);
            Console.WriteLine("Private Key Bitcoin secret: " + bitcoinSecretKey);

            System.IO.StreamWriter file = new System.IO.StreamWriter("Generated_Addresses.txt");
            file.WriteLine("Private Key: " + bitcoinSecretKey);
            file.WriteLine("Public key: " + publicKey);
            file.WriteLine("Address: " + address);
            file.WriteLine("Payment Script: " + paymentScript );
            
            file.Close();

            
            
            
            
            
            
            

        }
    }
}
