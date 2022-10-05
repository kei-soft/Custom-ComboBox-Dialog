using Xamarin.Forms;

namespace App10
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            for (int i = 0; i < 10; i++)
            {
                this.noPicker.Items.Add(i.ToString());
            }
        }
    }
}
