using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using Microsoft.Data.Sqlite;
using HamroPaisa.HamroPaisaCollection.Models;

namespace HamroPaisa.HamroPaisaCollection.Services
{
    public class PaisaServices : IPaisaService
    {
        private static readonly string _cacheDirectory = FileSystem.Current.CacheDirectory;
        private const string _dataFile = "data.json";
        private readonly string _filePath = Path.Combine(_cacheDirectory, _dataFile);
        private readonly string _dbPath = Path.Combine(_cacheDirectory, "PaisaDatabase.db");


        List<PaisaModel> _paisaModel = new List<PaisaModel>();


        public void AddTransaction(PaisaModel transaction)
        {
            try
            {
                _paisaModel.Add(transaction);
                Flush();
                SaveTransactionToDb(transaction);
            }
            catch (Exception ex)
            {
                LogErrorToFile("D:\\ErrorLog.txt", ex);
            }
        }


        public void Initialize()
        {
            try
            {
                LogDatabasePathToFile("D:\\DatabaseLog.txt");

                if (File.Exists(_filePath))
                {
                    var data = File.ReadAllText(_filePath);
                    _paisaModel = JsonSerializer.Deserialize<List<PaisaModel>>(data) ?? new List<PaisaModel>();
                }
                else
                {
                    _paisaModel = new List<PaisaModel>();
                }

                InitializeDatabase();
            }
            catch (Exception ex)
            {
                LogErrorToFile("D:\\ErrorLog.txt", ex);
            }
        }

        // Convert PaisaModel data into jsonfile 
        private void Flush()
        {
            try
            {
                var data = JsonSerializer.Serialize(_paisaModel);
                File.WriteAllText(_filePath, data);
            }
            catch (Exception ex)
            {
                LogErrorToFile("D:\\ErrorLog.txt", ex);
            }
        }

        public IEnumerable<PaisaModel> GetAllPaisa()
        {
            try
            {
                return _paisaModel;
            }
            catch (Exception ex)
            {
                LogErrorToFile("D:\\ErrorLog.txt", ex);
                return Enumerable.Empty<PaisaModel>();
            }
        }

        private void InitializeDatabase()
        {
            try
            {
                // Create SQLite database and table if it does not exist
                using var connection = new SqliteConnection($"Data Source={_dbPath}");
                connection.Open();

                var createTableQuery = @"
                    CREATE TABLE IF NOT EXISTS Transactions (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        Description TEXT,
                        Amount REAL,
                        Date TEXT
                    );";

                using var command = new SqliteCommand(createTableQuery, connection);
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                LogErrorToFile("D:\\ErrorLog.txt", ex);
            }
        }

        private void SaveTransactionToDb(PaisaModel transaction)
        {
            try
            {
                using var connection = new SqliteConnection($"Data Source={_dbPath}");
                connection.Open();

                var insertQuery = @"
                    INSERT INTO Transactions (Description, Amount, Date)
                    VALUES (@Description, @Amount, @Date);";

                using var command = new SqliteCommand(insertQuery, connection);
                command.Parameters.AddWithValue("@Description", transaction.Description);
                command.Parameters.AddWithValue("@Amount", transaction.Amount);
                command.Parameters.AddWithValue("@Date", transaction.Date.ToString("o")); // ISO 8601 format

                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                LogErrorToFile("D:\\ErrorLog.txt", ex);
            }
        }

        public IEnumerable<PaisaModel> GetAllFromDb()
        {
            try
            {
                var transactions = new List<PaisaModel>();

                using var connection = new SqliteConnection($"Data Source={_dbPath}");
                connection.Open();

                var selectQuery = "SELECT Description, Amount, Date FROM Transactions";

                using var command = new SqliteCommand(selectQuery, connection);
                using var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    transactions.Add(new PaisaModel
                    {
                        Description = reader.GetString(0),
                        Amount = (decimal)reader.GetDouble(1),
                        Date = DateTime.Parse(reader.GetString(2))
                    });
                }

                return transactions;
            }
            catch (Exception ex)
            {
                LogErrorToFile("D:\\ErrorLog.txt", ex);
                return Enumerable.Empty<PaisaModel>();
            }
        }

        private void LogDatabasePathToFile(string logFileName)
        {
            try
            {
                var logFilePath = logFileName; // Use the absolute path provided
                File.WriteAllText(logFilePath, $"Database file is located at: {_dbPath}");
            }
            catch (Exception ex)
            {
                LogErrorToFile("D:\\ErrorLog.txt", ex);
            }
        }

        private void LogErrorToFile(string logFileName, Exception ex)
        {
            var logFilePath = logFileName;
            var logMessage = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {ex.Message}\n{ex.StackTrace}\n";
            File.AppendAllText(logFilePath, logMessage);
        }
    }
}
