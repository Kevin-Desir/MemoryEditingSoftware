﻿using MemoryEditingSoftware.Core.Entities;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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

            ICollection<EditItem> editItems = Project.GetInstance().EditItems;

            foreach (var ei in editItems)
            {
                if (ei.IsRead)
                {
                    ItemsStackPanel.Children.Add(new ReadItem(ei));
                }
                else
                {
                    // TODO:
                }
            }
        }
    }
}