using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Emissary
{
    /// <summary>
    /// Handles creation of all tiles in a room
    /// </summary>
    public class TileManager
    {
        //Fields:
        public Tile[,] tiles;
        private int tilesX;
        private int tilesY;

        //Properties: - NONE -

        //Constructors:
        /// <summary>
        /// Parameterized constructor for the TileManager class
        /// </summary>
        /// <param name="filepath">filepath that leads to data file</param>
        public TileManager(string filepath)
        {
            this.tiles = new Tile[32, 20];
            this.tilesX = 32;
            this.tilesY = 20;

            CreateTiles(filepath);
            InitalizeCollidability(filepath);
        }
        
        //Methods:
        /// <summary>
        /// Determines from an external file which tiles will be collidable and not
        /// </summary>
        private void InitalizeCollidability(string filepath)
        {
            StreamReader reader = null!;

            try
            {
                reader = new StreamReader(filepath);
                string rawData;
                string[] splitData;
                int y = 0;

                //a loop to jump right to the collisions portion of the text file
                while ((rawData = reader.ReadLine()) != null)
                {
                    if (rawData == "COLLISIONS")
                    {
                        break;
                    }
                }

                //determining whether or not the tiles are collidable
                while ((rawData = reader.ReadLine()) != null)
                {
                    splitData = rawData.Split('|');

                    for (int x = 0; x < tilesX; x++)
                    {
                        if (splitData[x] == "1")
                        {
                            tiles[x, y].IsCollidable = true;
                        }
                        else
                        {
                            tiles[x, y].IsCollidable = false;
                        }
                    }

                    y++;
                }
            }
            catch (Exception error)
            {
                Debug.Print(error.Message);
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
            }
        }

        /// <summary>
        /// uses an external file to load in the tiles for this room
        /// </summary>
        /// <param name="filepath">Filepath to the data file</param>
        private void CreateTiles(string filepath)
        {
            StreamReader reader = null!;
            try
            {
                //opening a file and intializing the tiles in the 2D array
                reader = new StreamReader(filepath);
                Rectangle tilePosition = new Rectangle(0, 0, 50, 50);
                string rawData;
                string[] splitData;
                int ytileLocation = 0;

                //reading out the headline of the file
                reader.ReadLine();

                //looping until we cant and creating all tiles
                while ((rawData = reader.ReadLine()) != "COLLISIONS")
                {
                    splitData = rawData.Split('|');

                    for (int x = 0; x < tilesX; x++)
                    {
                        TileTexture texture =
                            (TileTexture)int.Parse(splitData[x]);

                        tiles[x, ytileLocation] = new Tile(
                                                    texture,
                                                    tilePosition);

                        tilePosition.X += 50;
                    }

                    tilePosition.X = 0;
                    tilePosition.Y += 50;
                    ytileLocation++;
                }

            }
            catch (Exception error)
            {
                Debug.Write(error.Message);
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
            }
        }

        /// <summary>
        /// compiles all colllidable rectangles in the current room to one list
        /// </summary>
        /// <returns>the list of currently collidable rectangles</returns>
        public List<Rectangle> GetCollidableTiles()
        {
            List<Rectangle> collidables = new List<Rectangle>();

            for (int x = 0; x < 32; x++)
            {
                for (int y = 0; y < 20; y++)
                {
                    if (tiles[x, y].IsCollidable)
                    {
                        collidables.Add(tiles[x, y].Position);
                    }
                }
            }

            return collidables;
        }

        /// <summary>
        /// Draw Method for the TileManager class
        /// </summary>
        public void Draw()
        {
            Globals.SB.Begin();
            for (int x = 0; x < 32; x++)
            {
                for (int y = 0; y < 20; y++)
                {
                    tiles[x, y].Draw();
                }
            }
            Globals.SB.End();
        }

    }
}
