using Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DTO.Payment
{
    public class PaymentDTO
    {
        public Guid ID { get; set; }
        public decimal? TotalPrice { get; set; }
        public string? PaymentMethod { get; set; }
        public string? PaymentCode { get; set; }

        public Guid? UserPaymentId { get; set; }
        public DateTime? PaymentEndDate { get; set; }

        public Guid? OrderDetailID { get; set; }
        public PaymentStatus Status { get; set; } 
    }
}
