using System;
using System.Runtime.InteropServices;
using System.Windows;

namespace MemoryEditingSoftware.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        [DllImport(@"MemoryManipulation.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int InitLink(string TargetProcessName);

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            TextBox.Text = InitLink(TextBox.Text).ToString();
        }
    }
}
