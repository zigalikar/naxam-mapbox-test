﻿using System;
using CoreLocation;
using Naxam.Controls.Mapbox.Forms;

namespace Naxam.Controls.Mapbox.Platform.iOS
{
    public static class PositionExtensions
    {
        public static CLLocationCoordinate2D ToCLCoordinate(this Position pos)
        {
            return new CLLocationCoordinate2D(pos.Lat, pos.Long);
        }
    }
}
