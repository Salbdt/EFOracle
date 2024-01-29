using EFOracle.Data;
using EFOracle.Entities;
using Microsoft.EntityFrameworkCore;

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
    }
}
