using MemoryEditingSoftware.Core.Entities;
using System.Collections.Generic;
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

    }
}
