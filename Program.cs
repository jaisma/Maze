using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maze
{
    class Program
    {
        static void Main(string[] args)
        {
            //string fileLocation = "file location";
            //string[] input = System.IO.File.ReadAllLines(fileLocation);
            //string[] input = System.IO.File.ReadAllLines(@"C:\Users\Jai\Documents\Visual Studio 2013\Projects\Maze\Maze.txt");
            string[] input = System.IO.File.ReadAllLines(@"C:\Users\Jai\Documents\Visual Studio 2013\Projects\Maze\OpenMaze.txt");
            //string[] input = System.IO.File.ReadAllLines(@"C:\Users\Jai\Documents\Visual Studio 2013\Projects\Maze\NoPathMaze.txt");
            //string[] input = System.IO.File.ReadAllLines(@"C:\Users\Jai\Documents\Visual Studio 2013\Projects\Maze\MultipleStartMaze.txt");
            //string[] input = System.IO.File.ReadAllLines(@"C:\Users\Jai\Documents\Visual Studio 2013\Projects\Maze\AdjacentMaze.txt");
            //string[] input = System.IO.File.ReadAllLines(@"C:\Users\Jai\Documents\Visual Studio 2013\Projects\Maze\ZigZagMaze.txt");
            Maze maze = new Maze(input);
            Cell testing = maze.getStartCell();
            while (maze.getStatus() == false) 
            { 
                testing = maze.move(testing); 
                maze.explore(testing); 
            }
            maze.findPath(testing);
        }
    }
}
