using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Emissary
{
    public struct FloatRectangle
    {

        //Fields:
        private float x;
        private float y;
        private float width;
        private float height;

        //Properties:
        public Vector2 Position
        {
            get { return new Vector2(x, y); }
            set 
            { 
                x = value.X;
                y = value.Y;
            }
        }

        public float Height
        {
            get { return height; }
            set { height = value; }
        }

        public float Width
        {
            get { return width; }
            set { width = value; }
        }

        public float X
        {
            get { return x; }
            set { x = value; }
        }

        public float Y
        {
            get { return y; }
            set { y = value; }
        }

        public float TopRight { get { return x + width; } }
        public float BottomRight { get { return (x + width) + height; } }
        public float BottomLeft { get { return x + height; } }

        //Constructors:
        /// <summary>
        /// Parameterized constructor containing width/height
        /// </summary>
        /// <param name="x">Top left X position of the FloatRectangle</param>
        /// <param name="y">Top left Y position of the FloatRectangle</param>
        /// <param name="width">width of the FloatRectangle</param>
        /// <param name="height">height of the FloatRectangle</param>
        public FloatRectangle(float x, float y, float width, float height)
        {
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
        }

        /// <summary>
        /// Parameterized constructor containing default width/height of 50
        /// </summary>
        /// <param name="x">Top left X position</param>
        /// <param name="y">Top left Y position</param>
        public FloatRectangle(float x, float y)
        {
            this.x = x;
            this.y = y;
            width = 50f;
            height = 50f;
        }

        /// <summary>
        /// Parameterized constructor containing Vector2s instead of individual float values
        /// </summary>
        /// <param name="location">Contains top left X and Y positioning</param>
        /// <param name="size">Contains width/height information</param>
        public FloatRectangle(Vector2 location, Vector2 size)
        {
            this.x = location.X;
            this.y = location.Y;
            this.width = size.X;
            this.height = size.Y;
        }

        //Methods:
        /// <summary>
        /// Checks if the FloatRectangle contains a point
        /// </summary>
        /// <param name="location">Point being checked</param>
        /// <returns>Whether or not the Point is in the FloatRectangle</returns>
        public bool Contains(Point location)
        {
            if (location.X < TopRight && location.X > x &&
                location.Y < BottomLeft && location.Y > y)
            {
                return true;
            }
            return false;
        }

        //Operator Overloads:
        /// <summary>
        /// Adds the X and Y values of a Vector2 to the X and Y coordinates of the FloatRectangle
        /// </summary>
        /// <param name="rect">Rectangle having its values changed</param>
        /// <param name="changeInPosition">positional change</param>
        /// <returns>A FloatRectangle with the positional updates</returns>
        public static FloatRectangle operator +(FloatRectangle rect, Vector2 changeInPosition)
        {
            float newX = rect.x + changeInPosition.X;
            float newY = rect.y + changeInPosition.Y;

            return new FloatRectangle(newX, newY, rect.Width, rect.Height);
        }

        /// <summary>
        /// Performs an AABB collision check against a MonoGame Rectangle
        /// </summary>
        /// <param name="rect">Rectangle being checked against</param>
        /// <returns>whether or not a collision has occured</returns>
        public bool AABBCheck(Rectangle rect)
        {
            //Determining whether a collision has occurred
            if (rect.X < (this.x + this.width) &&
                (rect.X + rect.Width) > this.x &&
                rect.Y < (this.y + this.height) &&
                (rect.Y + rect.Height) > this.y)
            {
                return true;
            }
            return false;
        }

    }
}
