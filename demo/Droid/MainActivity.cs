﻿
using System;
using Android.App;
using Android.Content.PM;
using Android.OS;
using Com.Mapbox.Mapboxsdk.Maps;
using Com.Mapbox.Mapboxsdk.Annotations;
using Com.Mapbox.Mapboxsdk.Geometry;
using static Com.Mapbox.Mapboxsdk.Maps.MapboxMap;
using Android.Views;
using Android.Widget;
using Android.Content;
using Android.Support.V4.Content;
using Android.Runtime;
using Android.Graphics;

namespace MapBoxQs.Droid
{
    [Activity(Label = "MapBoxQs.Droid", Icon = "@mipmap/ic_launcher", Theme = "@style/MyTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        private MapView mapView;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

            Com.Mapbox.Mapboxsdk.Mapbox.GetInstance(this, MapBoxQs.Services.MapBoxService.AccessToken);

            Acr.UserDialogs.UserDialogs.Init(() => this);

            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);

            System.Diagnostics.Debug.WriteLine("Mapbox version: " + Com.Mapbox.Mapboxsdk.BuildConfig.MapboxVersionString);

            LoadApplication(new App());
            //SetContentView(Resource.Layout.activity_main);
            //mapView = (MapView)FindViewById(Resource.Id.mapView);
            //mapView.OnCreate(savedInstanceState);
            //mapView.GetMapAsync(new MapReady(this));
            //mapView.OffsetTopAndBottom(0);

        }

        public class MapReady : Java.Lang.Object, IOnMapReadyCallback
        {
            Context _context;
            public MapReady(Context context)
            {
                _context = context;
            }

            public void Dispose()
            {
                throw new NotImplementedException();
            }

            public void OnMapReady(MapboxMap mapboxMap)
            {
                mapboxMap.AddMarker(new Com.Mapbox.Mapboxsdk.Annotations.MarkerOptions().SetPosition(new LatLng(40.416717, -3.703771)).SetTitle("spain"));
                mapboxMap.AddMarker(new Com.Mapbox.Mapboxsdk.Annotations.MarkerOptions().SetPosition(new LatLng(26.794531, 29.781524)).SetTitle("egypt"));
                mapboxMap.AddMarker(new Com.Mapbox.Mapboxsdk.Annotations.MarkerOptions().SetPosition(new LatLng(50.981488, 10.384677)).SetTitle("germany"));
                mapboxMap.InfoWindowAdapter = new InfoWindowAdapter(_context);
            }
        }

        public class InfoWindowAdapter : Java.Lang.Object, IInfoWindowAdapter
        {
            public void Dispose()
            {
                throw new NotImplementedException();
            }

            Context _context;
            public InfoWindowAdapter(Context c)
            {
                _context = c;
            }

            public View GetInfoWindow(Marker marker)
            {
                LinearLayout parent = new LinearLayout(_context);
                parent.LayoutParameters = (new LinearLayout.LayoutParams(ViewGroup.LayoutParams.WrapContent, ViewGroup.LayoutParams.WrapContent));
                parent.Orientation = Orientation.Vertical;
                parent.SetBackgroundColor(Color.Red);
                TextView txtTittle = new TextView(_context);
                ImageView countryFlagImage = new ImageView(_context);
                switch (marker.Title)
                {
                    case "spain":
                        txtTittle.SetText(marker.Title, TextView.BufferType.Normal);
                        countryFlagImage.SetImageDrawable(ContextCompat.GetDrawable(
                          _context, Resource.Drawable.icon));
                        break;
                    case "egypt":
                        txtTittle.SetText(marker.Title, TextView.BufferType.Normal);
                        countryFlagImage.SetImageDrawable(ContextCompat.GetDrawable(
                           _context, Resource.Drawable.icon));
                        break;
                    default:
                        txtTittle.SetText(marker.Title, TextView.BufferType.Normal);
                        countryFlagImage.SetImageDrawable(ContextCompat.GetDrawable(
                         _context, Resource.Drawable.icon));
                        break;
                }
                countryFlagImage.LayoutParameters = (new Android.Views.ViewGroup.LayoutParams(150, 100));
                txtTittle.LayoutParameters = (new Android.Views.ViewGroup.LayoutParams(150, 100));

                parent.AddView(countryFlagImage);
                parent.AddView(txtTittle);

                return parent;
            }
        }
    }
}
