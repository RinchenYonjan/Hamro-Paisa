using HamroPaisa.HamroPaisa.Models;
using HamroPaisa.HamroPaisa.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class PaisaServices : IPaisaService
{

    List<PaisaModel> paisamodels = new List<PaisaModel>()
    {

       

    };

    public IEnumerable<PaisaModel> getAllPaisa() => paisamodels;
  
}
