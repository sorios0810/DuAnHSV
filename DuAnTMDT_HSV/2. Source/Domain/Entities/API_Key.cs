namespace Domain.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class API_Key
    {
        public int id { get; set; }

        [StringLength(500)]
        public string client_id { get; set; }

        [StringLength(500)]
        public string client_secret { get; set; }

        [StringLength(200)]
        public string Return_Url { get; set; }

        public bool active { get; set; }

        public DateTime update_at { get; set; }

        [StringLength(100)]
        public string api_name { get; set; }

        [StringLength(300)]
        public string api_decription { get; set; }
    }
}
