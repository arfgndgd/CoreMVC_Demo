﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreMVC_Demo.Tools
{
    [Serializable]
    public class CartItem
    {
        [JsonProperty("ID")]
        public int ID { get; set; }
        [JsonProperty("Name")]
        public string Name { get; set; }
        [JsonProperty("Amount")]
        public short Amount { get; set; }
        [JsonProperty("Price")]
        public decimal Price { get; set; }
        [JsonProperty("ImagePath")]
        public string ImagePath { get; set; }
        [JsonProperty("SubTotal")]
        public decimal SubTotal 
        {
            get 
            {
                return Price * Amount;  //Set metodu olmayınca ReadOnly deniliyor.
            }
            
        }

        public CartItem()
        {
            Amount++; //Sepet işlemlerinde ürün arttırmak için gerekli
        }
    }
}
