using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public override string ToString()
        {
            return $"{ProjectName}";
        }
    }
}
