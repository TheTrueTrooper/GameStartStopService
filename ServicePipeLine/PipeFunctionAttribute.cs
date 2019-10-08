using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ServicePipeLine
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class PipeFunctionAttribute : Attribute
    {
        string _ActionName;

        public virtual string ActionName
        {
            get { return _ActionName; }
        }

        public PipeFunctionAttribute(string ActionName)
        {
            _ActionName = ActionName;
        }
    }
}
