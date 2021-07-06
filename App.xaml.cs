using System.Windows;
using Prism.Ioc;
using ProteiTestApp.Views;

namespace ProteiTestApp
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App
    {
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {

        }
        protected override Window CreateShell()
        {
            return Container.Resolve<StopwatchApp>();
        }
    }
}
