# NodeDataGrid

## 📌 Project Overview

NodeDataGrid is a custom WPF DataGrid designed to display 2D arrays of nodes with dynamic color coding based on value ranges. It is optimized for scenarios where sensor data or any other numerical data needs to be displayed in a grid format. The grid cells are color-coded (Yellow, Green, Red) based on value ranges, making it easy to visually identify data patterns.

## 🚀 Features

* Supports 2D array (Node\[,]) as the data source for the DataGrid.
* Automatic dynamic generation of columns and rows based on the array size.
* Customizable color coding based on value ranges.
* Clean, efficient, and fully documented code.

## 📝 Usage

### 1. Setup

* Add the `NodeDataGrid` class and `RangeToColorCategoryConverter` class to your WPF project.
* Ensure your XAML includes a `CustomDataGrid` control.

```xml
<Window x:Class="YourNamespace.MainWindow"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        xmlns:local="clr-namespace:YourNamespace"
        Title="NodeDataGrid Demo" Height="400" Width="600">
    <Grid>
        <local:CustomDataGrid x:Name="NodeGrid" AutoGenerateColumns="False" />
    </Grid>
</Window>
```

### 2. Initializing the Grid in Code-Behind

```csharp
using System.Windows;

namespace YourNamespace
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            var nodeArray = new Node[3, 3]
            {
                { new Node(12), new Node(15), new Node(20) },
                { new Node(5), new Node(25), new Node(10) },
                { new Node(8), new Node(30), new Node(2) }
            };

            NodeGrid.SetGridData(nodeArray);
            NodeGrid.SetGridColor(10, 20);
        }
    }
}
```

### 3. Understanding Color Coding

* Cells are color-coded based on the value ranges you specify using `SetGridColor(min, max)`.
* Values below `min` are coded as **Yellow**.
* Values between `min` and `max` are coded as **Green**.
* Values above `max` are coded as **Red**.

## 🌟 Customization

* Modify the `RangeToColorCategoryConverter` for different color schemes.
* Change the DataGrid layout and column headers in the `SetGridData` method.

## 💡 Contributing

Feel free to fork this repository and make enhancements. Pull requests are welcome.

## ⚡ License

This project is licensed under the MIT License.
