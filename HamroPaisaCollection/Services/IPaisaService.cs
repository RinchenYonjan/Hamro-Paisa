using HamroPaisa.HamroPaisaCollection.Models;


namespace HamroPaisa.HamroPaisaCollection.Services
{
    public interface IPaisaService
    {

        IEnumerable<PaisaModel> GetAllPaisa();
       


        void AddTransaction(PaisaModel transaction);
    }
}

