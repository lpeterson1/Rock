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
    /// Tag Service class
    /// </summary>
    public partial class TagService : Service<Tag>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TagService"/> class
        /// </summary>
        public TagService()
            : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TagService"/> class
        /// </summary>
        public TagService(IRepository<Tag> repository) : base(repository)
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
        public bool CanDelete( Tag item, out string errorMessage )
        {
            errorMessage = string.Empty;
            return true;
        }
    }

    /// <summary>
    /// Generated Extension Methods
    /// </summary>
    public static partial class TagExtensionMethods
    {
        /// <summary>
        /// Clones this Tag object to a new Tag object
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="deepCopy">if set to <c>true</c> a deep copy is made. If false, only the basic entity properties are copied.</param>
        /// <returns></returns>
        public static Tag Clone( this Tag source, bool deepCopy )
        {
            if (deepCopy)
            {
                return source.Clone() as Tag;
            }
            else
            {
                var target = new Tag();
                target.IsSystem = source.IsSystem;
                target.EntityTypeId = source.EntityTypeId;
                target.EntityTypeQualifierColumn = source.EntityTypeQualifierColumn;
                target.EntityTypeQualifierValue = source.EntityTypeQualifierValue;
                target.Name = source.Name;
                target.Order = source.Order;
                target.OwnerId = source.OwnerId;
                target.Id = source.Id;
                target.Guid = source.Guid;

            
                return target;
            }
        }
    }
}
