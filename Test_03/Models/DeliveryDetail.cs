using System.ComponentModel.DataAnnotations.Schema;

namespace Test_03.Models
{
    public class DeliveryDetail
    {
        public int DeliveryDetailId { get; set; }
        [ForeignKey("Customer")]
        public int CustomerId { get; set; }
        public string DeliVeryAddres { get; set; } = default!;
        public string  ContactPerson { get; set; } = default!;
        public string  Phone { get; set; } = default!;
        public virtual Customer? Customer { get; set;  }

    }
}
