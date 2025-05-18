using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ikea_business.DTO
{
    public record ProductCharacteristicInput(
          int ProductId,
          string Name,
          string Value
     );
}
