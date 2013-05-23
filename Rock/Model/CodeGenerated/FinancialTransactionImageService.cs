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
    /// FinancialTransactionImage Service class
    /// </summary>
    public partial class FinancialTransactionImageService : Service<FinancialTransactionImage>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FinancialTransactionImageService"/> class
        /// </summary>
        public FinancialTransactionImageService()
            : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FinancialTransactionImageService"/> class
        /// </summary>
        public FinancialTransactionImageService(IRepository<FinancialTransactionImage> repository) : base(repository)
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
        public bool CanDelete( FinancialTransactionImage item, out string errorMessage )
        {
            errorMessage = string.Empty;
            return true;
        }
    }

    /// <summary>
    /// Generated Extension Methods
    /// </summary>
    public static partial class FinancialTransactionImageExtensionMethods
    {
        /// <summary>
        /// Clones this FinancialTransactionImage object to a new FinancialTransactionImage object
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="deepCopy">if set to <c>true</c> a deep copy is made. If false, only the basic entity properties are copied.</param>
        /// <returns></returns>
        public static FinancialTransactionImage Clone( this FinancialTransactionImage source, bool deepCopy )
        {
            if (deepCopy)
            {
                return source.Clone() as FinancialTransactionImage;
            }
            else
            {
                var target = new FinancialTransactionImage();
                target.TransactionId = source.TransactionId;
                target.BinaryFileId = source.BinaryFileId;
                target.TransactionImageTypeValueId = source.TransactionImageTypeValueId;
                target.Id = source.Id;
                target.Guid = source.Guid;

            
                return target;
            }
        }
    }
}
