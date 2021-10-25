using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Northwind.Entities.Concrete;

namespace Northwind.Business.ValidationRules.FluentValidation
{
    public class ProductValidator:AbstractValidator<Product>
    {
        public ProductValidator()
        {
            RuleFor(p => p.ProductName).NotEmpty().WithMessage("Product name is empty!!!");//product name boş veri girilemez!
            RuleFor(p => p.CategoryID).NotEmpty().WithMessage("Category empty!!!");
            RuleFor(p => p.UnitPrice).NotEmpty().WithMessage("Unit price empty!!!");
            RuleFor(p => p.QuantityPerUnit).NotEmpty().WithMessage("Unit empty!!!");
            RuleFor(p => p.UnitsInStock).NotEmpty().WithMessage("Unit stock is empty!!!");

            RuleFor(p => p.UnitPrice).GreaterThan(0);//girilen deger 0 dan buyuk olmalı!
            //RuleFor(p => p.UnitsInStock).GreaterThanOrEqualTo((short)0);
            RuleFor(p => p.UnitPrice).GreaterThan(10).When(p => p.CategoryID == 2);//unit price girilen deger 10 dan buyuk olmalı eger categoriden 2. seçenek seçili ise !
            
                
            //RuleFor(p => p.ProductName).Must(StartWithA).WithMessage("!!!urun a harfi ile başlamalı");//kendi kısıtımızı yazmak icin must 
        }

        //private bool StartWithA(string arg)
        //{
        //    return arg.StartsWith("A");
        //}
    }
}
