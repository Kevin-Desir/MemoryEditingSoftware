using MemoryEditingSoftware.Core.Entities;
using System;
using System.Runtime.InteropServices;
using System.Windows.Controls;

namespace MemoryEditingSoftware.Run.Views
{
    /// <summary>
    /// Interaction logic for WriteItem.xaml
    /// </summary>
    public partial class WriteItem : UserControl
    {
        private readonly EditItem editItem;

        [DllImport(@"MemoryManipulation.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int WriteDoubleInMemory(string address, double value);

        [DllImport(@"MemoryManipulation.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int WriteIntInMemory(string address, int value);

        public WriteItem(EditItem editItem)
        {
            InitializeComponent();

            Name.Text = editItem.Name;
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
                    if (IsDigitsOnly(Val.Text))
                    {
                        // check if double or int
                        if (Val.Text.Contains("."))
                        {
                            // DEBUG ONLY
                            Console.WriteLine(editItem.Address + ": " + (double.Parse(Val.Text)).ToString());
                            //WriteDoubleInMemory(editItem.Address, double.Parse(Val.Text));
                        }
                        else
                        {
                            // DEBUG ONLY
                            Console.WriteLine(editItem.Address + ": " + (int.Parse(Val.Text)).ToString());
                            //WriteIntInMemory(editItem.Address, int.Parse(Val.Text));
                        }
                    }
                }
                // use the value from the editItem instance
                else
                {
                    // check that the string only contains numbers / digits
                    if (IsDigitsOnly(editItem.Value))
                    {
                        // check if double or int
                        if (editItem.Value.Contains("."))
                        {
                            // DEBUG ONLY
                            Console.WriteLine(editItem.Address + ": " + (double.Parse(editItem.Value)).ToString());
                            //WriteDoubleInMemory(editItem.Address, double.Parse(editItem.Value));
                        }
                        else
                        {
                            // DEBUG ONLY
                            Console.WriteLine(editItem.Address + ": " + (int.Parse(editItem.Value)).ToString());
                            //WriteIntInMemory(editItem.Address, int.Parse(editItem.Value));
                        }
                    }
                }
            }
            else if (ActivateButton.Content.ToString() == "Enable")
            {
                throw new NotImplementedException();
            }
            else if (ActivateButton.Content.ToString() == "Disable")
            {
                throw new NotImplementedException();
            }
        }

        // TODO: put this in Core
        bool IsDigitsOnly(string str)
        {
            foreach (char c in str)
            {
                if (c < '0' || c > '9')
                    return false;
            }

            return true;
        }
    }
}
