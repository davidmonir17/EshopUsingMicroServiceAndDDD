using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.DTOs
{
    public record PaymentDTO(string CardNumber, string CardName, string Expiration, string Cvv, int PaymentMethod);
    
}
