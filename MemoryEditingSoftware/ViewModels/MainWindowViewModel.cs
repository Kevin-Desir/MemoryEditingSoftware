using MemoryEditingSoftware.Core;
using MemoryEditingSoftware.Core.Business;
using MemoryEditingSoftware.Core.Dialogs;
using MemoryEditingSoftware.Core.Entities;
using MemoryEditingSoftware.Core.Events;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using Prism.Services.Dialogs;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Forms;

namespace MemoryEditingSoftware.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private const string RECENT_PROJECTS_FILENAME = @"C:\ProgramData\PUMA\RecentProjects.con";

        private string title = "Memory Editing Software";
        private readonly IRegionManager regionManager;
        private readonly IDialogService dialogService;
        private readonly IEventAggregator eventAggregator;

        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }

        private ObservableCollection<string> recentProjects;
        public ObservableCollection<string> RecentProjects
        {
            get { return recentProjects; }
            set { SetProperty(ref recentProjects, value); }
        }

        public DelegateCommand<string> NavigateCommand { get; private set; }
        public DelegateCommand ShowProjectSettingsDialogCommand { get; private set; }
        public DelegateCommand ShowNewProjectDialogCommand { get; private set; }
        public DelegateCommand<Window> QuitCommand { get; private set; }
        public DelegateCommand SaveProjectCommand { get; private set; }
        public DelegateCommand SaveProjectAsCommand { get; private set; }
        public DelegateCommand OpenProjectDialogCommand { get; private set; }
        public DelegateCommand RunDialogCommand { get; private set; }
        public DelegateCommand<string> OpenRecentProjectCommand { get; private set; }
        public DelegateCommand CloseProjectCommand { get; private set; }

        public MainWindowViewModel(IRegionManager regionManager, IDialogService dialogService, IEventAggregator eventAggregator)
        {
            this.regionManager = regionManager;
            this.dialogService = dialogService;
            this.eventAggregator = eventAggregator;

            NavigateCommand = new DelegateCommand<string>(Navigate);
            ShowNewProjectDialogCommand = new DelegateCommand(ShowNewProjectDialog);
            ShowProjectSettingsDialogCommand = new DelegateCommand(ShowProjectSettingsDialog);
            QuitCommand = new DelegateCommand<Window>(Quit);
            SaveProjectCommand = new DelegateCommand(SaveProject);
            SaveProjectAsCommand = new DelegateCommand(SaveProjectAs);
            OpenProjectDialogCommand = new DelegateCommand(OpenProjectDialog);
            RunDialogCommand = new DelegateCommand(Run);
            OpenRecentProjectCommand = new DelegateCommand<string>(OpenRecentProject);
            CloseProjectCommand = new DelegateCommand(CloseProject);

            eventAggregator.GetEvent<ShowErrorMessageEvent>().Subscribe(OnShowErrorMessageEventReceived);

            RecentProjects = new ObservableCollection<string>();

            if (File.Exists(RECENT_PROJECTS_FILENAME))
            {
                string[] lines = File.ReadAllLines(RECENT_PROJECTS_FILENAME);

                for (int i = lines.Length - 1; RecentProjects.Count < 10 && i > -1; i--)
                {
                    if (File.Exists(lines[i]) && !RecentProjects.Contains(lines[i]))
                    {
                        RecentProjects.Add(lines[i]);
                    }
                }
            }
            else
            {
                File.Create(RECENT_PROJECTS_FILENAME);
            }
        }

        private void OnShowErrorMessageEventReceived(string errorMessage)
        {
            this.dialogService.ShowDialog(DialogNames.ErrorDialog, new DialogParameters($"message={errorMessage}"), null);
        }

        private void CloseProject()
        {
            if (Project.GetInstance() != null)
            {
                Project.CloseProject();
            }
        }

        private void OpenRecentProject(string path)
        {
            if (path != null && !string.IsNullOrWhiteSpace(path))
            {
                ProjectService.LoadProject(path);
                Console.WriteLine(Project.GetInstance().Path);
                this.regionManager.Regions[RegionNames.ContentRegion.ToString()].RemoveAll();

                File.AppendAllText(RECENT_PROJECTS_FILENAME, $"{path}\n");
            }
        }

        private void Run()
        {
            if (Project.GetInstance() != null)
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
        }

        private void OpenProjectDialog()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "mes files (*.mes)|*.mes|All files (*.*)|*.*"
            };

            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (openFileDialog.FileName != null)
                {
                    ProjectService.LoadProject(openFileDialog.FileName);
                    Console.WriteLine(Project.GetInstance().Path);
                    this.regionManager.Regions[RegionNames.ContentRegion.ToString()].RemoveAll();

                    File.AppendAllText(RECENT_PROJECTS_FILENAME, $"{openFileDialog.FileName}\n");
                }
            }
        }

        private void SaveProjectAs()
        {
            Project project = Project.GetInstance();

            if (project != null)
            {
                System.Windows.Forms.SaveFileDialog saveFileDialog = new System.Windows.Forms.SaveFileDialog
                {
                    Filter = "mes files (*.mes)|*.mes|All files (*.*)|*.*"
                };

                if (saveFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    if (saveFileDialog.FileName != null)
                    {
                        ProjectService.SaveProject(project, saveFileDialog.FileName);
                        Console.WriteLine(saveFileDialog.FileName);
                        project.Path = saveFileDialog.FileName;

                        File.AppendAllText(RECENT_PROJECTS_FILENAME, $"{saveFileDialog.FileName}\n");
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
                    System.Windows.Forms.SaveFileDialog saveFileDialog = new System.Windows.Forms.SaveFileDialog
                    {
                        Filter = "mes files (*.mes)|*.mes|All files (*.*)|*.*"
                    };

                    if (saveFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        if (saveFileDialog.FileName != null)
                        {
                            ProjectService.SaveProject(project, saveFileDialog.FileName);
                            Console.WriteLine(saveFileDialog.FileName);
                            project.Path = saveFileDialog.FileName;

                            File.AppendAllText(RECENT_PROJECTS_FILENAME, $"{saveFileDialog.FileName}\n");
                        }
                    }
                }
            }
        }

        private void Quit(Window window)
        {
            window?.Close();
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
            this.regionManager.RequestNavigate(RegionNames.ContentRegion.ToString(), viewName, Callback);
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
