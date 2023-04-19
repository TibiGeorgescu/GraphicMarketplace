using MobyLabWebProgramming.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobyLabWebProgramming.Core.DataTransferObjects;

public class ProfileDTO
{
<<<<<<< HEAD
    public Guid Id { get; set; }
=======
    Guid Id { get; set; }
>>>>>>> dce5d55beda079ed5a7cf7f9861e0b1d6a191f40
    public Guid UserId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Address { get; set; }
    public string PhoneNumber { get; set; }
}
