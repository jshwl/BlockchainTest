using System;
using NBitcoin;
using QBitNinja.Client;

namespace BitApp
{
    // class Program
    // {
    //     static void Main(string[] args)
    //     {
    //         // Main net
    //         // Console.WriteLine("Hello World!");
    //         // Key privateKeyMain = new Key(); // generate a random private key
    //         // BitcoinSecret mainNetPrivateKey = privateKeyMain.GetBitcoinSecret(Network.Main);  // generate our Bitcoin secret(also known as Wallet Import Format or simply WIF) from our private key for the testnet

    //         // Console.WriteLine(mainNetPrivateKey);

    //         // PubKey publicKeyMain = privateKeyMain.PubKey;
    //         // BitcoinPubKeyAddress bitcoinPublicKeyMain = publicKeyMain.GetAddress(Network.Main); 
    //         // var addressMain = publicKeyMain.GetAddress(Network.Main);

    //         // Console.WriteLine("Public Key: " + publicKeyMain);
    //         // Console.WriteLine("Address: " + addressMain);
    //         // Console.WriteLine("Private Key: " + mainNetPrivateKey);


    //         // Test net
    //         Console.WriteLine("\n\n--------------------------------------------\n\n");
    //         Console.WriteLine("Test net");
    //         Key privateKey = new Key(); // generate a random private key
    //         BitcoinSecret testNetPrivateKey = privateKey.GetBitcoinSecret(Network.TestNet);  // generate our Bitcoin secret(also known as Wallet Import Format or simply WIF) from our private key for the testnet

    //         Console.WriteLine(testNetPrivateKey);

    //         PubKey publicKey = privateKey.PubKey;
    //         BitcoinPubKeyAddress bitcoinPublicKey = publicKey.GetAddress(Network.TestNet); 
    //         var address = publicKey.GetAddress(Network.TestNet);

    //         Console.WriteLine("Public Key: " + publicKey);
    //         Console.WriteLine("Address: " + address);
    //         Console.WriteLine("Private Key Bitcoin secret: " + testNetPrivateKey);
    //         Console.WriteLine("Public Key __: " + testNetPrivateKey.PrivateKey.PubKey);

    //         byte[] privkey = privateKey.ToBytes();
    //         string privkeyString = NBitcoin.DataEncoders.Base58Encoder.EncodeData(privkey, 0, privkey.Length);
    //         Console.WriteLine("--------------------------------------------");
    //         Console.WriteLine("String_test: " + privkeyString);
    //         //byte[] privkeyConverted = System.Text.Encoding.Default.GetBytes(privkeyString);
            
    //         BitcoinSecret secretKeyConverted = new BitcoinSecret("cS2U4Nz7tjn2SeUJaqQ3VnUrVXjVWPZG4KJmb3ecPa7YWuJwoh37");
    //         Console.WriteLine("Private Key Bitcoin secret reconv: " + secretKeyConverted);
            




    //     }
    // }
}
