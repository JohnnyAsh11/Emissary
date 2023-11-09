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
    /// Inventory class for the player's held items
    /// </summary>
    public class Inventory
    {
        //Fields:
        private InventorySlot[][] inventorySlots;
        private MouseState mState;
        private KeyboardState prevKBState;
        private MouseState prevMState;
        private bool isOpen;

        private Rectangle position;
        private Rectangle playerInventory;
        private Rectangle hotbarPosition;

        private Texture2D inventory;
        private Texture2D inventoryCursor;

        //Properties:
        //get property for whether the inventory is open
        public bool IsOpen { get { return isOpen; } }

        //Constructors:
        /// <summary>
        /// Default constructor for the inventory class
        /// </summary>
        public Inventory()
        {
            //initalizing the Jagged array
            this.inventorySlots = new InventorySlot[5][];
            inventorySlots[0] = new InventorySlot[8];
            inventorySlots[1] = new InventorySlot[8];
            inventorySlots[2] = new InventorySlot[8];
            inventorySlots[3] = new InventorySlot[6];
            inventorySlots[4] = new InventorySlot[10];

            position = new Rectangle(496, 325, 609, 351);
            playerInventory = new Rectangle(592, 402, 50, 50);
            hotbarPosition = new Rectangle(0, 0, 507, 80);

            inventory = Globals.GameTextures["Inventory"];
            inventoryCursor = Globals.GameTextures["InventoryCursor"];

            CreateInventory();
        }

        //Methods:
        /// <summary>
        /// creates all inventory slots
        /// </summary>
        private void CreateInventory()
        {
            for (int y = 0; y < inventorySlots.Length; y++)
            {
                for (int x = 0; x < inventorySlots[y].Length; x++)
                {
                    inventorySlots[y][x] = new InventorySlot(new Rectangle());
                }
            }

            inventorySlots[0][0] = new InventorySlot(new Rectangle(518, 347, 50, 50));
            inventorySlots[1][0] = new InventorySlot(new Rectangle(518, 407, 50, 50));
            inventorySlots[2][0] = new InventorySlot(new Rectangle(518, 467, 50, 50));

            inventorySlots[0][1] = new InventorySlot(new Rectangle(663, 347, 50, 50));
            inventorySlots[1][1] = new InventorySlot(new Rectangle(663, 407, 50, 50));
            inventorySlots[2][1] = new InventorySlot(new Rectangle(663, 467, 50, 50));

            //variables to assist with position instantiation
            int yPosition = 347;
            int xPosition = 745;

            //creating the larger inventory grid
            for (int y = 0; y < 3; y++)
            {
                for (int x = 2; x < inventorySlots[y].Length; x++)
                {
                    inventorySlots[y][x] =
                        new InventorySlot(new Rectangle(xPosition, yPosition, 50, 50));

                    xPosition += 57;
                }
                xPosition = 745;
                yPosition += 58;
            }

            //during execution, xPosition should already be set to 745
            //creating the bottom of the big grid
            for (int i = 0; i < inventorySlots[3].Length; i++)
            {
                inventorySlots[3][i] = new InventorySlot(
                    new Rectangle(xPosition, 521, 50, 50));

                xPosition += 57;
            }

            //reset X position variable to fit new needs
            xPosition = 518;

            //creating the last hand slots of the inventory
            for (int i = 0; i < inventorySlots[4].Length; i++)
            {
                inventorySlots[4][i] = new InventorySlot(
                    new Rectangle(xPosition, 603, 50, 50));

                xPosition += 57;
            }
        }

        /// <summary>
        /// per frame update method for the Inventory Class
        /// </summary>
        public void Update()
        {
            KeyboardState kbState = Keyboard.GetState();
            mState = Mouse.GetState();

            //checking for the inventory being opened
            if (kbState.IsKeyDown(Keys.E) &&
                prevKBState.IsKeyUp(Keys.E) && isOpen)
            {
                isOpen = false;
            }
            else if (kbState.IsKeyDown(Keys.E) &&
                     prevKBState.IsKeyUp(Keys.E))
            {
                isOpen = true;
            }

            //Updating all the inventory slots
            for (int y = 0; y < inventorySlots.Length; y++)
            {
                for (int x = 0; x < inventorySlots[y].Length; x++)
                {
                    inventorySlots[y][x].Update();
                }
            }

            //item movement
            if (mState.LeftButton == ButtonState.Pressed &&
                prevMState.LeftButton == ButtonState.Released &&
                isOpen)
            {
                for (int y = 0; y < inventorySlots.Length; y++)
                {
                    for (int x = 0; x < inventorySlots[y].Length; x++)
                    {
                        if (inventorySlots[y][x].Position.Contains(
                            mState.Position))
                        {
                            inventorySlots[y][x].IsClicked = true;
                        }
                    }
                }
            }

            prevMState = mState;
            prevKBState = kbState;
        }

        /// <summary>
        /// Draw method for the Inventory Class
        /// </summary>
        public void Draw()
        {
            Globals.SB.Begin();

            if (isOpen)
            {
                Globals.SB.Draw(
                    inventory,
                    position,
                    Color.White);
                Globals.SB.Draw(
                    Globals.GameTextures["Player"],
                    playerInventory,
                    new Rectangle(0, 0, 90, 90),
                    Color.White);

                for (int y = 0; y < inventorySlots.Length; y++)
                {
                    for (int x = 0; x < inventorySlots[y].Length; x++)
                    {
                        inventorySlots[y][x].Draw();
                    }
                }
            }

            Globals.SB.Draw(
                Globals.GameTextures["InventoryBar"],
                hotbarPosition,
                Color.White);

            Globals.SB.End();
            for (int i = 0; i < inventorySlots[4].Length; i++)
            {
                if (inventorySlots[4][i].StoredItem != null)
                {
                    inventorySlots[4][i].StoredItem.Draw(
                        Globals.HotbarPositions[i + 1]);
                }
            }

        }

        /// <summary>
        /// gathers all references from the jagged array into a list for other classes to access
        /// </summary>
        /// <returns>a list of all slots in the inventory</returns>
        public List<InventorySlot> GiveInventorySlots()
        {
            List<InventorySlot> slots = new List<InventorySlot>();

            for (int y = 0; y < inventorySlots.Length; y++)
            {
                for (int x = 0; x < inventorySlots[y].Length; x++)
                {
                    slots.Add(inventorySlots[y][x]);
                }
            }

            return slots;
        }
    }
}
