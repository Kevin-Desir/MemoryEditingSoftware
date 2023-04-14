using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoryEditingSoftware.Core.Dialogs
{
    public class SimpleDialogViewModel : BindableBase, IDialogAware
    {
        /// <summary>
        /// Title of the dialog.
        /// </summary>
        public string Title { get; private set; } = "Information";

        /// <summary>
        /// type of message :
        /// - Error
        /// - Information
        /// </summary>
        private DialogTypes dialogType;
        public DialogTypes DialogType
        {
            get { return dialogType; }
            set { SetProperty(ref dialogType, value); }
        }

        /// <summary>
        /// Message displayed in the dialog box.
        /// </summary>
        private string message;
        public string Message
        {
            get { return message; }
            set { SetProperty(ref message, value); }
        }

        /// <summary>
        /// Action to request close and transmit the ButtonResult.
        /// </summary>
        public event Action<IDialogResult> RequestClose;
        public DelegateCommand CloseDialogCommand { get; }

        public SimpleDialogViewModel()
        {
            CloseDialogCommand = new DelegateCommand(CloseDialog);
        }
        private void CloseDialog()
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
            Message = parameters.GetValue<string>("message");
            DialogType = parameters.GetValue<DialogTypes>("type");

            Title = DialogType.ToString();
        }
    }
}
