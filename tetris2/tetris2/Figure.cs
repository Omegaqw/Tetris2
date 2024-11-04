public class Figure
{
    private const int Occupied = 1; 
    private const int Empty = 0;    

    public int[,] Shape { get; private set; }
    public int X { get; set; }
    public int Y { get; set; }

    public Figure(int[,] shape, int startX)
    {
        Shape = shape;
        X = startX;
        Y = 0;
    }

    public void Rotate()
    {
        Shape = GetRotatedShape(Shape);
    }

    private static int[,] GetRotatedShape(int[,] shape)
    {
        int rows = shape.GetLength(0);
        int cols = shape.GetLength(1);
        int[,] rotatedShape = new int[cols, rows];

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                rotatedShape[j, rows - 1 - i] = shape[i, j];
            }
        }
        return rotatedShape;
    }

    public static Figure GetRandomFigure(int fieldWidth)
    {
        int[][][] figures = {
            new int[][] { new int[] { Occupied, Occupied, Occupied, Occupied } }, 
            new int[][] { new int[] { Occupied, Occupied, Empty }, new int[] { Empty, Occupied, Occupied } }, 
            new int[][] { new int[] { Empty, Occupied, Occupied }, new int[] { Occupied, Occupied, Empty } }, 
            new int[][] { new int[] { Occupied, Occupied }, new int[] { Occupied, Occupied } }, 
            new int[][] { new int[] { Empty, Occupied, Empty }, new int[] { Occupied, Occupied, Occupied } }, 
            new int[][] { new int[] { Occupied, Occupied, Occupied }, new int[] { Occupied, Empty, Empty } }, 
            new int[][] { new int[] { Occupied, Occupied, Occupied }, new int[] { Empty, Empty, Occupied } }  
        };

        int index = new Random().Next(figures.Length);
        int[,] shape = ConvertTo2DArray(figures[index]);
        return new Figure(shape, (fieldWidth - shape.GetLength(1)) / 2);
    }

    private static int[,] ConvertTo2DArray(int[][] jaggedArray)
    {
        int rows = jaggedArray.Length;
        int cols = jaggedArray[0].Length;
        int[,] result = new int[rows, cols];

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                result[i, j] = jaggedArray[i][j];
            }
        }
        return result;
    }
}
