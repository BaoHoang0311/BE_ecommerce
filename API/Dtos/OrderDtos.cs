﻿using API.Entites;
using API.Helpers;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace API.Dtos
{
    public class OrderDtos
    {
        public int OrderId { get; set; }
        public string OrderNo { get; set; }
        public int CustomerId { get; set; }
        [Required]
        [Range(0.01, 999999999, ErrorMessage = "Price must be greater than 0.00")]
        public decimal TotalPrice { get; set; }
        [ValidationListEmptyOrderDetail]
        public List<OrderDetailDtos> orderDetailDtos { get; set; }
    }
}
