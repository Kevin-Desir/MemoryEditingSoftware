using MemoryEditingSoftware.ErrorHandling.Dialogs.ViewModels;
using MemoryEditingSoftware.ErrorHandling.Dialogs.Views;
using MemoryEditingSoftware.Core.Dialogs;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace MemoryEditingSoftware.ErrorHandling
{
    public class ErrorHandlingModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {

        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterDialog<ErrorDialogView, ErrorDialogViewModel>(DialogNames.ErrorDialog);
        }
    }
}