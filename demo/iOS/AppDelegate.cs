﻿using Foundation;
using UIKit;

namespace MapBoxQs.iOS
{
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
		{
            Mapbox.MGLAccountManager.AccessToken = MapBoxQs.Services.MapBoxService.AccessToken;
            new Naxam.Controls.Mapbox.Platform.iOS.MapViewRenderer();

            global::Xamarin.Forms.Forms.Init();

            LoadApplication(new App());

            return base.FinishedLaunching(app, options);
        }
    }
}
