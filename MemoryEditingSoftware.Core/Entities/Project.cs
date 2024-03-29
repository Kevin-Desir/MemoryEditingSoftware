﻿using System;
using System.Collections.Generic;

namespace MemoryEditingSoftware.Core.Entities
{
    public class Project
    {
        public string ProjectName { get; set; }
        public string Description { get; set; }
        public string Version { get; set; }
        public string Creator { get; set; }
        // friendly / displayed name
        public string ProgramName { get; set; }
        // name of the .exe file for the program
        public string ExeName { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime LastUpdateDate { get; set; }
        public ICollection<EditItem> EditItems { get; set; }
        public string Path { get; set; }

        public Project() { }

        public override string ToString()
        {
            return $"{ProjectName}";
        }

        private static Project instance;

        private static readonly object _lock = new object();

        public static Project CreateInstance(string projectName, string description, string version, string creator, string programName, string exeName, DateTime creationDate, DateTime lastUpdateTime, ICollection<EditItem> editItems, string path)
        {
            if (instance == null)
            {
                lock (_lock)
                {
                    instance = new Project()
                    {
                        ProjectName = projectName,
                        Description = description,
                        Version = version,
                        Creator = creator,
                        ProgramName = programName,
                        ExeName = exeName,
                        CreationDate = creationDate,
                        LastUpdateDate = lastUpdateTime,
                        EditItems = editItems,
                        Path = path
                    };
                }
            }

            return instance;
        }

        public static void UpdateProject(Project project)
        {
            lock (_lock)
            {
                instance = project;
            }
        }

        public static Project CreateInstance(string path)
        {
            if (instance == null)
            {
                lock (_lock)
                {
                    instance = new Project()
                    {
                        Path = path
                    };
                }
            }

            return instance;
        }

        public static Project CreateNewInstance(string path)
        {
            lock (_lock)
            {
                instance = new Project()
                {
                    Path = path
                };

            }

            return instance;
        }

        public static Project GetInstance()
        {
            return instance;
        }

        public static void CloseProject()
        {
            lock (_lock)
            {
                instance = null;
            }
        }
    }
}
