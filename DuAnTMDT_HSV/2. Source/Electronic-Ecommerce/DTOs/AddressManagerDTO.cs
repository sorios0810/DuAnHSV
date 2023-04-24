using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Web;

namespace Electronic_Ecommerce.DTOs
{
    public class AddressManagerDTO
    {
        public int address_id { get; set; }

        [Required]
        public int account_id { get; set; }

        //số điện thoại
        public string phonenumber { get; set; }
        
        public string username { get; set; }

        //tỉnh thành phố
        public int province_id { get; set; }

        public string province_type { get; set; }

        public string province_name { get; set; }

        //quận, huyện
        public int district_id { get; set; }

        public string district_name { get; set; }

        public string district_type { get; set; }

        //phường xã
        public int ward_id { get; set; }

        public string ward_name { get; set; }

        public string ward_type { get; set; }

        //địa chỉ cụ thể
        [StringLength(50)]
        public string address_content { get; set; }

        //đặt làm đia chỉ mặc định hay không
        public bool address_default { get; set; }
    }
}