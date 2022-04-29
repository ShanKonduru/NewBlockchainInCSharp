using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace NewBlockchainInCSharp {
    public class Block {
        private readonly DateTime _timeStamp;
        private long _nonce;
        public string PreviousHash { get; set; }

        public List<Transaction> Transactions { get; set; }

        public string Hash { get; private set; }

        public Block (DateTime timeStamp, List<Transaction> _transactions, string _previousHash = "") {
            _timeStamp = timeStamp;
            _nonce = 0;
            Transactions = _transactions;
            PreviousHash = _previousHash;
            Hash = CreateHash ();
        }
        public void MineBlock (int proofOfWorkDifficulty) {
            string hashValidationTemplate = new String ('0', proofOfWorkDifficulty);
            while (Hash.Substring (0, proofOfWorkDifficulty) != hashValidationTemplate) {
                _nonce++;
                Hash = CreateHash ();
            }
            Console.WriteLine ("Blocked with HASH={0} successfully mined!", Hash);
        }
        public string CreateHash () {
            StringBuilder builder = new StringBuilder ();
            using (SHA256 sha256 = SHA256.Create ()) {
                string rawData = PreviousHash + _timeStamp + Transactions + _nonce;
                byte[] bytes = sha256.ComputeHash (Encoding.UTF8.GetBytes (rawData));
                for (int i = 0; i < bytes.Length; i++) {
                    builder.Append (bytes[i].ToString ("x2"));
                }
                return builder.ToString ();
            }
        }
    }
}