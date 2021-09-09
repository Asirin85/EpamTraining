using Ninject.Modules;
using StringToIntLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleUI
{
    class IOCConfigModule : NinjectModule
    {
        public override void Load()
        {
            Bind<Microsoft.Extensions.Logging.ILogger<StringLib>>().To<ConsoleAppLogger>();
            Bind<ConsoleAppLogger>().ToSelf(); // I don't need this because it'll do it automatically, but i am leaving this for learning purposes
        }
    }
}
