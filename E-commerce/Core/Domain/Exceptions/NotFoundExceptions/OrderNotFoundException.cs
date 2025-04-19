using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions.NotFoundExceptions
{
    public sealed class OrderNotFoundException(Guid id)
        : NotFoundException($"order with id {id} is not found ")
    {
    }
}
