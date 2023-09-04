using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookVerse.Models.ViewModels
{
    public class CartVM
    {
        public List<BasketItem> BasketItems { get; set; }

        public Cart Cart { get; set; }

    }
}
