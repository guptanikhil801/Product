using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Product.Classes.Models
{
    public class Cart
    {
        [Key]
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public int AvailableQuantity { get; private set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        private int _orderQuantity;
        public int OrderQuantity
        {
            get => _orderQuantity;
            set
            {
                if (value > AvailableQuantity)
                {
                    throw new ArgumentException("OrderQuantity cannot be greater than AvailableQuantity.");
                }
                _orderQuantity = value;
            }
        }

    }
}
