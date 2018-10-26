﻿using System;

namespace Naxam.Controls.Mapbox.Forms
{
    public class Layer
    {
        public string Id { get; set; }

        public bool IsVisible { get; set; }

        public Layer(string id)
        {
            Id = id;
            IsVisible = true;
        }
    }

    public class StyleLayer : Layer
    {
        public string SourceId
        {
            get;
            private set;
        }

        public StyleLayer(string id, string sourceId) : base(id)
        {
            SourceId = sourceId;
        }
    }
}