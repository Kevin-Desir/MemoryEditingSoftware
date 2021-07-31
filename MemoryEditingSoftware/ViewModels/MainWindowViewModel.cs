using MemoryEditingSoftware.Core;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;

namespace MemoryEditingSoftware.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private string title = "Prism Application";
        private readonly IRegionManager regionManager;

        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }

        public DelegateCommand<string> NavigateCommand { get; private set; }

        public MainWindowViewModel(IRegionManager regionManager)
        {
            NavigateCommand = new DelegateCommand<string>(Navigate);
            this.regionManager = regionManager;
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
