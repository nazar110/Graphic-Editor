using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raster_Graphics_Editor
{
    public class PointConverter : System.Windows.Data.IValueConverter
    {
        /// <summary>
        /// ConvertSystemDrawingToSystemWindows
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            System.Drawing.Point dp = (System.Drawing.Point)value;
            return new System.Windows.Point(dp.X, dp.Y);
        }
        /// <summary>
        /// ConvertSystemWindowsToSystemDrawing
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object ConvertBack(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            System.Windows.Point wp = (System.Windows.Point)value;
            return new System.Drawing.Point((int)wp.X, (int)wp.Y);
        }
    }
}
