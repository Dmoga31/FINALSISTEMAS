﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API_Rest.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int TypeId { get; set; }

        public double Price { get; set; }
        public int Quantity {  get; set; }

        public Product()
        {
        }


    }
}