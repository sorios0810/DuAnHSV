﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Electronic_Ecommerce.ZaloPay.Models
{
    public class BankDTO
    {
        public string bankcode { get; set; }
        public string name { get; set; }
        public int displayorder { get; set; }
        public int pmcid { get; set; }
    }
}