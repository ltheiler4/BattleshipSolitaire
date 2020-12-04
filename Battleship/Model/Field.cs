using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship.Model
{
    public class Field
    { 
        public int RowCoordinate { get; set; }
        public int ColumnCoordinate { get; set; }
        public bool IsShip { get; set; }

        public Field(int randomRow, int randomColumn, bool isShip)
        {
            RowCoordinate = randomRow;
            ColumnCoordinate = randomColumn;
            IsShip = isShip;
        }
    }
}
