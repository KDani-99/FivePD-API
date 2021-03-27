using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FivePD.API
{
    /// <summary>
    /// Bundles callout properties (CalloutPropertiesAttribute is used in the CalloutManager)
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = true)]
    public sealed class CalloutPropertiesAttribute : Attribute
    {
        /// <summary>The name of the callout as shown in the Debug menu and in the console (See <see cref="Callout.ShortName"/> for the dispatch name)</summary>
        public string name { get; private set; }
        public string author { get; private set; }

        /// <summary>
        /// The version of the callout.
        /// <example>
        /// For example: <c>1.2.3</c>
        /// </example>
        /// </summary>
        public string version { get; private set; }

        /// <summary>
        /// Callout properties
        /// </summary>
        /// <param name="name">The name of the callout as shown in the Debug menu and in the console (See <see cref="Callout.ShortName"/> for the dispatch name)</param>
        /// <param name="version">The version of the callout. <example>For example: <c>1.2.3</c></example></param>
        public CalloutPropertiesAttribute(string name, string author, string version)
        {
            this.name = name;
            this.version = version;
            this.author = author;
        }
    }
}
