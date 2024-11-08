﻿using System.ComponentModel.DataAnnotations.Schema;

namespace Awushi.Domain.Models
{
    public class BasketItem
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        public Product Product { get; set; }
        public int BasketId { get; set; }
        [ForeignKey("BasketId")]
        public Basket Basket { get; set; }

    }
}