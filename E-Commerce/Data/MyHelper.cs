using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Data
{
    public static class MyHelper
    {
        private static Dictionary<string, string> _settingsCache = new();
		private static IServiceProvider _serviceProvider;//to get dbContext service
		public static void Initialize(IServiceProvider serviceProvider)
		{
			_serviceProvider = serviceProvider;
		}
        public static void ClearSettingsCache()
        {
            _settingsCache.Clear();
        }
        public static string GetSetting(string title,string defaultVal="")
        {
            if (_settingsCache.Count()>0 && _settingsCache.ContainsKey(title))
            {
                return _settingsCache[title];
            }else if (_settingsCache.Count() > 0 && !_settingsCache.ContainsKey(title))
            {
				return defaultVal;
			}

              
            using (var scope = _serviceProvider.CreateScope())
            {
				var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
				var settings = context.Settings.AsNoTracking().ToList();
                foreach (var setting in settings)
                {
                    _settingsCache[setting.title] = setting.value;
                }

				if (_settingsCache.ContainsKey(title))
				{
					return _settingsCache[title];
				}
				else
				{
					return defaultVal;
				}
			}
        }

		public static int GetIntSettings(string title, int defaultValue=0) {
			return Convert.ToInt32(GetSetting(title, defaultValue.ToString()));
		}

        public static bool GetBoolSettings(string title, bool defaultValue = false)
        {
            return Convert.ToBoolean(GetSetting(title, defaultValue.ToString()));
        }

        public static DateOnly GetDateOnlySetting(string title, DateOnly defaultValue = default)
        {
            var value = GetSetting(title, defaultValue.ToString());
            return DateOnly.TryParse(value, out var dateOnlyValue) ? dateOnlyValue : defaultValue;
        }

        public static DateTime GetDateTimeSetting(string title, DateTime defaultValue = default)
        {
            var value = GetSetting(title, defaultValue.ToString());
            return DateTime.TryParse(value, out var dateTimeValue) ? dateTimeValue : defaultValue;
        }
    }
}
