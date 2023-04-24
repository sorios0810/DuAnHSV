namespace Domain.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Web;

    [Table("Account")]
    public partial class Account
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Account()
        {
            Carts = new HashSet<Cart>();
            AccountAddresses = new HashSet<AccountAddress>();
            CommentLikes = new HashSet<CommentLike>();
            Feedbacks = new HashSet<Feedback>();
            News = new HashSet<New>();
            NewComments = new HashSet<NewComment>();
            Orders = new HashSet<Order>();
            ReplyComments = new HashSet<ReplyComment>();
            ReplyCommentLikes = new HashSet<ReplyCommentLike>();
        }

        [Key]
        public int account_id { get; set; }

        [StringLength(100)]
        [RegularExpression("^((?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9]))(?=.*[#$^+=!*()@%&]).{8,}$",
          ErrorMessage = "M?t kh?u t?i thi?u 8 ký t? bao g?m: ch? th??ng, ch? hoa, ch? s? và 1 ký t? ??c bi?t")]
        public string password { get; set; }

        [StringLength(100, ErrorMessage = "Email t?i thi?u  ký t?", MinimumLength = 1)]
        [EmailAddress(ErrorMessage = "Vui lòng nh?p ?úng ??nh d?ng email")]
        public string Email { get; set; }

        [StringLength(100)]
        public string Requestcode { get; set; }

        [Required(ErrorMessage = "Nh?p h? tên")]
        [StringLength(20, ErrorMessage = "H? tên t?i ?a 20 ký t?", MinimumLength = 1)]
        [DataType(DataType.Text)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Nh?p s? ?i?n tho?i")]
        [StringLength(10)]
        [RegularExpression("^(0)([0-9]{9})$",
        ErrorMessage = "S? ?i?n tho?i ph?i b?t ??u b?ng 0, ch?a ký t? s? t? (0 -> 9) và ?? 10 ch? s?")]
        public string Phone { get; set; }

        [StringLength(500, ErrorMessage = "?nh ??i di?n không ???c quá 500 ký t?")]
        public string Avatar { get; set; }

        [Column(TypeName = "date")]
        [Required(ErrorMessage = "Nh?p ngày sinh")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy}")]
        public DateTime Dateofbirth { get; set; }

        [Required(ErrorMessage = "Ch?n gi?i tính")]
        [StringLength(1)]
        public string Gender { get; set; }

        [StringLength(100)]
        public string create_by { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime create_at { get; set; }

        [StringLength(100)]
        public string update_by { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime? update_at { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime expired_at { get; set; }

        [StringLength(1)]
        public string status { get; set; }

        public int? role_id { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Cart> Carts { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AccountAddress> AccountAddresses { get; set; }

        public virtual Role Role { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CommentLike> CommentLikes { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Feedback> Feedbacks { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<New> News { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<NewComment> NewComments { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Order> Orders { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ReplyComment> ReplyComments { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ReplyCommentLike> ReplyCommentLikes { get; set; }

        [NotMapped]
        public HttpPostedFileBase ImageUpload { get; set; }
    }
}
