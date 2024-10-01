using E_Commerce.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Reflection;
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

        /*
            To run dotnet ef [command], we need globalliy install dotnet-ef using below command
            dotnet tool install --global dotnet-ef
            after this we can update database using

            dotnet ef datatabase update
            
         */
        public async Task<IActionResult> UpdateDB()
        {
            string referrer = Request.Headers["Referer"].ToString();
            string redirectTo = string.IsNullOrEmpty(referrer) ? "/" : referrer;
            try
            {
                // Get the current assembly's location
                string assemblyPath = Assembly.GetExecutingAssembly().Location;

                // Get the project directory by navigating up to the parent directory
                string projectDirectory = Path.GetDirectoryName(assemblyPath);

                // Get the path to the .csproj file
                string projectFilePath = Path.GetFullPath(Path.Combine(projectDirectory, @"..\..\..\"));

                // Create a ProcessStartInfo object with the desired command and arguments
                ProcessStartInfo startInfo = new ProcessStartInfo
                {
                    FileName = "dotnet",
                    Arguments = $"ef database update",
                    WorkingDirectory = Path.GetDirectoryName(projectFilePath)
                };

                // Start the process
                Process process = new Process();
                process.StartInfo = startInfo;
                process.Start();

                // Wait for the process to complete
                await process.WaitForExitAsync();

                // Check the exit code to determine if the update was successful
                if (process.ExitCode != 0)
                { 
                    this.Flash("Error: Database update failed.", "danger");
                    return Redirect(redirectTo);
                }
                else
                { 
                    this.Flash("Database updated successfully.", "success");
                    return Redirect(redirectTo);
                }
            }
            catch (Exception ex)
            { 
                this.Flash(ex.Message, "danger");
                return Redirect(redirectTo);
            }
        }
    }
}
