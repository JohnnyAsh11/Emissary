using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Emissary
{
    /// <summary>
    /// Abstract class for anything that exists in the game. >>Does NOT use the Rectangle struct<<
    /// </summary>
    public abstract class GameObject
    {

        //fields:
        protected FloatRectangle hitbox;
        protected Texture2D asset;

        //Properties:
        //get/set property for the object's position
        public Vector2 Position
        {
            get { return hitbox.Position; }
            set { hitbox.Position = value; }
        }

        public float X
        {
            get { return hitbox.X; }
            set { hitbox.X = value; }
        }
        public float Y
        {
            get { return hitbox.Y; }
            set { hitbox.Y = value; }
        }

        //Constructors:
        /// <summary>
        /// Parameterized constructor for all GameObjects
        /// </summary>
        /// <param name="position">Rectanglular position of the GameObject</param>
        /// <param name="asset">Rendered texture for the GameObject</param>
        public GameObject(FloatRectangle position, Texture2D asset)
        {
            this.hitbox = position;
            this.asset = asset;
        }

        //Methods:
        /// <summary>
        /// Per frame logic update method
        /// </summary>
        public abstract void Update();

        /// <summary>
        /// Generic draw method for all GameObjects
        /// </summary>
        public virtual void Draw()
        {
            Globals.SB.Begin();
            Globals.SB.Draw(
                asset,
                hitbox.Position,
                Color.White);
            Globals.SB.End();
        }

    }
}
