using System;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms;
using ThemeSample.Effects;
using ThemeSample.Droid.Effects;
using Android.Widget;
using Android.Graphics.Drawables;
using Android.Support.V7.Widget;
using Android.Content.Res;

[assembly: ResolutionGroupName("Xamarin")]
[assembly: ExportEffect(typeof(AlterColorPlatformEffect), nameof(AlterColor))]
namespace ThemeSample.Droid.Effects
{
    public class AlterColorPlatformEffect : PlatformEffect
    {

        protected override void OnAttached()
        {
            UpdateColor();
        }

        protected override void OnDetached()
        {

        }

        void UpdateColor()
        {
            var color = AlterColor.GetColor(Element).ToAndroid();
            if (Element is NavigationPage) {
                UpdateStatusBar(color);
            }
            else if (Element is Slider) {
                UpdateSlider(color);
            }
            else if (Element is Xamarin.Forms.Switch) {
                UpdateSwitch(color);
            }
        }

        void UpdateStatusBar(Android.Graphics.Color color)
        {
            var window = (Container.Context as FormsAppCompatActivity).Window;
            window.SetStatusBarColor(color);
        }

        void UpdateSlider(Android.Graphics.Color color)
        {
            var seekBar = Control as SeekBar;

            var progress = (LayerDrawable)(seekBar.ProgressDrawable.Current);

            progress.GetDrawable(2).SetTint(color);
            var altColor = Android.Graphics.Color.Argb(76, color.R, color.G, color.B);
            progress.GetDrawable(0).SetTint(altColor);

            seekBar.Thumb.SetTint(color);
        }

        void UpdateSwitch(Android.Graphics.Color color)
        {

            var aSwitch = Control as SwitchCompat;

            var trackColors = new ColorStateList(new int[][]
                 {
                                new int[]{global::Android.Resource.Attribute.StateChecked},
                                new int[]{-global::Android.Resource.Attribute.StateChecked},
                 },
                new int[] {
                                Android.Graphics.Color.Argb(76,color.R,color.G,color.B),
                                Android.Graphics.Color.Argb(76, 117, 117, 117)
                 });


            aSwitch.TrackDrawable.SetTintList(trackColors);

            var thumbColors = new ColorStateList(new int[][]
                 {
                                new int[]{global::Android.Resource.Attribute.StateChecked},
                                new int[]{-global::Android.Resource.Attribute.StateChecked},
                 },
                new int[] {
                                color,
                                Android.Graphics.Color.Argb(255, 244, 244, 244)
                 });

            aSwitch.ThumbDrawable.SetTintList(thumbColors);

        }

        protected override void OnElementPropertyChanged(System.ComponentModel.PropertyChangedEventArgs args)
        {
            base.OnElementPropertyChanged(args);

            if (args.PropertyName == AlterColor.ColorProperty.PropertyName) {
                UpdateColor();
            }

        }
    }
}
