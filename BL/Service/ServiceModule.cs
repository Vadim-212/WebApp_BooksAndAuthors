using DL.Entity;
using DL.Repository;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Service
{
    public class ServiceModule : NinjectModule
    {
        string connection;
        public ServiceModule(string connection)
        {
            this.connection = connection;
        }
        public override void Load()
        {
            if (connection == "DefaultConnection")
                Bind<IUnitOfWork>().To<UnitOfWork>().WithConstructorArgument(new Library());
            else
                Bind<IUnitOfWork>().To<UnitOfWork>().WithConstructorArgument(connection);
            
        }
    }
}
