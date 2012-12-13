﻿using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace StarryEyes.Views.Utils
{
    public class CollectionExistsToVisibleConverter : OneWayConverter<IEnumerable<object>, Visibility>
    {
        public override Visibility ToTarget(IEnumerable<object> input, object parameter)
        {
            return input != null && input.Count() > 0 ? Visibility.Visible : Visibility.Collapsed;
        }
    }
}
