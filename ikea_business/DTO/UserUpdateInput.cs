using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ikea_business.DTO
{
    public record UserUpdateInput(
      bool IsAdmin,
      string FirstName,
      string LastName,
      DateTime BirthDate,
      string Country,
      string Address,
      string Phone,
      string Email);

}
