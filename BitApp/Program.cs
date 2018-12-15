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

            // UNCOMMENT THE FOLLOWING BEFORE SUBMISSION
           /* // ##################################################################
            // ### Wallet management in C#
            // ##################################################################
            generateAndSaveKeys();

            
            
            // ### A little bit of vanity and proof-of-work
            //genVanityAddress("mmanwa", 6);

            // ##################################################################
            // ### Interact with QBitNinja blockchain indexer in C#
            // ##################################################################
            // 2. Download all information available for this transaction
            getMainNetTransactionInfo("7ddde9aa86476c5a9064535aa220cb84019874db9d356497ed89f31841138b16");

            // 3. Download all information relative to your CoPay address
            getTestnetAddressInfo("mjoSet79rvh2nQQzYWmFhzYeQYu3ZV1bYx");

            // 5. Print the list of unspent transaction outputs
            getTestnetAdressUnspentBalance("mjoSet79rvh2nQQzYWmFhzYeQYu3ZV1bYx");

            // ##################################################################
            // ### Build your first bitcoin transaction in C#
            // ##################################################################
            sendMoney();

            monitorBlockConfirmations("ccc50aafe5b106f9ef7942e226a16b16614f25dee2c54f97d0a326626381d319", 20.0);*/
            
            
            

            // Our copay address with a few transactions:  n2obaQd1r3so39PFLMMm7yK4b146oK4Zpq
            // Other copay addresses: muGuZLu5ZbgsGK3L8M1cnmznSupe7ahh6c
            // Our address created with code that has some money in it: mtWoNV36TuoNzQDsfejZEoQAQhWxbnEfT2. Corresponding priv key: cS2U4Nz7tjn2SeUJaqQ3VnUrVXjVWPZG4KJmb3ecPa7YWuJwoh37
            // Our vanity address is: mjoSet79rvh2nQQzYWmFhzYeQYu3ZV1bYx. Corresponding priv key: cW696MyvRj5a2BM42ijrUMMBZErDGs1m6uVTBLmSyoChS6TZuAqU

        }
        static void generateAndSaveKeys() {

            // Generate new key
            //var privateKey = new Key();  // cS2U4Nz7tjn2SeUJaqQ3VnUrVXjVWPZG4KJmb3ecPa7YWuJwoh37  or cW696MyvRj5a2BM42ijrUMMBZErDGs1m6uVTBLmSyoChS6TZuAqU

            // Re-use our previously generated keys
            BitcoinSecret useOldKey = new BitcoinSecret("cW696MyvRj5a2BM42ijrUMMBZErDGs1m6uVTBLmSyoChS6TZuAqU");
            Key privateKey = useOldKey.PrivateKey; 
            

            PubKey publicKey = privateKey.PubKey;
            var publicKeyHash = publicKey.Hash;  
            System.IO.StreamWriter file = new System.IO.StreamWriter("Generated_Addresses.txt");

            // TestNet
            BitcoinSecret bitcoinSecretKey = privateKey.GetBitcoinSecret(Network.TestNet);
            BitcoinPubKeyAddress address = publicKey.GetAddress(Network.TestNet);
            var paymentScript = publicKeyHash.ScriptPubKey;  

            Console.WriteLine("============================\nTestNet:");
            Console.WriteLine("Public Key: " + publicKey);
            Console.WriteLine("Address: " + address.ToString());
            Console.WriteLine("Private Key Bitcoin secret: " + bitcoinSecretKey);

            file.WriteLine("\nTestNet:");
            file.WriteLine("Private Key Bitcoin Secret: " + bitcoinSecretKey);
            file.WriteLine("Public key: " + publicKey);
            file.WriteLine("Address: " + address);
            file.WriteLine("Payment Script: " + paymentScript );

            // Main net
            bitcoinSecretKey = privateKey.GetBitcoinSecret(Network.Main);
            address = publicKey.GetAddress(Network.Main);
            paymentScript = publicKeyHash.ScriptPubKey;  

            Console.WriteLine("============================\nMainNet:");
            Console.WriteLine("Public Key: " + publicKey);
            Console.WriteLine("Address: " + address.ToString());
            Console.WriteLine("Private Key Bitcoin secret: " + bitcoinSecretKey);

            file.WriteLine("\nMain net:");
            file.WriteLine("Private Key Bitcoin Secret: " + bitcoinSecretKey);
            file.WriteLine("Public key: " + publicKey);
            file.WriteLine("Address: " + address);
            file.WriteLine("Payment Script: " + paymentScript );

            file.Close();
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

        static void getMainNetTransactionInfo(string transactionIdString) {
            QBitNinjaClient client = new QBitNinjaClient(Network.Main);
            var transactionId = uint256.Parse(transactionIdString);
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
            // Get only Unspent balances
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


        static void sendMoney() {
            
            BitcoinSecret bitcoinPrivateKey = new BitcoinSecret("cW696MyvRj5a2BM42ijrUMMBZErDGs1m6uVTBLmSyoChS6TZuAqU");  // Our vanity address
            var network = bitcoinPrivateKey.Network;
            var address = bitcoinPrivateKey.GetAddress();

            var client = new QBitNinjaClient(network);
            // ccc50aafe5b106f9ef7942e226a16b16614f25dee2c54f97d0a326626381d319
            // 87e3531cb875f49182c78538ce464e38c2034ab0c695e767f1acfc2a318ae333
            var transactionId = uint256.Parse("87e3531cb875f49182c78538ce464e38c2034ab0c695e767f1acfc2a318ae333");
            var transactionResponse = client.GetTransaction(transactionId).Result;
            
            Console.WriteLine("Transaction ID: " + transactionResponse.TransactionId); 
            Console.WriteLine("Block confirmaitions: " + transactionResponse.Block.Confirmations); 


            var receivedCoins = transactionResponse.ReceivedCoins;
            OutPoint outPointToSpend = null;
            foreach (var coin in receivedCoins)
            {
                if (coin.TxOut.ScriptPubKey == bitcoinPrivateKey.ScriptPubKey)
                {
                    outPointToSpend = coin.Outpoint;
                }
            }
            if(outPointToSpend == null)
                throw new Exception("TxOut doesn't contain our ScriptPubKey");
            Console.WriteLine("We want to spend {0}. outpoint:", outPointToSpend.N + 1);

            
            // From where to spend the money
            var transaction = Transaction.Create(network);
            transaction.Inputs.Add(new TxIn() { PrevOut = outPointToSpend });
            


            // How much? 
            var amountToSend = new Money(0.0004m, MoneyUnit.BTC);
            var minerFee = new Money(0.00007m, MoneyUnit.BTC);

            var txInAmount = (Money)receivedCoins[(int) outPointToSpend.N].Amount;
            var changeAmount = txInAmount - amountToSend - minerFee;

            // To where, and how much?
            var receiverAddress = BitcoinAddress.Create("n2obaQd1r3so39PFLMMm7yK4b146oK4Zpq", Network.TestNet);
            TxOut receiverTxOut = new TxOut() {
                Value = amountToSend,
                ScriptPubKey = receiverAddress.ScriptPubKey
            };
            TxOut changeTxOut = new TxOut() {
                Value = changeAmount,
                ScriptPubKey = bitcoinPrivateKey.ScriptPubKey
            };

            transaction.Outputs.Add(receiverTxOut);
            transaction.Outputs.Add(changeTxOut);

            
            // Message
            var message = "Hello world transaction!";
            var bytes = System.Text.Encoding.UTF8.GetBytes(message);
            transaction.Outputs.Add(new TxOut()
            {
                Value = Money.Zero,
                ScriptPubKey = TxNullDataTemplate.Instance.GenerateScriptPubKey(bytes)
            });

            

            // Signing
            transaction.Inputs[0].ScriptSig =  bitcoinPrivateKey.ScriptPubKey;
            transaction.Sign(bitcoinPrivateKey, receivedCoins.ToArray());



            QBitNinja.Client.Models.BroadcastResponse broadcastResponse = client.Broadcast(transaction).Result;
            if (broadcastResponse.Success) {
                Console.WriteLine("Success! Transaction successful:");
                Console.WriteLine(transaction.GetHash());
            } else {
                Console.Error.WriteLine("ErrorCode: " + broadcastResponse.Error.ErrorCode);
                Console.Error.WriteLine("Error message: " + broadcastResponse.Error.Reason);   
            }

        }

        static void monitorBlockConfirmations(string transaction_id, double runForSeconds) {
            
            var client = new QBitNinjaClient(Network.TestNet);
            DateTime begin = DateTime.Now;
            var transactionId = uint256.Parse(transaction_id);
            int oldNumConfirmations = 0;
            DateTime now = DateTime.Now;
            var seconds = (now - begin).TotalSeconds; 
            
            while (seconds < runForSeconds) {
                var transactionResponse = client.GetTransaction(transactionId).Result;
                int newNumConfirmations = transactionResponse.Block.Confirmations;
                if (newNumConfirmations == oldNumConfirmations) {
                    Console.WriteLine("Change in number of confirmations");    
                    Console.WriteLine("Transaction ID: " + transactionResponse.TransactionId + ". Block confirmaitions: " + newNumConfirmations); 
                    oldNumConfirmations = newNumConfirmations;
                }
                System.Threading.Thread.Sleep(2000);
                now = DateTime.Now;
                seconds = (now - begin).TotalSeconds; 
            }

        }

    }
}
