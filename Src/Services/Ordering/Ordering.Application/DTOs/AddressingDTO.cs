using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.DTOs
{
    public  record AddressingDTO (
        string FirstName, string LastName, string? EmailAddress, string AddressLine, string Country, string State, string ZipCode
        );
    
}
