using Prism.Unity;
using PrismUnityApp2.Views;

namespace PrismUnityApp2
{
    public partial class App : PrismApplication
    {
        public App(IPlatformInitializer initializer = null) : base(initializer) { }

        protected override void OnInitialized()
        {
            InitializeComponent();

            NavigationService.NavigateAsync("MainPage");
        }

        protected override void RegisterTypes()
        {
            Container.RegisterTypeForNavigation<MainPage>();
            Container.RegisterTypeForNavigation<Bema>();
            Container.RegisterTypeForNavigation<Hovedside>();
            Container.RegisterTypeForNavigation<Scanner>();
        }
    }
}
