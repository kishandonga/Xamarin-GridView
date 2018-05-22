using Xamarin.Forms;

/*
 * Developed By : Kishan Donga, (Software Engineer Mobility)
 * Email Id : kishandonga.92@gmail.com
 * Contact : (+91)9712598499
 * Country : India
 * */

namespace XamGridView.Adapter.Alphabet
{
    public class AlphabetViewHolder
    {
        public Frame frame;
        public Label lbl;

        public AlphabetViewHolder()
        {
            lbl = new Label();
            lbl.TextColor = Color.Black;
            lbl.FontSize = 25;
            lbl.HorizontalTextAlignment = TextAlignment.Center;
            lbl.VerticalTextAlignment = TextAlignment.Center;

            frame = new Frame();
            frame.OutlineColor = Color.Black;
            frame.Content = lbl;
        }

        public View getRootView()
        {
            return frame;
        }
    }
}
