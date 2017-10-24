namespace WeShop.EFModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ProReview")]
    public partial class ProReview
    {
        public int Id { get; set; }

        [StringLength(50)]
        public string ProCode { get; set; }

        public int? CusId { get; set; }

        [Column(TypeName = "text")]
        public string Body { get; set; }

        [StringLength(50)]
        public string State { get; set; }

        [StringLength(50)]
        public string Rate { get; set; }

        public DateTime? CreateTime { get; set; }

        public virtual Customer Customer { get; set; }

        public virtual Product Product { get; set; }
    }
}
