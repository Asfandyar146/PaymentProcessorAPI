using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PaymentProcessorAPI.Models
{
    public class PaymentState
    {
        public PaymentState()
        {
            Request = new HashSet<Request>();
        }


        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public bool Failed { get; set; }
        public bool Processed { get; set; }
        public bool Pending { get; set; }

        public virtual ICollection<Request> Request { get; set; }


    }
}
