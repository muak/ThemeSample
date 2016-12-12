using System;
using Xamarin.Forms;
namespace ThemeSample.Effects
{
    public class AlterColor
    {
        public static readonly BindableProperty ColorProperty =
            BindableProperty.CreateAttached(
                    "Color",
                    typeof(Color),
                    typeof(AlterColor),
                    default(Color),
                    propertyChanged:OnColorChanged
                );

        public static void SetColor(BindableObject view, Color value)
        {
            view.SetValue(ColorProperty, value);
        }

        public static Color GetColor(BindableObject view)
        {
            return (Color)view.GetValue(ColorProperty);
        }

        private static void OnColorChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var elm = bindable as Element;
            if (elm == null) {
                return;
            }

            var oldColor = (Color)oldValue;
            var newColor = (Color)newValue;

            if (oldColor == Color.Default && newColor != Color.Default) {
                elm.Effects.Add(new AlterColorRoutingEffect());
            }
        }

        class AlterColorRoutingEffect : RoutingEffect
        {
            public AlterColorRoutingEffect() : base("Xamarin." + nameof(AlterColor))
            {

            }
        }
    }
}
