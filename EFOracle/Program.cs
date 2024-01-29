using EFOracle;
using EFOracle.Data;
using EFOracle.Entities;
using EFOracle.Queries;

string connectionString = "Data Source=//localhost:1521/xepdb1;User Id=hr;Password=HR";

// Iniciando programa
Utility.PrintTitle("Programa Principal", '#');
Console.WriteLine();

// Buscamos y mostramos todos los países
Console.WriteLine("PAÍSES");
Select.All<Country>(connectionString);
Console.WriteLine();

// Buscamos y mostramos todas las regiones
Console.WriteLine("REGIONES");
Select.All<Region>(connectionString);
Console.WriteLine();

// Buscamos y mostramos todas los países con sus regiones
Console.WriteLine("PAÍSES CON REGIONES");
Select.AllCountriesAndRegions(connectionString);
Console.WriteLine();

// Insertamos una región (PK Exception)
Console.WriteLine("INSERTAR REGION");
Insert.New<Region>(connectionString, new Region {Id = 1, Name = "Antártida" });
Console.WriteLine();

// Insertamos un país (FK Exception)
Console.WriteLine("INSERTAR PAÍS");
Insert.New<Country>(connectionString, new Country {Id = "CL", Name = "Chile", RegionId = 5});
Console.WriteLine();

// Contamos los países
Console.WriteLine("COUNT PAISES");
Select.Count<Country>(connectionString);
Console.WriteLine();

// Obtenemos una región
Console.WriteLine("SELECT REGION");
Region? region = Select.One<Region>(connectionString, 2);
Console.WriteLine();

// Editamos la región y la guardamos en la BD
if (region is not null)
{
    if (region.Name.Equals("America"))
        region.Name = "Americas";
    else
        region.Name = "América";
    Update.Existing<Region>(connectionString, region);
}
Select.One<Region>(connectionString, 2);
Console.WriteLine();

// Obtenemos todos los países desde un Stored Procedure
Console.WriteLine("PROCEDURE COUNTRIES_GET_ALL");
Procedure.GetAll<Country>(connectionString, "COUNTRIES_GET_ALL");
Console.WriteLine();

// Ejemplo de Transacción
Examples.Transaction(connectionString);
Console.WriteLine();