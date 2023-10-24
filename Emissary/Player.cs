using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Emissary
{
    /// <summary>
    /// Animation states for the Player class
    /// </summary>
    public enum AnimationState
    {
        WalkingLeft,
        WalkingRight,
        WalkingUp,
        WalkingDown,
        Idle
    }

    /// <summary>
    /// Player object class:
    /// controls roughly everything regarding the playable character
    /// </summary>
    public class Player : GameObject
    {

        //Fields:

        //movement fields
        private Vector2 velocity;
        private Vector2 direction;

        //Input fields
        private KeyboardState kbState;

        //Animation fields
        private AnimationState aState;

        //Properties:

        //Constructors:
        /// <summary>
        /// Default constructor for the Player class
        /// </summary>
        public Player()
            : base(new Vector2(100, 100), Globals.GameTextures["Player"])
        {
            velocity = new Vector2(5, 5);
            direction = Vector2.Zero;
            aState = AnimationState.Idle;
        }

        //Methods:
        /// <summary>
        /// Per frame logic update method for the Player class
        /// </summary>
        public override void Update()
        {
            kbState = Keyboard.GetState();

            //setting x direction based on input
            if (kbState.IsKeyDown(Keys.A))
            {
                direction.X = -1;
                aState = AnimationState.WalkingLeft;
            }
            else if (kbState.IsKeyDown(Keys.D))
            {
                direction.X = 1;
                aState = AnimationState.WalkingRight;
            }
            else 
            { 
                direction.X = 0;
                aState = AnimationState.Idle;
            }

            //setting y direction based on input
            if (kbState.IsKeyDown(Keys.W))
            {
                direction.Y = -1;
                aState = AnimationState.WalkingUp;
            }
            else if (kbState.IsKeyDown(Keys.S))
            {
                direction.Y = 1;
                aState = AnimationState.WalkingDown;
            }
            else
            {
                //purposefully not setting AnimationState to Idle here
                direction.Y = 0;
            }

            //calculating the new position
            position += direction * velocity;
        }

        /// <summary>
        /// Draw method for the Player class
        /// </summary>
        /// <param name="time">Game time passed for animations</param>
        public override void Draw(GameTime time)
        {
            Globals.SB.Begin();

            //counts from the start of the game to current frame in seconds
            //Debug.Print(time.TotalGameTime.Seconds.ToString());

            //if the player is walking left
            if (aState == AnimationState.WalkingLeft)
            {

            }
            //if the player is walking right
            else if (aState == AnimationState.WalkingRight)
            {

            }
            //if the player is walking up
            else if (aState == AnimationState.WalkingUp)
            {

            }
            //if the player is walking down
            else if (aState == AnimationState.WalkingDown)
            {

            }
            //if the player is Idle
            else if (aState == AnimationState.Idle)
            {
                Globals.SB.Draw(
                    asset,                          //Texture
                    position,                       //position
                    new Rectangle(0, 0, 90, 90),    //Source Rectangle
                    Color.White,                    //Color Mask
                    0f,                             //Rotation
                    Vector2.Zero,                   //Origin
                    new Vector2(.5f, .5f),          //Scale
                    SpriteEffects.None,             //SpriteEffects
                    0f);                            //Layer depth
            }

            Globals.SB.End();
        }


    }
}
