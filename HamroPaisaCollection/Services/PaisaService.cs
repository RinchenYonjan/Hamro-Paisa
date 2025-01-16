using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using HamroPaisa.HamroPaisaCollection.Models;
using MudBlazor;

namespace HamroPaisa.HamroPaisaCollection.Services
{
    public class PaisaServices : IPaisaService
    {
        private static readonly string _cacheDirectory = FileSystem.Current.CacheDirectory;
        private const string _dataFile = "data.json";
        private readonly IDialogService _dialogService;
        private readonly string _filePath = Path.Combine(_cacheDirectory, _dataFile);
        private readonly string _dbPath = Path.Combine(_cacheDirectory, "PaisaDatabase.db");

        List<PaisaModel> _paisaModel = new List<PaisaModel>();


        public PaisaServices(IDialogService dialogService)
        {
            _dialogService = dialogService;
        }


        public void AddTransaction(PaisaModel transaction)
        {
  
            _paisaModel.Add(transaction);
            Flush();
        }


        // Method on initialized
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
                catch (Exception e)
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


        // Method to export JSON file to desktop
        public async Task ExportToDesktopAsync()
        {
            try
            {
                // Get desktop path
                string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);

                // Define export path
                string exportFilePath = Path.Combine(desktopPath, "PaisaData.json");

                // Serialize current data
                string json = JsonSerializer.Serialize(_paisaModel);

                // Write to the desktop
                await File.WriteAllTextAsync(exportFilePath, json);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error exporting file: {e.Message}");
            }
        }


        // Method to clear data in json file
        public async Task ClearData()
        {
            // Clear in-memory data
            _paisaModel.Clear();

            // Define the path to the JSON file
            string jsonPath = Path.Combine(FileSystem.AppDataDirectory, "PaisaData.json");

            // Overwrite the JSON file with an empty array
            try
            {
                string emptyJson = JsonSerializer.Serialize(new List<PaisaModel>()); // Empty list
                await File.WriteAllTextAsync(jsonPath, emptyJson); // Clear the file content
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error clearing JSON file: {e.Message}");
            }
        }



    }
}
