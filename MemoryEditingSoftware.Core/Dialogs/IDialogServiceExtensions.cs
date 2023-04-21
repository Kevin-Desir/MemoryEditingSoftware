using Prism.Services.Dialogs;
using System;
using System.Windows;

namespace MemoryEditingSoftware.Core.Dialogs
{
    public static class IDialogServiceExtensions
    {

        /// <summary>
        /// Show a Dialog diplaying an error or an information to the user.
        /// 
        /// How to use it: 
        /// 
        /// Declare the service:
        /// <code>
        ///     private IDialogService _dialogService;
        /// </code>
        /// 
        /// Get the dialog service in the constructor (as a parameter given by Prism (IOC)):
        /// <code>
        ///     public MemoryEditingSoftware.Run(IDialogService dialogService) // example for the parameter: replace with your constructor.
        ///     _dialogService = dialogService;
        /// </code>
        /// 
        /// Call the function to open the dialog box and display the error message: 
        /// <code>
        ///     _dialogService.ShowSimpleDialog("Error occured...", DialogTypes.Error, null);
        /// </code>
        /// 
        /// </summary>
        /// <param name="dialogService">The IDialogService, passed automatically.</param>
        /// <param name="message">The message to display in the dialog.</param>
        /// <param name="callback">The result of the dialog (OK).</param>
        public static void ShowSimpleDialog(this IDialogService dialogService, string message, DialogTypes dialogType, Action<IDialogResult> callback)
        {
            DialogParameters param = new DialogParameters();
            param.Add("message", message);
            param.Add("type", dialogType);

            Application.Current.Dispatcher.Invoke((Action)delegate
            {
                dialogService.ShowDialog(DialogNames.SimpleDialog, param, callback);
            });
        }

    }
}
