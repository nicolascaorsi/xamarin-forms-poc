using System.Net.Http;
using DryIoc;
using Prism;
using Prism.DryIoc;
using Prism.Ioc;
using Prism.Modularity;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Company.MyApp
{
    public partial class Application : PrismApplication
    {
        /* 
         * The Xamarin Forms XAML Previewer in Visual Studio uses System.Activator.CreateInstance.
         * This imposes a limitation in which the App class must have a default constructor. 
         * App(IPlatformInitializer initializer = null) cannot be handled by the Activator.
         */
        public Application() : base(null)
        {
        }

        public Application(IPlatformInitializer initializer) : base(initializer)
        {
        }

        protected override async void OnInitialized()
        {
            InitializeComponent();
            await NavigationService.NavigateAsync($"NavigationPage/{Company.Modules.Message.MessageModule.MainPageUri}");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<NavigationPage>();
        }

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            moduleCatalog.AddModule<Company.Modules.Message.MessageModule>();
        }

        protected override Rules CreateContainerRules()
        {
            return base.CreateContainerRules()
                       .WithAutoConcreteTypeResolution()
                       .With(Made.Of(FactoryMethod.ConstructorWithResolvableArguments))
                       .WithDefaultIfAlreadyRegistered(IfAlreadyRegistered.Replace)
                       .WithTrackingDisposableTransients();
        }
    }
}