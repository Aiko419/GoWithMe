namespace GoWithMe.Areas.Admin.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("GOWITHME.Ticket")]
    public partial class Ticket
    {
        [Key]
        [Column(Order = 0)]
        public decimal TourID { get; set; }

        [Key]
        [Column(Order = 1)]
        public decimal CustomerID { get; set; }

        [Display(Name = "Ngày Đặt Tour")]
        public DateTime Date { get; set; }

        [Display(Name = "Số lượng khách đăng kí")]
        public decimal Quantyti { get; set; }

        [Display(Name = "Trạng Thái")]
        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string Tatus { get; set; }

        public virtual Customer Customer { get; set; }

        public virtual Tour Tour { get; set; }
    }
}
