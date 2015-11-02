using System;
using System.Windows;
using Microsoft.Win32;

namespace XwaImcEditor
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            this.Model = new ImcModel();

            this.DataContext = this;
        }

        public ImcModel Model { get; private set; }

        private void NewImcButton_Click(object sender, RoutedEventArgs e)
        {
            this.Model.Clear();
        }

        private void OpenImcButton_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog();
            dialog.Title = "Select a .imc file";
            dialog.DefaultExt = ".imc";
            dialog.CheckFileExists = true;
            dialog.Filter = "IMC files (*.imc)|*.imc";

            string fileName;

            if (dialog.ShowDialog(this) == true)
            {
                fileName = dialog.FileName;
            }
            else
            {
                return;
            }

            try
            {
                this.Model.Open(fileName);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.ToString(), this.Title);
            }
        }

        private void SaveImcButton_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new SaveFileDialog();
            dialog.Title = "Select a .imc file";
            dialog.DefaultExt = ".imc";
            dialog.Filter = "IMC files (*.imc)|*.imc";
            dialog.FileName = this.Model.Name;

            string fileName;

            if (dialog.ShowDialog(this) == true)
            {
                fileName = dialog.FileName;
            }
            else
            {
                return;
            }

            try
            {
                this.Model.Save(fileName);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.ToString(), this.Title);
            }
        }

        private void ImportWavButton_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog();
            dialog.Title = "Select a .wav file";
            dialog.DefaultExt = ".wav";
            dialog.CheckFileExists = true;
            dialog.Filter = "WAV files (*.wav)|*.wav";

            string fileName;

            if (dialog.ShowDialog(this) == true)
            {
                fileName = dialog.FileName;
            }
            else
            {
                return;
            }

            try
            {
                this.Model.ImportWav(fileName);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.ToString(), this.Title);
            }
        }

        private void ExportWavButton_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new SaveFileDialog();
            dialog.Title = "Select a .wav file";
            dialog.DefaultExt = ".wav";
            dialog.Filter = "WAV files (*.wav)|*.wav";
            dialog.FileName = System.IO.Path.ChangeExtension(this.Model.Name, ".wav");

            string fileName;

            if (dialog.ShowDialog(this) == true)
            {
                fileName = dialog.FileName;
            }
            else
            {
                return;
            }

            try
            {
                this.Model.ExportWav(fileName);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.ToString(), this.Title);
            }
        }

        private void ClearMapButton_Click(object sender, RoutedEventArgs e)
        {
            this.Model.Blocks.Clear();
        }

        private void NewTextMapButton_Click(object sender, RoutedEventArgs e)
        {
            this.Model.Blocks.Add(new TextBlock());
        }

        private void NewJumpMapButton_Click(object sender, RoutedEventArgs e)
        {
            this.Model.Blocks.Add(new JumpBlock());
        }

        private void RemoveMapButton_Click(object sender, RoutedEventArgs e)
        {
            var selected = this.MapList.SelectedItem as Block;

            if (selected != null)
            {
                this.Model.Blocks.Remove(selected);
            }
        }

        private void StartPlayerButton_Click(object sender, RoutedEventArgs e)
        {
            this.Model.StartPlayer();
        }

        private void StopPlayerButton_Click(object sender, RoutedEventArgs e)
        {
            this.Model.StopPlayer();
        }
    }
}
