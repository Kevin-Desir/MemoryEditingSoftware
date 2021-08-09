using MemoryEditingSoftware.Core;
using MemoryEditingSoftware.Core.Business;
using MemoryEditingSoftware.Core.Dialogs;
using MemoryEditingSoftware.Core.Entities;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using Prism.Services.Dialogs;
using System;
using System.Windows;
using System.Windows.Forms;

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
        public DelegateCommand ShowProjectSettingsDialogCommand { get; private set; }
        public DelegateCommand ShowNewProjectDialogCommand { get; private set; }
        public DelegateCommand<Window> QuitCommand { get; private set; }
        public DelegateCommand SaveProjectCommand { get; private set; }
        public DelegateCommand SaveProjectAsCommand { get; private set; }
        public DelegateCommand OpenProjectDialogCommand { get; private set; }
        public DelegateCommand RunDialogCommand { get; private set; }

        public MainWindowViewModel(IRegionManager regionManager, IDialogService dialogService)
        {
            this.regionManager = regionManager;
            this.dialogService = dialogService;

            NavigateCommand = new DelegateCommand<string>(Navigate);
            ShowNewProjectDialogCommand = new DelegateCommand(ShowNewProjectDialog);
            ShowProjectSettingsDialogCommand = new DelegateCommand(ShowProjectSettingsDialog);
            QuitCommand = new DelegateCommand<Window>(Quit);
            SaveProjectCommand = new DelegateCommand(SaveProject);
            SaveProjectAsCommand = new DelegateCommand(SaveProjectAs);
            OpenProjectDialogCommand = new DelegateCommand(OpenProjectDialog);
            RunDialogCommand = new DelegateCommand(Run);
        }

        private void Run()
        {
            dialogService.ShowDialog(DialogNames.RunDialog, r =>
            {
                //if (r.Result == ButtonResult.OK)
                //{
                //TODO:
                //    Console.WriteLine("New project OK:" + Project.GetInstance());
                //}
                //else
                //{
                //TODO:
                //    Console.WriteLine("New project CANCEL: " + Project.GetInstance());
                //}
            });
        }

        private void OpenProjectDialog()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "mes files (*.mes)|*.mes|All files (*.*)|*.*";

            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (openFileDialog.FileName != null)
                {
                    ProjectService.LoadProject(openFileDialog.FileName);
                    Console.WriteLine(Project.GetInstance().Path);
                    this.regionManager.Regions[RegionNames.ContentRegion].RemoveAll();
                }
            }
        }

        private void SaveProjectAs()
        {
            Project project = Project.GetInstance();

            if (project != null)
            {
                System.Windows.Forms.SaveFileDialog saveFileDialog = new System.Windows.Forms.SaveFileDialog();
                saveFileDialog.Filter = "mes files (*.mes)|*.mes|All files (*.*)|*.*";

                if (saveFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    if (saveFileDialog.FileName != null)
                    {
                        ProjectService.SaveProject(project, saveFileDialog.FileName);
                        Console.WriteLine(saveFileDialog.FileName);
                        project.Path = saveFileDialog.FileName;
                    }
                }
            }
        }

        private void SaveProject()
        {
            Project project = Project.GetInstance();

            if (project != null)
            {
                if (project.Path != null && !string.IsNullOrWhiteSpace(project.Path) && !string.IsNullOrEmpty(project.Path))
                {
                    ProjectService.SaveProject(project, project.Path);
                }
                else
                {
                    System.Windows.Forms.SaveFileDialog saveFileDialog = new System.Windows.Forms.SaveFileDialog();
                    saveFileDialog.Filter = "mes files (*.mes)|*.mes|All files (*.*)|*.*";

                    if (saveFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        if (saveFileDialog.FileName != null)
                        {
                            ProjectService.SaveProject(project, saveFileDialog.FileName);
                            Console.WriteLine(saveFileDialog.FileName);
                            project.Path = saveFileDialog.FileName;
                        }
                    }
                }
            }
        }

        private void Quit(Window window)
        {
            if (window != null)
            {
                window.Close();
            }
        }

        private void ShowNewProjectDialog()
        {
            dialogService.ShowDialog(DialogNames.NewProjectDialog, r =>
            {
                if (r.Result == ButtonResult.OK)
                {
                        // TODO:
                        Console.WriteLine("New project OK:" + Project.GetInstance());
                }
                else
                {
                        // TODO:
                        Console.WriteLine("New project CANCEL: " + Project.GetInstance());
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
                        Console.WriteLine("Update project OK:" + Project.GetInstance());
                }
                else
                {
                        // TODO:
                        Console.WriteLine("Update project CANCEL: " + Project.GetInstance());
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
