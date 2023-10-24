using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Emissary
{
    /// <summary>
    /// Item class that creates GameObjects for the player to collect
    /// </summary>
    public class Item
    {
        //Fields:
        protected bool isMoving;
        protected MouseState mState;

        private Rectangle position;
        private Texture2D asset;
        
        //Properties:        
        public bool IsMoving
        {
            get { return isMoving; }
            set { isMoving = value; }
        }
        
        public Rectangle Position
        {
            get { return position; }
            set { position = value; }
        }

        //Constructors:
        /// <summary>
        /// Parameterized constructor for the Item class
        /// </summary>
        /// <param name="position">rectangle position for the Item class</param>
        /// <param name="type">Type of item</param>
        public Item(Rectangle position, Texture2D asset)
        {
            this.position = position;
            this.asset = asset;
        }
        
        //Methods:
        //--------------------------------------------------------------
        //          not sure if Ill need this method
        /// <summary>
        /// Per frame update method for the item class
        /// </summary>
        public virtual void Update()
        {

        }
        //--------------------------------------------------------------

        /// <summary>
        /// draw method for the Item class
        /// </summary>
        public virtual void Draw()
        {
            Globals.SB.Begin();
            mState = Mouse.GetState();

            if (!isMoving)
            {
                Globals.SB.Draw(
                    asset,
                    position,
                    null,
                    Color.White);
            }
            else
            {
                Globals.SB.Draw(
                    asset,
                    new Rectangle(mState.Position, new Point(50, 50)),
                    null,
                    Color.White);
            }

            Globals.SB.End();
        }

        /// <summary>
        /// Overload draw method for the Item class
        /// </summary>
        /// <param name="position">new position for the drawing of hotbar items</param>
        public virtual void Draw(Rectangle position)
        {
            Globals.SB.Begin();
            Globals.SB.Draw(
                    asset,
                    position,
                    null,
                    Color.White);
            Globals.SB.End();
        }
    }
}
