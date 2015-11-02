
namespace XwaImcEditor
{
    public class TextBlock : Block
    {
        private string text;

        public string Text
        {
            get
            {
                return this.text;
            }

            set
            {
                if (value != this.text)
                {
                    this.text = value;
                    this.RaisePropertyChangedEvent("Text");
                }
            }
        }
    }
}
