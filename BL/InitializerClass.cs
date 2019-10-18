using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DL.Entity;

namespace BL
{
    public static class InitializerClass
    { 
        public static LibraryInitializer Initialize()
        {
            return new LibraryInitializer();
        }
    }
}
