using System;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms;
using ThemeSample.Effects;
using ThemeSample.Droid.Effects;

[assembly: ResolutionGroupName("Xamarin")]
[assembly: ExportEffect(typeof(AlterColorPlatformEffect), nameof(AlterColor))]
namespace ThemeSample.Droid.Effects
{
    public class AlterColorPlatformEffect:PlatformEffect
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
        }

        void UpdateStatusBar(Android.Graphics.Color color)
        {
            var window = (Container.Context as FormsAppCompatActivity).Window;
            window.SetStatusBarColor(color);
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
