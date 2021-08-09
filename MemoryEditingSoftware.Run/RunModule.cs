using MemoryEditingSoftware.Core.Dialogs;
using MemoryEditingSoftware.Run.ViewModels;
using MemoryEditingSoftware.Run.Views;
using Prism.Ioc;
using Prism.Modularity;

namespace MemoryEditingSoftware.Run
{
    public class RunModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
 
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterDialog<RunDialog, RunDialogViewModel>(DialogNames.RunDialog);
        }
    }
}