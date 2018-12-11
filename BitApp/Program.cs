using System;
using NBitcoin;
using QBitNinja.Client;
using System.Collections.Generic;

namespace BitApp
{
    class Program
    {
        static void Main(string[] args)
        {

            // Test net
            // Console.WriteLine("\n\n--------------------------------------------\n\n");
            // Console.WriteLine("Test net");
            // BitcoinSecret old_bitcoinSecretKey = new BitcoinSecret("cS2U4Nz7tjn2SeUJaqQ3VnUrVXjVWPZG4KJmb3ecPa7YWuJwoh37");
            // byte[] byteArray = old_bitcoinSecretKey.PrivateKey.ToBytes();
            // //byteArray[0] = 0;
            // //byteArray[1] = 223;
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
            Console.WriteLine("Address: " + address.ToString());
            Console.WriteLine("Private Key Bitcoin secret: " + bitcoinSecretKey);

            System.IO.StreamWriter file = new System.IO.StreamWriter("Generated_Addresses.txt");
            file.WriteLine("Private Key: " + bitcoinSecretKey);
            file.WriteLine("Public key: " + publicKey);
            file.WriteLine("Address: " + address);
            file.WriteLine("Payment Script: " + paymentScript );
            
            file.Close();

            
            //genVanityAddress("mmanwa", 6);
            //getMainNetTransactionInfo();
            getTestnetAddressInfo("n2obaQd1r3so39PFLMMm7yK4b146oK4Zpq");
            getTestnetAdressUnspentBalance("n2obaQd1r3so39PFLMMm7yK4b146oK4Zpq");
            
            
            
            

        }

        static void genVanityAddress(string vanity, int len) {
            if (len>7) {
                Console.WriteLine("You are too ambitious. Setting length to 7.");
                len=7;
            }
            if (len > vanity.Length) {
                len = vanity.Length;
            }
            if (len < vanity.Length) {
                vanity = vanity.Substring(0,len);
            }

            BitcoinSecret old_bitcoinSecretKey = new BitcoinSecret("cS2U4Nz7tjn2SeUJaqQ3VnUrVXjVWPZG4KJmb3ecPa7YWuJwoh37");
            byte[] byteArrayKey = old_bitcoinSecretKey.PrivateKey.ToBytes();
            // Byte[] byteArrayKey = new Byte[32];
            int keyArrayLength = byteArrayKey.Length;
            // for(int i = 0; i < keyArrayLength; i++) {
            //     byteArrayKey[i] = 0;
            // }
            
            bool allCombinationsTested = false;
            bool vanityFound = false;
            Int64 numCombinationsTested = 0;
            while (!allCombinationsTested) {
                vanityFound = testVanityAddress(vanity, len, byteArrayKey);
                numCombinationsTested++;
                if (vanityFound) {
                    break;
                }

                // Increment systematically
                int arrIdx = 0;
                bool incrementDone = false;
                while (!incrementDone) {
                    if (byteArrayKey[arrIdx]<255) {
                        byteArrayKey[arrIdx] = (byte)(byteArrayKey[arrIdx] + 1);
                        incrementDone = true;
                    } else {
                        byteArrayKey[arrIdx] = 0;
                        arrIdx++;
                    }
                    if (arrIdx >= keyArrayLength) {
                        incrementDone = true;
                        allCombinationsTested = true;
                    }
                }
                if (numCombinationsTested%1000 == 0) {
                    Console.WriteLine("genVanityAddress: Tested combinaiton count: " + numCombinationsTested);
                }
            }

            Console.WriteLine("Vanity address found:" + vanityFound);
            Console.WriteLine("All combinations tested:" + allCombinationsTested);
            Console.WriteLine("Num combinations tested:" + numCombinationsTested);

        }

        static bool testVanityAddress(string vanity, int len, byte[] byteArray) {
            if (len > vanity.Length || len > byteArray.Length) {
                Console.WriteLine("Error, aborting. Vanity str length: "+ vanity.Length + ". addr length: " + byteArray.Length + ". Requested len: " + len);
                System.Environment.Exit(-1 );
            }
            
            NBitcoin.DataEncoders.Base58Encoder encoder = new NBitcoin.DataEncoders.Base58Encoder();
            String key_base58 = encoder.EncodeData(byteArray, 0, byteArray.Length);
            
            // for (int i =0; i<4; i++) Console.Write(" " + byteArray[i]);
            // Console.WriteLine("");

            Key privateKey;
            try {
                privateKey = new Key(byteArray);
            } catch (System.ArgumentException e) {
                // Console.WriteLine("Private key creation exception occurred.");
                // Console.WriteLine("Base58String: " + key_base58);
                // Console.Write("Byte arr::  ");
                // for (int i =0; i<byteArray.Length; i++) Console.Write(" " + byteArray[i]);
                return false;
            }
            //privateKey = new Key();
            PubKey publicKey = privateKey.PubKey;
            var address = publicKey.GetAddress(Network.TestNet);
            
            string address_substr = address.ToString().Substring(0,len);
            // Console.WriteLine("Addr1:" + address);
            // Console.WriteLine("Addr2:" + address_substr);
            // Console.WriteLine("===================");
            bool vanity_found = address_substr.Equals(vanity, StringComparison.OrdinalIgnoreCase);
            if (vanity_found) {
                Console.WriteLine("\n\n====================================");
                Console.WriteLine("Vanity string found.");
                Console.WriteLine("Private Key: " + privateKey);
                Console.WriteLine("Public key: " + publicKey);
                Console.WriteLine("Address: " + address);

                System.IO.StreamWriter file = new System.IO.StreamWriter(vanity +"_"+address_substr + "_vanity_gen.txt");
                file.WriteLine("Private Key: " + privateKey);
                file.WriteLine("Public key: " + publicKey);
                file.WriteLine("Address: " + address);
                file.Close();
                return true;
            }
            return false;
        }

        static void getMainNetTransactionInfo() {
            QBitNinjaClient client = new QBitNinjaClient(Network.Main);
            // Parse transaction id to NBitcoin.uint256 so the client can eat it
            var transactionId = uint256.Parse("8678423e9437501e8c6c536b80240f101a74510331b7b1c09a6aa6c54aeb433e");
            QBitNinja.Client.Models.GetTransactionResponse transactionResponse = client.GetTransaction(transactionId).Result;

            NBitcoin.Transaction transaction = transactionResponse.Transaction;

            Console.WriteLine("\n========================================================");
            Console.WriteLine("Information for transiction id: " + transactionResponse.TransactionId); 


            // Received coins:
            Console.WriteLine("Received coins: ");
            List<ICoin> receivedCoins = transactionResponse.ReceivedCoins;
            Money total_received = 0;
            foreach (var coin in receivedCoins)
            {
                Money amount = (Money) coin.Amount;
                var paymentScript = coin.TxOut.ScriptPubKey;
                var address = paymentScript.GetDestinationAddress(Network.Main);
                Console.WriteLine("\tAmount [BTC]: " + amount.ToDecimal(MoneyUnit.BTC) + ". Destination address:" + address);
                total_received+= amount;
            }
            Console.WriteLine("Total amount received [BTC]: " + total_received.ToDecimal(MoneyUnit.BTC));
            Console.WriteLine();


            // Spent coins:
            Console.WriteLine("Spent coins: ");
            List<ICoin> spentCoins = transactionResponse.SpentCoins;
            Money total_spent = 0;
            foreach (var coin in spentCoins)
            {
                Money amount = (Money) coin.Amount;
                var paymentScript = coin.TxOut.ScriptPubKey;
                var address = paymentScript.GetDestinationAddress(Network.Main);
                Console.WriteLine("\tAmount [BTC]: " + amount.ToDecimal(MoneyUnit.BTC) + ". Destination address:" + address);
                total_spent+= amount;
            }
            Console.WriteLine("Total amount spent [BTC]: " + total_spent.ToDecimal(MoneyUnit.BTC));
            Console.WriteLine();

            var fee = transaction.GetFee(spentCoins.ToArray());
            Console.WriteLine("Transaction fee [BTC]: " + fee.ToDecimal(MoneyUnit.BTC));
        }


        static void getTestnetAddressInfo(string addressString) {
            QBitNinjaClient client = new QBitNinjaClient(Network.TestNet);

            var wAddress = BitcoinAddress.Create(addressString, Network.TestNet);


            
            
            
            QBitNinja.Client.Models.BalanceModel balanceModel = client.GetBalance(wAddress, false).Result;
            QBitNinja.Client.Models.BalanceSummary balanceSummary = client.GetBalanceSummary(wAddress).Result;

            

            Console.WriteLine("\n========================================================");
            Console.WriteLine("Information for address id: " + wAddress.ToString()); 
            Console.WriteLine("Balance Spendable amount [BTC]: " + balanceSummary.Spendable.Amount.ToString());
            Console.WriteLine("Total number of operations: " + balanceModel.Operations.Count);

            var opCount = 0;
            foreach (var operation in balanceModel.Operations) {
                opCount++;
                Console.WriteLine("\tOperation " + opCount + ": Spent coins: " + operation.SpentCoins.Count + " Received coins: " + operation.ReceivedCoins.Count);
                foreach (var coin in operation.ReceivedCoins) {
                    Money amount = (Money) coin.Amount;
                    var paymentScript = coin.TxOut.ScriptPubKey;
                    var address = paymentScript.GetDestinationAddress(Network.TestNet);
                    Console.WriteLine("\t\tReceived Amount [BTC]: " + amount.ToDecimal(MoneyUnit.BTC));
                }

                foreach (var coin in operation.SpentCoins) {
                    Money amount = (Money) coin.Amount;
                    var paymentScript = coin.TxOut.ScriptPubKey;
                    var address = paymentScript.GetDestinationAddress(Network.TestNet);
                    Console.WriteLine("\t\tSpent Amount [BTC]: " + amount.ToDecimal(MoneyUnit.BTC));
                }
            }
        }

        static void getTestnetAdressUnspentBalance(string addressString) {
            QBitNinjaClient client = new QBitNinjaClient(Network.TestNet);

            var wAddress = BitcoinAddress.Create(addressString, Network.TestNet);
            QBitNinja.Client.Models.BalanceModel balanceModel = client.GetBalance(wAddress, true).Result;

            Console.WriteLine("\n========================================================");
            Console.WriteLine("Unspent Transaction outputs in : " + wAddress.ToString()); 
            
            List<ICoin> receivedCoins = new List<ICoin>();
            receivedCoins.Clear();
            foreach (var operation in balanceModel.Operations) {
                receivedCoins.AddRange(operation.ReceivedCoins);
            }
            Console.WriteLine("Number of UTXOs: " + receivedCoins.Count);
            foreach (var coin in receivedCoins) {
                Money amount = (Money) coin.Amount;
                var paymentScript = coin.TxOut.ScriptPubKey;
                var address = paymentScript.GetDestinationAddress(Network.TestNet);
                Console.WriteLine("\tUnspent output: In transaction: " +  coin.Outpoint.Hash + ", Amount [BTC]: " + amount.ToDecimal(MoneyUnit.BTC));
            }
               
        }

    }
}
