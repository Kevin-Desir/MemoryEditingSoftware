using MemoryEditingSoftware.Core.Business;
using MemoryEditingSoftware.Core.Entities;
using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Controls;

namespace MemoryEditingSoftware.Run.Views
{
    /// <summary>
    /// Interaction logic for WriteItem.xaml
    /// </summary>
    public partial class WriteItem : UserControl
    {
        private readonly EditItem editItem;
        private bool stop = true;
        private int debugcpt = 0;

        [DllImport(@"MemoryManipulation.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int WriteDoubleInMemory(string address, double value);

        [DllImport(@"MemoryManipulation.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int WriteIntInMemory(string address, int value);

        public WriteItem(EditItem editItem)
        {
            InitializeComponent();

            NameTextBlock.Text = editItem.Name;
            if (editItem.IsLoop)
                ActivateButton.Content = "Enable";
            else
                ActivateButton.Content = "Activate";
            Val.Text = editItem.Value;

            if (editItem.IsEnterValue)
                Val.IsReadOnly = false;
            else
                Val.IsReadOnly = true;

            this.editItem = editItem;
        }

        private void ActivateButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            // check if in loop mode
            if (ActivateButton.Content.ToString() == "Activate")
            {
                // check if input from the gui (or from the editItem instance)
                if (editItem.IsEnterValue)
                {
                    // check that the string only contains numbers / digits
                    if (Utilities.IsDigitsOnly(Val.Text))
                    {
                        // check if double or int
                        if (Val.Text.Contains("."))
                        {
                            // DEBUG ONLY
                            Console.WriteLine(editItem.Address + ": " + (double.Parse(Val.Text)).ToString());
                            WriteDoubleInMemory(editItem.Address, double.Parse(Val.Text));
                        }
                        else
                        {
                            // DEBUG ONLY
                            Console.WriteLine(editItem.Address + ": " + (int.Parse(Val.Text)).ToString());
                            WriteIntInMemory(editItem.Address, int.Parse(Val.Text));
                        }
                    }
                }
                // use the value from the editItem instance
                else
                {
                    // check that the string only contains numbers / digits
                    if (Utilities.IsDigitsOnly(editItem.Value))
                    {
                        // check if double or int
                        if (editItem.Value.Contains("."))
                        {
                            // DEBUG ONLY
                            Console.WriteLine(editItem.Address + ": " + (double.Parse(editItem.Value)).ToString());
                            WriteDoubleInMemory(editItem.Address, double.Parse(editItem.Value));
                        }
                        else
                        {
                            // DEBUG ONLY
                            Console.WriteLine(editItem.Address + ": " + (int.Parse(editItem.Value)).ToString());
                            WriteIntInMemory(editItem.Address, int.Parse(editItem.Value));
                        }
                    }
                }
            }
            else if (ActivateButton.Content.ToString() == "Enable")
            {
                // check if input from the gui (or from the editItem instance)
                if (editItem.IsEnterValue)
                {
                    // check that the string only contains numbers / digits
                    if (Utilities.IsDigitsOnly(Val.Text))
                    {
                        // check if double or int
                        if (Val.Text.Contains("."))
                        {
                            this.stop = false;
                            // had to declare a variable v to prevent "The calling thread cannot access this object because a different thread owns it".
                            double v = double.Parse(Val.Text);
                            ThreadPool.QueueUserWorkItem(_ => EnableWrintingLoopDouble(v));
                            ActivateButton.Content = "Disable";
                            Val.IsEnabled = false;
                        }
                        else
                        {
                            this.stop = false;
                            int v = int.Parse(Val.Text);
                            ThreadPool.QueueUserWorkItem(_ => EnableWrintingLoopInt(v));
                            ActivateButton.Content = "Disable";
                            Val.IsEnabled = false;
                        }
                    }
                }
                // use the value from the editItem instance
                else
                {
                    // check that the string only contains numbers / digits
                    if (Utilities.IsDigitsOnly(editItem.Value))
                    {
                        // check if double or int 
                        if (editItem.Value.Contains("."))
                        {
                            this.stop = false;
                            ThreadPool.QueueUserWorkItem(_ => EnableWrintingLoopDouble(double.Parse(editItem.Value)));
                            ActivateButton.Content = "Disable";
                            Val.IsEnabled = false;
                        }
                        else
                        {
                            this.stop = false;
                            ThreadPool.QueueUserWorkItem(_ => EnableWrintingLoopInt(int.Parse(editItem.Value)));
                            ActivateButton.Content = "Disable";
                            Val.IsEnabled = false;
                        }
                    }
                }
            }
            else if (ActivateButton.Content.ToString() == "Disable")
            {
                this.stop = true;
                ActivateButton.Content = "Enable";
                Val.IsEnabled = true;
            }
        }

        private bool EnableWrintingLoopInt(int val)
        {
            while (!stop)
            {
                // DEBUG ONLY
                Console.WriteLine(editItem.Address + ": " + val.ToString() + "[" + debugcpt++ + "]");

                // TODO: Would be nice if user can adjust this value from the ui (with a slider for example) and independant
                Thread.Sleep(100);

                WriteIntInMemory(editItem.Address, val);
            }

            return false;
        }

        private bool EnableWrintingLoopDouble(double val)
        {
            while (!stop)
            {
                // DEBUG ONLY
                Console.WriteLine(editItem.Address + ": " + val.ToString() + "[" + debugcpt++ + "]");

                // TODO: Would be nice if user can adjust this value from the ui (with a slider for example) and independant
                Thread.Sleep(100);

                WriteDoubleInMemory(editItem.Address, val);
            }

            return false;
        }

    }
}
