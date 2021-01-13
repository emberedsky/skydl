
using System.Windows;


namespace Skydl
{
    
    public partial class HelpWindow : Window
    {
        public HelpWindow()
        {
            InitializeComponent();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {

            this.Close();
        }

        private void ydlwebsitebutton_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://youtube-dl.org/");
        }

        private void ffmpegwebsitebutton_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://ffmpeg.org/download.html");
        }

        private void walkthroughvideobutton_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.youtube.com/watch?v=5axVgHHDBvU");
        }

        private void atomicwebsitebutton_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://atomicparsley.sourceforge.net/");
        }

        private void TipButton_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://paypal.me/dawnoverdusk");
        }
    }
}
