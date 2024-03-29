﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookVerse.Models
{
    public class BasketItem
    {
        public int Id { get; set; }
        public int Quantity { get; set; }

        public double NetPrice { get; set; }

        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        public Product Product { get; set; }

        public Cart Cart { get; set; }

        public bool OrderStatus { get; set; }

    }
}
