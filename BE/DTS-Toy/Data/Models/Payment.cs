using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class Payment
    {
        public Guid ID { get; set; }
        public decimal? TotalPrice { get; set; }
        public string? PaymentMethod { get; set; }
        public string? PaymentCode { get; set; }

        [ForeignKey("UserPayment")]
        public Guid? UserPaymentId { get; set; } 

        public virtual User? UserPayment { get; set; }

        public DateTime? PaymentEndDate { get; set; }

        public Guid? OrderDetailID { get; set; }
        public OrderDetail? OrderDetail { get; set; }

        public PaymentStatus Status { get; set; } = PaymentStatus.Pending;
    }


    public enum PaymentStatus
    {
        Pending = 0,   
        Success = 1,  
        Failed = 2,     
        Refunded = 3   
    }
}
