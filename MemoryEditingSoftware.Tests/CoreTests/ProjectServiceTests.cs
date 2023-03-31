using MemoryEditingSoftware.Core.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace MemoryEditingSoftware.Tests.CoreTests
{
    [TestClass]
    public class ProjectServiceTests
    {
        [TestMethod]
        public void CreateInstanceWithParametersWhenNoneExists()
        {
            // ARRANGE
            EditItem editItem1 = new EditItem()
            {
                Address = "0x00000001",
                ID = Guid.NewGuid().ToString(),
                IsEnterValue = true,
                IsLoop = true,
                IsRead = true,
                Name = "Name 1",
                Value = "1"
            };
            
            EditItem editItem2 = new EditItem()
            {
                Address = "0x00000002",
                ID = Guid.NewGuid().ToString(),
                IsEnterValue = false,
                IsLoop = false,
                IsRead = false,
                Name = "Name 2",
                Value = "2"
            };

            List<EditItem> editItems = new List<EditItem>();
            editItems.Add(editItem1);
            editItems.Add(editItem2);

            DateTime creationDate = DateTime.MinValue;
            DateTime updateDate = DateTime.Now;

            Project project = Project.CreateInstance("Project name", "Description", "Version", "Creator", "Program name", "Exe name", creationDate, updateDate, editItems, "Path");

            // ACT


            // ASSERT
            Assert.IsNotNull(project);
            Assert.AreEqual("Project name", project.ProjectName);
            Assert.AreEqual("Description", project.Description);
            Assert.AreEqual("Version", project.Version);
            Assert.AreEqual("Creator", project.Creator);
            Assert.AreEqual("Program name", project.ProgramName);
            Assert.AreEqual("Exe name", project.ExeName);
            Assert.AreEqual(creationDate, project.CreationDate);
            Assert.AreEqual(updateDate, project.LastUpdateDate);
            Assert.AreEqual(editItems, project.EditItems);
            Assert.AreEqual("Path", project.Path);
        }

        [TestMethod]
        public void CreateInstanceWithParametersWhenOneAlreadyExists()
        {
            // ARRANGE
            EditItem editItem1 = new EditItem()
            {
                Address = "0x00000001",
                ID = Guid.NewGuid().ToString(),
                IsEnterValue = true,
                IsLoop = true,
                IsRead = true,
                Name = "Name 1",
                Value = "1"
            };

            EditItem editItem2 = new EditItem()
            {
                Address = "0x00000002",
                ID = Guid.NewGuid().ToString(),
                IsEnterValue = false,
                IsLoop = false,
                IsRead = false,
                Name = "Name 2",
                Value = "2"
            };

            List<EditItem> editItems = new List<EditItem>();
            editItems.Add(editItem1);
            editItems.Add(editItem2);

            DateTime creationDate = DateTime.MinValue;
            DateTime updateDate = DateTime.Now;

            Project project = Project.CreateInstance("Project name", "Description", "Version", "Creator", "Program name", "Exe name", creationDate, updateDate, editItems, "Path");

            // ACT
            project = Project.CreateInstance("Bad", "Bad", "Bad", "Bad", "Bad", "Bad", DateTime.MaxValue, DateTime.MaxValue, new List<EditItem>(), "Bad");

            // ASSERT
            Assert.IsNotNull(project);
            Assert.AreEqual("Project name", project.ProjectName);
            Assert.AreEqual("Description", project.Description);
            Assert.AreEqual("Version", project.Version);
            Assert.AreEqual("Creator", project.Creator);
            Assert.AreEqual("Program name", project.ProgramName);
            Assert.AreEqual("Exe name", project.ExeName);
            Assert.AreEqual(creationDate.Date, project.CreationDate.Date);
            Assert.AreEqual(updateDate.Date, project.LastUpdateDate.Date);
            Assert.AreEqual(editItems.Count, project.EditItems.Count);
            Assert.AreEqual("Path", project.Path);
        }
    }
}
