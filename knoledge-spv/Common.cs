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
    public class Common
    {
        public static string AppDir
        {
            get { return Directory.GetParent(typeof(FormMain).Assembly.Location).FullName; }
        }


        public static KnoledgeWallet[] LoadWallets(Network network)
        {
            List<KnoledgeWallet> wallets = new List<KnoledgeWallet>();
            foreach (var child in new DirectoryInfo(Common.AppDir).GetDirectories())
            {
                KnoledgeWallet kw = new KnoledgeWallet(network);
                kw.Name = child.Name;

                try
                {
                    kw.PrivateKeys =
                        File.ReadAllText(kw.PrivateKeyFile)
                        .Split(',')
                        .Select(c => new BitcoinExtKey(c, network))
                        .ToArray();

                    using (var fs = File.Open(kw.WalletFile, FileMode.Open))
                    {
                        kw._wallet = Wallet.Load(fs);
                    }
                    wallets.Add(kw);
                }
                catch (IOException)
                {
                }
            }
            return wallets.ToArray();
        }
    }
}
