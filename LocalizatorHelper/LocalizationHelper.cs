using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Windows;

namespace LocalizatorHelper
{
    public class LocalisationHelper : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertyChanged(string propertyName)
        {
            var evt = PropertyChanged;

            if (evt != null)
            {
                evt.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public LocalisationHelper()
        {
            if (!DesignHelpers.IsInDesignMode)
            {
                ResourceManagerService.LocaleChanged += (s, e) =>
                {
                    RaisePropertyChanged(string.Empty);
                };
            }
        }

        public string this[string key]
        {
            get
            {
                if (!ValidateKey(key))
                {
                    throw new ArgumentException(@"Incorrect format. [ManagerName].[ResourceKey]");
                }
                if (DesignHelpers.IsInDesignMode)
                {
                    return "[res]";
                }

                return ResourceManagerService.GetResourceString(GetManagerKey(key), GetResourceKey(key));
            }
        }

        #region Private Key Methods

        private bool ValidateKey(string input)
        {
            return input.Contains(".");
        }

        private string GetManagerKey(string input)
        {
            return input.Split('.')[0];
        }

        private string GetResourceKey(string input)
        {
            return input.Substring(input.IndexOf('.') + 1);
        }

        #endregion
    }
}


