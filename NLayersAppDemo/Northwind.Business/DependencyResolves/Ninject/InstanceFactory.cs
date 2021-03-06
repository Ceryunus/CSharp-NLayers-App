using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject;

namespace Northwind.Business.DependencyResolves.Ninject
{
    public class InstanceFactory
    {
        public static T GetInstance<T>()
        {

            var kernal = new StandardKernel(new BusinessModule());
            return kernal.Get<T>();

        }
    }
}
