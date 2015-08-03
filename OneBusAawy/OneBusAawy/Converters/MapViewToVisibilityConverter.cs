﻿/* Copyright 2014 Michael Braude and individual contributors.
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using OneBusAway.Model;
using System;

#if WINDOWS_PHONE
using System.Windows.Data;
using System.Windows;
using System.Globalization;
#else
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;
#endif

namespace OneBusAway.Converters
{
    /// <summary>
    /// Automatically hides a BusStop (or similar control) when the map view
    /// has zoomed outside of the maximum view.
    /// </summary>
    public class MapViewToVisibilityConverter : IValueConverter
    {
        /// <summary>
        /// Returns Visible if the maps zoom level is greater than the minimum bus stop visible zoom.
        /// </summary>
#if WINDOWS_PHONE
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
#else
        public object Convert(object value, Type targetType, object parameter, string language)
#endif
        {
            MapView mapView = value as MapView;
            if (mapView != null)
            {
                bool invert = false;
                if (parameter != null)
                {
                    bool.TryParse(parameter as string, out invert);
                }

                if (mapView.ZoomLevel < Constants.MinBusStopVisibleZoom)
                {
                    return (invert)
                        ? Visibility.Visible
                        : Visibility.Collapsed;                        
                }
                else
                {
                    return (invert)
                        ? Visibility.Collapsed
                        : Visibility.Visible;
                }
            }

            return Visibility.Collapsed;
        }

        /// <summary>
        /// Not supported.
        /// </summary>
#if WINDOWS_PHONE
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
#else
        public object ConvertBack(object value, Type targetType, object parameter, string language)
#endif
        {
            throw new NotSupportedException();
        }
    }
}
