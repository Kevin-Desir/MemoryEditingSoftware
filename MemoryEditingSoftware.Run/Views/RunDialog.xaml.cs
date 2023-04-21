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
        [DllImport(@"MemoryManipulation.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int InitLink(string TargetProcessName);

        public RunDialog()
        {
            InitializeComponent();

            int errorCode;

            if ((errorCode = InitLink(Project.GetInstance().ExeName)) > 0)
            {

                ICollection<EditItem> editItems = Project.GetInstance().EditItems;

                foreach (var ei in editItems)
                {
                    if (ei.IsRead)
                    {
                        ItemsStackPanel.Children.Add(new ReadItem(ei));
                    }
                    else
                    {
                        ItemsStackPanel.Children.Add(new WriteItem(ei));
                    }
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
