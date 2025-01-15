using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
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

            _paisaModel.Add(transaction);
            Flush();

        }


        public async Task InitializeData()
        {
            string JsonPath = Path.Combine(FileSystem.AppDataDirectory, "PaisaData.json");
            if (File.Exists(JsonPath))
            {
                try
                {
                    string ReadJsonData = await File.ReadAllTextAsync(JsonPath);
                    _paisaModel = JsonSerializer.Deserialize<List<PaisaModel>>(ReadJsonData) ?? new List<PaisaModel>();

                 
                }
                catch(Exception e)
                {
                    Console.WriteLine(e.Message);
                }
               
            }
            else
            {
                _paisaModel = new List<PaisaModel>();
            }
        }


        // Convert PaisaModel data into jsonfile 
        private async Task Flush()
        {
            string filepath = Path.Combine(FileSystem.AppDataDirectory, "PaisaData.json");
            string json = JsonSerializer.Serialize(_paisaModel);
            await File.WriteAllTextAsync(filepath, json);
        }


        public IEnumerable<PaisaModel> GetAllPaisa() => _paisaModel;
    
        
    }
}
