using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Reflection;

namespace Emissary
{
    /// <summary>
    /// Contains useful data or methods that the entire program will use
    /// </summary>
    public static class Globals
    {
        //Fields:
        private static SpriteBatch spriteBatch;
        private static SpriteFont debugFont;
        private static GameTime time;
        private static Dictionary<TileTexture, Rectangle> tileSources;
        private static Dictionary<string, Texture2D> gameTextures;
        private static Dictionary<int, Rectangle> hotbarPositions;

        //Properties:
        //Reference to the time object
        public static GameTime Time
        {
            get { return time; }
            set { time = value; }
        }

        //getter/setter for the Sprite render class in MonoGame
        public static SpriteBatch SB
        {
            get { return spriteBatch; }
            set { spriteBatch = value; }
        }
        //Getter/Setter for the Sprite Font that I use for debugging
        public static SpriteFont SF
        {
            get { return debugFont; }
            set { debugFont = value; }
        }

        //Getter/Setter for the Dictionary that holds all sprites for the game
        public static Dictionary<string, Texture2D> GameTextures
        {
            get { return gameTextures; }
            set { gameTextures = value; }
        }

        //Getter/Setter for the Dictionary that holds the rectangle locations of the Hotbar
        public static Dictionary<int, Rectangle> HotbarPositions
        {
            get { return hotbarPositions; }
            set { hotbarPositions = value; }
        }

        //getter/setter for the Tileset source rectangles as they match their respective enumeration
        public static Dictionary<TileTexture, Rectangle> PrintTiles
        {
            get { return tileSources; }
            set { tileSources = value; }
        }

        //Methods:
        #region Bitpacking methods
        /// <summary>
        /// Checks true/false values held within bits
        /// </summary>
        /// <param name="data">Byte containing data</param>
        /// <param name="index">Index of bit being checked in byte</param>
        /// <returns>the value held at that index</returns>
        public static bool ReadData(byte data, int index)
        {
            //checking index bounds
            if (index >= 0 && index <= 7)
            {
                //getting the index being checked in byte form
                byte indexChecked = (byte)(1 << index);

                //looping through all indices of a byte
                for (byte i = (byte)1 << 7; i > 0; i >>= 1)
                {
                    //if the current iteration matches the index we are attempting to check
                    if ((i & indexChecked) == indexChecked)
                    {
                        //check the data against i and return
                        // true or false accordingly
                        if ((data & i) == i)
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

            //default result
            return false;
        }

        /// <summary>
        /// Inserts true/false data into a bit
        /// </summary>
        /// <param name="data">Byte containing true/false data</param>
        /// <param name="index">Index of bit being changed</param>
        /// <param name="value">value of index being changed</param>
        /// <returns>The new Byte of data</returns>
        public static byte PackData(byte data, int index, bool value)
        {
            //default value
            byte newData = 0;

            //checking to make sure index is in bounds
            if (index >= 0 && index <= 7)
            {
                //placing a 1 at the location being altered
                byte changes = (byte)(1 << index);

                //if the value is being changed to 0 then filp everything and change
                //operator to AND
                if (!value)
                {
                    changes = (byte)~changes;
                    newData = (byte)(data & changes);
                }
                else
                {
                    //changing the passed in data specifically with OR
                    newData = (byte)(data | changes);
                }
                //returning the new data
                return newData;
            }
            else
            {
                return newData;
            }
        }

        /// <summary>
        /// FOR BYTES WITH ONLY 1 TRUE VALUE
        /// </summary>
        /// <param name="data">byte being searched</param>
        /// <returns>the index of the single true value</returns>
        public static int IndexAtTrue(byte data)
        {
            int index = 7;

            for (byte i = 1 << 7; i > 0; i >>= 1)
            {
                if ((data & i) == data)
                {
                    return index;
                }

                index--;
            }

            //default fail value
            return -1;
        }
        #endregion

    }
}
