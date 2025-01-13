//using HamroPaisa.HamroPaisaCollection.Models;
//using HamroPaisa.HamroPaisaCollection.Services;
//using System.Text.Json;
//using System.Transactions;


//namespace HamroPaisa.HamroPaisaCollection.Services
//{

//    // Class that extend IPaisaService interface
//    public class PaisaServices : IPaisaService
//    {
//        private static readonly string _cacheDirectory = FileSystem.Current.CacheDirectory;
//        private const string _dataFile = "data.jason";
//        private readonly string _filePath = Path.Combine(_cacheDirectory, _dataFile);


//        private List<PaisaModel> _paisamodel = 
//        [   new PaisaModel { Description = "Alu Paratha", Date = DateTime.Parse("2024-11-2"), Amount = 150, Type = "Expense" },
//            new PaisaModel { Description = "Buff Momo", Date = DateTime.Parse("2024-11-2"), Amount = 150, Type = "Expense" },
//            new PaisaModel { Description = "Milk Coffee", Date = DateTime.Parse("2024-11-2"), Amount = 150, Type = "Expense" }
//        ];


//        public void Add(string money)
//        {
//            _paisamodel.Add(money);
//            Flush();
//        }



//        public IEnumerable<PaisaModel> GetAllPaisa() => _paisamodel.GetAllPaisa(
//            orderings: [
//                  (OrderBy.Ascending, x => x.AscendingOrder)
//                  (OrderBy.Descending, x => x.DescendingOrder)
//                ]);


//        public void Initialize()
//        {
//            if (File.Exists(_filePath))
//            {
//                var data = File.ReadAllText(_filePath);
//                _paisamodel = JsonSerializer.Deserialize<List<PaisaModel>>(data) ?? [];
//            }
//            else
//            {
//                _paisamodel = [];
//            }
//        }

//        private void Flush()
//        {
//            var data = JsonSerializer.Serialize(_paisamodel);
//            File.WriteAllText(_filePath, data);
//        }

//    }


//}

