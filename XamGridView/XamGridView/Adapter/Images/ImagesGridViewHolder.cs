using Xamarin.Forms;

/*
 * Developed By : Kishan Donga, (Software Engineer Mobility)
 * Email Id : kishandonga.92@gmail.com
 * Contact : (+91)9712598499
 * Country : India
 * */

namespace XamGridView.Adapter.Images
{
    public class ImagesGridViewHolder
    {
        public Image img;

        public ImagesGridViewHolder()
        {
            img = new Image();
            img.Aspect = Aspect.Fill;
        }

        public View getRootView()
        {
            return img;
        }
    }
}
