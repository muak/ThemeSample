using System.Linq;
using System.Reflection;
using Microsoft.Practices.ObjectBuilder2;
using Prism.Unity;
using ThemeSample.Views;

namespace ThemeSample
{
    public partial class App : PrismApplication
    {
        public App(IPlatformInitializer initializer = null) : base(initializer) { }

        protected override void OnInitialized()
        {
            InitializeComponent();

            NavigationService.NavigateAsync("MyNavigationPage/MainPage");
        }

        protected override void RegisterTypes()
        {
            this.GetType().GetTypeInfo().Assembly.DefinedTypes
                          .Where(t => t.Namespace.EndsWith(".Views", System.StringComparison.Ordinal))
                          .ForEach(t => {
                              Container.RegisterTypeForNavigation(t.AsType(), t.Name);
                          });
        }

    }
}

