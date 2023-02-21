using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FourthLab.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ClassHierarchyAttribute : Attribute
    {
        public string Hierarchy { get; set; }

        /// <summary>
        /// Defines the hierarchy of a class.
        /// </summary>
        /// <param name="hierarchy">Array of related objects in the order of the least derived to the most.</param>
        public ClassHierarchyAttribute(params string[] hierarchy)
        {
            StringBuilder text = new();
            foreach (string className in hierarchy.Reverse())
                text.Append($"{className} -> ");

            Hierarchy = text.ToString()[..^3];
        }
    }
}
