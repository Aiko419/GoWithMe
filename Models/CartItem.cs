using GoWithMe.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GoWithMe.Models
{
    public class CartItem
    {
        public Tour productOrder { get; set; }
        public int Quality { get; set; }

    }
}