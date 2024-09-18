using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;

namespace Emissary
{
    public delegate FloatRectangle GetPosition();

    /// <summary>
    /// Enum containing the type of enemy that this enemy will be
    /// </summary>
    public enum EnemyType
    {
        Goblin
    }


    /// <summary>
    /// General enemy within Emissary
    /// </summary>
    public class Enemy : PhysicsAgent
    {

        //Fields:
        private EnemyType enemyType;
        private byte animationFrames;

        public event GetPosition GetTargetPosition;
        
        //Properties: - NONE -

        //Constructors:
        /// <summary>
        /// Parameterized constructor for the Enemy class
        /// </summary>
        /// <param name="position">FloatRectangle position of the Enemy</param>
        /// <param name="asset">Texture asset for the Enemy</param>
        /// <param name="mass">Mass of the enemy</param>
        public Enemy(FloatRectangle position, Texture2D asset, float mass)
            : base(position, asset, mass)
        {
            enemyType = EnemyType.Goblin;

            //altering the maxSpeed based on EnemyType
            if (enemyType == EnemyType.Goblin)
            {
                maxSpeed = 2f;
            }
        }

        //Methods:
        /// <summary>
        /// Enemy Class Steering Calcuation Method
        /// </summary>
        protected override void CalcSteeringForces()
        {
            if (GetTargetPosition != null)
            {
                Vector2 target;
            }

            totalForce += Wander(1, 1) * 5;
            totalForce += KeepInBounds();
        }

        /// <summary>
        /// Enemy class draw method 
        /// </summary>
        protected override void DrawAgent()
        {
            int speed = 4;

            //getting the time modded by an arbitrary number for speed of the animation
            int frameX = Globals.Time.TotalGameTime.Milliseconds % speed;

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
            //  then multiply by 64 sizing the in the source rectangle
            frameX = Globals.IndexAtTrue(animationFrames) * 192;

            Globals.SB.Begin();


            if (enemyType == EnemyType.Goblin)
            {
                //creating a new Vector for the rendering position since the animation asset
                // contains extra whitespace
                Vector2 renderPos = new Vector2(
                    hitbox.Position.X - (hitbox.Width * 1.5f),
                    hitbox.Position.Y - (hitbox.Height * 1.7f));

                //Determines which direction the enemy is moving for which direction
                // to render the Goblin
                if (direction.X < 0)
                {
                    Globals.SB.Draw(
                        asset,
                        renderPos,
                        new Rectangle(frameX, 0, 192, 192),
                        Color.White,
                        0f,
                        Vector2.Zero,
                        Vector2.One,
                        SpriteEffects.FlipHorizontally,
                        0f);
                }
                else if (direction.X > 0)
                {
                    Globals.SB.Draw(
                        asset,
                        renderPos,
                        new Rectangle(frameX, 0, 192, 192),
                        Color.White,
                        0f,
                        Vector2.Zero,
                        Vector2.One,
                        SpriteEffects.None,
                        0f);
                }
            }
            Globals.SB.End();
        }

    }
}
