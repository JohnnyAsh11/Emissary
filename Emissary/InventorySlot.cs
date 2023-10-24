using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Emissary
{
    /// <summary>
    /// Index class for the Inventory class's jagged array.  Holds data for Player's collected Items
    /// </summary>
    public class InventorySlot
    {
        //Fields:
        private Item storedItem;
        private Rectangle position;
        private Rectangle itemPosition;

        private MouseState mState;
        private MouseState prevMState;
        private bool isHovering;
        private bool isClicked;

        //Properties:
        //get/set property for the item stored in the inventory slot
        public Item StoredItem
        {
            get { return storedItem; }
            set { storedItem = value; }
        }

        public Rectangle ItemPosition { get { return itemPosition; } }

        //get/set property for whether or not the slot was clicked on
        public bool IsClicked
        {
            get { return isClicked; }
            set { isClicked = value; }
        }

        //get/set for the slot's position
        public Rectangle Position
        {
            get { return position; }
            set { position = value; }
        }

        //Constructors:
        /// <summary>
        /// default constructor for the InventoryVertex class
        /// </summary>
        public InventorySlot(Rectangle position)
        {
            this.position = position;
            this.itemPosition = new Rectangle(position.X, position.Y, 50, 50);
            this.storedItem = null!;
            this.isHovering = false;
        }

        //Methods:
        /// <summary>
        /// Per frame update method for the InventoryVertex class
        /// </summary>
        public void Update()
        {
            mState = Mouse.GetState();

            if (position.Contains(mState.Position))
            {
                isHovering = true;
            }
            else
            {
                isHovering = false;
            }

            if (isClicked && storedItem != null!)
            {
                storedItem.IsMoving = true;
                storedItem = null!;
            }

            prevMState = mState;
        }

        /// <summary>
        /// Standard draw method for the object inside the inventory slot
        /// </summary>
        public void Draw()
        {
            if (isHovering)
            {
                Globals.SB.Draw(
                    Globals.GameTextures["InventoryCursor"],
                    this.position,
                    Color.White);
            }
        }
    }
}
