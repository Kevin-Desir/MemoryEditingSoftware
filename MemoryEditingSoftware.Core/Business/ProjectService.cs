using MemoryEditingSoftware.Core.Entities;
using Newtonsoft.Json;
using System;
using System.IO;

namespace MemoryEditingSoftware.Core.Business
{
    public static class ProjectService
    {
        // save all the current project content/properties in a file
        public static int SaveProject(Project project, string path)
        {
            try
            {
                string projectJson = JsonConvert.SerializeObject(project); // add check --> The project is corrupted
                File.WriteAllText(path, projectJson);
            }
            catch (PathTooLongException ex) 
            {

            }
            catch (DirectoryNotFoundException ex)
            {

            }
            catch (IOException ex)
            {

            }
            catch (UnauthorizedAccessException ex)
            {

            }
            catch (NotSupportedException ex)
            {

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
                Project.UpdateProject(JsonConvert.DeserializeObject<Project>(fileContent));
            }
            catch (Exception ex)
            {
                // todo: error message
            }


            return 0;
        }
    }
}
