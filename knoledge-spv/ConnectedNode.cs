using NBitcoin;
using NBitcoin.Protocol;
using NBitcoin.Protocol.Behaviors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace knoledge_spv
{
    public class ConnectedNode
    {
        Node _node;

        public ConnectedNode(Node node)
        {
            _node = node;
        }

        public string Name
        {
            get
            {
                return _node.RemoteSocketAddress + ":" + _node.RemoteSocketPort;
            }
        }

        public string PerfCounter 
        {
            get
            {            
                var snap = _node.Counter.Snapshot();
                return snap.ToString();
            }
        }

        public string Version
        {
            get 
            {
                if (_node.PeerVersion != null)
                    return _node.PeerVersion.UserAgent;
                else
                    return string.Empty;
            }
        }

        public int StartHeight
        {
                        get 
            {
                if (_node.PeerVersion != null)
                    return _node.PeerVersion.StartHeight;
                else
                    return 0;
            }
        }

        public DateTime LastSeen
        { 
            get { return _node.LastSeen.LocalDateTime; } 
        }

        public int Latency 
        { 
            get
            {
                var behavior = _node.Behaviors.Find<PingPongBehavior>();
                if (behavior != null)
                {
                    behavior.Probe();
                    return (int)behavior.Latency.TotalMilliseconds;
                }
                else 
                { 
                    return 0; 
                }
            }
        }
        public DateTime ConnectedAt 
        {
            get { return _node.ConnectedAt.LocalDateTime; }
        }
    }
}
