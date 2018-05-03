namespace GoWithMe.Areas.Admin.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("GOWITHME.TourDetail")]
    public partial class TourDetail
    {
        [Key]
        [Column(Order = 0)]
        public decimal TourID { get; set; }

        [Key]
        [Column(Order = 1)]
        public decimal PlaceID { get; set; }

        public decimal Number { get; set; }

        public virtual Place Place { get; set; }

        public virtual Tour Tour { get; set; }
    }
}
