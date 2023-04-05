using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobyLabWebProgramming.Core.Entities
{
    public class Feedback
    {
        public int Id { get; set; }
        public int Rating { get; set; }

        public string SenderId { get; set; }
        public virtual User Sender { get; set; }

        public string ReceiverId { get; set; }
        public virtual User Receiver { get; set; }
    }
}
