using MAGIApp.Utils;
using MAGIApp.ViewModel;
using Xamarin.Essentials;
using Xamarin.Forms;
using MAGIApp.Services;
using System.Threading.Tasks;


namespace MAGIApp.Views
{
    public partial class HomePage : ContentPage
    {
        public WebAPiCallServices webAPiCallServices;
        public HomePageViewModel homePageViewModel;
        public HomePage()
        {
            bool loadnumber = true;
            InitializeComponent();
            BindingContext = homePageViewModel = new HomePageViewModel();
            VersionText.Text = VersionTracking.CurrentVersion;
			Appearing += async (sender, args) => await Load();


            if (Util.eventuserstatusid == "1" || Util.eventuserstatusid == "-2" || Util.eventuserstatusid == "-1")
            {
                FirstGrid.IsVisible = true;
                SecondGrid.IsVisible = false;
                //  FirstGrid.IsVisible = false;
                //  SecondGrid.IsVisible = true;
            }
            else
            {
                FirstGrid.IsVisible = false;
                SecondGrid.IsVisible = true;
                //  FirstGrid.IsVisible = true;
                //  SecondGrid.IsVisible = false;
            }
            async Task Load()
            {
                var viewModel = new HomePageViewModel();
                if (loadnumber == true)
                {
                    await viewModel.Load();
                }

                loadnumber = false;
            }
        }

        private double width = 0;
        private double height = 0;
        
        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);
            if (width != this.width || height != this.height)
            {
                this.width = width;
                this.height = height;
                if (width > height)
                {
                    //homePageViewModel.BannerImage = "mobilebanner1.gif";
                    homePageViewModel.heightpara = 45;
                }
                else
                {
                    //homePageViewModel.BannerImage = "LeftImage.gif";
                    homePageViewModel.heightpara = GridLength.Auto;
                }
            }
        }
    }
}
