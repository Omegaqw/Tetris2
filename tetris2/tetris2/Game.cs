using System;
using System.Threading;

public class Game
{
    private GameField field;
    private Figure currentFigure;
    private Figure nextFigure;
    private int score;
    private bool isGameOver;
    private bool isAcceleratingDown;

    public Game(int width, int height)
    {
        field = new GameField(width, height);
        currentFigure = Figure.GetRandomFigure(width);
        nextFigure = Figure.GetRandomFigure(width);
        score = 0;
        isGameOver = false;
        isAcceleratingDown = false;
    }

    public void Start()
    {
        while (!isGameOver)
        {
            Update();
            HandleInput();
            field.Draw(currentFigure, nextFigure, score);
            Thread.Sleep(isAcceleratingDown ? 100 : 500); 
        }

        Console.WriteLine("Game Over! Your score: " + score);
    }

    private void Update()
    {
        if (CanMoveDown(currentFigure))
        {
            currentFigure.Y++;
        }
        else
        {
            field.PlaceFigure(currentFigure);
            int linesCleared = field.ClearLines();
            score += linesCleared * 100;
            SwitchToNextFigure();
        }
    }

    private void SwitchToNextFigure()
    {
        currentFigure = nextFigure;
        nextFigure = Figure.GetRandomFigure(field.Width);

        if (!field.CanPlaceFigure(currentFigure))
        {
            isGameOver = true;
        }
    }

    private bool CanMoveDown(Figure figure)
    {
        figure.Y++;
        bool canMove = field.CanPlaceFigure(figure);
        figure.Y--; 
        return canMove;
    }

    private void HandleInput()
    {
        while (Console.KeyAvailable)
        {
            ConsoleKeyInfo keyInfo = Console.ReadKey(true);
            switch (keyInfo.Key)
            {
                case ConsoleKey.LeftArrow:
                    MoveFigure(-1);
                    break;
                case ConsoleKey.RightArrow:
                    MoveFigure(1);
                    break;
                case ConsoleKey.DownArrow:
                    isAcceleratingDown = true;
                    break;
                case ConsoleKey.UpArrow:
                    RotateFigure();
                    break;
            }
        }

       
        if (!Console.KeyAvailable && isAcceleratingDown)
        {
            isAcceleratingDown = false;
        }
    }

    private void MoveFigure(int direction)
    {
        currentFigure.X += direction;
        if (!field.CanPlaceFigure(currentFigure))
        {
            currentFigure.X -= direction; 
        }
    }

    private void RotateFigure()
    {
        currentFigure.Rotate();
        if (!field.CanPlaceFigure(currentFigure))
        {
            currentFigure.Rotate();
        }
    }
}
