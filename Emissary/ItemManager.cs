using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emissary
{
    public delegate List<InventorySlot> GetSlots();

    /// <summary>
    /// Manages all Items created in the game
    /// </summary>
    public class ItemManager
    {
        //Fields:
        private List<Item> gameItems;
        protected MouseState mState;
        protected MouseState prevMState;

        public event GetSlots GetInventorySlots;

        //Properties: - NONE -

        //Constructors:
        /// <summary>
        /// Default constructor for the ItemManager class
        /// </summary>
        public ItemManager()
        {
            gameItems = new List<Item>();
        }

        //Methods:
        /// <summary>
        /// Per frame Update Method for the ItemManager class
        /// </summary>
        public void Update()
        {
            mState = Mouse.GetState();

            List<InventorySlot> slots = null!;
            if (GetInventorySlots != null)
            {
                slots = GetInventorySlots();
            }

            //updating all Items in the game
            foreach (Item gameItem in gameItems)
            {
                gameItem.Update();
            }

            //for putting items back into slots after picking them up
            foreach (Item gameItem in gameItems)
            {
                if (gameItem.IsMoving &&
                    slots != null)
                {
                    foreach (InventorySlot slot in slots)
                    {
                        if (slot.StoredItem == null &&
                            slot.ItemPosition.Contains(mState.Position) &&
                            mState.LeftButton == ButtonState.Pressed &&
                            prevMState.LeftButton == ButtonState.Released)
                        {
                            slot.StoredItem = gameItem;
                            gameItem.Position = slot.ItemPosition;
                            slot.IsClicked = false;
                            gameItem.IsMoving = false;
                        }
                    }
                }
            }

            prevMState = mState;
        }

        /// <summary>
        /// Draw method for the ItemManager class -> draws all Items with their own Draw method
        /// </summary>
        public void Draw()
        {
            //Drawing each type of item
            foreach (Item gameItem in gameItems)
            {
                //if (gameItem is Weapon)
                //{
                //    Weapon weaponItem = (Weapon)gameItem;

                //    weaponItem.Draw();
                //}
                //else
                //{
                gameItem.Draw();
                //}
            }
        }

        /// <summary>
        /// puts an item in an inventory slot for testing
        /// </summary>
        public void TestMethod()
        {
            if (GetInventorySlots != null)
            {
                List<InventorySlot> slots = GetInventorySlots();

                gameItems.Add(new Item(slots[1].ItemPosition, Globals.GameTextures["DebugImage"]));

                slots[1].StoredItem = gameItems[0];
            }
        }


    }
}
