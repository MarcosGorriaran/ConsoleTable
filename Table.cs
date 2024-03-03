namespace ConsoleTable
{
    /// <summary>
    /// Class responsible of representing a table and displaying it as text.
    /// </summary>
    public class Table
    {
        const char TableCorner = '+';
        const char TableHorizontalLine = '-';
        const char TableVerticalLine = '|';
        const char WhiteSpace = ' ';
        const int Halfer = 2;
        const int AmountCorners = 2;
        private Cell[,] tableCells;
        private string title;
        private int[] longestStringCol;
        
        public Table(Cell[,] tableCells,string title) 
        {
            this.TableCells = tableCells;
            this.Title = title;
        }

        /// <summary>
        /// This method is responsible of checking the bidimensional array of cells and decide which string is the longest and
        /// store it on the array longestStringCol
        /// </summary>
        /// <param name="col">Which column is the method going to check</param>
        private void InitializeLongestStringCol(int col)
        {
            for(int i = 0; i < this.tableCells.GetLength(0); i++)
            {
                if (this.longestStringCol[col]< this.tableCells[i, col].Content.Length)
                {
                    this.longestStringCol[col] = this.tableCells[i, col].Content.Length;
                }
            }
        }
        /// <summary>
        /// This method is responsible of initializing the array of longestStringCol with by checking each column
        /// and deciding which row of the column has the longest string to store this value on the array
        /// </summary>
        private void InitializeLongestStringCol()
        {
            this.longestStringCol = new int[TableCells.GetLength(1)];
            this.longestStringCol.Initialize();
            for (int i = 0; i < this.tableCells.GetLength(1); i++)
            {
                InitializeLongestStringCol(i);
            }
        }
        /// <summary>
        /// This method is responsible of building a cell centered as best as posible in the center of the cell which will eventualy become an element of a row
        /// and later on another element of a table
        /// </summary>
        /// <param name="row">Which row of the table will it work with</param>
        /// <param name="col">Which column of the table will it work with</param>
        /// <returns>The cell with its content on the center and one of its corners on the right side so at the end of building all cells everything is closed on the last cell of the row</returns>
        private string BuildCenteredCell(int row, int col)
        {
            int startBuildingOn = (this.longestStringCol[col]) / Halfer - this.TableCells[row,col].Content.Length / Halfer;
            int endBuildingOn;

            
            if ((startBuildingOn * 2 + this.TableCells[row,col].Content.Length) > this.longestStringCol[col])
            {
                endBuildingOn = startBuildingOn - 1;
            }
            else if ((startBuildingOn * 2 + this.TableCells[row, col].Content.Length) < this.longestStringCol[col])
            {
                endBuildingOn = startBuildingOn + 1;
            }
            else
            {
                endBuildingOn = startBuildingOn;
            }
            if (startBuildingOn < 0)
            {
                startBuildingOn = 0;
            }
            if(endBuildingOn < 0)
            {
                endBuildingOn = 0;
            }
            return Table.TableVerticalLine + new string(Table.WhiteSpace, startBuildingOn) + this.TableCells[row, col].WriteContent() + new string(Table.WhiteSpace, endBuildingOn);
        }
        /// <summary>
        /// This method is responsible of building the title cell on the center of the line.
        /// </summary>
        /// <returns>A string which will has the title of the table on its center</returns>
        private string BuildCenteredCell()
        {
            int startBuildingOn = Convert.ToInt32(Math.Round(((decimal)this.TableRowLength()-AmountCorners)/Halfer - this.Title.Length / Halfer));
            int endBuildingOn;
            
            if ((startBuildingOn * Halfer + this.Title.Length) > this.TableRowLength() - AmountCorners)
            {
                endBuildingOn = startBuildingOn - 1;
            }
            else if((startBuildingOn * Halfer + this.Title.Length) < this.TableRowLength() - AmountCorners)
            {
                endBuildingOn = startBuildingOn + 1;
            }
            else
            {
                endBuildingOn = startBuildingOn;
            }
            if (startBuildingOn < 0)
            {
                startBuildingOn = 0;
            }
            if (endBuildingOn < 0)
            {
                endBuildingOn = 0;
            }
            return Table.TableVerticalLine+new string(Table.WhiteSpace, startBuildingOn)+this.Title+new string(Table.WhiteSpace, endBuildingOn)+ Table.TableVerticalLine+Environment.NewLine;
        }
        /// <summary>
        /// This method is responsible of building a cell with its content on the left side
        /// </summary>
        /// <param name="row">Which row of the table will it work with</param>
        /// <param name="col">Which column of the table will it work with</param>
        /// <returns>A string containing the content of a cell with a vertical line on the left</returns>
        private string BuildCell(int row, int col)
        {
            int whiteSpaces = this.longestStringCol[col] - this.TableCells[row, col].Content.Length;
            return Table.TableVerticalLine + this.TableCells[row, col].WriteContent() + new string(Table.WhiteSpace, whiteSpaces);
        }
        /// <summary>
        /// The objective of this method is to draw a line with a vertical line on each of its sides or a corner line if specified on the boolean
        /// </summary>
        /// <param name="cornerLine">True to specify the line has to have a corner line on the sides of the line and false if it is a vertical line</param>
        /// <returns>A string containing the representation of a line of a row with either a corner line on the sides or a vertical line</returns>
        private string BuildRowLine(bool cornerLine)
        {
            char cornerContent = cornerLine ? Table.TableCorner : Table.TableVerticalLine;
            return cornerContent + (new string (Table.TableHorizontalLine,this.TableRowLength()-AmountCorners))+ cornerContent + Environment.NewLine;
        }
        /// <summary>
        /// Method responsible of drawing a line with a vertical line on each of its side and it scretches as long as the content of the table.
        /// </summary>
        /// <returns>A string containg the representation of a line of a row with a vertical line on its sides</returns>
        private string BuildRowLine()
        {
            return BuildRowLine(false);
        }
        /// <summary>
        /// Method responsible of drawing a row with its cells and deciding if it should draw it centered if the cell is a header or normally if it isn't
        /// </summary>
        /// <param name="row">Which row will it draw</param>
        /// <returns>A string containing one of the rows of the table</returns>
        private string BuildRow(int row)
        {
            string rowResult = "";
            for (int i = 0; i < this.tableCells.GetLength(1); i++)
            {
                if (this.TableCells[row, i].Header)
                {
                    rowResult += this.BuildCenteredCell(row, i);
                }
                else
                {
                    rowResult += this.BuildCell(row, i);
                }
            }
            return rowResult+Table.TableVerticalLine+Environment.NewLine;
        }
        /// <summary>
        /// This method specifies how long a row is on the table
        /// </summary>
        /// <returns>A value which represents the length of a row on the table</returns>
        public int TableRowLength()
        {
            int sum = this.longestStringCol.Length+1;
            for(int i = 0; i < this.longestStringCol.Length; i++)
            {
                sum+= this.longestStringCol[i];
            }
            return sum;
        }
        /// <summary>
        /// This method is responsible of building the whole table on a string using the bidimensional array Cells as its reference for its contents
        /// </summary>
        /// <returns>A string representing the information stored on the table, ready to be displayed on console or wathever text format it is placed on</returns>
        public string toTableString()
        {
            string returnedValue = this.BuildRowLine(true);
            returnedValue += this.BuildCenteredCell();

            for (int i = 0; i < this.tableCells.GetLength(0); i++)
            {
                returnedValue += this.BuildRowLine();
                returnedValue += this.BuildRow(i);
            }
            return returnedValue + this.BuildRowLine(true);
        }
        /// <summary>
        /// Here's where all the information of each cell is stored in. The first dimension represents the row of the table and the second dimension represents
        /// the columns
        /// </summary>
        public Cell[,] TableCells
        {
            get { return this.tableCells; }
            set 
            {
                this.tableCells = value;
                this.InitializeLongestStringCol();
            }
        }
        /// <summary>
        /// The title the table has which will be displayed on the first row.
        /// </summary>
        public string Title
        {
            get { return this.title; }
            set { this.title = value; }
        }
        
    }
}
