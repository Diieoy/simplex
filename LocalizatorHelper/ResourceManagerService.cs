using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Resources;
using System.Globalization;
using System.Threading;

namespace LocalizatorHelper
{
    public static class ResourceManagerService
    {
        private static Dictionary<string, ResourceManager> _managers;

        public static event LocaleChangedEventHander LocaleChanged;

        private static void RaiseLocaleChanged(CultureInfo newLocale)
        {
            var evt = LocaleChanged;

            if (evt != null)
            {
                evt.Invoke(null, new LocaleChangedEventArgs(newLocale));
            }
        }

        public static CultureInfo CurrentLocale { get; private set; }

        static ResourceManagerService()
        {
            _managers = new Dictionary<string, ResourceManager>();

            ChangeLocale(CultureInfo.CurrentCulture.Name);
        }

        public static string GetResourceString(string managerName, string resourceKey)
        {
            ResourceManager manager = null;
            string resource = String.Empty;

            if (_managers.TryGetValue(managerName, out manager))
            {
                resource = manager.GetString(resourceKey);
            }
            return resource;
        }

        public static void ChangeLocale(string newLocaleName)
        {
            CultureInfo newCultureInfo = new CultureInfo(newLocaleName);
            Thread.CurrentThread.CurrentCulture = newCultureInfo;
            Thread.CurrentThread.CurrentUICulture = newCultureInfo;

            CurrentLocale = newCultureInfo;

            RaiseLocaleChanged(newCultureInfo);
        }

        public static void Refresh()
        {
            ChangeLocale(CultureInfo.CurrentCulture.IetfLanguageTag);
        }

        public static void RegisterManager(string managerName, ResourceManager manager)
        {
            RegisterManager(managerName, manager, false);
        }

        public static void RegisterManager(string managerName, ResourceManager manager, bool refresh)
        {
            ResourceManager _manager = null;

            _managers.TryGetValue(managerName, out _manager);

            if (_manager == null)
            {
                _managers.Add(managerName, manager);
            }

            if (refresh)
            {
                Refresh();
            }
        }

        public static void UnregisterManager(string name)
        {
            ResourceManager _manager = null;

            _managers.TryGetValue(name, out _manager);

            if (_manager != null)
            {
                _managers.Remove(name);
            }
        }
    }
}


