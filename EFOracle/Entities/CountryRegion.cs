using System.Drawing;
using System.Xml.Linq;

namespace EFOracle.Entities
{
    public class CountryRegion
    {
        public Country? Country { get; set; }
        public Region? Region { get; set; }

        public override string ToString()
        {
            string result;

            if (Country is not null && Region is not null)
                result = $"| ID: {Country.Id} | Name: {Country.Name.PadRight(40)} | RegionID: {Region.Id} | RegionName: {Region.Name}";
            else
                result = "Fila incompleta";

            return result;
        }
    }
}
