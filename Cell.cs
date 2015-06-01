using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maze
{
    class Cell
    {
        private int[] position;
        private int[] previous;
        private int previousPoint;
        private int point;

        public Cell(int horizontal, int vertical)
        {
            position = new int[] { horizontal, vertical };
            previous = new int[] { horizontal, vertical };
            previousPoint = 0;
            point = 0;
        }
        public Cell(Cell cell)
        {
            position = new int[] { cell.getPosition()[0], cell.getPosition()[1] };
            previous = new int[] { cell.getPrevious()[0], cell.getPrevious()[1] };
            previousPoint = cell.getPreviousPoint();
            point = cell.getPoint();
        }
        public int[] getPosition() { return position; }
        public void setPosition(int horizontal, int vertical)
        {
            position[0] = horizontal;
            position[1] = vertical;
        }

        public int getPoint() { return point; }
        public void setPoint(int newPoint) 
        { 
            point = newPoint; 
        }

        public int[] getPrevious() { return previous; }
        public void setPrevious(int horizontal, int vertical) 
        {
            previous[0] = horizontal;
            previous[1] = vertical;
        }

        public int getPreviousPoint() { return previousPoint; }
        public void setPreviousPoint(int newPoint)
        {
            previousPoint = newPoint;
        }

        public static bool operator!=(Cell lhs, Cell rhs)
        {
            if (lhs.getPosition()[0] == rhs.getPosition()[0] &&
                lhs.getPosition()[1] == rhs.getPosition()[1])
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public static bool operator==(Cell lhs, Cell rhs)
        {
            if (lhs.getPosition()[0] == rhs.getPosition()[0] &&
                lhs.getPosition()[1] == rhs.getPosition()[1])
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
