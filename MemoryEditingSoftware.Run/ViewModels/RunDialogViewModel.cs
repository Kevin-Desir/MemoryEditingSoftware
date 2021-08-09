using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;

namespace MemoryEditingSoftware.Run.ViewModels
{
    public class RunDialogViewModel : BindableBase, IDialogAware
    {
        public string Title => "Run Project";

        public DelegateCommand CloseCommand { get; private set; }

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
