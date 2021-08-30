using MemoryEditingSoftware.Core.Entities;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;

namespace MemoryEditingSoftware.ProjectSettings.ViewModels
{
    public class NewProjectDialogViewModel : BindableBase, IDialogAware
    {
        public string Title => "New project";

        private string projectName;
        public string ProjectName
        {
            get { return projectName; }
            set { SetProperty(ref projectName, value); }
        }

        private string description;
        public string Description
        {
            get { return description; }
            set { SetProperty(ref description, value); }
        }

        private string version;
        public string Version
        {
            get { return version; }
            set { SetProperty(ref version, value); }
        }

        private string creator;
        public string Creator
        {
            get { return creator; }
            set { SetProperty(ref creator, value); }
        }

        private string programName;
        public string ProgramName
        {
            get { return programName; }
            set { SetProperty(ref programName, value); }
        }

        private string exeName;
        public string ExeName
        {
            get { return exeName; }
            set { SetProperty(ref exeName, value); }
        }

        private DateTime creationDate;
        public DateTime CreationDate
        {
            get { return creationDate; }
            set { SetProperty(ref creationDate, value); }
        }

        public DelegateCommand CancelProjectCommand { get; private set; }
        public DelegateCommand CreateProjectCommand { get; private set; }

        public event Action<IDialogResult> RequestClose;

        public NewProjectDialogViewModel()
        {
            CancelProjectCommand = new DelegateCommand(CancelProject);
            CreateProjectCommand = new DelegateCommand(CreateProject);
        }

        private void CreateProject()
        {
            // Create an instance of the singletone project class to use it across the application
            Project.CreateInstance(
                ProjectName,
                Description,
                Version,
                Creator,
                ProgramName,
                ExeName,
                CreationDate,
                CreationDate,
                null, // TODO: handle date of creation and last updated
                null
                );

            RequestClose?.Invoke(new DialogResult(ButtonResult.OK));
        }

        private void CancelProject()
        {
            RequestClose?.Invoke(new DialogResult(ButtonResult.Cancel));
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
