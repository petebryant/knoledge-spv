using NBitcoin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace knoledge_spv
{
    public class WalletKey
    {
        Network _network;
        KnoledgeWallet _parent = null;

        public WalletKey(Network network, KnoledgeWallet parent)
        {
            _network = network;
            _parent = parent;
        }

        public BitcoinExtKey PrivateKey { get; set; }
        public BitcoinExtPubKey PubKey { get; set; }

        public void Generate()
        {
            PrivateKey = new ExtKey().GetWif(_network);
            PubKey = PrivateKey.ExtKey.Neuter().GetWif(_network);

            if (_parent != null)
            {
                _parent.Keys.Add(this);
                _parent.Update();
            }
        }

        public override string ToString()
        {
            return PubKey.ToString();
        }
    }
}
