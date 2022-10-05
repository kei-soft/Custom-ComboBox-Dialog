using System;

using Android.App;
using Android.Content;
using Android.Content.Res;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Views;
using Android.Widget;

using AndroidX.Core.Content;

using App10;
using App10.Droid;

using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CustomPicker), typeof(CustomPickerRenderer))]
namespace App10.Droid
{
    public class CustomPickerRenderer : PickerRenderer
    {
        CustomPicker element;
        private Xamarin.Forms.Picker picker;
        private Dialog dialog;

        public CustomPickerRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Picker> e)
        {
            base.OnElementChanged(e);

            element = (CustomPicker)this.Element;

            if (Control != null && this.Element != null && !string.IsNullOrEmpty(element.Image))
            {
                Control.Background = AddPickerStyles(element.Image);
            }

            if (Control != null)
            {
                picker = e.NewElement as Xamarin.Forms.Picker;

                Control.SetPadding(0, 0, 0, 0);
                Control.Gravity = GravityFlags.Center;

                Control.Click += Control_Click;
            }
        }

        public LayerDrawable AddPickerStyles(string imagePath)
        {
            ShapeDrawable border = new ShapeDrawable();
            border.Paint.Color = Android.Graphics.Color.Gray;
            border.SetPadding(10, 10, 10, 10);
            border.Paint.SetStyle(Paint.Style.Stroke);

            Drawable[] layers = { border, GetDrawable(imagePath) };
            LayerDrawable layerDrawable = new LayerDrawable(layers);
            layerDrawable.SetLayerInset(0, 0, 0, 0, 0);

            return layerDrawable;
        }

        private BitmapDrawable GetDrawable(string imagePath)
        {
            int resID = Resources.GetIdentifier(imagePath, "drawable", this.Context.PackageName);
            var drawable = ContextCompat.GetDrawable(this.Context, resID);
            var bitmap = ((BitmapDrawable)drawable).Bitmap;

            var result = new BitmapDrawable(Resources, Bitmap.CreateScaledBitmap(bitmap, 30, 30, true));
            result.Gravity = Android.Views.GravityFlags.Right;

            return result;
        }


        private void Control_Click(object sender, EventArgs e)
        {
            dialog = new Dialog(Context);

            dialog.SetContentView(Resource.Layout.Dialog);

            Android.Widget.TextView title = (Android.Widget.TextView)dialog.FindViewById(Resource.Id.pickerTitle);

            title.Text = picker.Title;

            Android.Widget.ListView listView = (Android.Widget.ListView)dialog.FindViewById(Resource.Id.pickerListView);

            listView.Adapter = new ArrayAdapter<string>(Context, Resource.Layout.CustomListItem, picker.Items);

            listView.ItemClick += ListView_ItemClick;

            dialog.Show();
        }

        private void ListView_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            picker.SelectedIndex = e.Position;
            dialog.Cancel();
        }
        protected override void Dispose(bool disposing)
        {
            Control.Click -= Control_Click;
            base.Dispose(disposing);
        }
    }
}