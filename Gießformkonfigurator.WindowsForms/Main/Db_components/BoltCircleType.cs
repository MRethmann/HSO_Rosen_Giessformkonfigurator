namespace Gießformkonfigurator.WindowsForms.Main.Db_components
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("BoltCircleType")]
    public partial class BoltCircleType
    {
        [Key]
        [Column("TypeDescription")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [StringLength(10)]
        public string TypeDescription { get; set; }

        public int Diameter { get; set; }

        public int HoleQty { get; set; }

        public decimal Angle { get; set; }

        public decimal HolepairAngle { get; set; }

        public decimal HoleDiameter { get; set; }
    }
}
