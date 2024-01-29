using EFOracle.Data;
using Microsoft.EntityFrameworkCore;
using Oracle.ManagedDataAccess.Client;

namespace EFOracle.Queries
{
    public static class Update
    {
        /// <summary>UPDATE [TABLA] SET ... WHERE ...</summary>
        public static void Existing<T>(string connectionString, T entity) where T : class
        {
            try
            {
                using (var context = new HRContext(connectionString))
                {
                    /*
                        context.Set<T> es lo mismo que context.Countries o context.Regions
                        La T reemplaza a la entidad a mapear
                    */
                    int affectedRows = context.SaveChanges();

                    Console.WriteLine($"Update: filas afectadas {affectedRows}");
                }
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException is not null && ex.InnerException is OracleException oracleEx)
                    Console.WriteLine($"OracleException: {oracleEx.Message}");
                else
                    Console.WriteLine($"DbUpdateException: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
            }
        }
    }
}
