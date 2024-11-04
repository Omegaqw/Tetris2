public class GameField
{
    private const int Occupied = 1;
    private const int Empty = 0;
    private int[,] field;

    public int Width { get; }
    public int Height { get; }

    public GameField(int width, int height)
    {
        Width = width;
        Height = height;
        field = new int[Height, Width];
    }

    public bool CanPlaceFigure(Figure figure)
    {
        for (int i = 0; i < figure.Shape.GetLength(0); i++)
        {
            for (int j = 0; j < figure.Shape.GetLength(1); j++)
            {
                if (figure.Shape[i, j] == Occupied)
                {
                    int newX = figure.X + j;
                    int newY = figure.Y + i;

                    
                    if (newX < 0 || newX >= Width || newY >= Height || (newY >= 0 && field[newY, newX] == Occupied))
                    {
                        return false; 
                    }
                }
            }
        }
        return true;
    }

    public void PlaceFigure(Figure figure)
    {
        for (int i = 0; i < figure.Shape.GetLength(0); i++)
        {
            for (int j = 0; j < figure.Shape.GetLength(1); j++)
            {
                if (figure.Shape[i, j] == Occupied)
                {
                    field[figure.Y + i, figure.X + j] = Occupied; 
                }
            }
        }
    }

    public int ClearLines()
    {
        int linesCleared = 0;

        for (int i = Height - 1; i >= 0; i--)
        {
            bool fullLine = true;
            for (int j = 0; j < Width; j++)
            {
                if (field[i, j] == Empty)
                {
                    fullLine = false;
                    break;
                }
            }

            if (fullLine)
            {
                linesCleared++;
                ShiftLinesDown(i);
                i++; 
            }
        }

        return linesCleared; 
    }

    private void ShiftLinesDown(int startRow)
    {
        for (int k = startRow; k > 0; k--)
        {
            for (int j = 0; j < Width; j++)
            {
                field[k, j] = field[k - 1, j]; 
            }
        }

        
        for (int j = 0; j < Width; j++)
        {
            field[0, j] = Empty;
        }
    }

    public void Draw(Figure activeFigure = null, Figure nextFigure = null, int score = 0)
    {
        Console.Clear();
        Console.WriteLine("Score: " + score);
        Console.WriteLine(new string('-', Width + 2));

        for (int i = 0; i < Height; i++)
        {
            DrawRow(i, activeFigure);
        }

        Console.WriteLine(new string('-', Width + 2));
        Console.WriteLine("Next Figure:");
        DisplayNextFigure(nextFigure);
    }

    private void DrawRow(int rowIndex, Figure activeFigure)
    {
        Console.Write("|");
        for (int j = 0; j < Width; j++)
        {
            Console.ForegroundColor = GetBlockColor(rowIndex, j, activeFigure);
            Console.Write(field[rowIndex, j] == Occupied || IsActiveFigureBlock(rowIndex, j, activeFigure) ? "■" : " ");
            Console.ResetColor();
        }
        Console.WriteLine("|");
    }

    private ConsoleColor GetBlockColor(int row, int col, Figure activeFigure)
    {
        if (activeFigure != null && IsActiveFigureBlock(row, col, activeFigure))
        {
            return ConsoleColor.Cyan; 
        }
        return field[row, col] == Occupied ? ConsoleColor.Yellow : ConsoleColor.Black; 
    }

    private bool IsActiveFigureBlock(int i, int j, Figure activeFigure)
    {
        int relativeX = j - activeFigure.X;
        int relativeY = i - activeFigure.Y;
        return relativeY >= 0 && relativeY < activeFigure.Shape.GetLength(0) &&
               relativeX >= 0 && relativeX < activeFigure.Shape.GetLength(1) &&
               activeFigure.Shape[relativeY, relativeX] == Occupied;
    }

    private void DisplayNextFigure(Figure nextFigure)
    {
        if (nextFigure == null) return;

        for (int i = 0; i < nextFigure.Shape.GetLength(0); i++)
        {
            for (int j = 0; j < nextFigure.Shape.GetLength(1); j++)
            {
                Console.Write(nextFigure.Shape[i, j] == Occupied ? "■" : " ");
            }
            Console.WriteLine();
        }
    }
}
