//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SekerWeb.Models.Entity
{
    using System;
    using System.Collections.Generic;
    
    public partial class Mesaj
    {
        public int İD { get; set; }
        public Nullable<int> HastaİD { get; set; }
        public Nullable<int> HekimİD { get; set; }
        public Nullable<int> Gonderen { get; set; }
        public string Mesaj1 { get; set; }
        public Nullable<System.DateTime> Tarih { get; set; }
    
        public virtual Hasta Hasta { get; set; }
        public virtual Hekim Hekim { get; set; }
    }
}
