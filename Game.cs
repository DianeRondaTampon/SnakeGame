using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsSnakeGame
{
    public enum Direction
    {
        Up,
        Down,
        Left,
        Right
    }

    public class Snake
    {
        //First point is HEAD, last point is TAIL
        public List<Point> points { get; set; }
        private Direction direction { get; set; }
        public bool bigger { get; set; }

        public Snake()
        {
            points = new List<Point>();
            direction = Direction.Right;
            bigger = false;
        }

        public void InitializeSnake(Point startPosition)
        {
            Point point = new Point(startPosition.X, startPosition.Y);
            points = new List<Point>();
            this.points.Add(point);
            direction = Direction.Right;
            bigger = false;
        }

        public void ChangeDirection(Direction direction)
        {
            switch (direction)
            {
                case Direction.Up:
                    if (direction != Direction.Down)
                    {
                        direction = Direction.Up;
                    }
                    break;
                case Direction.Down:
                    if (direction != Direction.Up)
                    {
                        direction = Direction.Down;
                    }
                    break;
                case Direction.Left:
                    if (direction != Direction.Right)
                    {
                        direction = Direction.Left;
                    }
                    break;
                case Direction.Right:
                    if (direction != Direction.Left)
                    {
                        direction = Direction.Right;
                    }
                    break;
            }

        }

        public void Update()
        {
            //move the snake: remove the tail, create new head

            //get point of current head
            int x = points.ElementAt(0).X;
            int y = points.ElementAt(0).Y;
            Point newHead;

            switch (this.direction)
            {
                case Direction.Left:
                    //add new head
                    x = x - 1;
                    newHead = new Point(x, y);
                    points.Insert(0, newHead);
                    break;
                case Direction.Right:
                    //add new head
                    x = x + 1;
                    newHead = new Point(x, y);
                    points.Insert(0, newHead);
                    break;
                case Direction.Up:
                    //add new head
                    y = y - 1;
                    newHead = new Point(x, y);
                    points.Insert(0, newHead);
                    break;
                case Direction.Down:
                    //add new head
                    y = y + 1;
                    newHead = new Point(x, y);
                    points.Insert(0, newHead);
                    break;
            }

            //remove tail
            if (!bigger)
            {
                points.RemoveAt(points.Count - 1);
            }
            else
            {
                //not remove the tail so the snake will increase the size
                bigger = false;
            }
        }
    }



    //public static class StaticClass
    //{
    //    public static int MyProperty { get; set; }
    //    public static void doSomething()
    //    {
    //        //...
    //    }
    //}



    public class Scenary
    {
        public Point boundariesLeftUp { get; set; }
        public Point boundariesRightDown { get; set; }
        public List<Point> fruits { get; set; }
        public TimeSpan intervalGenerateFruit { get; set; }
        public DateTime? whenLastfruitWasGenerated { get; set; }
        public bool alive { get; set; }

        public Scenary()
        {
            fruits = new List<Point>();
        }

        public void InitializeScenary(Point boundariesLeftUp, Point boundariesRightDown, TimeSpan intervalGenerateFruit)
        {
            fruits = new List<Point>();
            this.boundariesLeftUp = boundariesLeftUp;
            this.boundariesRightDown = boundariesRightDown;
            this.intervalGenerateFruit = intervalGenerateFruit;
            whenLastfruitWasGenerated = DateTime.Now;
            alive = true;
        }

        public void Update(Snake snake)
        {
            //check if is time for generate fruits
            if (DateTime.Now > this.whenLastfruitWasGenerated + this.intervalGenerateFruit)
            {
                //generate another fruit in random position
                Random random = new Random();
                int randomX = random.Next(this.boundariesLeftUp.X, this.boundariesRightDown.X);
                int randomY = random.Next(this.boundariesLeftUp.Y, this.boundariesRightDown.Y);
                Point fruit = new Point(randomX, randomY);
                fruits.Add(fruit);
                whenLastfruitWasGenerated = DateTime.Now;
            }

            //Point head = snake.points.ElementAt(0);
            Point head = snake.points.First();
            // Check collision between snake head (first point of snake) and fruit
            foreach (Point fruit in fruits.ToList()) // Iterate over a copy of the list to avoid modifying it while iterating
            {
                if (head.X == fruit.X && head.Y == fruit.Y)
                {
                    // after eating the fruit the snake will be bigger (needs to move to increase the size, so when moving we will make it bigger)
                    snake.bigger = true;

                    // Remove the fruit from the list
                    fruits.Remove(fruit);
                }
            }

            // Check collision between snake head (first point of snake) and boundaries
            if (head.X >= (boundariesRightDown.X - 1) || head.X <= boundariesLeftUp.X || 
                head.Y >= (boundariesRightDown.Y - 1) || head.Y <= boundariesLeftUp.Y)
            {
                alive = false;
            }

            // Check collision between snake head and snake body
            foreach (Point point in snake.points.Skip(1)) //skip the head
            {
                if (head.X == point.X && head.Y == point.Y)
                {
                    alive = false;
                }
            }
        }

    }
    
}
