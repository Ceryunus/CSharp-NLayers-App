using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Northwind.Business.Abstract;
using Northwind.Business.ValidationRules.FluentValidation;
using Northwind.DataAccess.Abstract;
using Northwind.DataAccess.Concrete;
using Northwind.DataAccess.Concrete.EntityFramework;
using Northwind.Entities.Concrete;

namespace Northwind.Business.Concrete
{

    public class ProductManager:IProductService
    {
        private IProductDal _productDal;

        public ProductManager(IProductDal productDal)
        {
            _productDal = productDal;
        }

        public List<Product> GetAll()
        {
            return _productDal.GetAll();
        }

        public List<Product> GetByCategoryId(int categoryId)
        {
            return _productDal.GetAll(p => p.CategoryID == categoryId);
        }

        public List<Product> GetByProductName(string productName)
        {
            if (string.IsNullOrEmpty(productName))
            {
                return _productDal.GetAll();
            }
            else
            {
                return _productDal.GetAll(p => p.ProductName.ToLower().Contains(productName.ToLower()));

            }
        }

        public void Add(Product product)
        {
            
            ProductValidator productValidator = new ProductValidator();
            var result = productValidator.Validate(product);//parametre olarak gelen product ı product validatore gondererek sorgulatıp bir deyişkene atıyor.
            if (result.Errors.Count>0)
            {
                throw new ValidationException(result.Errors);
            }
            else
            {
                _productDal.Add(product);

            }
        }

        public void Update(Product product)
        {
            _productDal.Update(product);
        }  public void Delete(Product product)
        {
            try
            {
                _productDal.Delete(product);
            }
            catch
            {
                throw new Exception("Deletion Failed");
                
            }   
        }
    }
}
