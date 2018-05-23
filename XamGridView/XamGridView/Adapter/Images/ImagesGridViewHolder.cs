using Xamarin.Forms;

/*
 * Developed By : Kishan Donga, (Software Engineer Mobility)
 * Email Id : kishandonga.92@gmail.com
 * Contact : (+91)9712598499
 * Country : India
 * */

namespace XamGridView.Adapter.Images
{
    // make your custom cell
    public class ImagesGridViewHolder
    {
        public Image img;

        public ImagesGridViewHolder()
        {
            img = new Image();
            img.Aspect = Aspect.Fill;
        }

        // to get the root view of the your cell
        public View getRootView()
        {
            return img;
        }
    }
}
