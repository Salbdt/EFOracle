using EFOracle.Data;
using Microsoft.EntityFrameworkCore;
using Oracle.ManagedDataAccess.Client;

namespace EFOracle.Queries
{
    public static class Select
    {
        /// <summary>SELECT COUNT(*) FROM [TABLA]</summary>
        public static void Count<T>(string connectionString) where T : class
        {
            try
            {
                using (var context = new HRContext(connectionString))
                {
                    /*
                        context.Set<T> es lo mismo que context.Countries o context.Regions
                        La T reemplaza a la entidad a mapear
                    */
                    int quantity = context.Set<T>().Count();

                    Console.WriteLine($"La tabla {context.Set<T>().EntityType.GetTableName()} tiene {quantity} filas");
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

        /// <summary>SELECT * FROM [TABLA] WHERE ...</summary>
        public static T? One<T>(string connectionString, object id) where T : class
        {
            T? entity = null;
            try
            {
                using (var context = new HRContext(connectionString))
                {
                    /*
                        Set<T>().EntityType.GetDefaultTableName()   Obtengo el nombre de la clase
                        Set<T>().EntityType.GetTableName()          Obtengo el nombre de la tabla
                    */
                    entity = context.Set<T>().Find(id);

                    if (entity is not null)
                    {
                        Console.WriteLine($"La entidad {context.Set<T>().EntityType.GetDefaultTableName()} se encontró con éxito");
                        Console.WriteLine(entity);
                    }
                    else
                        Console.WriteLine($"La entidad {context.Set<T>().EntityType.GetDefaultTableName()} no existe");
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

            return entity;
        }

        /// <summary>SELECT * FROM [TABLA]</summary>
        public static void All<T>(string connectionString) where T : class
        {
            try
            {
                using (var context = new HRContext(connectionString))
                {
                    /*
                        context.Set<T> es lo mismo que context.Countries o context.Regions
                        La T reemplaza a la entidad a mapear
                    */
                    var data = context.Set<T>().ToList();

                    foreach (var item in data)
                        Console.WriteLine(item);
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

        /// <summary>SELECT C.COUNTRY_ID, C.COUNTRY_NAME, R.REGION_ID, R.REGION_NAME
        /// <br/>FROM COUNTRIES C JOIN REGIONS R ON C.REGION_ID = R.REGION_ID</summary>
        public static void AllCountriesAndRegions(string connectionString)
        {
            try
            {
                using (var context = new HRContext(connectionString))
                {
                    var data = context.Countries.Join(
                        context.Regions,
                        c => c.RegionId,
                        r => r.Id,
                        (c, r) => new { c, r }
                    ).ToList();

                    foreach (var item in data)
                        Console.WriteLine(item);
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
