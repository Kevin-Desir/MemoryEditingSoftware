using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;

namespace MemoryEditingSoftware.ErrorHandling.Dialogs.ViewModels
{
    public class ErrorDialogViewModel : BindableBase, IDialogAware
    {
        #region Properties

        /// <summary>
        /// The title of the dialog box.
        /// </summary>
        public string Title => "An Error Occured";

        /// <summary>
        /// The error message displayed in the error dialog.
        /// </summary>
        private string errorMessage;
        public string ErrorMessage
        {
            get { return errorMessage; }
            set { SetProperty(ref errorMessage, value); }
        }

        #endregion

        #region Commands and Events

        /// <summary>
        /// Command to close the error dialog box.
        /// </summary>
        public DelegateCommand CloseCommand { get; private set; }

        public event Action<IDialogResult> RequestClose;

        #endregion

        #region Constructor 

        public ErrorDialogViewModel()
        {
            CloseCommand = new DelegateCommand(Close);
        }

        #endregion

        #region Private Methods

        private void Close()
        {
            RequestClose?.Invoke(null);
        }

        #endregion

        #region Public Methods

        public bool CanCloseDialog()
        {
            return true;
        }

        public void OnDialogClosed()
        {

        }

        public void OnDialogOpened(IDialogParameters parameters)
        {
            ErrorMessage = parameters.GetValue<string>("message");
        }

        #endregion
    }
}
