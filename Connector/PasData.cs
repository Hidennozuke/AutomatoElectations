//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AutomatoElectations.Connector
{
    using System;
    using System.Collections.Generic;
    
    public partial class PasData
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PasData()
        {
            this.Candidate = new HashSet<Candidate>();
            this.Member = new HashSet<Member>();
        }
    
        public int IdPas { get; set; }
        public string Serial { get; set; }
        public string Number { get; set; }
        public string Code { get; set; }
        public System.DateTime GivedDate { get; set; }
        public System.DateTime BirthDate { get; set; }
        public string GivedPlace { get; set; }
        public string BirthPlace { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Candidate> Candidate { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Member> Member { get; set; }
    }
}
