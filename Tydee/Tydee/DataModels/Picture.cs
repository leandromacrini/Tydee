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
    
    public partial class Picture
    {
        public int Id { get; set; }
        public string MainFile { get; set; }
        public string ThumbFile { get; set; }
        public int ItemId { get; set; }
    
        public virtual Item Item { get; set; }
    }
}
