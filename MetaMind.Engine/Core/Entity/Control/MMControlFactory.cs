using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaMind.Engine.Core.Entity.Control
{
    public class MMControlFactory
    {
        public static T Create<T>() where T: IMMControlComponent, new()
        {
            return new T();
        }
    }
}
