
namespace XwaImcEditor
{
    public abstract class Block : ObservableObject
    {
        private int position;

        public int Position
        {
            get
            {
                return this.position;
            }

            set
            {
                if (value != this.position)
                {
                    this.position = value;
                    this.RaisePropertyChangedEvent("Position");
                }
            }
        }
    }
}
