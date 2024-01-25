using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFOracle.Entities
{
    // La entidad hace referencia a la tabla de Oracle y a sus campos.
    // Deben estar en mayúsculas exactamente igual que en la BD.

    [Table("COUNTRIES")]
    public class Country
    {
        [Column("COUNTRY_ID")]
        public string Id { get; set; } = string.Empty;

        [Column("COUNTRY_NAME")]
        public string Name { get; set; } = string.Empty;

        [Column("REGION_ID")]
        public int RegionId { get; set; }

        public override string ToString()
        {
            return $"| ID: {Id} | Name: {Name.PadRight(40)} | RegionID: {RegionId} |";
        }
    }
}
