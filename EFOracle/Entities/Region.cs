using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;

namespace EFOracle.Entities
{
    // La entidad hace referencia a la tabla de Oracle y a sus campos.
    // Deben estar en mayúsculas exactamente igual que en la BD.

    [Table("REGIONS")]
    public class Region
    {
        [Column("REGION_ID")]
        public int Id { get; set; }

        [Column("REGION_NAME")]
        public string Name {  get; set; } = string.Empty;

        // Se utilizó el método ToString() especialmente para este proyecto de ejemplo
        public override string ToString()
        {
            return $"| ID: {Id} | Name: {Name.PadRight(25)} |";
        }
    }
}
