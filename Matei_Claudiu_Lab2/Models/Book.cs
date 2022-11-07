﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Matei_Claudiu_Lab2.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public int? AuhorID { get; set; }
        public decimal Price { get; set; }  

        public ICollection<Order>? Orders { get; set; }
        public Author? Author { get; set; }
    }
}
