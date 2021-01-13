using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using System.Diagnostics;
using System.Timers;
using System.Windows.Threading;




namespace Skydl
{

    public partial class MainWindow : Window
    {
        
        string versionlabeltext = "v1.0";
        
        public MainWindow()
        {
            InitializeComponent();
            currentversionlabel.Text = versionlabeltext;
        }



       

        
        
        DispatcherTimer tim = new DispatcherTimer();

        private void pasteclipboardbutton_Click(object sender, RoutedEventArgs e)
        {
            if (Clipboard.ContainsText() is false)
            {
                MessageBox.Show("There is nothing in your clipboard", "Empty Clipboard", MessageBoxButton.OK, MessageBoxImage.Information);

            }
            else
                if (Clipboard.GetText().Contains(" ") is true)
            {
                MessageBox.Show("There are spaces in your clipboard, there should not be any spaces", "Empty Characters Detected", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                if (link.Text != "")
                {
                    MessageBoxResult cnfrm = MessageBox.Show("This will overwrite your link(s), are you okay with that?", "Overwrite Confirmation", MessageBoxButton.YesNo,MessageBoxImage.Warning);
                    if (cnfrm == MessageBoxResult.Yes)
                    {
                        link.Text = Clipboard.GetText();
                        pastedpopup.Visibility = Visibility.Visible;

                        tim.Tick += new EventHandler(hidepasted);
                        tim.Interval = new TimeSpan(0, 0, 2);
                        tim.Start();
                    }
                }
                else
                {
                    link.Text = Clipboard.GetText();
                    pastedpopup.Visibility = Visibility.Visible;

                    tim.Tick += new EventHandler(hidepasted);
                    tim.Interval = new TimeSpan(0, 0, 2);
                    tim.Start();
                }


            }
        }

        private void appendclipboardbutton_Click(object sender, RoutedEventArgs e)
        {
            if (Clipboard.ContainsText() is false)
            {
                MessageBox.Show("There is nothing in your clipboard", "Empty Clipboard", MessageBoxButton.OK, MessageBoxImage.Information);

            }
            else
            {
                if (Clipboard.GetText().Contains(" ") is true)
                {
                    MessageBox.Show("There are spaces in your clipboard, there should not be any spaces", "Empty Characters Detected", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    if (link.Text.Contains(Clipboard.GetText()) is true)
                    {
                        MessageBox.Show("You already have this link included!", "Duplicate Warning", MessageBoxButton.OK, MessageBoxImage.Information);
                    }

                    else
                    {
                        if (playlistcheckbox.IsChecked is true)
                        {
                            MessageBox.Show("You have the playlist checkbox checked. If you would like to download multiple playlists with specific ranges, do one at a time. This is to distinguish which playlist the range is intended for. If you would like to download multiple playlists in their entirety, uncheck the playlist checkbox and treat them as normal links", "Multiple Playlists", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                        else
                        {
                            link.AppendText(" " + Clipboard.GetText());
                            addedpopup.Visibility = Visibility.Visible;

                            tim.Tick += new EventHandler(hidepasted);
                            tim.Interval = new TimeSpan(0, 0, 1);
                            tim.Start();
                        }
                        
                    }
                }
            }
        }

        private void downloadlocationpathcopybutton_Click(object sender, RoutedEventArgs e)
        {
            if (Downloadlocation.Text == "")
            {
                MessageBox.Show("You must choose a download location first!", "Empty Dowlnload Location Field", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                Clipboard.SetText(Downloadlocation.Text);
                copiedpopupfordownloadpath.Visibility = Visibility.Visible;
                tim.Tick += new EventHandler(hidecopied);
                tim.Interval = new TimeSpan(0, 0, 1);
                tim.Start();
            }
        }
        private void DownloadLocationButton_Click(object sender, RoutedEventArgs e)
        {

            using (var fbd = new System.Windows.Forms.FolderBrowserDialog())
            {
                System.Windows.Forms.DialogResult result = fbd.ShowDialog();
                Downloadlocation.Text = fbd.SelectedPath;
            }


        }

        string convertfiletitle;
        string convertfiletitlefinale;
        private void ConvertexplorerButton_Click(object sender, RoutedEventArgs e)
        {

            using (var fbd = new System.Windows.Forms.OpenFileDialog())
            {
                fbd.Title = "Choose a file to convert";
                fbd.InitialDirectory = "C:\\";
                System.Windows.Forms.DialogResult result = fbd.ShowDialog();
                filechosen.Text = fbd.FileName;
                convertfiletitle = (System.IO.Path.GetFileNameWithoutExtension(fbd.FileName));
            }


        }

        private void execheckbox_Clicked(object sender, RoutedEventArgs e)
        {
            if (execheckbox.IsChecked is false)
            {
                exenamelabel.Visibility = Visibility.Hidden;
                exename.Visibility = Visibility.Hidden;
                exename.Text = "";

            }

            else
            {
                exename.Text = "";
                exenamelabel.Visibility = Visibility.Visible;
                exename.Visibility = Visibility.Visible;
            }
        }
        private void playlistcheckbox_Clicked(object sender, RoutedEventArgs e)

        {
            if (portioncheck.IsChecked is true & playlistcheckbox.IsChecked is true)
            {

                MessageBox.Show("You can't download a portion of one video that's in a playlist." + "\r\n" + "\r\n" + "Uncheck the 'portion' checkbox if this is a playlist", "Portion and Playlist Overlap", MessageBoxButton.OK, MessageBoxImage.Information);
                playlistcheckbox.IsChecked = false;

            }

            else
            {

                if (playlistcheckbox.IsChecked is false)
                {
                    plrange.Visibility = Visibility.Hidden;
                    plrangelabel.Visibility = Visibility.Hidden;
                    plAll.Visibility = Visibility.Hidden;
                    plrange.Text = "";
                    plAll.IsChecked = false;

                }

                else
                {
                    
                    if (link.Text.Contains(" ") is true)
                    {
                        MessageBox.Show("Only one playlist at a time. If you would like to specify a range of items to download from a playlist, you should do one playlist link at a time. This is to be able to distinguish which playlist the range applies to", "Multiple Playlists Error");
                        playlistcheckbox.IsChecked = false;
                    }
                    else
                    {
                        plrange.Text = "";
                        plAll.IsChecked = false;
                        plrange.Visibility = Visibility.Visible;
                        plrangelabel.Visibility = Visibility.Visible;
                        plAll.Visibility = Visibility.Visible;
                    }
                }

            }


        }

        private void plAll_Click(object sender, RoutedEventArgs e)
        {

            if (plAll.IsChecked is true)
            {
                plrange.Visibility = Visibility.Hidden;
                plrange.Text = "";

            }

            else
            {
                plrange.Text = "";
                plrange.Visibility = Visibility.Visible;


            }
        }




        private void setpath_Click(object sender, RoutedEventArgs e)
        {

            if (setpath.IsChecked is false)
            {
                dlpathborder.Visibility = Visibility.Hidden;
                pathdragfolderherelabel.Visibility = Visibility.Hidden;
                pathrectangle.Visibility = Visibility.Hidden;
                pathorlabel.Visibility = Visibility.Hidden;
                pathexplorerbutton.Visibility = Visibility.Hidden;
                pathlabel.Visibility = Visibility.Hidden;
                dlpath.Visibility = Visibility.Hidden;
                adddlpathbutton.Visibility = Visibility.Hidden;
                dlpath.Text = "";

            }

            else
            {

                dlpath.Text = "";
                adddlpathbutton.Visibility = Visibility.Visible;
                pathdragfolderherelabel.Visibility = Visibility.Visible;
                pathrectangle.Visibility = Visibility.Visible;
                pathorlabel.Visibility = Visibility.Visible;
                pathexplorerbutton.Visibility = Visibility.Visible;
                pathlabel.Visibility = Visibility.Visible;
                dlpath.Visibility = Visibility.Visible;
                dlpathborder.Visibility = Visibility.Visible;

            }

        }


        private void SetpathfileButton_Click(object sender, RoutedEventArgs e)
        {

            using (var fbd = new System.Windows.Forms.FolderBrowserDialog())
            {
                System.Windows.Forms.DialogResult result = fbd.ShowDialog();
                dlpath.Text = fbd.SelectedPath;
            }


        }

        private void portioncheck_Clicked(object sender, RoutedEventArgs e)

        {

            if (playlistcheckbox.IsChecked is true & portioncheck.IsChecked is true)
            {
                MessageBox.Show("You can't download a portion of one video that's in a playlist." + "\r\n" + "\r\n" + "Uncheck the 'playlist' checkbox  if this is an individual video that you would like to download a portion of", "Portion and Playlist Overlap", MessageBoxButton.OK, MessageBoxImage.Information);
                portioncheck.IsChecked = false;

            }

            else
            {


                if (portioncheck.IsChecked is false)
                {
                    nodashesfornumbers.Visibility = Visibility.Hidden;
                    greaterthan60.Visibility = Visibility.Hidden;
                    numbersonly.Visibility = Visibility.Hidden;
                    from.Visibility = Visibility.Hidden;
                    to.Visibility = Visibility.Hidden;
                    toh.Visibility = Visibility.Hidden;
                    tohlabel.Visibility = Visibility.Hidden;
                    tom.Visibility = Visibility.Hidden;
                    tomlabel.Visibility = Visibility.Hidden;
                    tos.Visibility = Visibility.Hidden;
                    toslabel.Visibility = Visibility.Hidden;
                    fromh.Visibility = Visibility.Hidden;
                    fromhlabel.Visibility = Visibility.Hidden;
                    fromm.Visibility = Visibility.Hidden;
                    frommlabel.Visibility = Visibility.Hidden;
                    froms.Visibility = Visibility.Hidden;
                    fromslabel.Visibility = Visibility.Hidden;
                    ToEnd.Visibility = Visibility.Hidden;
                    portionformatselection.Visibility = Visibility.Hidden;
                    portionformatselectionlabel.Visibility = Visibility.Hidden;
                    providetitleforportion.Visibility = Visibility.Hidden;
                    newportiontitle.Visibility = Visibility.Hidden;

                    toh.Text = "00";
                    tom.Text = "00";
                    tos.Text = "00";
                    fromh.Text = "00";
                    fromm.Text = "00";
                    froms.Text = "00";
                    ToEnd.IsChecked = false;
                    portionformatselection.SelectedItem = null;
                    newportiontitle.Text = "";
                   



                }

                else
                {
                    newportiontitle.Text = "";
                    toh.Text = "00";
                    tom.Text = "00";
                    tos.Text = "00";
                    fromh.Text = "00";
                    fromm.Text = "00";
                    froms.Text = "00";
                    portionformatselection.SelectedItem = null;
                    ToEnd.IsChecked = false;

                    from.Visibility = Visibility.Visible;
                    to.Visibility = Visibility.Visible;
                    toh.Visibility = Visibility.Visible;
                    tohlabel.Visibility = Visibility.Visible;
                    tom.Visibility = Visibility.Visible;
                    tomlabel.Visibility = Visibility.Visible;
                    tos.Visibility = Visibility.Visible;
                    toslabel.Visibility = Visibility.Visible;
                    fromh.Visibility = Visibility.Visible;
                    fromhlabel.Visibility = Visibility.Visible;
                    fromm.Visibility = Visibility.Visible;
                    frommlabel.Visibility = Visibility.Visible;
                    froms.Visibility = Visibility.Visible;
                    fromslabel.Visibility = Visibility.Visible;
                    ToEnd.Visibility = Visibility.Visible;
                    portionformatselection.Visibility = Visibility.Visible;
                    portionformatselectionlabel.Visibility = Visibility.Visible;
                    providetitleforportion.Visibility = Visibility.Visible;
                    newportiontitle.Visibility = Visibility.Visible;



                }
            }
        }


        private void trimconvercheckbox_Click(object sender, RoutedEventArgs e)
        {
            if (trimconvercheckbox.IsChecked is false)
            {
                //nodashesfornumbers.Visibility = Visibility.Hidden;
                //greaterthan60.Visibility = Visibility.Hidden;
                //numbersonly.Visibility = Visibility.Hidden;
                fromcv.Visibility = Visibility.Hidden;
                tocv.Visibility = Visibility.Hidden;
                tohcv.Visibility = Visibility.Hidden;
                tohlabelcv.Visibility = Visibility.Hidden;
                tomcv.Visibility = Visibility.Hidden;
                tomlabelcv.Visibility = Visibility.Hidden;
                toscv.Visibility = Visibility.Hidden;
                toslabelcv.Visibility = Visibility.Hidden;
                fromhcv.Visibility = Visibility.Hidden;
                fromhlabelcv.Visibility = Visibility.Hidden;
                frommcv.Visibility = Visibility.Hidden;
                frommlabelcv.Visibility = Visibility.Hidden;
                fromscv.Visibility = Visibility.Hidden;
                fromslabelcv.Visibility = Visibility.Hidden;
                toendcvcheckbox.Visibility = Visibility.Hidden;
                

                tohcv.Text = "00";
                tomcv.Text = "00";
                toscv.Text = "00";
                fromhcv.Text = "00";
                frommcv.Text = "00";
                fromscv.Text = "00";
                toendcvcheckbox.IsChecked = false;
                




            }

            else
            {
                
                tohcv.Text = "00";
                tomcv.Text = "00";
                toscv.Text = "00";
                fromhcv.Text = "00";
                frommcv.Text = "00";
                fromscv.Text = "00";
                toendcvcheckbox.IsChecked = false;

                fromcv.Visibility = Visibility.Visible;
                tocv.Visibility = Visibility.Visible;
                tohcv.Visibility = Visibility.Visible;
                tohlabelcv.Visibility = Visibility.Visible;
                tomcv.Visibility = Visibility.Visible;
                tomlabelcv.Visibility = Visibility.Visible;
                toscv.Visibility = Visibility.Visible;
                toslabelcv.Visibility = Visibility.Visible;
                fromhcv.Visibility = Visibility.Visible;
                fromhlabelcv.Visibility = Visibility.Visible;
                frommcv.Visibility = Visibility.Visible;
                frommlabelcv.Visibility = Visibility.Visible;
                fromscv.Visibility = Visibility.Visible;
                fromslabelcv.Visibility = Visibility.Visible;
                toendcvcheckbox.Visibility = Visibility.Visible;
                



            }
        }

        private void specifycode_Click(object sender, RoutedEventArgs e)
        {

            if (specifycode.IsChecked is true)
            {

                formatcode.Text = "000";
                formatcode.Visibility = Visibility.Visible;
            }

            else
            {
                formatcode.Visibility = Visibility.Hidden;
                numbersonlyforcode.Visibility = Visibility.Hidden;
                nodashesforcode.Visibility = Visibility.Hidden;
                formatcode.Text = "000";

            }

        }

        private void Clear(object sender, DependencyPropertyChangedEventArgs e)
        {

            TextBox bux = (TextBox)sender;

            if (bux.IsKeyboardFocusWithin is true)
            {

                bux.Text = "";
            }
            else
            {
                if (bux.Text == "" || bux.Text == "0")
                {

                    numbersonly.Visibility = Visibility.Hidden;
                    nodashesfornumbers.Visibility = Visibility.Hidden;
                    bux.Text = "00";

                }

                else
                {
                    if (bux.Text.Contains("-") is true)
                    {
                        nodashesfornumbers.Visibility = Visibility.Visible;
                        numbersonly.Visibility = Visibility.Hidden;
                        bux.Focus();
                    }

                    else
                    {
                        int output;
                        if (int.TryParse(bux.Text, out output))
                        {
                            numbersonly.Visibility = Visibility.Hidden;
                            nodashesfornumbers.Visibility = Visibility.Hidden;
                        }

                        else
                        {
                            bux.Focus();
                            numbersonly.Visibility = Visibility.Visible;
                            nodashesfornumbers.Visibility = Visibility.Hidden;
                        }
                    }

                }
            }
        }


        private void Clearcv(object sender, DependencyPropertyChangedEventArgs e)
        {
            TextBox bux = (TextBox)sender;

            if (bux.IsKeyboardFocusWithin is true)
            {

                bux.Text = "";
            }
            else
            {
                if (bux.Text == "" || bux.Text == "0")
                {

                    numbersonlycv.Visibility = Visibility.Hidden;
                    nodashescv.Visibility = Visibility.Hidden;
                    bux.Text = "00";

                }

                else
                {
                    if (bux.Text.Contains("-") is true)
                    {
                        nodashescv.Visibility = Visibility.Visible;
                        numbersonlycv.Visibility = Visibility.Hidden;
                        bux.Focus();
                    }

                    else
                    {
                        int output;
                        if (int.TryParse(bux.Text, out output))
                        {
                            numbersonlycv.Visibility = Visibility.Hidden;
                            nodashescv.Visibility = Visibility.Hidden;
                        }

                        else
                        {
                            bux.Focus();
                            numbersonlycv.Visibility = Visibility.Visible;
                            nodashescv.Visibility = Visibility.Hidden;
                        }
                    }

                }
            }
        }

        private void Clearcvforms(object sender, DependencyPropertyChangedEventArgs e)
        {
            TextBox bux = (TextBox)sender;

            if (bux.IsKeyboardFocusWithin is true)
            {

                bux.Text = "";
            }
            else
            {
                if (bux.Text == "" || bux.Text == "0")
                {
                    numbersonlycv.Visibility = Visibility.Hidden;
                    nodashescv.Visibility = Visibility.Hidden;
                    bux.Text = "00";
                }

                else
                {
                    if (bux.Text.Contains("-") is true)
                    {
                        nodashescv.Visibility = Visibility.Visible;
                        numbersonlycv.Visibility = Visibility.Hidden;
                        bux.Focus();
                    }

                    else
                    {
                        int output;
                        if (int.TryParse(bux.Text, out output))
                        {
                            numbersonlycv.Visibility = Visibility.Hidden;
                            greaterthan60cv.Visibility = Visibility.Hidden;
                            nodashescv.Visibility = Visibility.Hidden;
                            if (output >= 60)
                            {
                                greaterthan60cv.Visibility = Visibility.Visible;
                                bux.Focus();
                            }
                        }

                        else
                        {
                            bux.Focus();
                            numbersonlycv.Visibility = Visibility.Visible;
                            nodashescv.Visibility = Visibility.Hidden;
                            greaterthan60cv.Visibility = Visibility.Hidden;
                        }
                    }

                }
            }
        }

        private void Clearforcode(object sender, DependencyPropertyChangedEventArgs e)
        {

            TextBox bux = (TextBox)sender;

            if (bux.IsKeyboardFocusWithin is true)
            {

                bux.Text = "";
            }
            else
            {
                if (bux.Text == "" || bux.Text == "0")
                {

                    numbersonlyforcode.Visibility = Visibility.Hidden;
                    nodashesforcode.Visibility = Visibility.Hidden;
                    threedigits.Visibility = Visibility.Hidden;
                    bux.Text = "00";

                }

                else
                {

                    if (bux.Text.Contains("-") is true)
                    {
                        nodashesforcode.Visibility = Visibility.Visible;
                        numbersonlyforcode.Visibility = Visibility.Hidden;
                        threedigits.Visibility = Visibility.Hidden;
                        bux.Focus();
                    }

                    else
                    {
                        int output;
                        if (int.TryParse(bux.Text, out output))
                        {
                            numbersonlyforcode.Visibility = Visibility.Hidden;
                            nodashesforcode.Visibility = Visibility.Hidden;
                            if (output < 0)
                            {
                                numbersonlyforcode.Visibility = Visibility.Visible;
                                nodashesforcode.Visibility = Visibility.Hidden;
                                threedigits.Visibility = Visibility.Hidden;
                                bux.Focus();
                            }
                            if (output < 100)
                            {
                                numbersonlyforcode.Visibility = Visibility.Hidden;
                                nodashesforcode.Visibility = Visibility.Hidden;
                                threedigits.Visibility = Visibility.Visible;
                                bux.Focus();
                            }
                        }

                        else
                        {
                            bux.Focus();
                            numbersonlyforcode.Visibility = Visibility.Visible;
                            nodashesforcode.Visibility = Visibility.Hidden;
                            threedigits.Visibility = Visibility.Hidden;
                        }
                    }

                }
            }
        }


        private void Clearforminutesandseconds(object sender, DependencyPropertyChangedEventArgs e)
        {

            TextBox bux = (TextBox)sender;

            if (bux.IsKeyboardFocusWithin is true)
            {

                bux.Text = "";
            }
            else
            {
                if (bux.Text == "" || bux.Text == "0")
                {
                    numbersonly.Visibility = Visibility.Hidden;
                    nodashesfornumbers.Visibility = Visibility.Hidden;
                    bux.Text = "00";
                }

                else
                {
                    if (bux.Text.Contains("-") is true)
                    {
                        nodashesfornumbers.Visibility = Visibility.Visible;
                        numbersonly.Visibility = Visibility.Hidden;
                        bux.Focus();
                    }

                    else
                    {
                        int output;
                        if (int.TryParse(bux.Text, out output))
                        {
                            numbersonly.Visibility = Visibility.Hidden;
                            greaterthan60.Visibility = Visibility.Hidden;
                            nodashesfornumbers.Visibility = Visibility.Hidden;
                            if (output >= 60)
                            {
                                greaterthan60.Visibility = Visibility.Visible;
                                bux.Focus();
                            }
                        }

                        else
                        {
                            bux.Focus();
                            numbersonly.Visibility = Visibility.Visible;
                            nodashesfornumbers.Visibility = Visibility.Hidden;
                            greaterthan60.Visibility = Visibility.Hidden;
                        }
                    }

                }
            }
        }

        private void Keyupy(object sender, KeyEventArgs e)
        {
            TextBox bux = (TextBox)sender;
            if (e.Key == Key.Enter)
            {
                e.Handled = true;
                bux.MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
            }

        }

        private void Keyupyforcode(object sender, KeyEventArgs e)
        {
            TextBox bux = (TextBox)sender;
            if (e.Key == Key.Enter)
            {
                e.Handled = true;
                bux.MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
            }

        }

        private void ToEnd_Click(object sender, RoutedEventArgs e)
        {

            if (ToEnd.IsChecked is true)
            {

                to.Visibility = Visibility.Hidden;
                toh.Visibility = Visibility.Hidden;
                tohlabel.Visibility = Visibility.Hidden;
                tom.Visibility = Visibility.Hidden;
                tomlabel.Visibility = Visibility.Hidden;
                tos.Visibility = Visibility.Hidden;
                toslabel.Visibility = Visibility.Hidden;
                toh.Text = "00";
                tom.Text = "00";
                tos.Text = "00";

            }

            else
            {

                to.Visibility = Visibility.Visible;
                toh.Visibility = Visibility.Visible;
                tohlabel.Visibility = Visibility.Visible;
                tom.Visibility = Visibility.Visible;
                tomlabel.Visibility = Visibility.Visible;
                tos.Visibility = Visibility.Visible;
                toslabel.Visibility = Visibility.Visible;


            }

        }



        private void toendcvcheckbox_Click(object sender, RoutedEventArgs e)
        {
            if (toendcvcheckbox.IsChecked is true)
            {

                tocv.Visibility = Visibility.Hidden;
                tohcv.Visibility = Visibility.Hidden;
                tohlabelcv.Visibility = Visibility.Hidden;
                tomcv.Visibility = Visibility.Hidden;
                tomlabelcv.Visibility = Visibility.Hidden;
                toscv.Visibility = Visibility.Hidden;
                toslabelcv.Visibility = Visibility.Hidden;
                tohcv.Text = "00";
                tomcv.Text = "00";
                toscv.Text = "00";

            }

            else
            {

                tocv.Visibility = Visibility.Visible;
                tohcv.Visibility = Visibility.Visible;
                tohlabelcv.Visibility = Visibility.Visible;
                tomcv.Visibility = Visibility.Visible;
                tomlabelcv.Visibility = Visibility.Visible;
                toscv.Visibility = Visibility.Visible;
                toslabelcv.Visibility = Visibility.Visible;


            }
        }


        string gate = "reset";
        string name = "youtube-dl.exe ";
        string playlistprereq = "";
        string thumbnailprereq = "";
        string subtitlesprereq = "";
        string formatprereq = "";
        string downloadloc = "";
        string alwaysthereprereq;
        string tosequenceforportion;
        string formatforportion;
        string fetchedurl;
        string ffmpegpresetspeed = " -preset veryfast -speed 6 ";


        int Queue = 0;


        private async void Download_Click(object sender, RoutedEventArgs e)

        {
            playlistprereq = "";
            thumbnailprereq = "";
            subtitlesprereq = "";
            name = "youtube-dl.exe ";
            formatprereq = "";
            alwaysthereprereq = "-ciw --no-mtime";
            tosequenceforportion = "";
            fetchedurl = "";

            gate = "reset";

            if (portioncheck.IsChecked is true)
            {

                

                if (fromh.Text.Contains("00") && fromm.Text.Contains("00") && froms.Text.Contains("00") && toh.Text.Contains("00") && tom.Text.Contains("00") && tos.Text.Contains("00"))
                {
                    MessageBox.Show("You did not include a 'from' or 'to' time signature." + "\r\n" + "\r\n" + "If you intend to download the video in its entirety please uncheck the portion checkbox");
                    gate = "nopass";
                }
                else
                {
                    if (fromh.Text.Contains("00") && fromm.Text.Contains("00") && froms.Text.Contains("00"))
                    {
                        MessageBox.Show("You did not include a 'from' time signature." + "\r\n" + "\r\n" + "If you intend to download the video in its entirety please uncheck the portion checkbox");
                        gate = "nopass";
                    }
                    else
                    {
                        if (toh.Text.Contains("00") && tom.Text.Contains("00") && tos.Text.Contains("00") && ToEnd.IsChecked is false)
                        {
                            MessageBox.Show("You did not include a 'to' time signature." + "\r\n" + "\r\n" + "If you intend to download the video to the end please check the 'To End' checkbox");
                            gate = "nopass";
                        }
                        else
                        {
                            if (newportiontitle.Text == "")
                            {
                                MessageBox.Show("Please provide a title to name the new file.", "No Title Detected");
                                gate = "nopass";
                            }
                            else
                            { 
                                if (ToEnd.IsChecked is false)
                                {
                                    int fromhour;
                                    int fromminute;
                                    int fromsecond;
                                    int tohour;
                                    int tominute;
                                    int tosecond;
                                    int secondcarryover;
                                    int minutecarryover;
                                    int seconds;
                                    int minutes;
                                    int hours;
                                    int.TryParse(fromh.Text, out fromhour);
                                    int.TryParse(fromm.Text, out fromminute);
                                    int.TryParse(froms.Text, out fromsecond);
                                    int.TryParse(toh.Text, out tohour);
                                    int.TryParse(tom.Text, out tominute);
                                    int.TryParse(tos.Text, out tosecond);


                                   

                                    if (fromhour < tohour)
                                        {
                                            gate = "ffmpegportion";
                                        }

                                    if (fromhour > tohour)
                                    {
                                        MessageBox.Show("You cannot process videos backwards, please choose a 'from' time that is before the 'to' time.", "Improper Time Direction Detected", MessageBoxButton.OK, MessageBoxImage.Error);
                                        gate = "nopass";
                                    }


                                    if (fromhour == tohour)
                                    {
                                        if (fromminute > tominute)
                                        {
                                            MessageBox.Show("You cannot process videos backwards, please choose a 'from' time that is before the 'to' time.", "Improper Time Direction Detected", MessageBoxButton.OK, MessageBoxImage.Error);
                                            gate = "nopass";
                                        }
                                        else
                                        {
                                                if (fromminute == tominute)
                                                {
                                                    if (fromsecond > tosecond)
                                                    {
                                                        MessageBox.Show("You cannot process videos backwards, please choose a 'from' time that is before the 'to' time.", "Improper Time Direction Detected", MessageBoxButton.OK, MessageBoxImage.Error);
                                                        gate = "nopass";
                                                    }
                                                    else
                                                    {
                                                        if (fromsecond == tosecond)
                                                        {
                                                            MessageBox.Show("You chose the same timestamps, please choose a 'from' time that is before the 'to' time.", "Improper Time Input Detected", MessageBoxButton.OK, MessageBoxImage.Error);
                                                            gate = "nopass";
                                                        }
                                                        else
                                                        {
                                                                gate = "ffmpegportion";
                                                        }

                                                    }
                                                } 
                                                    else
                                                    {
                                                        gate = "ffmpegportion";
                                                    }
                                            
                                        }
                                    }

                                    if (gate == "ffmpegportion")
                                    {


                                        if (tosecond - fromsecond < 0)
                                        {
                                            secondcarryover = 1;
                                            seconds = 60 - (fromsecond - tosecond);
                                        }
                                        else
                                        {
                                            secondcarryover = 0;
                                            seconds = (tosecond - fromsecond);
                                        }

                                        if (tominute - fromminute < 0)
                                        {
                                            minutecarryover = 1;
                                            minutes = (60 - (fromminute - tominute)) - secondcarryover;
                                        }
                                        else
                                        {
                                            minutecarryover = 0;
                                            minutes = (tominute - fromminute) - secondcarryover;
                                        }

                                        hours = (tohour - fromhour) - minutecarryover;

                                        tosequenceforportion = "-t " + hours + ":" + minutes + ":" + seconds;
                                    }

                                } 
                                else
                                {
                                    tosequenceforportion = "";
                                    gate = "ffmpegportion";
                                }
                                
                                
                            }

                        }
                    }
                }
            }

            if (Downloadlocation.Text == "")
            {
                MessageBox.Show("You did not choose a Download Location", "No Download Location Detected");
                gate = "nopass";
            }

            if (subtitles.IsChecked is true)
            {
                subtitlesprereq = " --write-sub";
            }

            if (thumbnail.IsChecked is true)
            {
                thumbnailprereq = " --embed-thumbnail";
            }

            if (prioritizehighestquality.IsChecked is true)
            {
                formatprereq = "";
            }

            else
            {
                formatprereq = " -f best";
            }


            if (setpath.IsChecked is true)
            {
                if (dlpath.Text == "")
                {
                    MessageBox.Show("You have set path checked but you did not set the path. If it's in your system path already, please uncheck the 'set path' checkbox", "Empty Path Field", MessageBoxButton.OK, MessageBoxImage.Information);
                    gate = "nopass";
                }
                
            }

            if (playlistcheckbox.IsChecked is true)
            {
                if (plrange.Text == "" && plAll.IsChecked is false)
                {
                    MessageBox.Show("You did not include the range of the playlist items." + "\r\n" + "\r\n" + "If you wanted items 4, 7 and 11 through 17, for example, you would write 4,7,11-17." + "\r\n" + "\r\n" + "If you want to download the entire playlist please check the 'All' checkbox", "Empty Range Field", MessageBoxButton.OK, MessageBoxImage.Information);
                    gate = "nopass";
                }

                else
                {

                    if (plrange.Text.Contains(" ") is true)
                    {
                        MessageBox.Show("Remove all spaces from the playlist range", "Empty Characters Detected", MessageBoxButton.OK, MessageBoxImage.Information);
                        gate = "nopass";
                    }
                    else
                    {
                        if (plAll.IsChecked is false)
                        {
                            playlistprereq = " --playlist-items " + plrange.Text;
                            formatprereq = " -f 140";
                        }
                        else
                        {
                            formatprereq = " -f 140";
                        }
                    }
                }
            }

            if (link.Text.Length == 0 || link.Text.Contains(" ") is true)
            {
                MessageBox.Show("You have not pasted any valid links", "Empty Link(s) Field", MessageBoxButton.OK, MessageBoxImage.Information);
                gate = "nopass";
            }
            else

            {

                if (execheckbox.IsChecked is true)
                {
                    if (exename.Text == "")
                    {
                        MessageBox.Show("You did not include the name you changed your youtube-dl exe file to." + "\r\n" + "\r\n" + "If you did not change it and kept it as 'youtube-dl', please uncheck the 'I changed the name' checkbox", "Empty Name Field", MessageBoxButton.OK, MessageBoxImage.Information);
                    }

                    else
                    {
                        if (exename.Text.Contains(".exe") is true)
                        {
                            MessageBox.Show("Do not include .exe in the name", "Exe Input Detected", MessageBoxButton.OK, MessageBoxImage.Information);
                            gate = "nopass";
                        }

                        else
                        {
                            if (exename.Text.Contains(" ") is true)
                            {
                                name = "\"" + exename.Text + "\"" + ".exe ";
                            }
                            else
                            {
                                name = exename.Text + ".exe ";
                            }
                        }

                    }
                }

            }

            if (portioncheck.IsChecked is true)
            {

                if (portionformatselection.SelectedValue == null)
                {
                    MessageBox.Show("You did not select a format.", "No Format Selection Detected", MessageBoxButton.OK, MessageBoxImage.Error);
                    gate = "nopass";
                }
            }

            if (gate == "ffmpegportion")
            {
                if (portionformatselection.SelectedIndex == 0)
                {
                    formatforportion = ".mp4";
                }
                if (portionformatselection.SelectedIndex == 1)
                {
                    formatforportion = ".mp3";
                }
                if (portionformatselection.SelectedIndex == 2)
                {
                    formatforportion = ".m4a";
                }
                if (portionformatselection.SelectedIndex == 3)
                {
                    formatforportion = ".webm";
                }
                if (portionformatselection.SelectedIndex == 4)
                {
                    formatforportion = ".avi";
                }
                if (portionformatselection.SelectedIndex == 5)
                {
                    formatforportion = ".mpeg";
                }
                if (portionformatselection.SelectedIndex == 6)
                {
                    formatforportion = ".mka";
                }
                if (portionformatselection.SelectedIndex == 7)
                {
                    formatforportion = ".mks";
                }
                if (portionformatselection.SelectedIndex == 8)
                {
                    formatforportion = ".mkv";
                }
                if (portionformatselection.SelectedIndex == 9)
                {
                    formatforportion = ".flv";
                }
                if (portionformatselection.SelectedIndex == 10)
                {
                    formatforportion = ".h264";
                }

            }
          
            

            if (specifycode.IsChecked is true)
            {
                formatprereq = " -f " + formatcode.Text;
                
            }

            if (link.Text.Contains("&t=") is true)
            {
                MessageBox.Show("Timestamps with ampersands (&) are not allowed, please remove it from your link (should be denoted with t=#)","Timestamp Detected",MessageBoxButton.OK,MessageBoxImage.Information);
                gate = "nopass";
            }

            if (gate == "reset")
            {
                downloadloc = " -o " + "\"" + Downloadlocation.Text + "\\%(title)s.%(ext)s" + "\" ";
                await RunAsync();
                
               
            }

            if (gate == "ffmpegportion")
            {
                await ffmpegportion();
            }
        }



        internal async Task RunAsync()

        {

            if (Queue == 0)
            {
                Queue = 1;
                processingpopup.Visibility = Visibility.Visible;
                abortbutton.Visibility = Visibility.Visible;

                output.AppendText(name + alwaysthereprereq + subtitlesprereq + thumbnailprereq + formatprereq + playlistprereq + downloadloc + link.Text + "\r\n" + "\r\n" + "Processing . . ." + "\r\n" + "\r\n");

                ProcessStartInfo outie = new ProcessStartInfo("cmd");
                outie.UseShellExecute = false;
                outie.RedirectStandardOutput = true;
                outie.RedirectStandardInput = true;
                outie.RedirectStandardError = true;
                outie.CreateNoWindow = true;
                var proc = Process.Start(outie);
                proc.StandardInput.WriteLine(name + alwaysthereprereq + subtitlesprereq + thumbnailprereq + formatprereq + playlistprereq + downloadloc + link.Text);
                output.ScrollToEnd();

                link.Text = "";
                proc.StandardInput.Flush();
                proc.StandardInput.Close();


                string line;
                string err;
                err = "";

                await Task.Run(() =>
                    {
                        Queue = 1;
                        while (proc.StandardOutput.EndOfStream is false)
                        {
                           
                            
                            try
                            {
                                this.Dispatcher.Invoke(() => output.AppendText(proc.StandardOutput.ReadLine() + "\r\n"));
                                this.Dispatcher.Invoke(() => output.ScrollToEnd());


                                if ((line = proc.StandardOutput.ReadLine()) != null)
                                {
                                    this.Dispatcher.Invoke(() => output.AppendText(proc.StandardOutput.ReadLine() + "\r\n"));
                                    this.Dispatcher.Invoke(() => output.ScrollToEnd());
                                }



                            }
                            catch (Exception)
                            {
                                proc.Kill();
                            }
                        }

                        if (proc.StandardError.EndOfStream is false)
                        {
                            err = proc.StandardError.ReadToEnd();
                        }


                    });

                if (err == "")
                {
                    output.ScrollToEnd();
                    Queue = 0;
                    processingpopup.Visibility = Visibility.Hidden;
                    abortbutton.Visibility = Visibility.Hidden;

                    this.Dispatcher.Invoke(() => output.AppendText("\r\n" + "\r\n" + "Completed with no errors" + "\r\n" + "\r\n"));
                }
                else
                {
                    if (err.Contains("is not recognized") is true)
                    {
                        this.Dispatcher.Invoke(() => output.AppendText("\r\n" + "\r\n" + name + "is not recognized as an internal or external command, operable program or batch file. Either add its location to your system variables manually or use this program to do so" + "\r\n" + "\r\n"));
                        MessageBox.Show(name + "was not recongnized by your system. This is because it is not in your system's path variables and you are not using the program from within the same directory as the program. This can also be because you simply do not have it downloaded. If you do have it installed but not in your system path, you can click the 'add path' checkbox to add the program from here if you would like to", "System Does Not Recognize Program", MessageBoxButton.OK, MessageBoxImage.Information);
                        output.ScrollToEnd();
                        Queue = 0;
                        processingpopup.Visibility = Visibility.Hidden;
                        abortbutton.Visibility = Visibility.Hidden;
                    }
                    else
                    {
                        output.AppendText(err);
                        output.ScrollToEnd();
                        Queue = 0;
                        processingpopup.Visibility = Visibility.Hidden;
                        abortbutton.Visibility = Visibility.Hidden;


                    }
                }


            }
            else
            {
                MessageBox.Show("Something is in queue", "Execution in Progress", MessageBoxButton.OK, MessageBoxImage.Information);
            }



        }


        internal async Task ffmpegportion()
        {
            if (Queue == 0)
            {
                Queue = 1;
                processingpopup.Visibility = Visibility.Visible;
                abortbutton.Visibility = Visibility.Visible;



                ProcessStartInfo outie = new ProcessStartInfo("cmd");
                outie.UseShellExecute = false;
                outie.RedirectStandardOutput = true;
                outie.RedirectStandardInput = true;
                outie.RedirectStandardError = true;
                outie.CreateNoWindow = true;
                var proc = Process.Start(outie);
                proc.StandardInput.WriteLine(name + "-f best -g " + link.Text);
                output.ScrollToEnd();
                proc.StandardInput.Flush();
                proc.StandardInput.Close();

                string line;
                fetchedurl = "";
                string err;
                err = "";

                await Task.Run(() =>
                {
                    Queue = 1;

                    this.Dispatcher.Invoke(() => output.AppendText(name + "-f best -g " + link.Text + "\r\n" + "\r\n" + "Processing . . ." + "\r\n" + "\r\n"));
                    this.Dispatcher.Invoke(() => output.ScrollToEnd());

                    while (proc.StandardOutput.EndOfStream is false)
                    {


                        try
                        {
                            line = proc.StandardOutput.ReadLine();

                            if (line.Length > 200)
                            {
                                fetchedurl = line;
                            }

                        }
                        catch (Exception)
                        {
                            proc.Kill();
                        }
                    }

                    if (proc.StandardError.EndOfStream is false)
                    {
                        err = proc.StandardError.ReadToEnd();
                    }

                });


                if (err == "")
                {

                    if (fetchedurl == "")
                    {
                        
                        MessageBox.Show("A url for media extraction from a direct link was unable to be fetched, please try a different link", "Could Not Extract Link", MessageBoxButton.OK, MessageBoxImage.Information);
                        output.ScrollToEnd();
                    } 
                    
                    else
                    {
                        output.ScrollToEnd();
                        await ffmpegportioncontinued();
                    }
                    
                }
                else
                {
                    if (err.Contains("is not recognized") is true)
                    {
                        this.Dispatcher.Invoke(() => output.AppendText("\r\n" + "\r\n" + name + "is not recognized as an internal or external command, operable program or batch file. Either add its location to your system variables manually or use this program to do so" + "\r\n" + "\r\n"));
                        MessageBox.Show(name + "was not recongnized by your system. This is because it is not in your system's path variables and you are not using the program from within the same directory as the program. This can also be because you simply do not have it downloaded. If you do have it installed but not in your system path, you can click the 'add path' checkbox to add the program from here if you would like to", "System Does Not Recognize Program", MessageBoxButton.OK, MessageBoxImage.Information);
                        output.ScrollToEnd();
                        Queue = 0;
                        processingpopup.Visibility = Visibility.Hidden;
                        abortbutton.Visibility = Visibility.Hidden;
                    }
                    else
                    {
                        output.AppendText(err);
                        output.ScrollToEnd();
                        Queue = 0;
                        processingpopup.Visibility = Visibility.Hidden;
                        abortbutton.Visibility = Visibility.Hidden;


                    }
                }

                

            }
            else
            {
                MessageBox.Show("Something is in queue", "Execution in Progress", MessageBoxButton.OK, MessageBoxImage.Information);
            }

        }

        internal async Task ffmpegportioncontinued()
        {


            
            ProcessStartInfo outie = new ProcessStartInfo("cmd");
            outie.UseShellExecute = false;
            outie.RedirectStandardOutput = true;
            outie.RedirectStandardInput = true;
            outie.RedirectStandardError = true;
            outie.CreateNoWindow = true;
            var proc = Process.Start(outie);
            proc.StandardInput.WriteLine("ffmpeg " + "-ss " + fromh.Text + ":" + fromm.Text + ":" + froms.Text + " -i " + "\"" + fetchedurl + "\" " + ffmpegpresetspeed + tosequenceforportion + " \"" + Downloadlocation.Text + "\\" + newportiontitle.Text + formatforportion + "\"");
            output.ScrollToEnd();
            link.Text = "";
            proc.StandardInput.Flush();
            proc.StandardInput.Close();

            string line;
            string err;
            err = "";
            await Task.Run(() =>
            {
                Queue = 1;
                this.Dispatcher.Invoke(() => output.AppendText("ffmpeg " + "-ss " + fromh.Text + ":" + fromm.Text + ":" + froms.Text + " -i " + "\"" + fetchedurl + "\" " + ffmpegpresetspeed + tosequenceforportion + " \"" + Downloadlocation.Text + "\\" + newportiontitle.Text + formatforportion + "\"" + "\r\n" + "\r\n" + "Processing . . ." + "\r\n" + "\r\n"));
                this.Dispatcher.Invoke(() => output.ScrollToEnd());
                while (proc.StandardOutput.EndOfStream is false)
                {
                    while ((line = proc.StandardError.ReadLine()) != null)
                    {
                        try
                        {
                            err = proc.StandardError.ReadLine();

                            this.Dispatcher.Invoke(() => output.AppendText(proc.StandardError.ReadLine() + "\r\n"));
                            this.Dispatcher.Invoke(() => output.ScrollToEnd());

                        }
                        catch (Exception)
                        {
                            proc.Kill();
                        }
                    }

                    try
                    {
                        this.Dispatcher.Invoke(() => output.AppendText("\r\n" + proc.StandardOutput.ReadLine() + "\r\n" + "\r\n"));
                        this.Dispatcher.Invoke(() => output.ScrollToEnd());

                    }
                    catch (Exception)
                    {
                        proc.Kill();
                    }
                }


               

            });



            if (err != null)
            {
                if (err.Contains("operable program") is true)
                {
                    this.Dispatcher.Invoke(() => output.AppendText("\r\n" + "\r\n" + "FFmpeg is not recognized as an internal or external command, operable program or batch file. Either add its location to your system variables manually or use this program to do so" + "\r\n" + "\r\n"));
                    output.ScrollToEnd();
                    MessageBox.Show("Ffmpeg was not recognized by your system. This is because it is not in your system's path variables and you are not using the program from within the same directory as the program. This can also be because you simply do not have it downloaded. If you do have it installed but not in your system path, you can click the 'add path' checkbox to add the program from here if you would like to", "System Does Not Recognize Program", MessageBoxButton.OK, MessageBoxImage.Information);
                    Queue = 0;
                    processingpopup.Visibility = Visibility.Hidden;
                    abortbutton.Visibility = Visibility.Hidden;
                    portioncheck.IsChecked = false;
                }

                output.ScrollToEnd();
                Queue = 0;
                processingpopup.Visibility = Visibility.Hidden;
                abortbutton.Visibility = Visibility.Hidden;
                portioncheck.IsChecked = false;
            }

            
            else
            {
                this.Dispatcher.Invoke(() => output.AppendText("\r\n" + "\r\n" + "Completed" + "\r\n" + "\r\n"));
                output.ScrollToEnd();
                Queue = 0;
                processingpopup.Visibility = Visibility.Hidden;
                abortbutton.Visibility = Visibility.Hidden;
                portioncheck.IsChecked = false;
            }

            link.Text = "";

            
        }
        private void copycommandbutton_Click(object sender, RoutedEventArgs e)
        {

            

            playlistprereq = "";
            thumbnailprereq = "";
            subtitlesprereq = "";
            name = "youtube-dl.exe ";
            formatprereq = "";
            alwaysthereprereq = " -ciw --no-mtime";

            gate = "reset";

            if (subtitles.IsChecked is true)
            {
                subtitlesprereq = " --write-sub";
            }

            if (thumbnail.IsChecked is true)
            {
                thumbnailprereq = " --embed-thumbnail";
            }

            if (prioritizehighestquality.IsChecked is true)
            {
                formatprereq = "";
            }

            else
            {
                formatprereq = " -f best";
            }


            if (setpath.IsChecked is true)
            {
                if (dlpath.Text == "")
                {
                    MessageBox.Show("You have set path checked but you did not set the path. If it's in your system path already, please uncheck the 'set path' checkbox", "Empty Path Field", MessageBoxButton.OK, MessageBoxImage.Information);
                    gate = "nopass";
                }

            }


            if (playlistcheckbox.IsChecked is true)
            {
                if (plrange.Text == "" && plAll.IsChecked is false)
                {
                    MessageBox.Show("You did not include the range of the playlist items." + "\r\n" + "\r\n" + "If you wanted items 4, 7 and 11 through 17, for example, you would write 4,7,11-17." + "\r\n" + "\r\n" + "If you want to download the entire playlist please check the 'All' checkbox", "Empty Range Field", MessageBoxButton.OK, MessageBoxImage.Information);
                    gate = "nopass";
                }

                else
                {

                    if (plrange.Text.Contains(" ") is true)
                    {
                        MessageBox.Show("Remove all spaces from the playlist range", "Empty Characters Detected", MessageBoxButton.OK, MessageBoxImage.Information);
                        gate = "nopass";
                    }
                    else
                    {
                        if (plAll.IsChecked is false)
                        {
                            playlistprereq = " --playlist-items " + plrange.Text;
                            formatprereq = " -f 140";
                        }
                        else
                        {
                            formatprereq = " -f 140";
                        }
                    }
                }
            }



            if (portioncheck.IsChecked is true)
            {
                gate = "ffmpegportion";
            }

            if (link.Text.Length == 0 || link.Text.Contains(" ") is true)
            {
                MessageBox.Show("You have not pasted any valid links", "Empty Link or Spaces in Link Detected", MessageBoxButton.OK, MessageBoxImage.Information);
                gate = "nopass";
            }
            else

            {

                if (execheckbox.IsChecked is true)
                {
                    if (exename.Text == "")
                    {
                        MessageBox.Show("You did not include the name you changed your youtube-dl exe file to." + "\r\n" + "\r\n" + "If you did not change it and kept it as 'youtube-dl', please uncheck the 'I changed the name' checkbox", "Empty Name Field", MessageBoxButton.OK, MessageBoxImage.Information);
                        gate = "nopass";
                    }

                    else
                    {
                        if (exename.Text.Contains(".exe") is true)
                        {
                            MessageBox.Show("Do not include .exe in the name", "Exe Input Detected", MessageBoxButton.OK, MessageBoxImage.Information);
                            gate = "nopass";
                        }

                        else
                        {
                            if (exename.Text.Contains(" ") is true)
                            {
                                name = "\"" + exename.Text + "\"" + ".exe ";
                            }
                            else
                            {
                                name = exename.Text + ".exe ";
                            }
                        }

                    }
                }

            }

            if (specifycode.IsChecked is true)
            {
                formatprereq = " -f " + formatcode.Text;
            }

            if (gate == "reset")
            {
                if (Downloadlocation.Text == "")
                {
                    MessageBox.Show("You did not choose a Download Location", "Empty Download Location Field", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    downloadloc = " -o " + "\"" + Downloadlocation.Text + "\\%(title)s.%(ext)s" + "\" ";
                   
                    
                    Clipboard.SetText(name + alwaysthereprereq + subtitlesprereq + thumbnailprereq + formatprereq + playlistprereq + downloadloc + link.Text);
                    
                    copiedpopupcopycommand.Visibility = Visibility.Visible;

                    tim.Tick += new EventHandler(hidecopied);
                    tim.Interval = new TimeSpan(0, 0, 1);
                    tim.Start();
                }


            }

            if (gate == "ffmpegportion")
            {
                MessageBox.Show("This is not possible for the portion download command, since this is a two-line command process. First youtube-dl is used to get a video's url, then that is fed into the input for ffmpeg's command to download a specified portion of that link. If you would like to copy both these commands separately, please observe and use the output window once you download a portion of a video link to do so.", "Two-Line Command", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }


        private void abortbutton_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(Environment.ExitCode);
        }


        private void resetbutton_Click(object sender, RoutedEventArgs e)
        {
            link.Text = "";
            filechosen.Text = "";
            Downloadlocation.Text = "";
            playlistcheckbox.IsChecked = false;
            execheckbox.IsChecked = false;
            portioncheck.IsChecked = false;
            setpath.IsChecked = false;
            specifycode.IsChecked = false;
            thumbnail.IsChecked = false;
            subtitles.IsChecked = false;
            prioritizehighestquality.IsChecked = false;
            changeconverttitlecheckbox.IsChecked = false;
            output.Text = "";
            trimconvercheckbox.IsChecked = false;
            setpath.Visibility = Visibility.Visible;
            pathifnotinsystemalrdylabel.Visibility = Visibility.Visible;
            

        }

        private void filechosen_TextChanged(object sender, TextChangedEventArgs e)
        {

            if (filechosen.Text == "")
            {
                
                formatbox.Visibility = Visibility.Hidden;
                formatboxlabel.Visibility = Visibility.Hidden;
                changeconverttitlecheckbox.Visibility = Visibility.Hidden;
                newconvertfiletitle.Visibility = Visibility.Hidden;
                newtitlelabel.Visibility = Visibility.Hidden;
                trimconvercheckbox.Visibility = Visibility.Hidden;
                changeconverttitlecheckbox.IsChecked = false;
                trimconvercheckbox.IsChecked = false;
                

            }
            else
            {
                formatbox.Visibility = Visibility.Visible;
                formatboxlabel.Visibility = Visibility.Visible;
                changeconverttitlecheckbox.Visibility = Visibility.Visible;
                trimconvercheckbox.Visibility = Visibility.Visible;
                
            }

        }



        private void convertarea_Drop(object sender, DragEventArgs e)
        {

            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] fileList = (string[])e.Data.GetData(DataFormats.FileDrop, false);
                if (System.IO.Path.GetExtension(fileList[0]) == "")
                {
                    this.Dispatcher.BeginInvoke((Action)(() => MessageBox.Show("Choose a file to convert, not a folder", "Folder Chosen", MessageBoxButton.OK, MessageBoxImage.Information)));
                    filechosen.Text = "";
                }
                else



                if ((System.IO.Path.GetExtension(fileList[0]) == ".pdf") || (System.IO.Path.GetExtension(fileList[0]) == ".txt") || (System.IO.Path.GetExtension(fileList[0]) == ".dll"))
                {
                    this.Dispatcher.BeginInvoke((Action)(() => MessageBox.Show("Please choose a convertable file with an audio or video extension", "Improper Extension Detected", MessageBoxButton.OK, MessageBoxImage.Information)));
                    filechosen.Text = "";
                }
                else
                {
                    filechosen.Text = System.IO.Path.GetFullPath(fileList[0]);
                    convertfiletitle = System.IO.Path.GetFileNameWithoutExtension(fileList[0]);
                }

            }
        }

        private void dlpathborder_Drop(object sender, DragEventArgs e)
        {

            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] fileList = (string[])e.Data.GetData(DataFormats.FileDrop, false);

                if (System.IO.Path.GetExtension(fileList[0]) == ".exe")
                {
                    this.Dispatcher.BeginInvoke((Action)(() => MessageBox.Show("Choose the folder that youtube-dl exists inside of, not an exe file", "Executable Detected", MessageBoxButton.OK, MessageBoxImage.Information)));
                    dlpath.Text = "";
                }
                else
                {
                    if (System.IO.Path.GetExtension(fileList[0]) != "")
                    {
                        this.Dispatcher.BeginInvoke((Action)(() => MessageBox.Show("Choose the folder that youtube-dl exists inside of, not a file", "File Input Detected", MessageBoxButton.OK, MessageBoxImage.Information)));
                        dlpath.Text = "";
                    }
                    else
                    {
                        dlpath.Text = System.IO.Path.GetFullPath(fileList[0]);
                    }
                }

            }
        }

       

        private void convertpathcopybutton_Click(object sender, RoutedEventArgs e)
        {
            if (filechosen.Text == "")
            {
                this.Dispatcher.BeginInvoke((Action)(() => MessageBox.Show("There is no file path chosen!", "Empty Path Field", MessageBoxButton.OK, MessageBoxImage.Information)));
            }
            else
            {
                Clipboard.SetText(filechosen.Text);
                copiedpopupforconvertpath.Visibility = Visibility.Visible;

                tim.Tick += new EventHandler(hidecopied);
                tim.Interval = new TimeSpan(0, 0, 1);
                tim.Start();



            }
        }

        private void hideadded(object sender, EventArgs e)
        {
            addedpopupfordlpath.Visibility = Visibility.Hidden;
        }

        private void hidecopied(object sender, EventArgs e)
        {
            
            copiedpopupforconvertpath.Visibility = Visibility.Hidden;
            copiedpopupfordownloadpath.Visibility = Visibility.Hidden;
            copiedpopupcopycommand.Visibility = Visibility.Hidden;
        }

        private void hidepasted(object sender, EventArgs e)
        {
            pastedpopup.Visibility = Visibility.Hidden;
            addedpopup.Visibility = Visibility.Hidden;

        }

        private void darkmode_Click(object sender, RoutedEventArgs e)
        {
            if (darkmode.IsChecked is true)
            {
                entireborder.Background = Brushes.DarkSlateGray;
                output.Background = Brushes.Black;
                output.Foreground = Brushes.White;
                output.BorderBrush = Brushes.White;
                filechosen.Background = Brushes.Black;
                filechosen.Foreground = Brushes.White;
                filechosen.BorderBrush = Brushes.White;
                newconvertfiletitle.Background = Brushes.Black;
                newconvertfiletitle.Foreground = Brushes.White;
                newconvertfiletitle.BorderBrush = Brushes.White;
                Downloadlocation.Background = Brushes.Black;
                Downloadlocation.Foreground = Brushes.White;
                Downloadlocation.BorderBrush = Brushes.White;
                newportiontitle.Background = Brushes.Black;
                newportiontitle.Foreground = Brushes.White;
                newportiontitle.BorderBrush = Brushes.White;
                dlpath.Background = Brushes.Black;
                dlpath.Foreground = Brushes.White;
                dlpath.BorderBrush = Brushes.White;
                plrange.Background = Brushes.Black;
                plrange.BorderBrush = Brushes.White;
                plrange.Foreground = Brushes.White;
                exename.Background = Brushes.Black;
                exename.Foreground = Brushes.White;
                exename.BorderBrush = Brushes.White;
                link.Background = Brushes.Black;
                link.Foreground = Brushes.White;
                link.BorderBrush = Brushes.White;
                fromh.Background = Brushes.Black;
                fromh.Foreground = Brushes.White;
                fromh.BorderBrush = Brushes.White;
                fromm.Background = Brushes.Black;
                fromm.Foreground = Brushes.White;
                fromm.BorderBrush = Brushes.White;
                froms.Background = Brushes.Black;
                froms.Foreground = Brushes.White;
                froms.BorderBrush = Brushes.White;
                fromhcv.Background = Brushes.Black;
                fromhcv.Foreground = Brushes.White;
                fromhcv.BorderBrush = Brushes.White;
                frommcv.Background = Brushes.Black;
                frommcv.Foreground = Brushes.White;
                frommcv.BorderBrush = Brushes.White;
                fromscv.Background = Brushes.Black;
                fromscv.Foreground = Brushes.White;
                fromscv.BorderBrush = Brushes.White;
                toh.Background = Brushes.Black;
                toh.Foreground = Brushes.White;
                toh.BorderBrush = Brushes.White;
                tom.Background = Brushes.Black;
                tom.Foreground = Brushes.White;
                tom.BorderBrush = Brushes.White;
                tos.Background = Brushes.Black;
                tos.Foreground = Brushes.White;
                tos.BorderBrush = Brushes.White;
                tohcv.Background = Brushes.Black;
                tohcv.Foreground = Brushes.White;
                tohcv.BorderBrush = Brushes.White;
                tomcv.Background = Brushes.Black;
                tomcv.Foreground = Brushes.White;
                tomcv.BorderBrush = Brushes.White;
                toscv.Background = Brushes.Black;
                toscv.Foreground = Brushes.White;
                toscv.BorderBrush = Brushes.White;
                formatcode.Background = Brushes.Black;
                formatcode.Foreground = Brushes.White;
                formatcode.BorderBrush = Brushes.White;

                pasteclipboardbutton.Background = Brushes.DarkRed;
                pasteclipboardbutton.Foreground = Brushes.FloralWhite;
                appendclipboardbutton.Background = Brushes.DarkRed;
                appendclipboardbutton.Foreground = Brushes.FloralWhite;
                clearlinks.Background = Brushes.DarkRed;
                clearlinks.Foreground = Brushes.FloralWhite;

                borderforoutlinecolor.BorderBrush = Brushes.White;

                currentversionlabel.Foreground = Brushes.White;

                convertarea.Stroke = Brushes.White;
                pathrectangle.Stroke = Brushes.White;
            }
            else
            {
                entireborder.Background = Brushes.PeachPuff;
                output.Background = Brushes.White;
                output.Foreground = Brushes.Black;
                output.BorderBrush = Brushes.Black;
                filechosen.Background = Brushes.White;
                filechosen.Foreground = Brushes.Black;
                filechosen.BorderBrush = Brushes.Black;
                newconvertfiletitle.Background = Brushes.White;
                newconvertfiletitle.Foreground = Brushes.Black;
                newconvertfiletitle.BorderBrush = Brushes.Black;
                Downloadlocation.Background = Brushes.White;
                Downloadlocation.Foreground = Brushes.Black;
                Downloadlocation.BorderBrush = Brushes.Black;
                newportiontitle.Background = Brushes.White;
                newportiontitle.Foreground = Brushes.Black;
                newportiontitle.BorderBrush = Brushes.Black;
                dlpath.Background = Brushes.White;
                dlpath.Foreground = Brushes.Black;
                dlpath.BorderBrush = Brushes.Black;
                plrange.Background = Brushes.White;
                plrange.Foreground = Brushes.Black;
                plrange.BorderBrush = Brushes.Black;
                exename.Background = Brushes.White;
                exename.Foreground = Brushes.Black;
                exename.BorderBrush = Brushes.Black;
                link.Background = Brushes.White;
                link.Foreground = Brushes.Black;
                link.BorderBrush = Brushes.Black;
                fromh.Background = Brushes.White;
                fromh.Foreground = Brushes.Black;
                fromh.BorderBrush = Brushes.Black;
                fromm.Background = Brushes.White;
                fromm.Foreground = Brushes.Black;
                fromm.BorderBrush = Brushes.Black;
                froms.Background = Brushes.White;
                froms.Foreground = Brushes.Black;
                froms.BorderBrush = Brushes.Black;
                fromhcv.Background = Brushes.White;
                fromhcv.Foreground = Brushes.Black;
                fromhcv.BorderBrush = Brushes.Black;
                frommcv.Background = Brushes.White;
                frommcv.Foreground = Brushes.Black;
                frommcv.BorderBrush = Brushes.Black;
                fromscv.Background = Brushes.White;
                fromscv.Foreground = Brushes.Black;
                fromscv.BorderBrush = Brushes.Black;
                toh.Background = Brushes.White;
                toh.Foreground = Brushes.Black;
                toh.BorderBrush = Brushes.Black;
                tom.Background = Brushes.White;
                tom.Foreground = Brushes.Black;
                tom.BorderBrush = Brushes.Black;
                tos.Background = Brushes.White;
                tos.Foreground = Brushes.Black;
                tos.BorderBrush = Brushes.Black;
                tohcv.Background = Brushes.White;
                tohcv.Foreground = Brushes.Black;
                tohcv.BorderBrush = Brushes.Black;
                tomcv.Background = Brushes.White;
                tomcv.Foreground = Brushes.Black;
                tomcv.BorderBrush = Brushes.Black;
                toscv.Background = Brushes.White;
                toscv.Foreground = Brushes.Black;
                toscv.BorderBrush = Brushes.Black;
                formatcode.Background = Brushes.White;
                formatcode.Foreground = Brushes.Black;
                formatcode.BorderBrush = Brushes.Black;
               
                pasteclipboardbutton.Background = Brushes.Orange;
                pasteclipboardbutton.Foreground = Brushes.Black;
                appendclipboardbutton.Background = Brushes.Orange;
                appendclipboardbutton.Foreground = Brushes.Black;
                clearlinks.Background = Brushes.Orange;
                clearlinks.Foreground = Brushes.Black;

                borderforoutlinecolor.BorderBrush = Brushes.Black;

                currentversionlabel.Foreground = Brushes.Black;

                convertarea.Stroke = Brushes.Black;
                pathrectangle.Stroke = Brushes.Black;


            }
        }

        private void prioritizehighestquality_Click(object sender, RoutedEventArgs e)
        {
            if (prioritizehighestquality.IsChecked is true)
            {
                if (thumbnail.IsChecked is true)
                {
                    MessageBox.Show("Embedding thumbnails is only supported with m4a and mp3/4, so uncheck thumbnails if you would rather prioritize quality. Otherwise keep this unchecked", "Atomic Parsley Limitation", MessageBoxButton.OK, MessageBoxImage.Information);
                    prioritizehighestquality.IsChecked = false;
                }
                else
                {
                    formatprereq = "";
                }

            }
            else
            {
                formatprereq = "-f best";
            }

            if (specifycode.IsChecked is true && prioritizehighestquality.IsChecked is true)
            {
                MessageBox.Show("You cannot priotize highest quality while specifying a format. Either uncheck the 'specify format' checkbox or keep prioritize highest quality unchecked", "Format Overlap", MessageBoxButton.OK, MessageBoxImage.Information);
                prioritizehighestquality.IsChecked = false;
            }
        }

        private async void seeformatsbutton_Click(object sender, RoutedEventArgs e)
        {

            
            name = "youtube-dl.exe ";
            gate = "reset";


            if (link.Text.Length == 0 || link.Text.Contains(" ") is true)
            {
                MessageBox.Show("You have not pasted any valid links", "Empty Link Field", MessageBoxButton.OK, MessageBoxImage.Information);
                gate = "nopass";
            }


            if (setpath.IsChecked is true)
            {
                if (dlpath.Text == "")
                {
                    MessageBox.Show("You have set path checked but you did not set the path. If it's in your system path already, please uncheck the 'set path' checkbox", "Empty Path Field", MessageBoxButton.OK, MessageBoxImage.Information);
                    gate = "nopass";
                }

            }

            if (execheckbox.IsChecked is true)
            {
                if (exename.Text == "")
                {
                    MessageBox.Show("You did not include the name you changed your youtube-dl exe file to." + "\r\n" + "\r\n" + "If you did not change it and kept it as 'youtube-dl', please uncheck the 'I changed the name' checkbox", "Empty Name Field", MessageBoxButton.OK, MessageBoxImage.Information);
                    gate = "nopass";
                }

                else
                {
                    if (exename.Text.Contains(".exe") is true)
                    {
                        MessageBox.Show("Do not include .exe in the name", "Exe Input Detected", MessageBoxButton.OK, MessageBoxImage.Information);
                        gate = "nopass";
                    }

                    else
                    {
                        if (exename.Text.Contains(" ") is true)
                        {
                            name = "\"" + exename.Text + "\"" + ".exe ";
                        }
                        else
                        {
                            name = exename.Text + ".exe ";
                        }
                    }

                }
            }

            if (gate != "nopass")
            {
                await RunAsyncFormatList();
            }

        }

        internal async Task RunAsyncFormatList()

        {

            if (Queue == 0)
            {
                Queue = 1;
                processingpopup.Visibility = Visibility.Visible;
                abortbutton.Visibility = Visibility.Visible;



                ProcessStartInfo outie = new ProcessStartInfo("cmd");
                outie.UseShellExecute = false;
                outie.RedirectStandardOutput = true;
                outie.RedirectStandardInput = true;
                outie.RedirectStandardError = true;
                outie.CreateNoWindow = true;
                var proc = Process.Start(outie);
                proc.StandardInput.WriteLine(name + "-F " + link.Text);
                proc.StandardInput.Flush();
                proc.StandardInput.Close();

                string err;
                err = "";


                await Task.Run(() =>
                {
                    Queue = 1;
                    while (proc.StandardOutput.EndOfStream is false)
                    {

                      
                        try
                        {
                            this.Dispatcher.Invoke(() => output.AppendText(proc.StandardOutput.ReadLine() + "\r\n"));
                            this.Dispatcher.Invoke(() => output.ScrollToEnd());
                        }
                        catch (Exception)
                        {
                            proc.Kill();
                        }
                    }

                    if (proc.StandardError.EndOfStream is false)
                    {
                        err = proc.StandardError.ReadToEnd();
                    }


                });

                if (err == "")
                {
                    output.ScrollToEnd();
                    Queue = 0;
                    processingpopup.Visibility = Visibility.Hidden;
                    abortbutton.Visibility = Visibility.Hidden;

                    this.Dispatcher.Invoke(() => output.AppendText("\r\n" + "\r\n" + "Completed with no errors" + "\r\n" + "\r\n"));
                }
                else
                {
                    if (err.Contains("is not recognized") is true)
                    {
                        this.Dispatcher.Invoke(() => output.AppendText("\r\n" + "\r\n" + name + "is not recognized as an internal or external command, operable program or batch file. Either add its location to your system variables manually or use this program to do so" + "\r\n" + "\r\n"));
                        MessageBox.Show(name + "was not recongnized by your system. This is because it is not in your system's path variables and you are not using the program from within the same directory as the program. This can also be because you simply do not have it downloaded. If you do have it installed but not in your system path, you can click the 'add path' checkbox to add the program from here if you would like to", "System Does Not Recognize Program", MessageBoxButton.OK, MessageBoxImage.Information);
                        output.ScrollToEnd();
                        Queue = 0;
                        processingpopup.Visibility = Visibility.Hidden;
                        abortbutton.Visibility = Visibility.Hidden;
                    }
                    else
                    {
                        output.AppendText(err);
                        output.ScrollToEnd();
                        Queue = 0;
                        processingpopup.Visibility = Visibility.Hidden;
                        abortbutton.Visibility = Visibility.Hidden;


                    }
                }


            }
            else
            {
                MessageBox.Show("Something is in queue", "Execution in Progress", MessageBoxButton.OK, MessageBoxImage.Information);
            }



        }


        private async void UpdateYoutubedlbutton_Click(object sender, RoutedEventArgs e)
        {

            
            name = "youtube-dl.exe ";
            gate = "reset";

            if (execheckbox.IsChecked is true)
            {
                if (exename.Text == "")
                {
                    MessageBox.Show("You did not include the name you changed your youtube-dl exe file to." + "\r\n" + "\r\n" + "If you did not change it and kept it as 'youtube-dl', please uncheck the 'I changed the name' checkbox", "Empty Name Field", MessageBoxButton.OK, MessageBoxImage.Information);
                    gate = "nopass";
                }

                else
                {
                    if (exename.Text.Contains(".exe") is true)
                    {
                        MessageBox.Show("Do not include .exe in the name", "Exe Input Detected", MessageBoxButton.OK, MessageBoxImage.Information);
                        gate = "nopass";
                    }

                    else
                    {
                        if (exename.Text.Contains(" ") is true)
                        {
                            name = "\"" + exename.Text + "\"" + ".exe ";
                        }
                        else
                        {
                            name = exename.Text + ".exe ";
                        }
                    }

                }
            }

            if (setpath.IsChecked is true)
            {
                if (dlpath.Text == "")
                {
                    MessageBox.Show("You have set path checked but you did not set the path. If it's in your system path already, please uncheck the 'set path' checkbox", "Empty Path Field", MessageBoxButton.OK, MessageBoxImage.Information);
                    gate = "nopass";
                }

            }

            if (gate != "nopass")
            {
                await RunAsyncUpdateYoutubeDL();
            }

        }
        internal async Task RunAsyncUpdateYoutubeDL()

        {

            if (Queue == 0)
            {
                Queue = 1;
                processingpopup.Visibility = Visibility.Visible;
                abortbutton.Visibility = Visibility.Visible;



                ProcessStartInfo outie = new ProcessStartInfo("cmd");
                outie.UseShellExecute = false;
                outie.RedirectStandardOutput = true;
                outie.RedirectStandardInput = true;
                outie.RedirectStandardError = true;
                outie.CreateNoWindow = true;
                var proc = Process.Start(outie);
                proc.StandardInput.WriteLine(name + "-U ");
                proc.StandardInput.Flush();
                proc.StandardInput.Close();


                string err;
                err = "";

                await Task.Run(() =>
                {
                    Queue = 1;
                    while (proc.StandardOutput.EndOfStream is false)
                    {

                       
                        try
                        {
                            this.Dispatcher.Invoke(() => output.AppendText(proc.StandardOutput.ReadLine() + "\r\n"));
                            this.Dispatcher.Invoke(() => output.ScrollToEnd());
                        }
                        catch (Exception)
                        {
                            proc.Kill();
                        }
                    }

                    if (proc.StandardError.EndOfStream is false)
                    {
                        err = proc.StandardError.ReadToEnd();
                    }
                    

                });

                if (err == "")
                {
                    output.ScrollToEnd();
                    Queue = 0;
                    processingpopup.Visibility = Visibility.Hidden;
                    abortbutton.Visibility = Visibility.Hidden;

                    this.Dispatcher.Invoke(() => output.AppendText("\r\n" + "\r\n" + "Completed with no errors" + "\r\n" + "\r\n"));
                } else
                {
                    if (err.Contains("is not recognized") is true)
                    {
                        this.Dispatcher.Invoke(() => output.AppendText("\r\n" + "\r\n" + name + "is not recognized as an internal or external command, operable program or batch file. Either add its location to your system variables manually or use this program to do so" + "\r\n" + "\r\n"));
                        MessageBox.Show(name + "was not recongnized by your system. This is because it is not in your system's path variables and you are not using the program from within the same directory as the program. This can also be because you simply do not have it downloaded. If you do have it installed but not in your system path, you can click the 'add path' checkbox to add the program from here if you would like to","System Does Not Recognize Program", MessageBoxButton.OK, MessageBoxImage.Information);
                        output.ScrollToEnd();
                        Queue = 0;
                        processingpopup.Visibility = Visibility.Hidden;
                        abortbutton.Visibility = Visibility.Hidden;
                    } else
                    {
                        output.AppendText(err);
                        output.ScrollToEnd();
                        Queue = 0;
                        processingpopup.Visibility = Visibility.Hidden;
                        abortbutton.Visibility = Visibility.Hidden;

                       
                    }
                }

                
            }
            else
            {
                MessageBox.Show("Something is in queue", "Execution in Progress", MessageBoxButton.OK, MessageBoxImage.Information);
            }



        }

        private async void adddlpathbutton_Click(object sender, RoutedEventArgs e)
        {
           

            
            gate = "reset";

            if (dlpath.Text == "")
            {
                MessageBox.Show("There is no folder path chosen to add!", "No Path Detected", MessageBoxButton.OK, MessageBoxImage.Information);
                gate = "nopass";
            }

            if (gate == "reset")
            {
                
                await AddDLPath();
            }
                
                
            
        }

        internal async Task AddDLPath()

        {

            if (Queue == 0)
            {
                Queue = 1;
                processingpopup.Visibility = Visibility.Visible;
                abortbutton.Visibility = Visibility.Visible;



                ProcessStartInfo outie = new ProcessStartInfo("cmd");
                outie.UseShellExecute = false;
                outie.RedirectStandardOutput = true;
                outie.RedirectStandardInput = true;
                outie.RedirectStandardError = true;
                outie.CreateNoWindow = true;
                var proc = Process.Start(outie);
                proc.StandardInput.WriteLine("setx PATH \"%PATH%;" + dlpath.Text + "\"");
                proc.StandardInput.Flush();
                proc.StandardInput.Close();

                string err;
                err = "";


                await Task.Run(() =>
                {
                    Queue = 1;
                    while (proc.StandardOutput.EndOfStream is false)
                    {


                        try
                        {
                            err = proc.StandardError.ReadLine();
                            this.Dispatcher.Invoke(() => output.AppendText(proc.StandardOutput.ReadLine() + "\r\n"));
                            this.Dispatcher.Invoke(() => output.ScrollToEnd());
                        }
                        catch (Exception)
                        {
                            proc.Kill();
                        }
                    }

                });

                if (err == "")
                {

                    this.Dispatcher.Invoke(() => output.AppendText("\r\n" + "\r\n" + "Completed" + "\r\n" + "\r\n"));
                    output.ScrollToEnd();

                    setpath.IsChecked = false;
                    setpath.Visibility = Visibility.Hidden;
                    pathifnotinsystemalrdylabel.Visibility = Visibility.Hidden;

                    addedpopupfordlpath.Visibility = Visibility.Visible;
                    

                    tim.Tick += new EventHandler(hideadded);
                    tim.Interval = new TimeSpan(0, 0, 1);
                    tim.Start();
                    Queue = 0;
                    processingpopup.Visibility = Visibility.Hidden;
                    abortbutton.Visibility = Visibility.Hidden;
                } else
                {
                    output.AppendText("\r\n" + "\r\n" + err);
                    output.AppendText("\r\n" + "\r\n" + "Something went wrong, try adding it manually or try again. If you do not know how to add it manually, press on help." + "\r\n" + "\r\n");
                    output.ScrollToEnd();
                    setpath.IsChecked = false;
                    Queue = 0;
                    processingpopup.Visibility = Visibility.Hidden;
                    abortbutton.Visibility = Visibility.Hidden;
                }

                
            }
            else
            {
                MessageBox.Show("Something is in queue", "Execution in Progress", MessageBoxButton.OK, MessageBoxImage.Information);
            }



        }
        private async void wipecachebutton_Click(object sender, RoutedEventArgs e)
        {

            
            name = "youtube-dl.exe ";
            gate = "reset";

            if (execheckbox.IsChecked is true)
            {
                if (exename.Text == "")
                {
                    MessageBox.Show("You did not include the name you changed your youtube-dl exe file to." + "\r\n" + "\r\n" + "If you did not change it and kept it as 'youtube-dl', please uncheck the 'I changed the name' checkbox","Empty Name Field", MessageBoxButton.OK, MessageBoxImage.Information);
                    gate = "nopass";
                }

                else
                {
                    if (exename.Text.Contains(".exe") is true)
                    {
                        MessageBox.Show("Do not include .exe in the name", "Exe Input Detected", MessageBoxButton.OK, MessageBoxImage.Information);
                        gate = "nopass";
                    }

                    else
                    {
                        if (exename.Text.Contains(" ") is true)
                        {
                            name = "\"" + exename.Text + "\"" + ".exe ";
                        }
                        else
                        {
                            name = exename.Text + ".exe ";
                        }
                    }

                }
            }

            if (setpath.IsChecked is true)
            {
                if (dlpath.Text == "")
                {
                    MessageBox.Show("You have set path checked but you did not set the path. If it's in your system path already, please uncheck the 'set path' checkbox", "Empty Path Field", MessageBoxButton.OK, MessageBoxImage.Information);
                    gate = "nopass";
                }

            }

            if (gate != "nopass")
            {
                await RunAsyncWipeCache();
            }

        }
        internal async Task RunAsyncWipeCache()

        {

            if (Queue == 0)
            {
                Queue = 1;
                processingpopup.Visibility = Visibility.Visible;
                abortbutton.Visibility = Visibility.Visible;



                ProcessStartInfo outie = new ProcessStartInfo("cmd");
                outie.UseShellExecute = false;
                outie.RedirectStandardOutput = true;
                outie.RedirectStandardInput = true;
                outie.RedirectStandardError = true;
                outie.CreateNoWindow = true;
                var proc = Process.Start(outie);
                proc.StandardInput.WriteLine(name + "--rm-cache");
                proc.StandardInput.Flush();
                proc.StandardInput.Close();


                string err;
                err = "";

                await Task.Run(() =>
                {
                    Queue = 1;
                    while (proc.StandardOutput.EndOfStream is false)
                    {

                        
                        try
                        {
                            this.Dispatcher.Invoke(() => output.AppendText(proc.StandardOutput.ReadLine() + "\r\n"));
                            this.Dispatcher.Invoke(() => output.ScrollToEnd());
                        }
                        catch (Exception)
                        {
                            proc.Kill();
                        }
                    }

                    if (proc.StandardError.EndOfStream is false)
                    {
                        err = proc.StandardError.ReadToEnd();
                    }


                });

                if (err == "")
                {
                    output.ScrollToEnd();
                    Queue = 0;
                    processingpopup.Visibility = Visibility.Hidden;
                    abortbutton.Visibility = Visibility.Hidden;

                    this.Dispatcher.Invoke(() => output.AppendText("\r\n" + "\r\n" + "Completed with no errors" + "\r\n" + "\r\n"));
                }
                else
                {
                    if (err.Contains("is not recognized") is true)
                    {
                        this.Dispatcher.Invoke(() => output.AppendText("\r\n" + "\r\n" + name + "is not recognized as an internal or external command, operable program or batch file. Either add its location to your system variables manually or use this program to do so" + "\r\n" + "\r\n"));
                        MessageBox.Show(name + "was not recongnized by your system. This is because it is not in your system's path variables and you are not using the program from within the same directory as the program. This can also be because you simply do not have it downloaded. If you do have it installed but not in your system path, you can click the 'add path' checkbox to add the program from here if you would like to", "System Does Not Recognize Program", MessageBoxButton.OK, MessageBoxImage.Information);
                        output.ScrollToEnd();
                        Queue = 0;
                        processingpopup.Visibility = Visibility.Hidden;
                        abortbutton.Visibility = Visibility.Hidden;
                    }
                    else
                    {
                        output.AppendText(err);
                        output.ScrollToEnd();
                        Queue = 0;
                        processingpopup.Visibility = Visibility.Hidden;
                        abortbutton.Visibility = Visibility.Hidden;


                    }
                }


            }
            else
            {
                MessageBox.Show("Something is in queue", "Execution in Progress", MessageBoxButton.OK, MessageBoxImage.Information);
            }



        }

        private void clearlinks_Click(object sender, RoutedEventArgs e)
        {

            if (link.Text != "")
            {
                MessageBoxResult cnfrm = MessageBox.Show("This will delete your link(s), are you okay with that?", "Delete Confirmation", MessageBoxButton.YesNo);
                if (cnfrm == MessageBoxResult.Yes)
                {
                    link.Text = "";
                }
            }
            else
            {
                link.Text = "";
            }

        }

        string convertformat;
        string tosequenceforcv;
        string fromsequenceforcv;
        private async void convertbutton_Click(object sender, RoutedEventArgs e)
        {

            
            gate = "reset";
            fromsequenceforcv = "";
            tosequenceforcv = "";

            if (trimconvercheckbox.IsChecked is true)
            {
                if (toendcvcheckbox.IsChecked is false)
                {
                    int fromhour;
                    int fromminute;
                    int fromsecond;
                    int tohour;
                    int tominute;
                    int tosecond;
                    int secondcarryover;
                    int minutecarryover;
                    int seconds;
                    int minutes;
                    int hours;
                    int.TryParse(fromhcv.Text, out fromhour);
                    int.TryParse(frommcv.Text, out fromminute);
                    int.TryParse(fromscv.Text, out fromsecond);
                    int.TryParse(tohcv.Text, out tohour);
                    int.TryParse(tomcv.Text, out tominute);
                    int.TryParse(toscv.Text, out tosecond);




                    if (fromhour < tohour)
                    {
                        gate = "ffmpegportion";
                    }

                    if (fromhour > tohour)
                    {
                        MessageBox.Show("You cannot process videos backwards, please choose a 'from' time that is before the 'to' time.", "Improper Time Direction Detected", MessageBoxButton.OK, MessageBoxImage.Error);
                        gate = "nopass";
                    }


                    if (fromhour == tohour)
                    {
                        if (fromminute > tominute)
                        {
                            MessageBox.Show("You cannot process videos backwards, please choose a 'from' time that is before the 'to' time.", "Improper Time Direction Detected", MessageBoxButton.OK, MessageBoxImage.Error);
                            gate = "nopass";
                        }
                        else
                        {
                            if (fromminute == tominute)
                            {
                                if (fromsecond > tosecond)
                                {
                                    MessageBox.Show("You cannot process videos backwards, please choose a 'from' time that is before the 'to' time.", "Improper Time Direction Detected", MessageBoxButton.OK, MessageBoxImage.Error);
                                    gate = "nopass";
                                }
                                else
                                {
                                    if (fromsecond == tosecond)
                                    {
                                        MessageBox.Show("You chose the same timestamps, please choose a 'from' time that is before the 'to' time.", "Improper Time Input Detected", MessageBoxButton.OK, MessageBoxImage.Error);
                                        gate = "nopass";
                                    }
                                   

                                }
                            }
                            

                        }
                    }

                    if (gate != "nopass")
                    {


                        if (tosecond - fromsecond < 0)
                        {
                            secondcarryover = 1;
                            seconds = 60 - (fromsecond - tosecond);
                        }
                        else
                        {
                            secondcarryover = 0;
                            seconds = (tosecond - fromsecond);
                        }

                        if (tominute - fromminute < 0)
                        {
                            minutecarryover = 1;
                            minutes = (60 - (fromminute - tominute)) - secondcarryover;
                        }
                        else
                        {
                            minutecarryover = 0;
                            minutes = (tominute - fromminute) - secondcarryover;
                        }

                        hours = (tohour - fromhour) - minutecarryover;


                        fromsequenceforcv = "-ss " + fromhcv.Text + ":" + frommcv.Text + ":" + fromscv.Text + " " ;
                        tosequenceforcv = "-t " + hours + ":" + minutes + ":" + seconds;
                    }

                }
                else
                {
                    fromsequenceforcv = "-ss " + fromhcv.Text + ":" + frommcv.Text + ":" + fromscv.Text + " ";
                    tosequenceforportion = "";
                    
                } 
            } else
            {
                tosequenceforportion = "";
                fromsequenceforcv = "";
            }

            if (filechosen.Text == "")
            {
                MessageBox.Show("You did not choose a file to convert", "Empty Path Field", MessageBoxButton.OK, MessageBoxImage.Information);
                gate = "nopass";


            } else
            {
                if (formatbox.SelectedItems.Count == 0)
                {
                    MessageBox.Show("You did not select a format to convert to", "No Format Chosen", MessageBoxButton.OK, MessageBoxImage.Information);
                    gate = "nopass";
                }
            }

            if (Downloadlocation.Text == "")
            {
                MessageBox.Show("You did not choose a download location", "No Download Location Detected", MessageBoxButton.OK, MessageBoxImage.Information);
                gate = "nopass";
            }

            
            else
            {
                if (mp4.IsSelected is true)
                {
                    convertformat = ".mp4";
                }
                if (m4a.IsSelected is true)
                {
                    convertformat = ".m4a";
                }
                if (mp3.IsSelected is true)
                {
                    convertformat = ".mp3";
                }
                if (avi.IsSelected is true)
                {
                    convertformat = ".avi";
                }
                if (mkv.IsSelected is true)
                {
                    convertformat = ".mkv";
                }
                if (webm.IsSelected is true)
                {
                    convertformat = ".webm";
                }
                if (aac.IsSelected is true)
                {
                    convertformat = ".aac";
                }
                if (mov.IsSelected is true)
                {
                    convertformat = ".mov";
                }
                if (mpeg.IsSelected is true)
                {
                    convertformat = ".mpeg";
                }
                if (srt.IsSelected is true)
                {
                    convertformat = ".srt";
                }
                if (mka.IsSelected is true)
                {
                    convertformat = ".mka";
                }
                if (mks.IsSelected is true)
                {
                    convertformat = ".mks";
                }
                if (flv.IsSelected is true)
                {
                    convertformat = ".flv";
                }
                if (h264.IsSelected is true)
                {
                    convertformat = ".h264";
                }
                if (bit.IsSelected is true)
                {
                    convertformat = ".bit";
                }
            }


            if (execheckbox.IsChecked is true)
            {
                if (exename.Text == "")
                {
                    MessageBox.Show("You did not include the name you changed your youtube-dl exe file to." + "\r\n" + "\r\n" + "If you did not change it and kept it as 'youtube-dl', please uncheck the 'I changed the name' checkbox", "Empty Name Field", MessageBoxButton.OK, MessageBoxImage.Information);
                    gate = "nopass";
                   
                }

                else
                {
                    if (exename.Text.Contains(".exe") is true)
                    {
                        MessageBox.Show("Do not include .exe in the name", "Exe Input Detected", MessageBoxButton.OK, MessageBoxImage.Information);
                        gate = "nopass";
                    }

                    else
                    {
                        if (exename.Text.Contains(" ") is true)
                        {
                            name = "\"" + exename.Text + "\"" + ".exe ";
                        }
                        else
                        {
                            name = exename.Text + ".exe ";
                        }
                    }

                }
            }

            if (setpath.IsChecked is true)
            {
                if (dlpath.Text == "")
                {
                    MessageBox.Show("You have set path checked but you did not set the path. If it's in your system path already, please uncheck the 'set path' checkbox", "Empty Path Field", MessageBoxButton.OK, MessageBoxImage.Information);
                    gate = "nopass";
                }

            }

            if (changeconverttitlecheckbox.IsChecked is true)
            {
                if (newconvertfiletitle.Text == "")
                {
                    MessageBox.Show("You did not write the file's new title. If you would like to keep it the same please uncheck the 'Change Title' checkbox", "Empty Title Field", MessageBoxButton.OK, MessageBoxImage.Information);
                    gate = "nopass";
                } 
                else
                {
                    convertfiletitlefinale = newconvertfiletitle.Text;
                }
            } else
            {
                convertfiletitlefinale = convertfiletitle;
            }

            if (gate != "nopass")
            {
                if (Queue == 0)
                {
                    await RunAsyncConvert();
                }
                else
                {
                    MessageBox.Show("Something is in queue", "Execution in Progress", MessageBoxButton.OK, MessageBoxImage.Information);
                }

                
            }

        }
        internal async Task RunAsyncConvert()

        {

            if (Queue == 0)
            {
                Queue = 1;
                processingpopup.Visibility = Visibility.Visible;
                abortbutton.Visibility = Visibility.Visible;



                ProcessStartInfo outie = new ProcessStartInfo("cmd");
                outie.UseShellExecute = false;
                outie.RedirectStandardOutput = true;
                outie.RedirectStandardInput = true;
                outie.RedirectStandardError = true;
                outie.CreateNoWindow = true;
                var proc = Process.Start(outie);
                proc.StandardInput.WriteLine("ffmpeg " + fromsequenceforcv + " -i " + "\"" + filechosen.Text + "\" " + ffmpegpresetspeed + tosequenceforcv + " \"" + Downloadlocation.Text + "\\" + convertfiletitlefinale + "\"" + convertformat);
                proc.StandardInput.Flush();
                proc.StandardInput.Close();


                string line;
                string err;
                err = "";
                await Task.Run(() =>
                {
                    Queue = 1;
                    this.Dispatcher.Invoke(() => output.AppendText("ffmpeg " + fromsequenceforcv + " -i " + "\"" + filechosen.Text + "\" " + ffmpegpresetspeed + tosequenceforcv + " \"" + Downloadlocation.Text + "\\" + convertfiletitlefinale + "\"" + convertformat + "\r\n" + "\r\n" + "Processing . . ." + "\r\n" + "\r\n"));
                    while (proc.StandardOutput.EndOfStream is false)
                    {
                        while ((line = proc.StandardError.ReadLine()) != null)
                        {
                            try
                            {
                                err = proc.StandardError.ReadLine();
                                this.Dispatcher.Invoke(() => output.AppendText(proc.StandardError.ReadLine() + "\r\n"));
                                this.Dispatcher.Invoke(() => output.ScrollToEnd());

                            }
                            catch (Exception)
                            {
                                proc.Kill();
                            }
                        }

                        try
                        {
                            this.Dispatcher.Invoke(() => output.AppendText(proc.StandardOutput.ReadLine() + "\r\n"));
                            this.Dispatcher.Invoke(() => output.ScrollToEnd());
                           
                        }
                        catch (Exception)
                        {
                            proc.Kill();
                        }
                    }
                    

                   
                });


                if (err.Contains("operable program") is true)
                {
                    this.Dispatcher.Invoke(() => output.AppendText("\r\n" + "\r\n" + "FFmpeg is not recognized as an internal or external command, operable program or batch file. Either add its location to your system variables manually or use this program to do so" + "\r\n" + "\r\n"));
                    output.ScrollToEnd();
                    MessageBox.Show("Ffmpeg was not recognized by your system. This is because it is not in your system's path variables and you are not using the program from within the same directory as the program. This can also be because you simply do not have it downloaded. If you do have it installed but not in your system path, you can click the 'add path' checkbox to add the program from here if you would like to", "System Does Not Recognize Program", MessageBoxButton.OK, MessageBoxImage.Information);
                    Queue = 0;
                    processingpopup.Visibility = Visibility.Hidden;
                    abortbutton.Visibility = Visibility.Hidden;

                } else
                {
                    this.Dispatcher.Invoke(() => output.AppendText("\r\n" + "\r\n" + "Completed" + "\r\n" + "\r\n"));
                    output.ScrollToEnd();
                    Queue = 0;
                    processingpopup.Visibility = Visibility.Hidden;
                    abortbutton.Visibility = Visibility.Hidden;
                }

               
                formatbox.SelectedItem = null;
                filechosen.Text = "";
            }
            else
            {
                MessageBox.Show("Something is in queue", "Execution in Progress", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

       

        private void changeconverttitlecheckbox_Click(object sender, RoutedEventArgs e)
        {
            if (changeconverttitlecheckbox.IsChecked is true)
            {
                newconvertfiletitle.Text = "";
                newconvertfiletitle.Visibility = Visibility.Visible;
                newtitlelabel.Visibility = Visibility.Visible;

            }

            else
            {
                newconvertfiletitle.Visibility = Visibility.Hidden;
                newtitlelabel.Visibility = Visibility.Hidden;
                newconvertfiletitle.Text = "";
            }
        }

       
        private void clearoutputbutton_Click(object sender, RoutedEventArgs e)
        {
            output.Text = "";
        }

        private void helpbutton_Click(object sender, RoutedEventArgs e)
        {
            HelpWindow help = new HelpWindow();
            help.Show();
        }

        
        private void updateskydlbutton_Click(object sender, RoutedEventArgs e)
        {
            //currentversion = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();

            Process.Start("https://github.com/emberedsky/skydl/releases");
        }
    }
}
