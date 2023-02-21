using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FourthLab.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ImplementedInterfacesAttribute : Attribute
    {
        public string[] Interfaces { get; set; }

        /// <summary>
        /// Defines the implemented interfaces of a class.
        /// </summary>
        /// <param name="hierarchy">Array of the implemented interfaces' names.</param>
        public ImplementedInterfacesAttribute(params string[] interfaces)
        {
            Interfaces = interfaces;
        }
    }
}
