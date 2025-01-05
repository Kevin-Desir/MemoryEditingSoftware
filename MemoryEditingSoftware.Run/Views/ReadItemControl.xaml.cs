using MemoryEditingSoftware.Core;
using MemoryEditingSoftware.Core.Attributes;
using MemoryEditingSoftware.Core.Entities;
using MemoryEditingSoftware.Run.Controls;
using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows;
using System.Windows.Controls;

namespace MemoryEditingSoftware.Run.Views
{
    /// <summary>
    /// Interaction logic for ReadItemControl.xaml
    /// </summary>
    [DroppableView(typeof(SimpleReader), "Simple read")]
    public partial class ReadItemControl : UserControl, IComponentControl
    {
        public SimpleReader SimpleReader { get; set; }

        public object MainObject { get; }

        public ReadItemControl(SimpleReader simpleReader)
        {
            InitializeComponent();

            SimpleReader = simpleReader;
            MainObject = SimpleReader;

            name.Text = simpleReader.Name;
            val.Text = "No value received / Wrong format, please check address via editor";

            // DEBUG ONLY, to be uncommented before commit
            try
            {
                if (val.Text.Contains("."))
                {
                    ThreadPool.QueueUserWorkItem(_ => StartReadingDouble(val, simpleReader.Address));
                }
                else
                {
                    ThreadPool.QueueUserWorkItem(_ => StartReadingInt(val, simpleReader.Address));
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
