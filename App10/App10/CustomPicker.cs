using Xamarin.Forms;

namespace App10
{
    public class CustomPicker : Picker
    {
        public static readonly BindableProperty ImageProperty =
            BindableProperty.Create(nameof(Image), typeof(string), typeof(CustomPicker), string.Empty);

        public string Image
        {
            get { return (string)GetValue(ImageProperty); }
            set { SetValue(ImageProperty, value); }
        }

        public CustomPicker() : base()
        {
            Margin = 3;
        }
    }
}
