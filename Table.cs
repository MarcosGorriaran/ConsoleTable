namespace ConsoleTable
{
    public class Table
    {
        const char TableCorner = '+';
        const char TableHorizontalLine = '-';
        const char TableVerticalLine = '|';
        const char WhiteSpace = ' ';
        const int Halfer = 2;
        const int AmountCorners = 2;
        const int CornersForCell = 1;
        private Cell[,] tableCells;
        private string title;
        private int[] longestStringCol;
        
        public Table(Cell[,] tableCells,string title) 
        {
            this.TableCells = tableCells;
            this.Title = title;
            
            
        }
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
        private void InitializeLongestStringCol()
        {
            this.longestStringCol = new int[TableCells.GetLength(1)];
            this.longestStringCol.Initialize();
            for (int i = 0; i < this.tableCells.GetLength(1); i++)
            {
                InitializeLongestStringCol(i);
            }
        }
        private string BuildCenteredCell(int row, int col)
        {
            int startBuildingOn = (this.longestStringCol[col] - CornersForCell) / Halfer - this.TableCells[row,col].Content.Length / Halfer;
            return Table.TableVerticalLine + new string(' ', startBuildingOn) + this.TableCells[row, col].WriteContent() + new string(' ', startBuildingOn);
        }
        private string BuildCenteredCell()
        {
            int startBuildingOn = (this.TableRowLength()-AmountCorners)/Halfer - this.Title.Length / Halfer;
            return Table.TableVerticalLine+new string(' ', startBuildingOn)+this.Title+new string(' ',startBuildingOn)+ Table.TableVerticalLine;
        }
        private string BuildCell(int row, int col)
        {
            int whiteSpaces = this.longestStringCol[col] - this.TableCells[row, col].Content.Length;
            return Table.TableVerticalLine + this.TableCells[row, col].WriteContent() + new string(' ', whiteSpaces) + Table.TableVerticalLine;
        }
        private string BuildRowLine(bool cornerLine)
        {
            char cornerContent = cornerLine ? Table.TableCorner : Table.TableVerticalLine;
            return cornerContent + (new string (Table.TableHorizontalLine,this.TableRowLength()-AmountCorners))+ cornerContent + Environment.NewLine;
        }
        private string BuildRowLine()
        {
            return BuildRowLine(false);
        }
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
        
        public int TableRowLength()
        {
            int sum = this.longestStringCol.Length+1;
            for(int i = 0; i < this.longestStringCol.Length; i++)
            {
                sum+= this.longestStringCol[i];
            }
            return sum;
        }
        public Cell[,] TableCells
        {
            get { return this.tableCells; }
            set 
            {
                this.tableCells = value;
                this.InitializeLongestStringCol();
            }
        }
        public string Title
        {
            get { return this.title; }
            set { this.title = value; }
        }
        public string toTableString()
        {
            string returnedValue = this.BuildRowLine(true);
            returnedValue += this.BuildCenteredCell();

            for(int i = 0; i<this.tableCells.GetLength(0); i++)
            {
                returnedValue += this.BuildRowLine();
                returnedValue += this.BuildRow(i);
            }
            return returnedValue+this.BuildRowLine(true);
        }
    }
}
