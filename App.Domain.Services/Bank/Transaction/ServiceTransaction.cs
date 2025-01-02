using App.Domain.Core.Bank;
using Microsoft.Data.SqlClient;


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using static System.Net.Mime.MediaTypeNames;




    public class ServiceTransaction : IServiceTransaction
    {
   
        IRepositoryTransaction _repositoryTransaction;

    public ServiceTransaction(IRepositoryTransaction repositoryTransaction)
    {
        _repositoryTransaction = repositoryTransaction;
    }

    public void GenerateVerificationCode(string CardSouNumber)
    {
        _repositoryTransaction.GenerateVerificationCode(CardSouNumber);
    }

    public Card GetCard(string CardNumber)
    {
       return  _repositoryTransaction.GetCard(CardNumber);
    }

    public List<GetTrranDto> GetListOfTransactions(string CardNumber)
    {
        return _repositoryTransaction.GetListOfTransactions(CardNumber);
    }

    public string ReadVerificationCode()
    {
        return _repositoryTransaction.ReadVerificationCode();
    }

    public float SumTransactionCard(string CardNumber, float Amount)
    {
       return  _repositoryTransaction.SumTransactionCard(CardNumber, Amount);
    }

    public void Transfer(Transaction transaction)
    {
        _repositoryTransaction.Transfer(transaction);
    }
}

