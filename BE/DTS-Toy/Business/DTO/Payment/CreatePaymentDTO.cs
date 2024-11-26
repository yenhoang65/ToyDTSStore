using Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DTO.Payment
{
    public class CreatePaymentDTO
    {
        public string? PaymentMethod { get; set; }
        public string? PaymentCode { get; set; }       

    }
}
