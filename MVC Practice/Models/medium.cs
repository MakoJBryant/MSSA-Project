//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MVC_Practice.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class medium
    {
        public int id { get; set; }
        public string title { get; set; }
        public string media_desc { get; set; }
        public string file_title { get; set; }
        public int size { get; set; }
        public Nullable<int> likes { get; set; }
        public Nullable<int> media_views { get; set; }
        public Nullable<int> downloads { get; set; }
        public Nullable<int> category_id { get; set; }
        public int users_id { get; set; }
        public int media_type_id { get; set; }
        public System.DateTime created_at { get; set; }
    }
}