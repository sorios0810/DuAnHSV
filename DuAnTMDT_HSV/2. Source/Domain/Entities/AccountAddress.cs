namespace Domain.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("AccountAddress")]
    public partial class AccountAddress
    {
        [Key]
        public int account_address_id { get; set; }

        public int account_id { get; set; }

        [StringLength(10)]
        public string account_address_phonenumber { get; set; }

        [StringLength(20)]
        public string account_address_username { get; set; }

        public int province_id { get; set; }

        public int district_id { get; set; }

        public int ward_id { get; set; }

        [StringLength(50)]
        public string account_address_content { get; set; }

        public bool account_address_default { get; set; }

        public virtual Account Account { get; set; }

        public virtual District District { get; set; }

        public virtual Province Province { get; set; }

        public virtual Ward Ward { get; set; }
    }
}
