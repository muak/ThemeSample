using System;
using ThemeSample.Effects;
using ThemeSample.iOS.Effects;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using UIKit;

[assembly: ResolutionGroupName("Xamarin")]
[assembly: ExportEffect(typeof(AlterColorPlatformEffect), nameof(AlterColor))]
namespace ThemeSample.iOS.Effects
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
            var color = AlterColor.GetColor(Element).ToUIColor();
            if (Element is Slider) {
                UpdateSlider(color);
            }
            else if (Element is Switch) {
                UpdateSwitch(color);
            }
        }

        void UpdateSlider(UIColor color)
        {
            var slider = Control as UISlider;
            slider.MinimumTrackTintColor = color;
            slider.MaximumTrackTintColor = color.ColorWithAlpha(0.3f);
        }

        void UpdateSwitch(UIColor color)
        {
            var uiSwitch = Control as UISwitch;
            uiSwitch.OnTintColor = color;
            //Offの時の背景を変えるならこれ
            //uiSwitch.BackgroundColor = UIColor.FromRGB(244,244,244);
            //uiSwitch.Layer.CornerRadius = uiSwitch.Frame.Size.Height / 2;
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
