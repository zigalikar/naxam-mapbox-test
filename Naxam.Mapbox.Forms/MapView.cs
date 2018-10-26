
using Naxam.Mapbox.Forms.AnnotationsAndFeatures;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Xamarin.Forms;

namespace Naxam.Controls.Mapbox.Forms
{

    public class AnnotationChangeEventArgs : EventArgs
    {
        public IEnumerable<Annotation> OldAnnotation { get; set; }
        public IEnumerable<Annotation> NewAnnotation { get; set; }
    }
    public class PositionChangeEventArgs : EventArgs
    {
        public PositionChangeEventArgs(Position newPosition)
        {
            NewPosition = newPosition;
        }

        public Position NewPosition { get; private set; }
    }

    public partial class MapView : View
    {

        public event EventHandler<AnnotationChangeEventArgs> AnnotationChanged;
        public static readonly BindableProperty IsMarkerClickedProperty = BindableProperty.Create(
            nameof(IsMarkerClicked),
            typeof(bool),
            typeof(MapView),
            default(bool),
            BindingMode.TwoWay
        );
        public bool IsMarkerClicked
        {
            get
            {
                return (bool)GetValue(IsMarkerClickedProperty);
            }
            set { SetValue(IsMarkerClickedProperty, value); }
        }

        public static readonly BindableProperty DragFinishedCommandProperty = BindableProperty.Create(
            nameof(DragFinishedCommand),
            typeof(ICommand),
            typeof(MapView),
            default(ICommand),
            BindingMode.OneWay);
        public ICommand DragFinishedCommand
        {
            get { return (ICommand)GetValue(DragFinishedCommandProperty); }
            set { SetValue(DragFinishedCommandProperty, value); }
        }

        public static readonly BindableProperty FocusPositionProperty = BindableProperty.Create(
            nameof(FocusPosition),
            typeof(bool),
            typeof(MapView),
            default(bool),
            BindingMode.OneWay);
        public bool FocusPosition
        {
            get
            {
                return (bool)GetValue(FocusPositionProperty);
            }
            set { SetValue(FocusPositionProperty, value); }
        }

        public static readonly BindableProperty CenterProperty = BindableProperty.Create(
            nameof(Center),
            typeof(Position),
            typeof(MapView),
            default(Position),
            BindingMode.TwoWay);
        public Position Center
        {
            get => (Position)GetValue(CenterProperty);
            set => SetValue(CenterProperty, value);
        }


        public static readonly BindableProperty UserLocationProperty = BindableProperty.Create(
            nameof(UserLocation),
            typeof(Position),
            typeof(MapView),
            default(Position),
            BindingMode.OneWayToSource);
        public Position UserLocation
        {
            get => (Position)GetValue(UserLocationProperty);
            set => SetValue(UserLocationProperty, value);
        }

        public static BindableProperty ShowUserLocationProperty = BindableProperty.Create(
            nameof(ShowUserLocation),
            typeof(bool),
            typeof(MapView),
            default(bool),
            BindingMode.OneWay
        );
        public bool ShowUserLocation
        {
            get { return (bool)GetValue(ShowUserLocationProperty); }
            set { SetValue(ShowUserLocationProperty, value); }
        }

        public static readonly BindableProperty ZoomLevelProperty = BindableProperty.Create(
            nameof(ZoomLevel),
            typeof(double),
            typeof(MapView),
            10.0,
            BindingMode.TwoWay);
        public double ZoomLevel
        {
            get => (double)GetValue(ZoomLevelProperty);
            set
            {
                if (Math.Abs(value - ZoomLevel) > 0.01)
                {
                    SetValue(ZoomLevelProperty, value);
                }
            }
        }

        public static readonly BindableProperty PitchProperty = BindableProperty.Create(
            nameof(Pitch),
            typeof(double),
            typeof(MapView),
            0.0,
            BindingMode.TwoWay);
        public double Pitch
        {
            get => (double)GetValue(PitchProperty);
            set => SetValue(PitchProperty, value);
        }

        public static readonly BindableProperty PitchEnabledProperty = BindableProperty.Create(
            nameof(PitchEnabled),
            typeof(bool),
            typeof(MapView),
            default(bool),
            BindingMode.TwoWay);
        public bool PitchEnabled
        {
            get => (bool)GetValue(PitchEnabledProperty);
            set => SetValue(PitchEnabledProperty, value);
        }

        public static readonly BindableProperty ScrollEnabledProperty = BindableProperty.Create(
            nameof(ScrollEnabled),
            typeof(bool),
            typeof(MapView),
            default(bool),
            BindingMode.OneWay);
        public bool ScrollEnabled
        {
            get { return (bool)GetValue(ScrollEnabledProperty); }
            set { SetValue(ScrollEnabledProperty, value); }
        }

        public static readonly BindableProperty RotateEnabledProperty = BindableProperty.Create(
            nameof(RotateEnabled),
            typeof(bool),
            typeof(MapView),
            default(bool),
            BindingMode.TwoWay);
        public bool RotateEnabled
        {
            get => (bool)GetValue(RotateEnabledProperty);
            set => SetValue(RotateEnabledProperty, value);
        }

        public static readonly BindableProperty RotatedDegreeProperty = BindableProperty.Create(
            nameof(RotatedDegree),
            typeof(double),
            typeof(MapView),
            0.0,
            BindingMode.TwoWay);

        public double RotatedDegree
        {
            get => (double)GetValue(RotatedDegreeProperty);
            set => SetValue(RotatedDegreeProperty, value);
        }

        public static readonly BindableProperty MapStyleProperty = BindableProperty.Create(
            nameof(MapStyle),
            typeof(MapStyle),
            typeof(MapView),
            default(MapStyle),
            BindingMode.TwoWay);
        public MapStyle MapStyle
        {
            get => (MapStyle)GetValue(MapStyleProperty);
            set => SetValue(MapStyleProperty, value);
        }

        public static readonly BindableProperty AnnotationsProperty = BindableProperty.Create(
            nameof(Annotations),
            typeof(IEnumerable<Annotation>),
            typeof(MapView),
            default(IEnumerable<Annotation>),
            BindingMode.TwoWay,
          propertyChanged: OnAnnotationChanged
            );

        private static void OnAnnotationChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is MapView control)
            {
                control.OnAnnotationChanged((IEnumerable<Annotation>)oldValue, (IEnumerable<Annotation>)newValue);
            }
        }

        public IEnumerable<Annotation> Annotations
        {
            get => (IEnumerable<Annotation>)GetValue(AnnotationsProperty);
            set => SetValue(AnnotationsProperty, value);
        }

        public static BindableProperty MarkersProperty = BindableProperty.Create(
           nameof(Markers),
           typeof(IEnumerable<PointAnnotation>),
           typeof(MapView),
           default(IEnumerable<PointAnnotation>),
           BindingMode.OneWay
           );
        public IEnumerable<PointAnnotation> Markers
        {
            get => (IEnumerable<PointAnnotation>)GetValue(MarkersProperty);
            set => SetValue(MarkersProperty, value);
        }

        public static BindableProperty PolylinesProperty = BindableProperty.Create(
           nameof(Polylines),
           typeof(IEnumerable<PolylineAnnotation>),
           typeof(MapView),
           default(IEnumerable<PolylineAnnotation>),
           BindingMode.OneWay);
        public IEnumerable<PolylineAnnotation> Polylines
        {
            get => (IEnumerable<PolylineAnnotation>)GetValue(PolylinesProperty);
            set => SetValue(PolylinesProperty, value);
        }


        public static BindableProperty RegionProperty = BindableProperty.Create(
           nameof(Region),
           typeof(MapRegion),
           typeof(MapView),
           MapRegion.Empty,
           BindingMode.TwoWay);
        public MapRegion Region
        {
            get => (MapRegion)GetValue(RegionProperty);
            set => SetValue(RegionProperty, value);
        }

        public static BindableProperty InfoWindowTemplateProperty = BindableProperty.Create(
            nameof(InfoWindowTemplate),
            typeof(DataTemplate),
            typeof(MapView),
            default(DataTemplate),
            BindingMode.OneWay
        );
        public DataTemplate InfoWindowTemplate
        {
            get { return (DataTemplate)GetValue(InfoWindowTemplateProperty); }
            set { SetValue(InfoWindowTemplateProperty, value); }
        }

        void OnAnnotationChanged(IEnumerable<Annotation> oldAnnotation, IEnumerable<Annotation> newAnnotation)
        {
            AnnotationChanged?.Invoke(this, new AnnotationChangeEventArgs
            {
                OldAnnotation = oldAnnotation,
                NewAnnotation = newAnnotation
            });
        }
    }
}
