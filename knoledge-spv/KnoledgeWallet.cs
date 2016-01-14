using NBitcoin;
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

        public KnoledgeWallet( Network network)
        {
            _network = network;

            PrivateKeys = new [] { new ExtKey().GetWif(network) };
            PubKeys = new [] { PrivateKeys[0].ExtKey.Neuter().GetWif(network) };

            WalletCreation creation = new WalletCreation()
            {
                SignatureRequired = 1,
                UseP2SH = false,
                Network = network,

                RootKeys = PubKeys.Where(k => k != null).Select(k => k.ExtPubKey).ToArray()
            };

            Wallet = new NBitcoin.SPV.Wallet(creation);
            Load();
        }

        public Wallet Wallet { get; set; }

        private string AppDir
        {
            get { return Directory.GetParent(this.GetType().Assembly.Location).FullName; }
        }

        public string WalletDir
        {
            get { return Path.Combine(AppDir, WALLET_NAME); }
        }

        public BitcoinAddress CurrentAddress {get;set;}


        public void Save()
        {
            if (!Directory.Exists(WalletDir))
                Directory.CreateDirectory(WalletDir);

            using (var fs = File.Open(WalletFile(), FileMode.Create))
            {
                Wallet.Save(fs);
            }

            File.WriteAllText(PrivateKeyFile(), string.Join(",", PrivateKeys.AsEnumerable()));
        }

        private string WalletFile()
        {
            return Path.Combine(WalletDir, "Wallet.dat");
        }

        private string PrivateKeyFile()
        {
            return Path.Combine(WalletDir, "PrivateKeys");
        }

        public BitcoinExtKey[] PrivateKeys {get;set;}
        public BitcoinExtPubKey[] PubKeys { get; set; }


        private void Load()
        {
            try
            {
                BitcoinExtKey[] privateKeys =
                    File.ReadAllText(PrivateKeyFile())
                    .Split(',')
                    .Select(c => new BitcoinExtKey(c, _network))
                    .ToArray();

                using (var fs = File.Open(WalletFile(), FileMode.Open))
                {
                    Wallet = Wallet.Load(fs);
                }
            }
            catch (IOException)
            {
            }
        }

        public void Update()
        {
            if (Wallet != null)
            {
                CurrentAddress = Wallet
                                    .GetKnownScripts(true)
                                    .Where(s => s.Value.Indexes[0] == 0) //On public branch
                                    .OrderByDescending(s => s.Value.Indexes[1]) //We generate HD on the path : 0/N, the highest is the latest scriptPubKey
                                    .Select(s => s.Key.GetDestinationAddress(_network))
                                    .FirstOrDefault();

            }
        }
    }
}
