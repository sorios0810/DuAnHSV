namespace Domain.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Diagnostics.CodeAnalysis;
    using System.Web.Security;

    [Table("RolePermission")]
    public class RolePermission
    {
        [Key]
        [Column(Order = 0)]
        public int role_id { get; set; }

        [Key]
        [Column(Order = 1)]
        public int permission_id { get; set; }

        public virtual Role Roles { get; set; }

        public virtual Permission Permissions { get; set; }
    }
}
