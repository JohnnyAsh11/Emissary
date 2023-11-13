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
        public event GetPosition GetPlayerPosition;

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
            
        }

        /// <summary>
        /// Enemy class draw method 
        /// </summary>
        /// <param name="time">Time reference for animations</param>
        protected override void DrawAgent(GameTime time)
        {

        }

    }
}
