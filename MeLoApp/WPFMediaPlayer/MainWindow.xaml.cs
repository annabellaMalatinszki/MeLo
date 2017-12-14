using System;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Threading;
using Microsoft.Win32;
using WPFMediaPlayer;

namespace WPFGUI.Audio_and_Video
{
    public partial class AudioVideoPlayerCompleteSample : Window
    {
        private GUISupport support;
        private bool mediaPlayerIsPlaying = false;
        private bool userIsDraggingSlider = false;
        private Uri previous;
        private Uri next;

        public AudioVideoPlayerCompleteSample()
        {
            InitializeComponent();
            Loaded += MediaPlayerLoaded;
        }

        private void MediaPlayerLoaded(object sender, RoutedEventArgs e)
        {
            support = new GUISupport();
            support.ReadBackFilesFromMemory();
            openedFileList.ItemsSource = support.Filenames;

            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if ((mediaPlayer.Source != null) && (mediaPlayer.NaturalDuration.HasTimeSpan) && (!userIsDraggingSlider))
            {
                sliProgress.Minimum = 0;
                sliProgress.Maximum = mediaPlayer.NaturalDuration.TimeSpan.TotalSeconds;
                sliProgress.Value = mediaPlayer.Position.TotalSeconds;
                lblTotalTime.Text = TimeSpan.FromSeconds(sliProgress.Maximum).ToString(@"hh\:mm\:ss");
            }
        }

        private void Open_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void Open_Executed(object sender, ExecutedRoutedEventArgs e)
        {
                mediaPlayer.Source = support.OpenFile();
                openedFileList.ItemsSource = support.Filenames;
                PlayMediaPlayer();
        }

        private void Play_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = (mediaPlayer != null) && (mediaPlayer.Source != null);
        }

        private void Play_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            PlayMediaPlayer();
        }

        private void Rewind_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = (mediaPlayer != null) && (mediaPlayer.Source != null);
        }

        private void Rewind_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            mediaPlayer.Stop();
            PlayMediaPlayer();
        }


        private void PlayMediaPlayer()
        {
            mediaPlayer.Play();
            mediaPlayerIsPlaying = true;

        }

        private void Previous_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (previous != null)
            {
                e.CanExecute = true;
            } else
            {
                e.CanExecute = false;
            }

        }

        private void Previous_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (previous != null)
            {
                next = mediaPlayer.Source;
                mediaPlayer.Source = previous;
                previous = null;
                PlayMediaPlayer();
            }
        }

        private void Next_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (next != null)
            {
                e.CanExecute = true;
            }
            else
            {
                e.CanExecute = false;
            }
        }

        private void Next_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            previous = mediaPlayer.Source;
            mediaPlayer.Source = next;
            next = null;
            PlayMediaPlayer();
        }

        private void Pause_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = mediaPlayerIsPlaying;
        }

        private void Pause_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            mediaPlayer.Pause();
        }

        private void Stop_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = mediaPlayerIsPlaying;
        }

        private void Stop_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            mediaPlayer.Stop();
            previous = mediaPlayer.Source;
            mediaPlayerIsPlaying = false;
        }

        private void SliProgress_DragStarted(object sender, DragStartedEventArgs e)
        {
            userIsDraggingSlider = true;
        }

        private void SliProgress_DragCompleted(object sender, DragCompletedEventArgs e)
        {
            previous = mediaPlayer.Source;
            userIsDraggingSlider = false;
            mediaPlayer.Position = TimeSpan.FromSeconds(sliProgress.Value);
            // go back to start
        }

        private void SliProgress_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            lblProgressStatus.Text = TimeSpan.FromSeconds(sliProgress.Value).ToString(@"hh\:mm\:ss");
        }

        private void Grid_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            mediaPlayer.Volume += (e.Delta > 0) ? 0.1 : -0.1;
        }

        private void openedFileList_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            previous = mediaPlayer.Source;
            string selectedFile = openedFileList.SelectedItem.ToString();
            foreach (FileDialog file in support.RecentlyOpenedFiles)
            {
                if (file.SafeFileName.Equals(selectedFile))
                {
                    mediaPlayer.Source = new Uri(file.FileName);
                    PlayMediaPlayer();
                }
            }
        }
    }
}