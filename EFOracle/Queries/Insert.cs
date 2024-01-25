using EFOracle.Data;
using Microsoft.EntityFrameworkCore;
using Oracle.ManagedDataAccess.Client;

namespace EFOracle.Queries
{
    public static class Insert
    {
        /// <summary>INSERT INTO [TABLA] values (...)</summary>
        public static void New<T>(string connectionString, T entity) where T : class
        {
            try
            {
                using (var context = new HRContext(connectionString))
                {
                    /*
                        context.Set<T> es lo mismo que context.Countries o context.Regions
                        La T reemplaza a la entidad a mapear
                    */
                    context.Set<T>().Add(entity);
                    int affectedRows = context.SaveChanges();

                    Console.WriteLine($"Insert: filas afectadas {affectedRows}");
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
