using GalaSoft.MvvmLight;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace MMSaveEditor.Utils
{
    public class IsTypeVisiblityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object inParameter, CultureInfo culture)
        {
            if (inParameter == null)
            {
                return null;
            }
            string[] parameters = ((string)inParameter).Split(',');
            bool canConvert = false;
            foreach (string parameter in parameters)
            {
                Type type = Type.GetType($"MMSaveEditor.ViewModel.{parameter}");
                if (value != null && value.GetType().IsAssignableFrom(type))
                {
                    canConvert = true;
                    break;
                }
            }
            return canConvert ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Don't need to implement this
            return value;
        }
    }

    public class BooleanToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool boolValue = (bool)value;
            boolValue = (parameter != null) ? !boolValue : boolValue;
            return boolValue ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class IsNullVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (ViewModelBase.IsInDesignModeStatic)
                return Visibility.Visible;

            return value != null ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }


}
