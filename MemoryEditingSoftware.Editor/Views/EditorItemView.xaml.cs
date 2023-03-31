using MemoryEditingSoftware.Core.Entities;
using Prism.Commands;
using System.Windows.Controls;
using System.Windows.Input;

namespace MemoryEditingSoftware.Editor.Views
{
    /// <summary>
    /// Interaction logic for EditorItemView
    /// </summary>
    public partial class EditorItemView : UserControl
    {
        #region Properties 

        public EditItem _editItem { get; set; }

        #endregion

        #region Commands 

        /// <summary>
        /// Remove the edit item chosen by the user from everywhere.
        /// This delegate command is passed from the EditorViewModel, where the implementation is located.
        /// </summary>
        public DelegateCommand<EditorItemView> RemoveCommand { get; set; }
        /// <summary>
        /// Apply the modifications the user has made to an edit item.
        /// This delegate command is passed from the EditorViewModel, where the implementation is located.
        /// </summary>
        public DelegateCommand<EditorItemView> ApplyChangesToAnItemCommand { get; set; }

        #endregion

        #region Constructor 

        public EditorItemView(EditItem editItem, DelegateCommand<EditorItemView> applyChangesToAnItemCommand, DelegateCommand<EditorItemView> removeCommand)
        {
            InitializeComponent();
            _editItem = editItem;
            RemoveCommand = removeCommand;
            ApplyChangesToAnItemCommand = applyChangesToAnItemCommand;

            IdTextBlock.Text = editItem.ID.ToString();
            NameTextBox.Text = editItem.Name.ToString();
            AddressTextBox.Text = editItem.Address.ToString();
            ReadWriteCollectionComboBox.Items.Add("Read");
            ReadWriteCollectionComboBox.Items.Add("Write");
            ReadWriteCollectionComboBox.SelectedItem = editItem.IsRead ? "Read" : "Write";
            ValueTextBox.Text = editItem.Value.ToString();
            LoopingCollectionComboBox.Items.Add("One time");
            LoopingCollectionComboBox.Items.Add("Loop");
            LoopingCollectionComboBox.SelectedItem = editItem.IsLoop ? "Loop" : "One time";
            EnterValueCollectionComboBox.Items.Add("Yes");
            EnterValueCollectionComboBox.Items.Add("No");
            EnterValueCollectionComboBox.SelectedItem = editItem.IsEnterValue ? "Yes" : "No";
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Called when the user wants to validate the changes made to an edit item.
        /// </summary>
        private void Validate_Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            _editItem.ID = int.Parse(IdTextBlock.Text);
            _editItem.Name = NameTextBox.Text;
            _editItem.Address = AddressTextBox.Text;
            _editItem.IsRead = ReadWriteCollectionComboBox.SelectedItem.Equals("Read");
            _editItem.Value = ValueTextBox.Text;
            _editItem.IsLoop = LoopingCollectionComboBox.SelectedItem.Equals("Loop");
            _editItem.IsEnterValue = EnterValueCollectionComboBox.SelectedItem.Equals("Yes");

            ApplyChangesToAnItemCommand.Execute(this);
        }

        /// <summary>
        /// Called when the user wants to cancel the changes made to an edit item.
        /// </summary>
        private void Cancel_Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            IdTextBlock.Text = _editItem.ID.ToString();
            NameTextBox.Text = _editItem.Name.ToString();
            AddressTextBox.Text = _editItem.Address.ToString();
            ReadWriteCollectionComboBox.SelectedItem = _editItem.IsRead ? "Read" : "Write";
            ValueTextBox.Text = _editItem.Value.ToString();
            LoopingCollectionComboBox.SelectedItem = _editItem.IsLoop ? "Loop" : "One time";
            EnterValueCollectionComboBox.SelectedItem = _editItem.IsEnterValue ? "Yes" : "No";
        }

        /// <summary>
        /// Called when the user wants to delete an edit item.
        /// </summary>
        private void Remove_Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            RemoveCommand.Execute(this);
        }

        #endregion

        #region Public Methods

        #endregion

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
