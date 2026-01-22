using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Car.ApplicationServices.Dtos
{
    public class CarUpdateDto : CarCreateDto
    {
        public System.Guid Id { get; set; }
    }
}
