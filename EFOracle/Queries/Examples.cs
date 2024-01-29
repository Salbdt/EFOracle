using EFOracle.Data;
using EFOracle.Entities;
using Microsoft.EntityFrameworkCore;
using Oracle.ManagedDataAccess.Client;

namespace EFOracle.Queries
{
    public static class Examples
    {
        public static void Transaction(string connectionString)
        {
            using (var context = new HRContext(connectionString))
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    var affectedRows = context.Database.ExecuteSql($"INSERT INTO REGIONS values (5, 'Oceania')");

                    transaction.CreateSavepoint("Insert New Region");

                    Console.WriteLine("Creando Oceania");
                    foreach (var region in context.Regions.ToList())
                        Console.WriteLine(region);
                    Console.WriteLine();

                    transaction.Rollback();                                 // Volvemos todo atrás
                    //transaction.RollbackToSavepoint("Insert New Region"); // Volvemos atrás hasta el SavePoint
                    //transaction.Commit();                                 // Si commiteamos, el nuevo registro queda guardado.

                    Console.WriteLine("Deshaciendo Oceania");
                    foreach (var region in context.Regions.ToList())
                        Console.WriteLine(region);
                }
            }
        }
        public static void Query(string connectionString, string query)
        {
            using (var context = new HRContext(connectionString))
            {
                try
                {
                    var affectedRows = context.Database.ExecuteSqlRaw(query);
                    context.SaveChanges();
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
                    if (ex.InnerException is not null && ex.InnerException is OracleException oracleEx)
                        Console.WriteLine($"ExceptionOracle: {oracleEx.Message}");
                    else if (ex.GetBaseException() is not null && ex.GetBaseException() is OracleException oracleExc)
                        Console.WriteLine($"ExceptionOracle: {oracleExc.Message}");
                    else
                        Console.WriteLine($"Exception: {ex.Message}");
                }
            }
        }
    }
}
