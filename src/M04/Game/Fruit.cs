﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    class Fruit : Coordinates
    {
        public string Name { get; private set; }
        public int Score { get; private set; }
        private CoordinateStructure _position;
        public Fruit(string name, int score, CoordinateStructure position)
        {
            if (name is { Length: > 0 })
            {
                Name = name;
                Score = score;
                _position = position;
            }
            else throw new ArgumentException("Name can not be null or empty");
        }
        public override CoordinateStructure GetCoordinates()
        {
            return _position;
        }
    }
}
