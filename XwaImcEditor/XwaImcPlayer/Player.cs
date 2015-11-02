using System;
using System.Media;
using System.Windows.Threading;
using JeremyAnsel.Xwa.Imc;

namespace XwaImcPlayer
{
    public sealed class Player : ObservableObject
    {
        private SoundPlayer player;

        private DispatcherTimer timer;

        private string streamName;

        private double position;

        private double length;

        public Player()
        {
            this.player = new SoundPlayer();

            this.timer = new DispatcherTimer();
            this.timer.Interval = TimeSpan.FromMilliseconds(200);
            this.timer.Tick += this.timer_Tick;
        }

        public string StreamName
        {
            get
            {
                return this.streamName;
            }

            private set
            {
                if (value != this.streamName)
                {
                    this.streamName = value;
                    this.RaisePropertyChangedEvent("StreamName");
                }
            }
        }

        public double Position
        {
            get
            {
                return this.position;
            }

            private set
            {
                if (value != this.position)
                {
                    this.position = value;
                    this.RaisePropertyChangedEvent("Position");
                }
            }
        }

        public double Length
        {
            get
            {
                return this.length;
            }

            private set
            {
                if (value != this.length)
                {
                    this.length = value;
                    this.RaisePropertyChangedEvent("Length");
                }
            }
        }

        public void Stop()
        {
            this.timer.Stop();
            this.player.Stop();
            this.player.Stream = null;
            this.Position = 0;
            this.Length = 0;
            this.StreamName = null;
        }

        public void Play(ImcFile imc, int start, int end)
        {
            this.Stop();

            this.player.Stream = imc.RetrieveWaveStream(start, end);
            this.Length = (double)(end - start) / imc.SampleRate;
            this.StreamName = imc.Name;

            this.player.Play();
            this.timer.Start();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            double position = this.Position + this.timer.Interval.TotalSeconds;

            if (position > this.Length)
            {
                position = this.Length;
                this.timer.Stop();
            }

            this.Position = position;

            if (this.position >= this.Length)
            {
                this.Stop();
            }
        }
    }
}
