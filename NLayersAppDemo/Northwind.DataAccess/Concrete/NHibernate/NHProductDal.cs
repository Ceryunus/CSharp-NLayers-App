using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Northwind.DataAccess.Abstract;
using Northwind.Entities.Concrete;

namespace Northwind.DataAccess.Concrete.NHibernate
{
    public class NHProductDal:IProductDal

    {
        

        public List<Product> GetAll(Expression<Func<Product, bool>> filter = null)
        {
            List<Product> myProduct = new List<Product>
            {
                new Product{
                    CategoryID = 1, ProductID = 2,
                    ProductName = "laptop",
                    QuantityPerUnit = "monster",
                    UnitPrice = 2,
                    UnitsInStock = 22
                }
            };
            return myProduct;
        }

        public Product Get(Expression<Func<Product, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public void Add(Product product)
        {
            throw new NotImplementedException();
        }

        public void Update(Product product)
        {
            throw new NotImplementedException();
        }

        public void Delete(Product product)
        {
            throw new NotImplementedException();
        }
    }
}
