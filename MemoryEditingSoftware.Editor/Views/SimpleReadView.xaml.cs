using MemoryEditingSoftware.Core.Entities;
using Prism.Commands;
using System.Windows.Controls;
using System.Windows.Input;

namespace MemoryEditingSoftware.Editor.Views
{
    /// <summary>
    /// Interaction logic for SimpleReadView.xaml
    /// </summary>
    public partial class SimpleReadView : UserControl
    {
        #region Properties 

        public SimpleReader SimpleReader { get; set; }

        #endregion

        public SimpleReadView(EditItem editItem)
        {
            InitializeComponent();

            if (editItem is SimpleReader reader)
            {
                SimpleReader = reader;
            }
        }

        /// <summary>
        /// Force the user to type integers in a view component.
        /// </summary>
        private void ForceIntTextChanged(object sender, TextCompositionEventArgs e)
        {
            // Check if the entered character is a digit
            if (!char.IsDigit(e.Text[0]))
            {
                e.Handled = true; // Ignore non-numeric input
            }
        }
    }
}
