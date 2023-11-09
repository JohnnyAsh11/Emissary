using System;
using System.Collections.Generic;
using System.Collections.Specialized;
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

    public delegate List<Rectangle> GetCollidableTiles();

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
        private byte animationFrames;

        //collision fields
        public event GetCollidableTiles GetCurrentCollidableTiles;
        private bool isColliding;
        private byte collisions;

        //Properties: - NONE -

        //Constructors:
        /// <summary>
        /// Default constructor for the Player class
        /// </summary>
        public Player()
            : base(new FloatRectangle(100, 100, 45, 45), Globals.GameTextures["Player"])
        {
            velocity = new Vector2(5, 5);
            direction = Vector2.Zero;
            aState = AnimationState.Idle;
            animationFrames = 1;

            collisions = 0;
        }

        //Methods:
        /// <summary>
        /// Per frame logic update method for the Player class
        /// </summary>
        public override void Update()
        {
            kbState = Keyboard.GetState();
            FloatRectangle nextHitbox = hitbox;

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
                aState = AnimationState.Idle;
                direction.Y = 0;
            }

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
                //purposefully not setting AnimationState to Idle here
                direction.X = 0;
            }

            //calculating the next position
            nextHitbox += direction * velocity;

            //Collision system
            if (GetCurrentCollidableTiles != null)
            {
                //getting the new collidable tiles
                List<Rectangle> tiles = GetCurrentCollidableTiles();

                foreach (Rectangle tile in tiles)
                {
                    //Simple AABB Collision check
                    if (nextHitbox.AABBCheck(tile))
                    {
                        //left and right collisions
                        if ((nextHitbox.X + nextHitbox.Width) >= tile.X && direction.X == 1 ||      //Moving Right
                            nextHitbox.X <= (tile.X + tile.Width) && direction.X == -1)             //Moving Left
                        {
                            //subtract the movement that would be added the player's
                            // real hitbox rectangle -> X axis
                            nextHitbox.X -= direction.X * velocity.X;
                        }

                        //Top and Bottom collisions
                        if ((nextHitbox.Y + nextHitbox.Height) >= tile.Y && direction.Y == 1 ||     //Moving Down
                            nextHitbox.Y <= (tile.Y + tile.Height) && direction.Y == -1)            //Moving Up
                        {
                            //subtract the movement that would be added the player's
                            // real hitbox rectangle -> Y axis
                            nextHitbox.Y -= direction.Y * velocity.Y;
                        }
                    }
                }
            }

            //calculating the position
            hitbox = nextHitbox;
        }

        /// <summary>
        /// Draw method for the Player class
        /// </summary>
        /// <param name="time">Game time passed for animations</param>
        public override void Draw(GameTime time)
        {
            int speed = 10;

            //getting the time modded by an arbitrary number for speed of the animation
            int frameX = time.TotalGameTime.Milliseconds % speed;
            
            Globals.SB.Begin();

            Globals.SB.DrawString(
                Globals.SF,
                $"{isColliding}",
                new Vector2(800, 100),
                Color.White);

            //Rendering the player's hitbox
            Globals.SB.Draw(
                Globals.GameTextures["DebugImage"],
                new Rectangle((int)hitbox.X, (int)hitbox.Y, (int)hitbox.Width, (int)hitbox.Height),
                Color.Orange);

            //everytime the time passed is divisible by the speed move the frame true bit over 1 index
            if (frameX == 0)
            {
                //moving the true bit over
                animationFrames <<= 1;

                //if the 1 gets moved over far enough the byte will just equal zero
                if (animationFrames == 0)
                {
                    //in which case reset the byte to 1
                    animationFrames = 1;
                }
            }

            //retrieve the index (basically the frame) at which the 1 currently is in the byte
            //  then multiply by 90 sizing the in the source rectangle
            frameX = Globals.IndexAtTrue(animationFrames) * 90;

            //if the player is walking left
            if (aState == AnimationState.WalkingLeft)
            {
                Globals.SB.Draw(
                    asset,
                    hitbox.Position,
                    new Rectangle(frameX, 270, 90, 90),
                    Color.White,
                    0f,
                    Vector2.Zero,
                    new Vector2(.5f, .5f),
                    SpriteEffects.None,
                    0f);

            }
            //if the player is walking right
            else if (aState == AnimationState.WalkingRight)
            {
                Globals.SB.Draw(
                    asset,
                    hitbox.Position,
                    new Rectangle(frameX, 180, 90, 90),
                    Color.White,
                    0f,
                    Vector2.Zero,
                    new Vector2(.5f, .5f),
                    SpriteEffects.None,
                    0f);
            }
            //if the player is walking up
            else if (aState == AnimationState.WalkingUp)
            {
                Globals.SB.Draw(
                    asset,
                    hitbox.Position,
                    new Rectangle(frameX, 90, 90, 90),
                    Color.White,
                    0f,
                    Vector2.Zero,
                    new Vector2(.5f, .5f),
                    SpriteEffects.None,
                    0f);
            }
            //if the player is walking down
            else if (aState == AnimationState.WalkingDown)
            {
                Globals.SB.Draw(
                    asset,
                    hitbox.Position,
                    new Rectangle(frameX, 0, 90, 90),
                    Color.White,
                    0f,
                    Vector2.Zero,
                    new Vector2(.5f, .5f),
                    SpriteEffects.None,
                    0f);
            }
            //if the player is Idle
            else if (aState == AnimationState.Idle)
            {
                Globals.SB.Draw(
                    asset,                          //Texture
                    hitbox.Position,                //position
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
