using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Naxam.Controls.Mapbox.Forms
{
    public sealed class PreserveAttribute : System.Attribute
    {
        public bool AllMembers;
        public bool Conditional;
    }

    public class MapStyle : BindableObject
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Owner { get; set; }

        public double[] Center { get; set; }

        private string urlString;
        public string UrlString
        {
            get
            {
                return this.urlString;
                //return "asset://vector.mb-style.json";
                //return "http://mpromet-dars.geoprostor.net/mapbox/assets/res/mb-styles/vector.mb-style.json";
                //return "Assets/MbStyles/mock.mb-style.json";
                //return "https://rmap2.realis.si/lokacija.si.app/assets/res/mb-styles/vector.mb-style.json";
                //if (!string.IsNullOrEmpty(Id) && !string.IsNullOrEmpty(Owner))
                //{

                //    return "mapbox://styles/" + Owner + "/" + Id;
                //}
                //return null;
            }
        }

        public MapStyle()
        {
        }

        public MapStyle(string id, string name, double[] center = null, string owner = null)
        {
            Id = id;
            Name = name;
            Center = center;
            Owner = owner;
        }

        public MapStyle(string urlString)
        {
            this.SetUrl(urlString);
        }

        public void SetUrl(string urlString)
        {
            this.urlString = urlString;
        }

        //public MapStyle(string urlString)
        //{
        //    UpdateIdAndOwner(urlString);
        //}

        //public void SetUrl(string urlString)
        //{
        //    UpdateIdAndOwner(urlString);
        //}

        //void UpdateIdAndOwner(string urlString)
        //{
        //    if (!string.IsNullOrEmpty(urlString))
        //    {
        //        var segments = (new Uri(urlString)).Segments;
        //        if (string.IsNullOrEmpty(Id) && segments.Length != 0)
        //        {
        //            Id = segments[segments.Length - 1].Trim('/');
        //        }
        //        if (string.IsNullOrEmpty(Owner) && segments.Length > 1)
        //        {
        //            Owner = segments[segments.Length - 2].Trim('/');
        //        }
        //    }
        //}

        public static readonly BindableProperty CustomSourcesProperty = BindableProperty.Create(
            nameof(CustomSources),
            typeof(IEnumerable<MapSource>),
            typeof(MapView),
            default(IEnumerable<MapSource>),
            BindingMode.TwoWay);

        public IEnumerable<MapSource> CustomSources
        {
            get => (IEnumerable<MapSource>)GetValue(CustomSourcesProperty);
            set => SetValue(CustomSourcesProperty, value);
        }

        public static readonly BindableProperty CustomLayersProperty = BindableProperty.Create(
            nameof(CustomLayers),
            typeof(IEnumerable<Layer>),
            typeof(MapStyle),
            default(IEnumerable<Layer>),
            BindingMode.TwoWay);

        public IEnumerable<Layer> CustomLayers
        {
            get => (IEnumerable<Layer>)GetValue(CustomLayersProperty);
            set => SetValue(CustomLayersProperty, value);
        }

        public static readonly BindableProperty OriginalLayersProperty = BindableProperty.Create(
                    nameof(CustomLayers),
                    typeof(Layer[]),
                    typeof(MapStyle),
                    default(Layer[]),
            BindingMode.OneWayToSource);
        public Layer[] OriginalLayers
        {
            get => (Layer[])GetValue(OriginalLayersProperty);
            set => SetValue(OriginalLayersProperty, value);
        }
    }
}
