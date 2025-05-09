using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomDataGridLib
{
    /// <summary>
    /// Represents a node with a value for the custom DataGrid.
    /// </summary>
    public class Node
    {
        public double Value { get; set; }

        public Node(double value)
        {
            Value = value;
        }
    }
}
