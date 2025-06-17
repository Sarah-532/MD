using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Test_03.Models
{
    public class Customer
    {
        public int CustomerId { get; set; }
        public string Name { get; set; } = default!;
        public string  Phone { get; set; } = default!;
        public string Photo { get; set; } = default!;
        [Column(TypeName ="Date"), Display(Name ="Business Start"), DisplayFormat(DataFormatString ="{0:yyyy-MM-dd}", ApplyFormatInEditMode =true)]
        public DateTime BusinessStart { get; set; }
        [Column(TypeName ="money")]
        public decimal CreditDetails { get; set; }
        public ICollection<DeliveryDetail> DeliveryDetails { get; set; } = new List<DeliveryDetail>();

    }
}
