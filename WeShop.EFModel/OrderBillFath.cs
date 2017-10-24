namespace WeShop.EFModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("OrderBillFath")]
    public partial class OrderBillFath
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public OrderBillFath()
        {
            OrderBillChis = new HashSet<OrderBillChi>();
        }

        [Key]
        [StringLength(50)]
        public string Code { get; set; }

        public int? CusId { get; set; }

        [StringLength(50)]
        public string State { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? OrderPrice { get; set; }

        public int? ExpressPrice { get; set; }

        [StringLength(10)]
        public string SumPrice { get; set; }

        [StringLength(50)]
        public string Payment { get; set; }

        [StringLength(50)]
        public string Memo { get; set; }

        [StringLength(50)]
        public string CheckUser { get; set; }

        public DateTime? CreateTime { get; set; }

        public DateTime? PatTime { get; set; }

        public DateTime? PostTime { get; set; }

        public DateTime? ReceTime { get; set; }

        public virtual Customer Customer { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderBillChi> OrderBillChis { get; set; }

        public virtual Payment Payment1 { get; set; }
    }
}
