using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ServicePipeLine
{
    /// <summary>
    /// this is a class to allow maping of a method Info to be mapped on to its object
    /// </summary>
    internal class InstancedAndMethodInfo
    {
        /// <summary>
        /// the object we will call from 
        /// </summary>
        public object Instance { get; set; }
        /// <summary>
        /// the info on the method that we would like to call
        /// </summary>
        public MethodInfo MethodInfo { get; set; }
    }
}
