using MemoryEditingSoftware.Core;
using MemoryEditingSoftware.Core.Dialogs;
using MemoryEditingSoftware.Core.Entities;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using Prism.Services.Dialogs;
using System;

namespace MemoryEditingSoftware.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private string title = "Memory Editing Software";
        private readonly IRegionManager regionManager;
        private readonly IDialogService dialogService;

        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }

        public DelegateCommand<string> NavigateCommand { get; private set; }
        public DelegateCommand ShowProjectSettingsDialogCommand{ get; set; }
        public DelegateCommand ShowNewProjectDialogCommand { get; set; }

        public MainWindowViewModel(IRegionManager regionManager, IDialogService dialogService)
        {
            this.regionManager = regionManager;
            this.dialogService = dialogService;

            NavigateCommand = new DelegateCommand<string>(Navigate);
            ShowNewProjectDialogCommand = new DelegateCommand(ShowNewProjectDialog);
            ShowProjectSettingsDialogCommand = new DelegateCommand(ShowProjectSettingsDialog);
        }

        private void ShowNewProjectDialog()
        {
            dialogService.ShowDialog(DialogNames.NewProjectDialog, r =>
            {
                if (r.Result == ButtonResult.OK)
                {
                    // TODO:
                    Console.WriteLine("OK:" + Project.GetInstance());
                }
                else
                {
                    // TODO:
                    Console.WriteLine("CANCEL: " + Project.GetInstance());
                }
            });
        }

        private void ShowProjectSettingsDialog()
        {
            dialogService.ShowDialog(DialogNames.ProjectSettingsDialog, r =>
            {
                if (r.Result == ButtonResult.OK)
                {
                    // TODO:
                }
                else
                {
                    // TODO:
                }
            });
        }

        private void Navigate(string viewName)
        {
            this.regionManager.RequestNavigate(RegionNames.ContentRegion, viewName, Callback);
        }

        private void Callback(NavigationResult result)
        {
            if (result.Error != null)
            {
                // TODO: handle error
            }
        }
    }
}
