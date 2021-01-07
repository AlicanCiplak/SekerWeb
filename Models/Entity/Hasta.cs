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
    
    public partial class Hasta
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Hasta()
        {
            this.Besin = new HashSet<Besin>();
            this.DoktorHasta = new HashSet<DoktorHasta>();
            this.Sekerlerim = new HashSet<Sekerlerim>();
            this.TestAtama = new HashSet<TestAtama>();
        }
    
        public int İD { get; set; }
        public string AdSoyad { get; set; }
        public Nullable<System.DateTime> TARİH { get; set; }
        public Nullable<int> HekimİD { get; set; }
        public Nullable<long> TC { get; set; }
        public Nullable<long> TelefonNumarası { get; set; }
        public string Adres { get; set; }
        public Nullable<int> Tip { get; set; }
        public string KullanıcıAdı { get; set; }
        public string Sifre { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Besin> Besin { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DoktorHasta> DoktorHasta { get; set; }
        public virtual Hekim Hekim { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Sekerlerim> Sekerlerim { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TestAtama> TestAtama { get; set; }
    }
}
