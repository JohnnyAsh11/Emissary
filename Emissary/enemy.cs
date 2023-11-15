using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emissary
{
    public delegate FloatRectangle GetPosition();

    /// <summary>
    /// General enemy within Emissary
    /// </summary>
    public class Enemy : PhysicsAgent
    {

        //Fields:
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
            Globals.SB.Begin();
            Globals.SB.Draw(
                asset,
                hitbox.ToRectangle,
                Color.Aqua);
            Globals.SB.End();
        }

    }
}
