using E_Commerce.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient; 
using System.Text.RegularExpressions;

namespace E_Commerce.Controllers
{
    public class DbController : Controller
    {
        private readonly string _connectionString;
        public DbController(IConfiguration configuration)
        {
            // Get the connection string from your appsettings.json or environment variable
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }
        public string Index()
        {
            return "Hmmm";
        }

        public async Task<IActionResult> updateviews()
        {
            try
            {
                // Specify the path to your SQL file
                var sqlFilePath = Path.Combine(Directory.GetCurrentDirectory(), "views.sql");

                // Read the content of the SQL file
                var sqlQuery = await System.IO.File.ReadAllTextAsync(sqlFilePath);

                // Split the SQL file by the 'GO' keyword (case-insensitive)
                var sqlStatements = Regex.Split(sqlQuery, @"^\s*GO\s*$", RegexOptions.Multiline | RegexOptions.IgnoreCase);


                // Execute each SQL statement
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    foreach (var statement in sqlStatements)
                    {
                        // Ignore empty or whitespace statements
                        if (!string.IsNullOrWhiteSpace(statement))
                        {
                            using (var command = new SqlCommand(statement, connection))
                            {
                                await command.ExecuteNonQueryAsync();
                            }
                        }
                    }
                }

                // Redirect to the referer page
                if (Request.Headers.ContainsKey("Referer"))
                {
                    this.Flash("View recreated successfully", "success");
                    return Redirect(Request.Headers["Referer"].ToString());
                }

                return RedirectToAction("Index"); // Fallback to index if no referer found
            }
            catch (Exception ex)
            {
                // Log error (for production, ensure to log the error)
                return BadRequest($"Error executing SQL file: {ex.Message}");
            }
        }


    }
}
