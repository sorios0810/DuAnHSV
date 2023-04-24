namespace Domain.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ParentGenre")]
    public partial class ParentGenre
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ParentGenre()
        {
            Genres = new HashSet<Genre>();
        }

        public int id { get; set; }

        [Required]
        [StringLength(150)]
        public string name { get; set; }

        [StringLength(159)]
        public string slug { get; set; }

        [StringLength(20)]
        public string icon { get; set; }

        [StringLength(500)]
        public string image { get; set; }

        [StringLength(200)]
        public string description { get; set; }

        [StringLength(1)]
        public string status { get; set; }

        public DateTime create_at { get; set; }

        [Required]
        [StringLength(100)]
        public string create_by { get; set; }

        [Required]
        [StringLength(100)]
        public string update_by { get; set; }

        public DateTime update_at { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Genre> Genres { get; set; }
    }
}
