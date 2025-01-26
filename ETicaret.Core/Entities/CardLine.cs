using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaret.Core.Entities
{
    public class CardLine
    {
        public int Id { get; set; } 
        public Product Product { get; set; }

        public int Quantity { get; set; }

    }
}
