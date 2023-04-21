using MemoryEditingSoftware.Core.Dialogs;
using MemoryEditingSoftware.Editor;
using MemoryEditingSoftware.ProjectSettings;
using MemoryEditingSoftware.Run;
using MemoryEditingSoftware.Views;
using Prism.Ioc;
using Prism.Modularity;
using System.Windows;

namespace MemoryEditingSoftware
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterDialog<SimpleDialogView, SimpleDialogViewModel>();
        }

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            moduleCatalog.AddModule<EditorModule>();
            moduleCatalog.AddModule<ProjectSettingsModule>();
            moduleCatalog.AddModule<RunModule>();
        }
    }
}
