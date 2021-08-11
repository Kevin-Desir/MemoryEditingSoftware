using MemoryEditingSoftware.Core.Entities;
using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Controls;

namespace MemoryEditingSoftware.Run.Views
{
    /// <summary>
    /// Interaction logic for ReadItem.xaml
    /// </summary>
    public partial class ReadItem : UserControl
    {
        [DllImport(@"MemoryManipulation.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern double ReadDoubleFromMemory(string address);

        [DllImport(@"MemoryManipulation.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int ReadIntFromMemory(string address);

        public ReadItem(EditItem editItem)
        {
            InitializeComponent();

            name.Text = editItem.Name;
            val.Text = "No value received / Wrong format, please check address via editor";

            try
            {
                if (val.Text.Contains("."))
                {
                    ThreadPool.QueueUserWorkItem(_ => StartReadingDouble(editItem.Address));
                }
                else
                {
                    ThreadPool.QueueUserWorkItem(_ => StartReadingInt(editItem.Address));
                }
            }
            catch (Exception) { }



        }

        // 2 separate methods for double or int to make the parsing only once instead of 
        // continually in the threads
        private void StartReadingDouble(string address)
        {
            val.Text = ReadDoubleFromMemory(address).ToString();
        }

        private void StartReadingInt(string address)
        {
            val.Text = ReadIntFromMemory(address).ToString();
        }
        
    }
}
