using Microsoft.AspNetCore.Mvc;
using TrackSpense.BL.Contracts;
using TrackSpense.Shared.BusinessModels;

namespace TrackSpense.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TransactionController:Controller
    {
        private readonly ITransactionService _transactionService;
        public TransactionController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        //add a new transaction
        [HttpPost("AddTransaction")]
        public async Task<Business_Transaction> Post(Business_Transaction transaction)
        {
            transaction.TransactionDate = DateTime.Now;
            return await _transactionService.Add(transaction);
        }

        //get all transactions
        [HttpGet]
        public async Task<List<Business_Transaction>> Get(string UserId)
        {
            return await _transactionService.GetAll(UserId);
        }

        //Delete Transaction
        [HttpDelete]
        public async Task<Business_Transaction> Delete(Business_Transaction transaction)
        {
            return await _transactionService.Delete(transaction);
        }

        //Update Trasaction
        [HttpPut]
        public async Task<Business_Transaction> Update(Business_Transaction transaction)
        {
            return await _transactionService.Update(transaction);
        }

    }
}
