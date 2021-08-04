using MemoryEditingSoftware.Core.Entities;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoryEditingSoftware.ProjectSettings.ViewModels
{
    public class ProjectSettingsDialogViewModel : BindableBase, IDialogAware
    {
        public string Title => "Project settings";

        Project project;

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

        private DateTime lastUpdate;
        public DateTime LastUpdate
        {
            get { return lastUpdate; }
            set { SetProperty(ref lastUpdate, value); }
        }

        public DelegateCommand CancelProjectCommand { get; private set; }
        public DelegateCommand UpdateProjectCommand { get; private set; }

        public event Action<IDialogResult> RequestClose;

        public ProjectSettingsDialogViewModel()
        {
            CancelProjectCommand = new DelegateCommand(CancelProject);
            UpdateProjectCommand = new DelegateCommand(UpdateProject);

            this.project = Project.GetInstance();
            if (this.project != null)
            {
                ProjectName = project.ProjectName;
                Description = project.Description;
                Version = project.Version;
                Creator = project.Creator;
                ProgramName = project.ProgramName;
                ExeName = project.ExeName;
                CreationDate = project.CreationDate;
                LastUpdate = project.LastUpdateDate;
            }
            else
            {
                // TODO: Handle user trying to change settings of project when none is initialized
            }
        }

        private void UpdateProject()
        {
            project.ProjectName = ProjectName;
            project.Description = Description;
            project.Version = Version;
            project.Creator = Creator;
            project.ProgramName = ProgramName;
            project.ExeName = ExeName;
            project.LastUpdateDate = LastUpdate;

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
