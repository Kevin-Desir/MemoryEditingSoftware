using MemoryEditingSoftware.Core.Dialogs;
using MemoryEditingSoftware.ProjectSettings.ViewModels;
using MemoryEditingSoftware.ProjectSettings.Views;
using Prism.Ioc;
using Prism.Modularity;

namespace MemoryEditingSoftware.ProjectSettings
{
    public class ProjectSettingsModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
 
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterDialog<NewProjectDialog, NewProjectDialogViewModel>(DialogNames.NewProjectDialog);
            containerRegistry.RegisterDialog<ProjectSettingsDialog, ProjectSettingsDialogViewModel>(DialogNames.ProjectSettingsDialog);
        }
    }
}