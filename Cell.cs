
namespace ConsoleTable
{
    /// <summary>
    /// The Cell class is a class made as an adition to the class Table
    /// to form the diferent cells a table has.
    /// </summary>
    public class Cell
    {
        const string ConsoleBold = "\x1b[1m";
        const string ConsoleReset = "\x1b[0m";
        private string content;
        private bool header;
        /// <summary>
        /// Constructor with biggest logical load
        /// </summary>
        /// <param name="content">The text that will be shown on the text of the cell</param>
        /// <param name="header">If the Cell is a header</param>
        public Cell(string content, bool header)
        {
            this.Content = content;
            this.Header = header;
        }
        public string WriteContent()
        {
            return this.header ? ConsoleBold+this.Content+ConsoleReset : this.Content;
        }
        public string Content
        {
            get { return this.content; }
            set { this.content = value; }
        }
        public bool Header
        {
            get { return this.header; }
            set { this.header = value; }
        }
    }
}
