using NBitcoin;
using NBitcoin.Protocol;
using NBitcoin.SPV;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace knoledge_spv
{
    public class KnoledgeWallet
    {
        const string WALLET_NAME = "Wallet";
        Network _network;

        public KnoledgeWallet(Network network)
        {
            _network = network;
            SigRequired = 1;
            Keys.Add(new WalletKey(_network, this));
        }

        public string Name { get; set; }
        public int SigRequired { get; set; }

        public bool IsP2SH { get; set; }

        public BitcoinAddress CurrentAddress { get; set; }

        public IList<BitcoinAddress> Addresses { get; set; }

        IList<WalletKey> _keys = new List<WalletKey>();
        public IList<WalletKey> Keys
        {
            get
            {
                return _keys;
            }
        }

        private string WalletDir
        {
            get
            {
                return Path.Combine(Common.AppDir, Name);
            }
        }

        internal string WalletFile
        {
            get { return Path.Combine(WalletDir, "Wallet.Dat"); }
        }

        internal string PrivateKeyFile
        {
            get { return Path.Combine(WalletDir, "PrivateKeys"); }
        }

        internal Wallet _wallet;
        public Wallet Wallet
        {
            get
            {
                return _wallet;
            }
        }

        public WalletState State
        {
            get { return _wallet.State; }
        }

        private List<KnoledgeTransaction> _Transactions = new List<KnoledgeTransaction>();
        public List<KnoledgeTransaction> Transactions
        {
            get
            {
                return _Transactions;
            }
            set
            {
                if (value != _Transactions)
                {
                    _Transactions = value;
                }
            }
        }

        public BitcoinExtKey[] PrivateKeys
        {
            get;
            set;
        }

        public Script CurrentAddressScriptPubKey
        {
            get { return CurrentAddress.ScriptPubKey; }
        }


        public bool IsValid
        {
            get
            {
                return RealKeys.Count() != 0 &&
                    SigRequired <= RealKeys.Count() &&
                    SigRequired >= 1;
            }
        }

        public IEnumerable<WalletKey> RealKeys
        {
            get
            {
                return _keys.Where(k => k.PubKey != null);
            }
        }

        //Always have one Key even if it is null.
        internal void Update()
        {
            foreach (var key in _keys.Reverse().ToList())
            {
                if (key.PubKey == null && _keys.Count > 1)
                    _keys.Remove(key);
            }

            if (_wallet != null)
            {
                CurrentAddress = _wallet
                                    .GetKnownScripts(true)
                                    .Where(s => s.Value.Indexes[0] == 0) //On public branch
                                    .OrderByDescending(s => s.Value.Indexes[1]) //We generate HD on the path : 0/N, the highest is the latest scriptPubKey
                                    .Select(s => s.Key.GetDestinationAddress(_network))
                                    .FirstOrDefault();

                Addresses = _wallet
                                .GetKnownScripts(true)
                                .Where(s => s.Value.Indexes[0] == 0)
                                .OrderByDescending(s => s.Value.Indexes[1])
                                .Select(s => s.Key.GetDestinationAddress(_network))
                                .ToList();

                if (_wallet.State != WalletState.Created)
                    Transactions = _wallet.GetTransactions().Select(t => new KnoledgeTransaction(t)).ToList();
            }
        }

        internal NBitcoin.SPV.WalletCreation CreateWalletCreation()
        {
            return new NBitcoin.SPV.WalletCreation()
            {
                SignatureRequired = SigRequired,
                UseP2SH = IsP2SH,
                Network = _network,
                RootKeys = _keys.Where(k => k.PubKey != null).Select(k => k.PubKey.ExtPubKey).ToArray()
            };
        }

        internal void Set(NBitcoin.SPV.Wallet wallet)
        {
            _wallet = wallet;
            PrivateKeys = Keys.Where(k => k.PrivateKey != null).Select(k => k.PrivateKey).ToArray();
        }

        internal void Save()
        {
            if (!Directory.Exists(WalletDir))
                Directory.CreateDirectory(WalletDir);

            using (var fs = File.Open(WalletFile, FileMode.Create))
            {
                Wallet.Save(fs);
            }

            File.WriteAllText(PrivateKeyFile, string.Join(",", PrivateKeys.AsEnumerable()));
        }

        public override string ToString()
        {
            return Name;
        }

        public Script GetNextScriptPubKey()
        {
            if (_wallet == null)
                return null;
            else
                return _wallet.GetNextScriptPubKey();
        }

        public WalletTransactionsCollection GetTransactions()
        {
            if (_wallet == null)
                return null;
            else
                return _wallet.GetTransactions();
        }

        public decimal GetBalance(MoneyUnit unit = MoneyUnit.BTC)
        {
            WalletTransactionsCollection transactions = null;
            Money amount = Money.Zero;

            try
            {
                transactions = _wallet.GetTransactions();
            }
            catch { }


            if (transactions != null && transactions.Count > 0)
                amount = transactions.Summary.Spendable.Amount;

            return amount.ToUnit(unit);
        }

        public string GetBalanceString()
        {
            return GetBalance(MoneyUnit.BTC).ToString() + " BTC";
        }

        public ICoin[] GetCoinSource()
        {
            WalletTransactionsCollection transactions = null;
            try
            {
                transactions = _wallet.GetTransactions();
            }
            catch { }


            if (transactions == null || transactions.Count == 0)
                return null;
            else
                return transactions.GetSpendableCoins().ToArray();
        }

        public ExtKey[] GetKeysForCoins(ICoin[] coins)
        {
            // Get the Key for the Coins we are going to spend
            IList<ExtKey> keys = new List<ExtKey>();
            ExtKey masterKey = PrivateKeys[0];

            foreach (ICoin coin in coins)
            {
                KeyPath keyPath = _wallet.GetKeyPath(coin.TxOut.ScriptPubKey);
                keys.Add(masterKey.Derive(keyPath));
            }

            return keys.ToArray();
        }

        public void Connect(NodeConnectionParameters parameters)
        {
            _wallet.Connect(parameters);
        }
    }
}
