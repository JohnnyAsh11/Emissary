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
    /// Contains useful data or methods that the entire program will use
    /// </summary>
    public static class Globals
    {

        //Properties:

        public static SpriteBatch SB { get; set; }

        public static Dictionary<string, Texture2D> GameTextures { get; set; }

        //Methods: - NONE -

    }
}
