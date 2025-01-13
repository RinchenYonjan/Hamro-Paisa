//using HamroPaisa.HamroPaisaCollection.Models;
//using SQLite;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Transactions;


//namespace HamroPaisa
//{
//    public class LocalDbService
//    {
//        private const string DB_NAME = "HamroPaisa_Local_db.db3";
//        private readonly SQLiteAsyncConnection _connection;


//        public LocalDbService()
//        {
//            _connection = new SQLiteAsyncConnection(Path.Combine(FileSystem.AppDataDirectory, DB_NAME));
//            _connection.CreateTableAsync<PaisaModel>();

//        }

//        public async Task<List<PaisaModel>> GetAllPaisa()
//        {
//            return await _connection.Table<PaisaModel>().ToListAsync();
//        }

//        public async Task CreateTransaction(Transaction transaction)
//        {
//            await _connection.InsertAsync(transaction);
//        }
//    }
//}
