//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace OnlineAgenda.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class TblVak
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TblVak()
        {
            this.TblAgendaItems = new HashSet<TblAgendaItem>();
        }
    
        public string VakCode { get; set; }
        public string Vaknaam { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TblAgendaItem> TblAgendaItems { get; set; }
    }
}