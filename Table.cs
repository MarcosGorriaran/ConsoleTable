namespace ConsoleTable
{
    /// <summary>
    /// C
    /// </summary>
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
        private string BuildCenteredCell()
        {
            int startBuildingOn = Convert.ToInt32(Math.Round(((decimal)this.TableRowLength()-AmountCorners)/Halfer - this.Title.Length / Halfer));
            int endBuildingOn;
            
            if ((startBuildingOn * 2 + this.Title.Length) > this.TableRowLength() - AmountCorners)
            {
                endBuildingOn = startBuildingOn - 1;
            }
            else if((startBuildingOn * 2 + this.Title.Length) < this.TableRowLength() - AmountCorners)
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
        private string BuildCell(int row, int col)
        {
            int whiteSpaces = this.longestStringCol[col] - this.TableCells[row, col].Content.Length;
            return Table.TableVerticalLine + this.TableCells[row, col].WriteContent() + new string(Table.WhiteSpace, whiteSpaces);
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
