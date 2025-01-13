//using AndroidX.Startup;

using HamroPaisa.HamroPaisaCollection.Services;

namespace HamroPaisa
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            PaisaServices ps = new PaisaServices();
            ps.Initialize();
        }
    }
}
