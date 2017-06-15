using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ProblemSolving
{
    public class Cryptocurrency
    {
        private string _inputURL = @"D:\input.txt";
        private string _outputURL = @"D:\output.txt";
        private List<TransactionInfo> _transactions;

        public Cryptocurrency()
        {
            _transactions = new List<TransactionInfo>();
            ReadFromInputFile();
            ProcessTransactions();
        }

        private void ReadFromInputFile()
        {
            var fileData = File.ReadAllLines(_inputURL);

            _transactions = fileData.Select(x =>
           {
               var data = x.Replace(" ", string.Empty).Split(',');
               var lineOutput = new TransactionInfo(data[1], data[0]);
               lineOutput.ToyChain = data.Skip(2).Select(y => new ToyBlock(y)).ToList();
               return lineOutput;
           }).ToList();
        }

        private void ProcessTransactions()
        {
            var criminals = new List<ToyBlock>();

            foreach (var transaction in _transactions.Where(x => x.IsCoinCreation))
                foreach (var item in transaction.ToyChain)
                    MoneyTrace(item);
        }

        private void MoneyTrace(ToyBlock block)
        {
            var tranferredTo = _transactions.Where(n => n.Digest.Equals(block.Hexdigest, StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault();

            if (tranferredTo == null)
                return;
            else
                _transactions.Where(n => n.Digest.Equals(block.Hexdigest, StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault().TransactionCount++;

            foreach (var item in tranferredTo.ToyChain)
                MoneyTrace(item);
        }
    }

    internal class Transaction
    {
        public uint Id { get; set; }
        public bool IsCoinCreation { get; set; }
        public List<ToyBlock> ToyChain { get; set; }
    }

    internal class TransactionInfo : Transaction
    {
        public uint AccountNumber { get; set; }
        public string PassKey { get; set; }
        public string Digest { get; set; }
        public int TransactionCount { get; set; }

        public TransactionInfo(string input, string baseId)
        {
            Id = uint.Parse(baseId);
            Digest = MD5HASH.GetMD5Hash(input);
            IsCoinCreation = false;

            var data = input.Split(':');
            AccountNumber = uint.Parse(data[0]);
            if (data.Length > 2)
            {
                if (uint.Parse(data[1]) != base.Id || !Digest.Substring(Digest.Length - 2, 2).Equals("00"))
                    throw new Exception("Invalid Transaction!");

                PassKey = data[2];
                IsCoinCreation = true;
            }
            else
                PassKey = data[1];
        }
    }

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
