namespace GoWithMe.Areas.Admin.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("GOWITHME.Tour")]
    public partial class Tour
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Tour()
        {
            Tickets = new HashSet<Ticket>();
            TourDetails = new HashSet<TourDetail>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public decimal ID { get; set; }

        [Display(Name = "Tên Tour")]
        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string Name { get; set; }

        [Display(Name = "Số Lượng Khách Tối Đa")]
        public decimal Quantyti { get; set; }

        [Display(Name = "Giá")]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        [Display(Name = "Mô Tả")]
        [StringLength(100, MinimumLength = 3)]
        public string Discription { get; set; }

        [Display(Name = "Ngày Bắt Đầu")]
        public DateTime StartDay { get; set; }

        [Display(Name = "Thời Gian")]
        [StringLength(50, MinimumLength = 3)]
        public string Duration { get; set; }

        [Display(Name = "Hình Ảnh")]
        [Required]
        [StringLength(200)]
        public string Image { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Ticket> Tickets { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TourDetail> TourDetails { get; set; }
    }
}
