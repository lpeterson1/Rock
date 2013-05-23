//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by the Rock.CodeGeneration project
//     Changes to this file will be lost when the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
//
// THIS WORK IS LICENSED UNDER A CREATIVE COMMONS ATTRIBUTION-NONCOMMERCIAL-
// SHAREALIKE 3.0 UNPORTED LICENSE:
// http://creativecommons.org/licenses/by-nc-sa/3.0/
//

using System;
using System.Linq;

using Rock.Data;

namespace Rock.Model
{
    /// <summary>
    /// MetricValue Service class
    /// </summary>
    public partial class MetricValueService : Service<MetricValue>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MetricValueService"/> class
        /// </summary>
        public MetricValueService()
            : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MetricValueService"/> class
        /// </summary>
        public MetricValueService(IRepository<MetricValue> repository) : base(repository)
        {
        }

        /// <summary>
        /// Determines whether this instance can delete the specified item.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <param name="errorMessage">The error message.</param>
        /// <returns>
        ///   <c>true</c> if this instance can delete the specified item; otherwise, <c>false</c>.
        /// </returns>
        public bool CanDelete( MetricValue item, out string errorMessage )
        {
            errorMessage = string.Empty;
            return true;
        }
    }

    /// <summary>
    /// Generated Extension Methods
    /// </summary>
    public static partial class MetricValueExtensionMethods
    {
        /// <summary>
        /// Clones this MetricValue object to a new MetricValue object
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="deepCopy">if set to <c>true</c> a deep copy is made. If false, only the basic entity properties are copied.</param>
        /// <returns></returns>
        public static MetricValue Clone( this MetricValue source, bool deepCopy )
        {
            if (deepCopy)
            {
                return source.Clone() as MetricValue;
            }
            else
            {
                var target = new MetricValue();
                target.IsSystem = source.IsSystem;
                target.MetricId = source.MetricId;
                target.Value = source.Value;
                target.Description = source.Description;
                target.xValue = source.xValue;
                target.isDateBased = source.isDateBased;
                target.Label = source.Label;
                target.Order = source.Order;
                target.Id = source.Id;
                target.Guid = source.Guid;

            
                return target;
            }
        }
    }
}
