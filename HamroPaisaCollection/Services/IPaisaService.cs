using HamroPaisa.HamroPaisaCollection.Models;


namespace HamroPaisa.HamroPaisaCollection.Services
{
    public interface IPaisaService
    {

        IEnumerable<PaisaModel> GetAllPaisa();

        Task InitializeData();

        void AddTransaction(PaisaModel transaction);
    }
}

