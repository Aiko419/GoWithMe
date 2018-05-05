namespace GoWithMe.Areas.Admin.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("GOWITHME.News")]
    public partial class News
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public decimal ID { get; set; }

        public decimal PlaceID { get; set; }

        [Display(Name = "Tên Tin Tức")]
        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string Name { get; set; }

        [Display(Name = "Nội Dung")]
        [Required]
        public string Content { get; set; }

        [Display(Name = "Hình Ảnh")]
        [Required]
        [StringLength(200)]
        public string Image { get; set; }

        public virtual Place Place { get; set; }
    }
}
