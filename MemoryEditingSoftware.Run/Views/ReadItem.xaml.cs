﻿using MemoryEditingSoftware.Core;
using MemoryEditingSoftware.Core.Entities;
using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows;
using System.Windows.Controls;

namespace MemoryEditingSoftware.Run.Views
{
    /// <summary>
    /// Interaction logic for ReadItem.xaml
    /// </summary>
    public partial class ReadItem : UserControl
    {
        public ReadItem(EditItem editItem)
        {
            InitializeComponent();

            name.Text = editItem.Name;
            val.Text = "No value received / Wrong format, please check address via editor";

            // DEBUG ONLY, to be uncommented before commit
            try
            {
                if (val.Text.Contains("."))
                {
                    ThreadPool.QueueUserWorkItem(_ => StartReadingDouble(val, editItem.Address));
                }
                else
                {
                    ThreadPool.QueueUserWorkItem(_ => StartReadingInt(val, editItem.Address));
                }
            }
            catch (Exception) { }



        }

        // 2 separate methods for double or int to make the parsing only once instead of 
        // continually in the threads
        private static void StartReadingDouble(TextBox val, string address)
        {
            string oldValue = "No value received / Wrong format, please check address via editor";
            while (true)
            {
                string newValue = MemoryAccess.ReadDoubleFromMemory(address).ToString();

                if (newValue != oldValue)
                {
                    oldValue = newValue;

                    // Must change the text of the textbox via the ui thread
                    Application.Current.Dispatcher.Invoke((Action)delegate
                    {
                        val.Text = newValue;
                    });
                }
            }
        }

        private static void StartReadingInt(TextBox val, string address)
        {
            string oldValue = "No value received / Wrong format, please check address via editor";
            while (true)
            {
                string newValue = MemoryAccess.ReadIntFromMemory(address).ToString();

                if (newValue != oldValue)
                {
                    oldValue = newValue;

                    // Must change the text of the textbox via the ui thread
                    Application.Current.Dispatcher.Invoke((Action)delegate
                    {
                        val.Text = newValue;
                    });
                }
            }
        }
        
    }
}
