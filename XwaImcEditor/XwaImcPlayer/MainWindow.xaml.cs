using System;
using System.Collections.ObjectModel;
using System.Windows;
using JeremyAnsel.Xwa.Imc;
using Microsoft.Win32;

namespace XwaImcPlayer
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            this.Player = new Player();
            this.Files = new ObservableCollection<string>();

            this.DataContext = this;
        }

        public Player Player { get; private set; }

        public ObservableCollection<string> Files { get; private set; }

        private void PlayButton_Click(object sender, RoutedEventArgs e)
        {
            this.Player.Stop();

            var selectedItem = this.FilesListView.SelectedItem as string;

            if (selectedItem == null)
            {
                return;
            }

            ImcFile imc;

            try
            {
                imc = ImcFile.FromFile(selectedItem);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.ToString(), this.Title);
                return;
            }

            this.Player.Play(imc, 0, imc.Length);
        }

        private void StopButton_Click(object sender, RoutedEventArgs e)
        {
            this.Player.Stop();
        }

        private void OpenMusicDirectory_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog();
            dialog.Title = "Select a folder";
            dialog.DefaultExt = ".imc";
            dialog.CheckFileExists = true;
            dialog.Filter = "IMC files (*.imc)|*.imc";

            string directory;

            if (dialog.ShowDialog(this) == true)
            {
                directory = System.IO.Path.GetDirectoryName(dialog.FileName);
            }
            else
            {
                return;
            }

            this.Files.Clear();

            foreach (var fileName in System.IO.Directory.EnumerateFiles(directory, "*.IMC"))
            {
                this.Files.Add(fileName);
            }
        }
    }
}
