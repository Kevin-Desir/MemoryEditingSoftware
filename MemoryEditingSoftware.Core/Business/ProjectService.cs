using MemoryEditingSoftware.Core.Entities;
using MemoryEditingSoftware.Core.Events;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Windows;

namespace MemoryEditingSoftware.Core.Business
{
    public static class ProjectService
    {
        public static JsonSerializerSettings Settings
        {
            get
            {
                return new JsonSerializerSettings()
                {
                    TypeNameHandling = TypeNameHandling.All,
                };
            }
        }

        // save all the current project content/properties in a file
        public static int SaveProject(Project project, string path)
        {
            try
            {
                string projectJson = JsonConvert.SerializeObject(project, Settings); // add check --> The project is corrupted
                File.WriteAllText(path, projectJson);
            }
            catch (Exception ex)
            {
                // unhandled exception
            }

            return 0;
        }

        // read inside a file to retrieve all the project's content
        public static int LoadProject(string path)
        {
            // need to check if a project was previously loaded or not
            if (Project.GetInstance() == null)
            {
                Project.CreateInstance(path);
            }
            else
            {
                Project.CreateNewInstance(path);
            }

            try
            {
                string fileContent = File.ReadAllText(path);
                Project.UpdateProject(JsonConvert.DeserializeObject<Project>(fileContent, Settings));
            }
            catch (Exception ex)
            {
                // todo: error message
            }


            return 0;
        }
    }
}
