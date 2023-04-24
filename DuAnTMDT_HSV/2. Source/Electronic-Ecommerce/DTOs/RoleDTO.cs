using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Electronic_Ecommerce.DTOs
{
    public class RoleDTO
    {
        public List<RolePermissCheckbox> RolePermissions { get; set; }
        public int role_id { get; set; }
        public string role_name { get; set; }
        public int count_account_role { get; set; }
        public int permiss_id { get; set; }
        public string permiss_name { get; set; }

        public class RolePermissCheckbox
        {
            public int role_id { get; set; }
            public int permiss_id { get; set; }
            public string permiss_name { get; set; }
            public bool Checked { get; set; }
        }
    }
}