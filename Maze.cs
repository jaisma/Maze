using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maze
{
    class Maze
    {
        private string[] maze;
        private static Cell start;
        private static Cell finish;
        private static List<Cell> openList;
        private static List<Cell> closedList;
        private static List<Cell> validPath;
        private static int lengthCount;
        private static int widthCount;
        private static bool status;
        
        public Maze (string[] input)
        {
            this.maze = input;
            status = false;
            openList = new List<Cell> ();
            closedList = new List<Cell> ();
            validPath = new List<Cell> ();
            int horizontal = 0;
            int vertical = 0;
            foreach (string line in maze)
            {
                foreach (char a in line)
                {
                    if (a == 'S') { start = new Cell(horizontal, vertical); }
                    else if (a == 'F') { finish = new Cell(horizontal, vertical); }
                    horizontal++;
                }
                horizontal = 0;
                vertical++;
            }
            widthCount = maze[0].Length;
            lengthCount = vertical;
            start.setPreviousPoint(0);
            pointCalculator(start);
            validPath.Add(start);
            firstStep(start);
        }

        public void firstStep(Cell currentCell)
        {
            int x = currentCell.getPosition()[0];
            int y = currentCell.getPosition()[1];
            if (x == 0) // when left starting
            {
                if (maze[y][x + 1] == '0')
                {
                    Cell rightCell = new Cell(x + 1, y);
                    pointCalculator(rightCell);
                    rightCell.setPrevious(x, y);
                    rightCell.setPreviousPoint(start.getPoint());
                    openList.Add(rightCell);
                    validPath.Add(rightCell);
                }
            }
            else if (x == widthCount) // when right starting
            {
                if (maze[y][x - 1] == '0')
                {
                    Cell leftCell = new Cell(x - 1, y);
                    pointCalculator(leftCell);
                    leftCell.setPrevious(x, y);
                    leftCell.setPreviousPoint(start.getPoint());
                    openList.Add(leftCell);
                    validPath.Add(leftCell);
                }
            }
            else if (y == 0) // when top starting
            {
                if (maze[y + 1][x] == '0')
                {
                    Cell botCell = new Cell(x, y + 1);
                    pointCalculator(botCell);
                    botCell.setPrevious(x, y);
                    botCell.setPreviousPoint(start.getPoint());
                    openList.Add(botCell);
                    validPath.Add(botCell);
                }
            }
            else if (y == lengthCount) // when on bot starting
            {
                if (maze[y - 1][x] == '0')
                {
                    Cell topCell = new Cell(x, y - 1);
                    pointCalculator(topCell);
                    topCell.setPrevious(x, y);
                    topCell.setPreviousPoint(start.getPoint());
                    openList.Add(topCell);
                    validPath.Add(topCell);
                }
            }
        }

        public Cell getStartCell() { return start; }

        public Cell getFinishCell() { return finish; }

        public bool getStatus() { return status; }

        public Cell pointCalculator(Cell currentCell)
        {
            int x = Math.Abs(currentCell.getPosition()[0] - finish.getPosition()[0]);
            int y = Math.Abs(currentCell.getPosition()[1] - finish.getPosition()[1]);
            int n = Math.Abs(currentCell.getPosition()[0] - start.getPosition()[0]);
            int m = Math.Abs(currentCell.getPosition()[1] - start.getPosition()[1]);
            int u = currentCell.getPreviousPoint() + 1;
            int z = x + y + n + m + u;
            currentCell.setPoint(z);
            return currentCell;
        }

        public void explore(Cell currentCell)
        {
            int x = currentCell.getPosition()[0];
            int y = currentCell.getPosition()[1];
            if (maze[y][x - 1] == '0')
            {
                Cell leftCell = new Cell(x - 1, y);
                //pointCalculator(leftCell);
                if (!closedList.Exists(b => b == leftCell) && (!openList.Exists(b => b == leftCell)))
                {
                    leftCell.setPrevious(x, y);
                    leftCell.setPreviousPoint(currentCell.getPoint());
                    leftCell = pointCalculator(leftCell);
                    openList.Add(leftCell);
                }
            }
            if (maze[y][x + 1] == '0')
            {
                Cell rightCell = new Cell(x + 1, y);
                //pointCalculator(rightCell);
                if (!closedList.Exists(b => b == rightCell) && (!openList.Exists(b => b == rightCell)))
                {
                    rightCell.setPrevious(x, y);
                    rightCell.setPreviousPoint(currentCell.getPoint());
                    rightCell = pointCalculator(rightCell);
                    openList.Add(rightCell);
                }
            }
            if (maze[y - 1][x] == '0')
            {
                Cell topCell = new Cell(x, y - 1);
                //pointCalculator(topCell);
                if (!closedList.Exists(b => b == topCell) && (!openList.Exists(b => b == topCell)))
                {
                    topCell.setPrevious(x, y);
                    topCell.setPreviousPoint(currentCell.getPoint());
                    topCell = pointCalculator(topCell);
                    openList.Add(topCell);
                }
            }
            if (maze[y + 1][x] == '0')
            {
                Cell botCell = new Cell(x, y + 1);
                //pointCalculator(botCell);
                if (!closedList.Exists(b => b == botCell) && (!openList.Exists(b => b == botCell)))
                {
                    botCell.setPrevious(x, y);
                    botCell.setPreviousPoint(currentCell.getPoint());
                    botCell = pointCalculator(botCell);
                    openList.Add(botCell);
                }
            }
            if (maze[y][x - 1] == 'F' || maze[y][x + 1] == 'F' || maze[y - 1][x] == 'F' || maze[y + 1][x] == 'F')
            {
                //check
                ///Console.WriteLine("Finished the maze!");
                finish.setPrevious(x, y);
                status = true;
            }
        }
        
        public Cell getLowestPoint()
        {
            Cell lowest = openList[0];
            foreach (Cell cell in openList)
            {
                if (cell.getPoint() <= lowest.getPoint()) 
                { 
                    lowest = cell;
                    return lowest;
                }
            }
            return lowest;
        }

        public Cell move(Cell currentCell)
        {
            ////check
            //int size = openList.Count();
            //Console.WriteLine("Current openList size: " + size);
            //int temp = 0;
            //foreach (Cell cell in openList)
            //{
            //    temp++;
            //    Console.Write("#" + temp + "= x:" + cell.getPosition()[0] +
            //        " y:" + cell.getPosition()[1] + " Point:" + cell.getPoint());
            //    Console.WriteLine("\tPrevious= x:" + cell.getPrevious()[0] +
            //        " y:" + cell.getPrevious()[1]);
            //}
            ////end check

            Cell lowest = new Cell(getLowestPoint());
            if (!closedList.Exists(x => x == currentCell))
            {
                closedList.Add(currentCell);
            }
            foreach (Cell cell in openList)
            {
                if (lowest == cell)
                {
                    openList.Remove(cell);
                    break;
                }
            }
            currentCell = lowest;

            ////check
            //size = closedList.Count();
            //Console.WriteLine("Current closedList size: " + size);
            //temp = 0;
            //foreach (Cell cell in closedList)
            //{
            //    temp++;
            //    Console.Write("#" + temp + "= x:" + cell.getPosition()[0] +
            //        " y:" + cell.getPosition()[1] + " Point:" + cell.getPoint());
            //    Console.WriteLine("\tPrevious= x:" + cell.getPrevious()[0] +
            //        " y:" + cell.getPrevious()[1]);
            //}
            ////end check

            return currentCell;
        }
        public void findPath(Cell womp)
        {
            Console.WriteLine("Path Finding, backwards tracking: \nKEY: x = horizontal, y = vertical\n");
            Console.WriteLine("Finish\nx:" + finish.getPosition()[0] + " y:" + finish.getPosition()[1]);
            Console.WriteLine("x:" + finish.getPrevious()[0] + " y:" + finish.getPrevious()[1]);
            Cell track = new Cell(womp);
            int a = track.getPrevious()[0];
            int b = track.getPrevious()[1];
            while (a != start.getPosition()[0] && b != start.getPosition()[1])
            {
                Console.WriteLine("x:" + a + " y:" + b);
                foreach (Cell cell in closedList)
                {
                    if (cell.getPosition()[0] == a && cell.getPosition()[1] == b)
                    {
                        validPath.Add(cell);
                        //track = cell;
                        //a = cell.getPrevious()[0];
                        //b = cell.getPrevious()[1];
                        track = cell;
                        track.setPosition(cell.getPosition()[0], cell.getPosition()[1]);
                        track.setPrevious(cell.getPrevious()[0], cell.getPrevious()[1]);
                        a = track.getPrevious()[0];
                        b = track.getPrevious()[1];
                    }
                    //if (cell == closedList[closedList.Count()-1])
                    //{
                        
                    //}
                }
            }
            int check = 0;
            if (b == 1)
            {
                int temp = a;
                while (check <= temp)
                {
                    foreach (Cell cell in closedList)
                    {
                        if (cell.getPosition()[0] == a && cell.getPosition()[1] == b)
                        {
                            validPath.Add(cell);
                            Console.WriteLine("x:" + a + " y:" + b);
                            //track = cell;
                            //a = cell.getPrevious()[0];
                            //b = cell.getPrevious()[1];
                            track = cell;
                            track.setPosition(cell.getPosition()[0], cell.getPosition()[1]);
                            track.setPrevious(cell.getPrevious()[0], cell.getPrevious()[1]);
                            a = track.getPrevious()[0];
                            b = track.getPrevious()[1];
                        }

                    }
                    check++;
                }
            }
            if (a == 1)
            {
                int temp = b;
                while (check <= temp)
                {
                    foreach (Cell cell in closedList)
                    {
                        if (cell.getPosition()[0] == a && cell.getPosition()[1] == b)
                        {
                            validPath.Add(cell);
                            Console.WriteLine("x:" + a + " y:" + b);
                            //track = cell;
                            //a = cell.getPrevious()[0];
                            //b = cell.getPrevious()[1];
                            track = cell;
                            track.setPosition(cell.getPosition()[0], cell.getPosition()[1]);
                            track.setPrevious(cell.getPrevious()[0], cell.getPrevious()[1]);
                            a = track.getPrevious()[0];
                            b = track.getPrevious()[1];
                        }

                    }
                    check++;
                }
            }
            Console.WriteLine("Start\n");
            //Console.WriteLine("x:" + closedList[0].getPosition()[0] + " y:" + closedList[0].getPosition()[1] + " Start\n");

            //foreach (Cell cell in validPath)
            //{
            //    Console.WriteLine("x:" + cell.getPrevious()[0] + " y:" + cell.getPrevious()[1]);
            //}
        }
    }
}
