using System;
using NBitcoin;
using QBitNinja.Client;

namespace BitApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Start program");
            Key privateKey;
            string vanity = "jose";
            while(true){
                privateKey = new Key();
                var publicKey = privateKey.PubKey; // compute public key
                var testNetPrivateKey = privateKey.GetBitcoinSecret(Network.TestNet);
                var address = publicKey.GetAddress(Network.TestNet);
                string v_address = address.ToString().Substring(1,5);
                Console.WriteLine("VanityAddress: \t" + v_address + "\t address:\t" + address);
                if(vanity.Equals(v_address.ToLower())){
                    Console.WriteLine("public key : \t" + publicKey);
                    Console.WriteLine("TestNet private key: \t" + testNetPrivateKey);
                    Console.WriteLine("Vanityzaddress:\t" + address);
                    break;
                }
            }
            
            //var testNetPrivateKey = privateKey.GetBitcoinSecret(Network.TestNet);

            //Console.WriteLine(testNetPrivateKey);
            
            

                            //Console.WriteLine("TestNet private key: \t" + testNetPrivateKey);
                //Console.WriteLine("Public Key : \t" + publicKey);
                //Console.WriteLine("pkstring: \t" + pkstring.ToLower();

            
            Console.WriteLine("end\n");
        }
    }
}
