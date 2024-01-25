using EFOracle.Data;
using Microsoft.EntityFrameworkCore;
using Oracle.ManagedDataAccess.Client;

namespace EFOracle.Queries
{
    public static class Procedure
    {
        /// <summary>EXECUTE [PROCEDURE] -> BEGIN [PROCEDURE]; END;</summary>
        public static void GetAll<T>(string connectionString, string procedureName, List<OracleParameter>? parameters = null) where T: class
        {
            try
            {
                using (var context = new HRContext(connectionString))
                {
                    // Revisamos si se ingresaron parámetros
                    string parametersString = "";
                    if (parameters is not null && parameters.Any())
                        foreach (OracleParameter item in parameters)
                            parametersString += item.ParameterName.ToString() + ", ";

                    // Creamos la query para ejecutar con o sin parámetros
                    string query = $"BEGIN {procedureName}({parametersString}); END;".Replace(", )", ")").Replace("()", "");

                    // Obtenemos y mostramos los datos del procedimiento
                    var data = context.Set<T>().FromSqlRaw(query).ToList();

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