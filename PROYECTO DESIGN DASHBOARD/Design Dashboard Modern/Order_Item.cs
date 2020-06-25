using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Design_Dashboard_Modern
{
    class Order_Item
    {
        private string Pro_id;
        private string Pro_name;
        private double Pro_Price;
        private int Pro_quantity;

        public string Pro_id1 { get => Pro_id; set => Pro_id = value; }
        public string Pro_name1 { get => Pro_name; set => Pro_name = value; }
        public double Pro_Price1 { get => Pro_Price; set => Pro_Price = value; }
        public int Pro_quantity1 { get => Pro_quantity; set => Pro_quantity = value; }
    }
}
