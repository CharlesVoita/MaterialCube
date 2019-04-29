using UnityEditor;
using UnityEngine;

namespace Global.Structs
{
    public struct Coords
    {
        public float x, y;

        public Coords(float x, float y)
        {
            this.x = x;
            this.y = y;
        }

        // TODO log object name
        public void log()
        {
            Debug.Log("Coords -- X: '"+ x + "', Y: '"+ y + "'");
        }

        public void add(float x, float y)
        {
            this.x += x;
            this.y += y;
        }
        
        public Coords returnAdd(float x, float y)
        {
            return new Coords(this.x + x, this.y + y); 
        }

        public bool isZero()
        {
            return (int) x == 0 && (int) y == 0;
        }
    }
    
    public struct IntCoords
    {
        public int x, y;

        public IntCoords(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        // TODO log object name
        public void log()
        {
            Debug.Log("Coords -- X: '"+ x + "', Y: '"+ y + "'");
        }

        public void add(int x, int y)
        {
            this.x += x;
            this.y += y;
        }
        
        public IntCoords returnAdd(int x, int y)
        {
            return new IntCoords(this.x + x, this.y + y); 
        }

        public bool isZero()
        {
            return x == 0 && y == 0;
        }
    }
}