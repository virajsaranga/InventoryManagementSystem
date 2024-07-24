using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryManagementSystem4.Models
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductID { get; set; }

        [Required]
        [StringLength(100)]
        public string ProductName { get; set; }

        [ForeignKey("Category")]
        public int CategoryID { get; set; }

        [ForeignKey("Supplier")]
        public int SupplierID { get; set; }

        [Required]
        [StringLength(100)]
        public string QuantityPerUnit { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public decimal UnitPrice { get; set; }

        [Required]
        public int UnitsInStock { get; set; }

        // Navigation properties
        public virtual Category Category { get; set; }
        public virtual Supplier Supplier { get; set; }
    }
}
