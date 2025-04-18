//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MVVMFirma.Models.Entities
{
    using System;
    using System.Collections.Generic;
    
    public partial class Zamowienia
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Zamowienia()
        {
            this.Faktury = new HashSet<Faktury>();
            this.Reklamacje = new HashSet<Reklamacje>();
            this.SzczegolyZamowienia = new HashSet<SzczegolyZamowienia>();
        }
    
        public int IdZamowienia { get; set; }
        public Nullable<int> IdKlienta { get; set; }
        public Nullable<System.DateTime> DataZamowienia { get; set; }
        public string Status { get; set; }
        public Nullable<decimal> CenaCalkowita { get; set; }
        public Nullable<bool> Zamkniete { get; set; }
        public string NumerReferencyjny { get; set; }
        public Nullable<int> IdPracownika { get; set; }
        public string KanalZamowienia { get; set; }
        public string Komentarze { get; set; }
        public string MetodaPlatnosci { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Faktury> Faktury { get; set; }
        public virtual Klienci Klienci { get; set; }
        public virtual Pracownicy Pracownicy { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Reklamacje> Reklamacje { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SzczegolyZamowienia> SzczegolyZamowienia { get; set; }
    }
}
