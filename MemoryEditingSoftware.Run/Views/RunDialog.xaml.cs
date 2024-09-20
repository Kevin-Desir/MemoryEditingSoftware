using MemoryEditingSoftware.Core;
using MemoryEditingSoftware.Core.Entities;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;

namespace MemoryEditingSoftware.Run.Views
{
    /// <summary>
    /// Interaction logic for RunDialog.xaml
    /// </summary>
    public partial class RunDialog : UserControl
    {
        public RunDialog()
        {
            InitializeComponent();

            int errorCode;

            if ((errorCode = MemoryAccess.InitLink(Project.GetInstance().ExeName)) > 0)
            {

                ICollection<EditItem> editItems = Project.GetInstance().EditItems;

                List<UserControl> components = new List<UserControl>();

                foreach (var ei in editItems)
                {
                    if (ei.IsRead)
                    {
                        components.Add(new ReadItem(ei));
                    }
                    else
                    {
                        if (ei is SimpleWriter simpleWriter)
                        components.Add(new WriteItem(simpleWriter));
                    }
                }

                foreach (var component in components)
                {
                    ItemsStackPanel.Children.Add(component);
                }
            }
            else
            {
                // TODO: handle error if cannot connect to process
                Console.WriteLine(errorCode);
            }
        }
    }
}
