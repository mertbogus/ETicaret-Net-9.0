﻿using ETicaret.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaret.Service.Abstract
{
    public interface ICardService
    {
        void AddProduct(Product product,int quantity);
        void UpdateProduct(Product product,int quantity);
        void RemoveProduct(Product product);
        decimal TotalPrice();

        void ClearAll();

    }
}
