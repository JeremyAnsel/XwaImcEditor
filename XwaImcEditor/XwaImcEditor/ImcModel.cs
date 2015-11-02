using JeremyAnsel.Xwa.Imc;

namespace XwaImcEditor
{
    public class ImcModel : ObservableObject
    {
        private ImcFile file;

        public ImcModel()
        {
            this.file = new ImcFile();
            this.Blocks = new BlockCollection();
            this.Player = new Player();
        }

        public string FileName { get { return this.file.FileName; } }

        public string Name { get { return this.file.Name; } }

        public int BitsPerSample { get { return this.file.BitsPerSample; } }

        public int SampleRate { get { return this.file.SampleRate; } }

        public int Channels { get { return this.file.Channels; } }

        public int Length { get { return this.file.Length; } }

        public double TimeLength { get { return this.file.TimeLength; } }

        public BlockCollection Blocks { get; private set; }

        public Player Player { get; private set; }

        public void Clear()
        {
            this.Player.Stop();

            this.file = new ImcFile();
            this.RaiseFilePropertyChangedEvents();
            this.Blocks.Clear();
        }

        public void Open(string fileName)
        {
            this.Player.Stop();

            this.file = ImcFile.FromFile(fileName);
            this.RaiseFilePropertyChangedEvents();

            this.Blocks.Clear();

            foreach (var item in this.file.Map)
            {
                if (item is ImcJump)
                {
                    var jump = (ImcJump)item;

                    this.Blocks.Add(new JumpBlock
                    {
                        Position = jump.Position,
                        Destination = jump.Destination,
                        Id = jump.HookId,
                        Delay = jump.Delay
                    });
                }
                else if (item is ImcText)
                {
                    var text = (ImcText)item;

                    this.Blocks.Add(new TextBlock
                    {
                        Position = text.Position,
                        Text = text.Text
                    });
                }
            }
        }

        public void Save(string fileName)
        {
            this.file.Map.Clear();

            foreach (var block in this.Blocks)
            {
                if (block is JumpBlock)
                {
                    var jump = (JumpBlock)block;

                    this.file.Map.Add(new ImcJump
                    {
                        Position = jump.Position,
                        Destination = jump.Destination,
                        HookId = jump.Id,
                        Delay = jump.Delay
                    });
                }
                else if (block is TextBlock)
                {
                    var text = (TextBlock)block;

                    this.file.Map.Add(new ImcText
                    {
                        Position = text.Position,
                        Text = text.Text
                    });
                }
            }

            this.file.Save(fileName);
        }

        public void ImportWav(string fileName)
        {
            this.Player.Stop();

            this.file.SetRawDataFromWave(fileName);
            this.RaiseFilePropertyChangedEvents();
        }

        public void ExportWav(string fileName)
        {
            this.file.SaveAsWave(fileName);
        }

        public void StartPlayer()
        {
            this.Player.Play(this.file, 0, this.file.Length);
        }

        public void StopPlayer()
        {
            this.Player.Stop();
        }

        private void RaiseFilePropertyChangedEvents()
        {
            this.RaisePropertyChangedEvent("FileName");
            this.RaisePropertyChangedEvent("Name");
            this.RaisePropertyChangedEvent("BitsPerSample");
            this.RaisePropertyChangedEvent("SampleRate");
            this.RaisePropertyChangedEvent("Channels");
            this.RaisePropertyChangedEvent("Length");
            this.RaisePropertyChangedEvent("TimeLength");
        }
    }
}
