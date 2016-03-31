using NBitcoin;
using NBitcoin.SPV;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace knoledge_spv
{
    public class KnoledgeTransaction
    {
        WalletTransaction _transaction;
        public KnoledgeTransaction(WalletTransaction transaction)
        {
            _transaction = transaction;
        }

        public WalletTransaction Transaction
        {
            get { return _transaction; }
        }
        public string TransactionId
        {
            get { return _transaction.Transaction.GetHash().ToString(); }
        }
        public string Balance
        {
            get { return _transaction.Balance.ToUnit(MoneyUnit.BTC).ToString(); }
        }

        public string BlockId
        {
            get { return _transaction.BlockInformation == null ? null : _transaction.BlockInformation.Header.GetHash().ToString(); }
        }

        public string Confirmations
        {
            get { return (_transaction.BlockInformation == null ? 0 : _transaction.BlockInformation.Confirmations).ToString(); }
        }
    }
}
