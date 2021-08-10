using MemoryEditingSoftware.Run.Views;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using Prism.Services.Dialogs;
using System;
using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace MemoryEditingSoftware.Run.ViewModels
{
    public class RunDialogViewModel : BindableBase, IDialogAware
    {
        public string Title => "Run Project";

        public DelegateCommand CloseCommand { get; private set; }

        private ObservableCollection<ReadItem> readItems;

        public ObservableCollection<ReadItem> ReadItems
        {
            get { return readItems; }
            set { SetProperty(ref readItems, value); }
        }

        public event Action<IDialogResult> RequestClose;

        public RunDialogViewModel()
        {
            CloseCommand = new DelegateCommand(Close);
        }

        private void Close()
        {
            RequestClose?.Invoke(new DialogResult(ButtonResult.OK));
        }

        public bool CanCloseDialog()
        {
            return true;
        }

        public void OnDialogClosed()
        {
            
        }

        public void OnDialogOpened(IDialogParameters parameters)
        {
            
        }
    }
}
