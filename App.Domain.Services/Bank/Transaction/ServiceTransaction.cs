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
   
        IRepositoryTransaction repositoryTransaction;
    public ServiceTransaction()
    {
        repositoryTransaction = new RepositoryTransaction();
       
    }

    public void GenerateVerificationCode(string CardSouNumber)
    {
        repositoryTransaction.GenerateVerificationCode(CardSouNumber);
    }

    public Card GetCard(string CardNumber)
    {
       return  repositoryTransaction.GetCard(CardNumber);
    }

    public List<GetTrranDto> GetListOfTransactions(string CardNumber)
    {
        return repositoryTransaction.GetListOfTransactions(CardNumber);
    }

    public string ReadVerificationCode()
    {
        return repositoryTransaction.ReadVerificationCode();
    }

    public float SumTransactionCard(string CardNumber, float Amount)
    {
       return  repositoryTransaction.SumTransactionCard(CardNumber, Amount);
    }

    public void Transfer(Transaction transaction)
    {
        repositoryTransaction.Transfer(transaction);
    }
}

