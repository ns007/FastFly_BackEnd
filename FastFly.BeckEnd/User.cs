//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FastFly.BeckEnd
{
    using System;
    using System.Collections.Generic;
    
    public partial class User
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public User()
        {
            this.ApplyDocuments = new HashSet<ApplyDocument>();
            this.ReckoningDocuments = new HashSet<ReckoningDocument>();
        }
    
        public string Id { get; set; }
        public string Password { get; set; }
        public string EmailAddress { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Role { get; set; }
        public Nullable<int> PercentageJob { get; set; }
        public string CellNum { get; set; }
        public int DepartmentId { get; set; }
        public int FacultyId { get; set; }
        public int ApplicationRoleId { get; set; }
        public bool UserEnable { get; set; }
    
        public virtual Department Department1 { get; set; }
        public virtual Faculty Faculty1 { get; set; }
        public virtual ApplicationRole ApplicationRole1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ApplyDocument> ApplyDocuments { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ReckoningDocument> ReckoningDocuments { get; set; }
    }
}
