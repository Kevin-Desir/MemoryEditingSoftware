using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows;

namespace MemoryEditingSoftware.Core.Converters
{
    /// <summary>
    /// Converter class used to set the visibility of a grid according to the 
    /// dialog type (error, information).
    /// 
    /// Called from the xaml files.
    /// 
    /// This namespace must be added in xaml file:
    /// <code>
    ///     xmlns:converter="clr-namespace:MemoryEditingSoftware.Core.Converters;assembly=MemoryEditingSoftware.Core"
    /// </code>
    /// 
    /// As well as this resource:
    /// <code>
    /// <UserControl.Resources>
    ///     <converter:DialogTypesToVisibilityConverter x:Key="DialogTypesToVisibilityConverter" />
    /// </UserControl.Resources>
    /// </code>
    /// 
    /// And the converter can be used like such: (where DialogType is a boolean property declared in the ViewModel)
    /// <code>
    ///     <Grid Visibility="{Binding DialogType, Converter={StaticResource DialogTypesToVisibilityConverter}, ConverterParameter=Error}" >
    /// </code>
    /// </summary>
    [ValueConversion(typeof(DialogTypes), typeof(System.Windows.Visibility))]
    public class DialogTypesToVisibilityConverter : IValueConverter
    {
        /// <summary>
        /// Convert a string DialogType (error, information) to a Visibility (Visible or Collapsed).
        /// </summary>
        /// <param name="value">The DialogType input value ("Information" or "Error").</param>
        /// <param name="targetType">The type of the DialogType value (should be DialogTypes).</param>
        /// <returns>The Visibility (Visible or Collapsed).</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (targetType != typeof(Visibility))
            {
                throw new InvalidOperationException("The target must be a System.Windows.Visibility.");
            }

            if (((DialogTypes)value).Equals(DialogTypes.Information) && ((string)parameter).Equals("Information"))
            {
                return System.Windows.Visibility.Visible;
            }
            else if (((DialogTypes)value).Equals(DialogTypes.Error) && ((string)parameter).Equals("Error"))
            {
                return System.Windows.Visibility.Visible;
            }
            else
            {
                return System.Windows.Visibility.Collapsed;
            }
        }

        /// <summary>
        /// This method is not used nor supported and will throw an exception.
        /// </summary>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
