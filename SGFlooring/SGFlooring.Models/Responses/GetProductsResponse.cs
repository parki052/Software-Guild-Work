﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGFlooring.Models.Responses
{
    public class GetProductsResponse : Response
    {
        public List<Material> Materials { get; set; }
    }
}
