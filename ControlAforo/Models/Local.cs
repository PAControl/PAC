//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ControlAforo.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Local
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Local()
        {
            this.Control = new HashSet<Control>();
        }
    
        public int id { get; set; }
        public string usuario { get; set; }
        public string contraseña { get; set; }
        public string direccion { get; set; }
        public Nullable<int> telefono { get; set; }
        public int aforo { get; set; }
        public int aforoMax { get; set; }
        public bool rol { get; set; }
        public int id_empresa { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Control> Control { get; set; }
        public virtual Empresa Empresa { get; set; }
    }
}
