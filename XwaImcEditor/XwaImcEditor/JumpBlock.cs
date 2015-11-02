
namespace XwaImcEditor
{
    public class JumpBlock : Block
    {
        public JumpBlock()
        {
            this.Delay = 500;
        }

        private int destination;

        private int id;

        private int delay;

        public int Destination
        {
            get
            {
                return this.destination;
            }

            set
            {
                if (value != this.destination)
                {
                    this.destination = value;
                    this.RaisePropertyChangedEvent("Destination");
                }
            }
        }

        public int Id
        {
            get
            {
                return this.id;
            }

            set
            {
                if (value != this.id)
                {
                    this.id = value;
                    this.RaisePropertyChangedEvent("Id");
                }
            }
        }

        public int Delay
        {
            get
            {
                return this.delay;
            }

            set
            {
                if (value != this.delay)
                {
                    this.delay = value;
                    this.RaisePropertyChangedEvent("Delay");
                }
            }
        }
    }
}
