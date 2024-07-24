using System.ComponentModel.DataAnnotations;

namespace InventoryManagementSystem4.Models
{
    public class Supplier
    {
        [Key]
        public int SupplierID { get; set; }

        [Required]
        [StringLength(100)]
        public string SupplierName { get; set; }

        [StringLength(100)]
        public string ContactName { get; set; }

        [StringLength(50)]
        public string ContactTitle { get; set; }

        [StringLength(150)]
        public string Address { get; set; }

        [StringLength(100)]
        public string City { get; set; }

        [StringLength(20)]
        public string PostalCode { get; set; }

        [StringLength(50)]
        public string Country { get; set; }

        [StringLength(20)]
        public string Phone { get; set; }

    }
}
