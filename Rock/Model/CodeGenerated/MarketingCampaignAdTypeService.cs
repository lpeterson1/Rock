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
    /// MarketingCampaignAdType Service class
    /// </summary>
    public partial class MarketingCampaignAdTypeService : Service<MarketingCampaignAdType>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MarketingCampaignAdTypeService"/> class
        /// </summary>
        public MarketingCampaignAdTypeService()
            : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MarketingCampaignAdTypeService"/> class
        /// </summary>
        public MarketingCampaignAdTypeService(IRepository<MarketingCampaignAdType> repository) : base(repository)
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
        public bool CanDelete( MarketingCampaignAdType item, out string errorMessage )
        {
            errorMessage = string.Empty;
 
            if ( new Service<MarketingCampaignAd>().Queryable().Any( a => a.MarketingCampaignAdTypeId == item.Id ) )
            {
                errorMessage = string.Format( "This {0} is assigned to a {1}.", MarketingCampaignAdType.FriendlyTypeName, MarketingCampaignAd.FriendlyTypeName );
                return false;
            }  
            return true;
        }
    }

    /// <summary>
    /// Generated Extension Methods
    /// </summary>
    public static class MarketingCampaignAdTypeExtensionMethods
    {
        /// <summary>
        /// Copies all the entity properties from another MarketingCampaignAdType entity
        /// </summary>
        public static void CopyPropertiesFrom( this MarketingCampaignAdType target, MarketingCampaignAdType source )
        {
            target.IsSystem = source.IsSystem;
            target.Name = source.Name;
            target.DateRangeType = source.DateRangeType;
            target.Id = source.Id;
            target.Guid = source.Guid;

        }
    }
}
