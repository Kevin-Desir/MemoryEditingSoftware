using MemoryEditingSoftware.Core;
using MemoryEditingSoftware.Core.Attributes;
using MemoryEditingSoftware.Core.Business;
using MemoryEditingSoftware.Core.Entities;
using MemoryEditingSoftware.Run.Controls;
using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Controls;

namespace MemoryEditingSoftware.Run.Views
{
    /// <summary>
    /// Interaction logic for WriteItemView.xaml
    /// </summary>
    [DroppableView(typeof(SimpleWriter), "Simple write")]
    public partial class WriteItemControl : UserControl, IComponentControl
    {
        public SimpleWriter SimpleWriter { get; }
        private bool stop = true;
        private int debugcpt = 0;

        public object MainObject { get; }

        public WriteItemControl(SimpleWriter simpleWriter)
        {
            InitializeComponent();
            
            SimpleWriter = simpleWriter;
            MainObject = SimpleWriter;

            NameTextBlock.Text = SimpleWriter.Name;
            if (SimpleWriter.IsLoop)
                ActivateButton.Content = "Enable";
            else
                ActivateButton.Content = "Activate";
            Val.Text = SimpleWriter.Value;

            if (SimpleWriter.IsEnterValue)
                Val.IsReadOnly = false;
            else
                Val.IsReadOnly = true;

        }

        private void ActivateButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            // check if in loop mode
            if (ActivateButton.Content.ToString() == "Activate")
            {
                // check if input from the gui (or from the EditItem instance)
                if (SimpleWriter.IsEnterValue)
                {
                    // check that the string only contains numbers / digits
                    if (Utilities.IsDigitsOnly(Val.Text))
                    {
                        // check if double or int
                        if (Val.Text.Contains("."))
                        {
                            // DEBUG ONLY
                            Console.WriteLine(SimpleWriter.Address + ": " + (double.Parse(Val.Text)).ToString());
                            MemoryAccess.WriteDoubleInMemory(SimpleWriter.Address, double.Parse(Val.Text));
                        }
                        else
                        {
                            // DEBUG ONLY
                            Console.WriteLine(SimpleWriter.Address + ": " + (int.Parse(Val.Text)).ToString());
                            MemoryAccess.WriteIntInMemory(SimpleWriter.Address, int.Parse(Val.Text));
                        }
                    }
                }
                // use the value from the EditItem instance
                else
                {
                    // check that the string only contains numbers / digits
                    if (Utilities.IsDigitsOnly(SimpleWriter.Value))
                    {
                        // check if double or int
                        if (SimpleWriter.Value.Contains("."))
                        {
                            // DEBUG ONLY
                            Console.WriteLine(SimpleWriter.Address + ": " + (double.Parse(SimpleWriter.Value)).ToString());
                            MemoryAccess.WriteDoubleInMemory(SimpleWriter.Address, double.Parse(SimpleWriter.Value));
                        }
                        else
                        {
                            // DEBUG ONLY
                            Console.WriteLine(SimpleWriter.Address + ": " + (int.Parse(SimpleWriter.Value)).ToString());
                            MemoryAccess.WriteIntInMemory(SimpleWriter.Address, int.Parse(SimpleWriter.Value));
                        }
                    }
                }
            }
            else if (ActivateButton.Content.ToString() == "Enable")
            {
                // check if input from the gui (or from the EditItem instance)
                if (SimpleWriter.IsEnterValue)
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
                // use the value from the EditItem instance
                else
                {
                    // check that the string only contains numbers / digits
                    if (Utilities.IsDigitsOnly(SimpleWriter.Value))
                    {
                        // check if double or int 
                        if (SimpleWriter.Value.Contains("."))
                        {
                            this.stop = false;
                            ThreadPool.QueueUserWorkItem(_ => EnableWrintingLoopDouble(double.Parse(SimpleWriter.Value)));
                            ActivateButton.Content = "Disable";
                            Val.IsEnabled = false;
                        }
                        else
                        {
                            this.stop = false;
                            ThreadPool.QueueUserWorkItem(_ => EnableWrintingLoopInt(int.Parse(SimpleWriter.Value)));
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
                Console.WriteLine(SimpleWriter.Address + ": " + val.ToString() + "[" + debugcpt++ + "]");

                // TODO: Would be nice if user can adjust this value from the ui (with a slider for example) and independant
                Thread.Sleep(100);

                MemoryAccess.WriteIntInMemory(SimpleWriter.Address, val);
            }

            return false;
        }

        private bool EnableWrintingLoopDouble(double val)
        {
            while (!stop)
            {
                // DEBUG ONLY
                Console.WriteLine(SimpleWriter.Address + ": " + val.ToString() + "[" + debugcpt++ + "]");

                // TODO: Would be nice if user can adjust this value from the ui (with a slider for example) and independant
                Thread.Sleep(100);

                MemoryAccess.WriteDoubleInMemory(SimpleWriter.Address, val);
            }

            return false;
        }

    }
}
