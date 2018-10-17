using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Windows;

namespace LocalizatorHelper
{
    public static class DesignHelpers
    {
        public static bool IsInDesignMode
        {
            get
            {
                return (bool)(DesignerProperties.IsInDesignModeProperty
                    .GetMetadata(typeof(DependencyObject))
                    .DefaultValue);
            }
        }
    }
}
