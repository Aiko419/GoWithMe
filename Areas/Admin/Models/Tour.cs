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

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public decimal Quantyti { get; set; }

        public decimal Price { get; set; }

        [StringLength(100)]
        public string Discription { get; set; }

        public DateTime StartDay { get; set; }

        [StringLength(50)]
        public string Duration { get; set; }

        [Required]
        [StringLength(200)]
        public string Image { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Ticket> Tickets { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TourDetail> TourDetails { get; set; }
    }
}
