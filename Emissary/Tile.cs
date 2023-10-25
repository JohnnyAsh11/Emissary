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
    /// Specifies the tile from the tileset being printed
    /// </summary>
    public enum TileTexture
    {
        //title      int value    src rectangle
        //(remember: int values technically start at 0)
        GrassMiddle1,   //1       (0, 0, 32, 32)
        GrassMiddle2,   //2       (32, 0, 32, 32)
        GrassMiddle3,   //3       (64, 0, 32, 32)
        GrassMiddle4,   //4       (96, 0, 32, 32)
        FlowerMiddle1,   //17     (128, 0, 32, 32)
        FlowerMiddle2,   //18     (160, 0, 32, 32)
        FlowerMiddle3,   //19     (192, 0, 32, 32)
        FlowerMiddle4,   //20     (224, 0, 32, 32)

        GrassMiddle5,   //5       (0, 32, 32, 32)
        GrassMiddle6,   //6       (32, 32, 32, 32)
        GrassMiddle7,   //7       (64, 32, 32, 32)
        GrassMiddle8,   //8       (96, 32, 32, 32)
        FlowerMiddle5,   //21     (128, 32, 32, 32)
        FlowerMiddle6,   //22     (160, 32, 32, 32)
        FlowerMiddle7,   //23     (192, 32, 32, 32)
        FlowerMiddle8,   //24     (224, 32, 32, 32)

        GrassMiddle9,   //9       (0, 64, 32, 32)
        GrassMiddle10,  //10      (32, 64, 32, 32)
        GrassMiddle11,  //11      (64, 64, 32, 32)
        GrassMiddle12,  //12      (96, 64, 32, 32)
        FlowerMiddle9,   //25     (128, 64, 32, 32)
        FlowerMiddle10,   //26    (160, 64, 32, 32)
        FlowerMiddle11,   //27    (192, 64, 32, 32)
        FlowerMiddle12,   //28    (224, 64, 32, 32)

        GrassMiddle13,  //13      (0, 96, 32, 32)
        GrassMiddle14,  //14      (32, 96, 32, 32)
        GrassMiddle15,  //15      (64, 96, 32, 32)
        GrassMiddle16,  //16      (96, 96, 32, 32)       
        FlowerMiddle13,   //29    (128, 96, 32, 32)
        FlowerMiddle14,   //30    (160, 96, 32, 32)
        FlowerMiddle15,   //31    (192, 96, 32, 32)
        FlowerMiddle16,   //32    (224, 96, 32, 32)

        Path1,            //33    (0, 128, 32, 32)
        Path2,            //34    (32, 128, 32, 32)
        Path3,            //35    (64, 128, 32, 32)
        Path4,            //36    (96, 128, 32, 32)
        Path17,           //49    (128, 128, 32, 32)
        Path18,           //50    (160, 128, 32, 32)
        Path19,           //51    (192, 128, 32, 32)
        Path20,           //52    (224, 128, 32, 32)

        Path5,            //37    (0, 160, 32, 32)
        Path6,            //38    (32, 160, 32, 32)
        Path7,            //39    (64, 160, 32, 32)
        Path8,            //40    (96, 160, 32, 32)
        Path21,           //53    (128, 160, 32, 32)
        Path22,           //54    (160, 160, 32, 32)
        Path23,           //55    (192, 160, 32, 32)
        Path24,           //56    (224, 160, 32, 32)

        Path9,            //41    (0, 192, 32, 32)
        Path10,           //42    (32, 192, 32, 32)
        Path11,           //43    (64, 192, 32, 32)
        Path12,           //44    (96, 192, 32, 32)
        Path25,           //57    (128, 192, 32, 32)
        Path26,           //58    (160, 192, 32, 32)
        Path27,           //59    (192, 192, 32, 32)
        Path28,           //60    (224, 192, 32, 32)

        Path13,           //45    (0, 224, 32, 32)
        Path14,           //46    (32, 224, 32, 32)
        Path15,           //47    (64, 224, 32, 32)
        Path16,           //48    (96, 224, 32, 32)
        Path29,           //61    (128, 224, 32, 32)
        Path30,           //62    (160, 224, 32, 32)
        Path31,           //63    (192, 224, 32, 32)
        Path32,           //64    (224, 224, 32, 32)
    }

    public class Tile
    {

        //Fields:
        private TileTexture texture;
        private Texture2D tileset;
        private Rectangle position;
        private bool isCollidable;

        //Properties:
        public Rectangle Position { get { return position; } }

        //get/set property for whether or not the tile can be collided with
        public bool IsCollidable
        {
            get { return isCollidable; }
            set { isCollidable = value; }
        }


        //Constructors:
        /// <summary>
        /// Parameterized constructor for the Tile class
        /// </summary>
        /// <param name="texture">asset for this tile</param>
        /// <param name="position">Rectangular position of this tile</param>
        public Tile(TileTexture texture, Rectangle position)
        {
            this.texture = texture;
            this.position = position;
            tileset = Globals.GameTextures["GrassTiles"];
            this.isCollidable = false;
        }

        //Methods:
        /// <summary>
        /// Draw method for the individual tiles of the game
        /// </summary>
        public void Draw()
        {
            Globals.SB.Draw(
                Globals.GameTextures["GrassTiles"],
                position,
                Globals.PrintTiles[texture],
                Color.White);
        }

    }
}
