//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Tydee.DataModels
{
    using System;
    using System.Collections.Generic;
    
    public partial class CollectionOption
    {
        public int Id { get; set; }
        public bool ShowExchanged { get; set; }
        public bool ShowArchived { get; set; }
        public bool ShowSold { get; set; }
        public bool ShowSearched { get; set; }
        public bool AlphaOrder { get; set; }
        public bool FavoriteFirst { get; set; }
        public bool OwnedFirst { get; set; }
    
        public virtual Collection Collection { get; set; }
    }
}
