using Entity.Entities.Common;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entity.Entities
{
    public class ElectricBill : BaseEntity
    {
        [Column(TypeName = "varchar(50)")]
        public string Name { get; set; }

        [Column(TypeName = "varchar(30)")]
        public string BillId { get; set; }

        [Column(TypeName = "varchar(30)")]
        public string PayId { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public double ConsumedEnergy { get; set; }
    }
}
