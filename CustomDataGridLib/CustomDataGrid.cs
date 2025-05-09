using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows;
using System.Globalization;

namespace CustomDataGridLib
{


    /// <summary>
    /// A custom DataGrid that supports 2D array of nodes with color-coded cells.
    /// </summary>
    public class CustomDataGrid : DataGrid
    {
        /// <summary>
        /// Sets the color of grid cells based on value ranges.
        /// </summary>
        public void SetGridColor(double min, double max)
        {
            var converter = new RangeToColorCategoryConverter { Min = min, Max = max };

            foreach (var column in this.Columns.OfType<DataGridTextColumn>())
            {
                if (column.Header.ToString() == "DayName")
                    continue;

                var bindingPath = ((Binding)column.Binding).Path.Path;
                var style = new Style(typeof(DataGridCell));

                style.Triggers.Add(CreateColorTrigger(bindingPath, "Yellow", Brushes.Yellow, converter));
                style.Triggers.Add(CreateColorTrigger(bindingPath, "Green", Brushes.LightGreen, converter));
                style.Triggers.Add(CreateColorTrigger(bindingPath, "Red", Brushes.IndianRed, converter));

                column.CellStyle = style;
            }
        }

        private static DataTrigger CreateColorTrigger(string path, string value, Brush color, IValueConverter converter)
        {
            var trigger = new DataTrigger
            {
                Binding = new Binding(path) { Converter = converter },
                Value = value
            };
            trigger.Setters.Add(new Setter(DataGridCell.BackgroundProperty, color));
            return trigger;
        }

        /// <summary>
        /// Populates the grid with a 2D array of nodes.
        /// </summary>
        public void SetGridData(Node[,] nodeArray2D)
        {
            Columns.Clear();

            int totalRows = nodeArray2D.GetLength(0);
            int totalColumns = nodeArray2D.GetLength(1);

            Columns.Add(new DataGridTextColumn { Header = "DayName", Binding = new Binding("DayName") });

            for (int colIndex = 0; colIndex < totalColumns; colIndex++)
            {
                Columns.Add(new DataGridTextColumn
                {
                    Header = $"Hour {colIndex + 1}",
                    Binding = new Binding($"Column{colIndex}")
                });
            }

            var dynamicRows = new List<ExpandoObject>();

            for (int rowIndex = 0; rowIndex < totalRows; rowIndex++)
            {
                dynamic row = new ExpandoObject();
                var dict = (IDictionary<string, object>)row;

                dict["DayName"] = $"Day {rowIndex + 1}";

                for (int colIndex = 0; colIndex < totalColumns; colIndex++)
                {
                    dict[$"Column{colIndex}"] = nodeArray2D[rowIndex, colIndex].Value;
                }

                dynamicRows.Add(row);
            }

            this.ItemsSource = dynamicRows;
        }
    }

    /// <summary>
    /// A value converter that categorizes values into color-coded categories.
    /// </summary>
    public class RangeToColorCategoryConverter : IValueConverter
    {
        public double Min { get; set; }
        public double Max { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || !double.TryParse(value.ToString(), out double val))
                return Binding.DoNothing;

            if (val < Min) return "Yellow";
            if (val > Max) return "Red";

            return "Green";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) =>
            throw new NotImplementedException();
    }
}
