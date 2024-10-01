namespace E_Commerce.Data
{
    public class ClearSettingsCacheMiddleware
    {
        private readonly RequestDelegate _next;

        public ClearSettingsCacheMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Clear the settings cache before processing each request
            MyHelper.ClearSettingsCache();

            // Call the next middleware in the pipeline
            await _next(context);
        }
    }
}
