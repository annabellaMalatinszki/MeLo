using System;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Threading;
using Microsoft.Win32;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using WPFMediaPlayer;

namespace WPFGUI.Audio_and_Video
{
    public partial class AudioVideoPlayerCompleteSample : Window
    {
        private GUISupport support;
        private bool mediaPlayerIsPlaying = false;
        private bool userIsDraggingSlider = false;

        public AudioVideoPlayerCompleteSample()
        {
            InitializeComponent();
            support = new GUISupport();
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += timer_Tick;
            timer.Start();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            if ((mediaPlayer.Source != null) && (mediaPlayer.NaturalDuration.HasTimeSpan) && (!userIsDraggingSlider))
            {
                sliProgress.Minimum = 0;
                sliProgress.Maximum = mediaPlayer.NaturalDuration.TimeSpan.TotalSeconds;
                // we should write length of the sound/movie to the gui
                sliProgress.Value = mediaPlayer.Position.TotalSeconds;
            }
        }

        private void Open_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void Open_Executed(object sender, ExecutedRoutedEventArgs e)
        {
                mediaPlayer.Source = support.OpenFile();
                recentlyOpenedFileList.ItemsSource = support.Filenames;
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

        private void PlayMediaPlayer()
        {
            mediaPlayer.Play();
            //mediaPlayer.
            mediaPlayerIsPlaying = true;

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
            mediaPlayerIsPlaying = false;
        }

        private void sliProgress_DragStarted(object sender, DragStartedEventArgs e)
        {
            userIsDraggingSlider = true;
        }

        private void sliProgress_DragCompleted(object sender, DragCompletedEventArgs e)
        {
            userIsDraggingSlider = false;
            mediaPlayer.Position = TimeSpan.FromSeconds(sliProgress.Value);
            // go back to start
        }

        private void sliProgress_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            lblProgressStatus.Text = TimeSpan.FromSeconds(sliProgress.Value).ToString(@"hh\:mm\:ss");
        }

        private void Grid_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            mediaPlayer.Volume += (e.Delta > 0) ? 0.1 : -0.1;
        }



        private void recentlyOpenedFileList_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            string selectedFile = recentlyOpenedFileList.SelectedItem.ToString();
            label1.Content = selectedFile;
            foreach (FileDialog file in support.RecentlyOpenedFiles)
            {
                if (file.SafeFileName.Equals(selectedFile))
                {
                    mediaPlayer.Source = new Uri(file.FileName); ;
                    PlayMediaPlayer();
                }
            }
        }
    }
}