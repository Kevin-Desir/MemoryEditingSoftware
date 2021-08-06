using MemoryEditingSoftware.Core.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace MemoryEditingSoftware.Core.Business
{
    public static class ProjectService
    {
        public static int SaveProject(Project project, string path)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append($"ProjectName*{project.ProjectName}\n");
            sb.Append($"Description*{project.Description}\n");
            sb.Append($"Version*{project.Version}\n");
            sb.Append($"Creator*{project.Creator}\n");
            sb.Append($"ProgramName*{project.ProgramName}\n");
            sb.Append($"ExeName*{project.ExeName}\n");
            sb.Append($"CreationDate*{project.CreationDate.ToString()}\n");
            sb.Append($"LastUpdate*{project.LastUpdateDate.ToString()}\n");

            if (project.EditItems != null)
            {
                foreach (EditItem ei in project.EditItems)
                {
                    sb.Append($"{ei.ID}*Name*{ei.Name}\n");
                    sb.Append($"{ei.ID}*Address*{ei.Address}\n");
                    sb.Append($"{ei.ID}*IsRead*{ei.IsRead}\n");
                    sb.Append($"{ei.ID}*Value*{ei.Value}\n");
                    sb.Append($"{ei.ID}*IsLoop*{ei.IsLoop}\n");
                    sb.Append($"{ei.ID}*IsEnterValue*{ei.IsEnterValue}\n");
                }
            }

            System.IO.File.WriteAllText(path, sb.ToString());

            return 0;
        }

        public static int LoadProject(string path)
        {
            if (Project.GetInstance() == null)
            {
                Project.CreateInstance(path);
            }
            else
            {
                Project.CreateNewInstance(path);
            }

            IEnumerable<string> lines = System.IO.File.ReadLines(path);
            EditItem ei = null;

            foreach (string line in lines)
            {
                string[] split = line.Split('*');
                switch (split[0])
                {
                    case "ProjectName":
                        Project.GetInstance().ProjectName = split[1];
                        Console.WriteLine(Project.GetInstance().ProjectName);
                        break;
                    case "Description":
                        Project.GetInstance().Description = split[1];
                        Console.WriteLine(Project.GetInstance().Description);
                        break;
                    case "Version":
                        Project.GetInstance().Version = split[1];
                        Console.WriteLine(Project.GetInstance().Version);
                        break;
                    case "Creator":
                        Project.GetInstance().Creator = split[1];
                        Console.WriteLine(Project.GetInstance().Creator);
                        break;
                    case "ProgramName":
                        Project.GetInstance().ProgramName = split[1];
                        Console.WriteLine(Project.GetInstance().ProgramName);
                        break;
                    case "ExeName":
                        Project.GetInstance().ExeName = split[1];
                        Console.WriteLine(Project.GetInstance().ExeName);
                        break;
                    case "CreationDate":
                        Project.GetInstance().CreationDate = DateTime.Parse(split[1]);
                        Console.WriteLine(Project.GetInstance().CreationDate);
                        break;

                    case "LastUpdate":
                        Project.GetInstance().CreationDate = DateTime.Parse(split[1]);
                        Console.WriteLine(Project.GetInstance().CreationDate);
                        break;
                    default:
                        if (IsDigitsOnly(split[0]))
                        {
                            switch (split[1])
                            {
                                case "Name":
                                    ei = new EditItem();
                                    ei.Name = split[2];
                                    ei.ID = int.Parse(split[0]); // TODO:
                                    break;
                                case "Address":
                                    ei.Address = split[2];
                                    break;
                                case "IsRead":
                                    ei.IsRead = split[2].Equals("True");
                                    break;
                                case "Value":
                                    ei.Value = split[2];
                                    break;
                                case "IsLoop":
                                    ei.IsLoop = split[2].Equals("True");
                                    break;
                                case "IsEnterValue":
                                    ei.IsEnterValue = split[2].Equals("True");
                                    if (Project.GetInstance().EditItems == null)
                                    {
                                        Project.GetInstance().EditItems = new Collection<EditItem>();
                                    }
                                    Project.GetInstance().EditItems.Add(ei);
                                    ei = null;
                                    break;
                            }
                        }
                        else
                        {
                            return -1;
                        }
                        break;
                }
            }

            return 0;
        }

        private static bool IsDigitsOnly(string str)
        {
            foreach (char c in str)
            {
                if (c < '0' || c > '9')
                {
                    return false;
                }
            }

            return true;
        }

    }
}
