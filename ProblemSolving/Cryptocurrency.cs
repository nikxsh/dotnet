using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace ProblemSolving
{
    public class Cryptocurrency
    {
        private string _inputURL = @"D:\input1.txt";
        private string _outputURL = @"D:\output.txt";
        private List<TransactionInfo> _transactions;

        public Cryptocurrency()
        {
            _transactions = new List<TransactionInfo>();
        }

        /// <summary>
        /// Call this method to run this app.
        /// </summary>
        public void Start()
        {
            try
            {
                ReadFromInputFile();
                ProcessTransactions();
            }
            catch (Exception ex)
            {
                LogError(ex.Message);
            }
        }

        /// <summary>
        /// Read Data From Input File
        /// </summary>
        private void ReadFromInputFile()
        {
            try
            {
                var fileData = File.ReadAllLines(_inputURL);

                //Map all transaction into objects
                _transactions = fileData.Select(x =>
                {
                    var data = x.Replace(" ", string.Empty).Split(',');
                    if (data.Count() < 3)
                        throw new Exception("Invalid Transaction!");

                    var lineOutput = new TransactionInfo(data[1], data[0]);
                    lineOutput.ToyChain = data.Skip(2).Select(y => new ToyBlock(y)).ToList();
                    return lineOutput;
                }).ToList();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Process All transactions
        /// </summary>
        private void ProcessTransactions()
        {
            try
            {
                //Get All coin creators
                var coinCreators = _transactions.Where(x => x.IsCoinCreation);

                if (coinCreators.Count() > 0)
                {
                    //Trace all coins of each creators
                    foreach (var transaction in coinCreators)
                        foreach (var item in transaction.ToyChain)
                            MoneyTrace(item, transaction.AccountNumber, transaction.Id);

                    //Fetch trailed accounts
                    var criminalAccounts = _transactions
                        .Where(x => x.IsCoinCreation)
                        .SelectMany(y => y.TrailedDeposits)
                        .GroupBy(p => p)
                        .Where(n => n.Count() == coinCreators.Count())
                        .Select(z => z.Key);

                    //write to output file
                    WriteToOutputFile(criminalAccounts);
                }
                else
                    LogError("Source must exist!");
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Trace money recursively
        /// </summary>
        /// <param name="block">ToyBlock</param>
        /// <param name="accountNumber">uint</param>
        /// <param name="transactionId">uint</param>
        private void MoneyTrace(ToyBlock block, uint accountNumber, uint transactionId)
        {
            var tranferredTo = _transactions.Where(n => !n.IsCoinCreation && n.Digest.Equals(block.Hexdigest, StringComparison.InvariantCultureIgnoreCase)).ToList();

            foreach (var transaction in tranferredTo.OrderBy(a => a.AccountNumber))
            {
                if (transaction.ToyChain.Sum(x => x.Coins) != block.Coins)
                    continue;
                else
                {
                    foreach (var item in transaction.ToyChain)
                        MoneyTrace(item, transaction.AccountNumber, transactionId);

                    break;
                }
            }

            if (tranferredTo.Count == 0)
            {
                _transactions.Where(n => n.Id == transactionId).FirstOrDefault().TrailedDeposits.Add(accountNumber);
                return;
            }
        }

        /// <summary>
        /// Write output to file
        /// </summary>
        /// <param name="output">IEnumerable<uint></param>
        private void WriteToOutputFile(IEnumerable<uint> output)
        {
            var lines = output.Select(x => x.ToString()).ToArray();
            File.WriteAllLines(_outputURL, lines);
        }


        /// <summary>
        /// Log error to file
        /// </summary>
        /// <param name="output"></param>
        private void LogError(string output)
        {
            File.WriteAllText(_outputURL, output);
        }
    }

    /// <summary>
    /// Base class for Transaction
    /// </summary>
    internal class Transaction
    {
        public uint Id { get; set; }
        public bool IsCoinCreation { get; set; }
        public List<ToyBlock> ToyChain { get; set; }
    }

    /// <summary>
    /// Class for TransactionInfo
    /// </summary>
    internal class TransactionInfo : Transaction
    {
        public uint AccountNumber { get; set; }
        public string PassKey { get; set; }
        public string Digest { get; set; }
        public List<uint> TrailedDeposits { get; set; }

        public TransactionInfo(string input, string baseId)
        {
            Id = uint.Parse(baseId);
            Digest = MD5HASH.GetMD5Hash(input);
            IsCoinCreation = false;

            var data = input.Split(':');
            AccountNumber = uint.Parse(data[0]);
            if (data.Length > 2)
            {
                TrailedDeposits = new List<uint>();

                if (uint.Parse(data[1]) != base.Id || !Digest.Substring(Digest.Length - 2, 2).Equals("00"))
                    throw new Exception("Invalid Transaction!");

                PassKey = data[2];
                IsCoinCreation = true;
            }
            else
                PassKey = data[1];
        }
    }

    /// <summary>
    /// Class to create Toychain
    /// </summary>
    internal class ToyBlock
    {
        public uint Coins { get; set; }
        public string Hexdigest { get; set; }

        public ToyBlock(string input)
        {
            var data = input.Split('=');
            Coins = uint.Parse(data[0]);
            Hexdigest = data[1];
        }
    }

    /// <summary>
    /// Generate hexdigest MD5 Hash
    /// </summary>
    internal class MD5HASH
    {
        public static string GetMD5Hash(string input)
        {
            // Convert the input string to a byte array and compute the hash.
            byte[] data = MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("X2"));
            }
            // Return the hexadecimal string.
            return sBuilder.ToString();
        }
    }
}
