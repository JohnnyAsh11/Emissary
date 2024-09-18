using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;

namespace Emissary
{
    /// <summary>
    /// Physics abstract class that gives the capabillity for forces to be applied 
    /// to whatever GameObject they are apart off.  
    /// Effectively replaces the GameObject class for physics objects
    /// </summary>
    public abstract class PhysicsAgent : GameObject
    {

        //Fields:
        protected float maxSpeed;
        protected Random rng;

        protected float mass;
        protected Vector2 acceleration;
        protected Vector2 velocity;
        protected Vector2 totalForce;

        //Vector2 direction of the PhysicsAgent
        protected Vector2 direction;

        protected Vector2 maxVelocity;
        protected Vector2 maxForce;

        private int timer;
        private float wanderAngle;
        private Vector2 startingPosition;

        //Properties: - NONE -

        //Constructors:
        /// <summary>
        /// Parameterized constructor for the PhysicsAgent abstract class
        /// </summary>
        /// <param name="hitbox">Hitbox of the Physics Game Object</param>
        /// <param name="asset">Texture asset for the Physics Game Object</param>
        /// <param name="mass">Mass of the Physics Game Object</param>
        public PhysicsAgent(FloatRectangle hitbox, Texture2D asset, float mass)
            : base(hitbox, asset)
        {
            maxSpeed = 5f;
            velocity = Vector2.Zero;
            acceleration = Vector2.Zero;
            rng = new Random();
            startingPosition = hitbox.Position;

            maxVelocity = new Vector2(10, 10);
            maxForce = new Vector2(5, 5);
            this.mass = mass;
        }

        /// <summary>
        /// Parameterized constructor for the PhysicsAgent that allows for setting of the
        /// maxVelocity and MaxForce
        /// </summary>
        /// <param name="hitbox">Hitbox of the Physics Game Object</param>
        /// <param name="asset">Texture asset for the Physics Game Object</param>
        /// <param name="mass">Mass of the Physics Game Object</param>
        /// <param name="maxVelocity">maximum velocity of the agent</param>
        /// <param name="maxForce">maximum force that can be applied to the agent</param>
        public PhysicsAgent(FloatRectangle hitbox, Texture2D asset, float mass, 
                            Vector2 maxVelocity, Vector2 maxForce)
            : base(hitbox, asset)
        {
            maxSpeed = 5f;
            velocity = Vector2.Zero;
            acceleration = Vector2.Zero;
            rng = new Random();
            startingPosition = hitbox.Position;

            this.maxVelocity = maxVelocity;
            this.maxForce = maxForce;
            this.mass = mass;
        }

        //Methods:
        /// <summary>
        /// Standard Update method for all Physics Agents 
        /// </summary>
        public override void Update()
        {
            //reset the Acceleration every frame
            acceleration = Vector2.Zero;

            //reseting the totalForce
            totalForce = Vector2.Zero;

            //calling CalcSteeringForces to perform whatever steer methods this Agent
            //  would be using
            this.CalcSteeringForces();

            //applying the calculated steering force
            this.ApplyForce(totalForce);

            //applying the acceleration to the velocity
            velocity += acceleration;

            //applying the velocity to the position
            hitbox.Position += velocity;

            //calculating the direction
            direction = Vector2.Normalize(velocity);
        }

        /// <summary>
        /// The Seek Force calculation method
        /// </summary>
        /// <param name="targetPosition">The position of the object being seeked</param>
        /// <returns>An approiate force to seek that object</returns>
        protected Vector2 Seek(Vector2 targetPosition)
        {
            //declaring the variable that will hold our seeking force
            Vector2 seekingForce;

            //calculating the vector that would point to the desired location
            Vector2 desiredVelocity = targetPosition - hitbox.Position;

            //normalizing that and multiplying it buy the maxSpeed of this physics agent
            desiredVelocity = Vector2.Normalize(desiredVelocity) * maxSpeed;

            //performing desiredVelocity - velocity to retrieve a vector force that will
            // smoothly track closer and closer to the desired velocity every frame that 
            // this method is called
            seekingForce = desiredVelocity - velocity;

            return seekingForce;
        }

        /// <summary>
        /// Algorithm for AI PhysicsAgent wandering
        /// </summary>
        /// <param name="time">The time being used for future position calculations</param>
        /// <param name="radius">Radius of the circle used for the Wander Algorithm</param>
        /// <returns>The Seek force being applied to acceleration for wandering</returns>
        protected Vector2 Wander(float time, float radius)
        {
            //finding the location of the projected wander circle
            Vector2 futurePosition = CalcFuturePosition(time);

            timer--;
            if (timer <= 0)
            {                
                wanderAngle = (float)(rng.NextDouble() * (Math.PI * 2));
                timer = 30;
            }

            //getting a random point on the circle
            //float randAngle = (float)(rng.NextDouble() * (Math.PI * 2));

            //calculating the x and y position of the point on the circle
            float x = futurePosition.X + (float)(Math.Cos(wanderAngle)) * radius;
            float y = futurePosition.Y + (float)(Math.Sin(wanderAngle)) * radius;

            //seeking the point found
            return Seek(new Vector2(x, y));
        }

        /// <summary>
        /// Calculated the future position based on the current velocity and a number of iterations
        /// </summary>
        /// <param name="time">Amount of time passed</param>
        /// <returns>The future position of the PhysicsAgent</returns>
        protected Vector2 CalcFuturePosition(float time)
        {
            Vector2 futurePosition;

            //getting the future position by getting the current position and
            // adding velecity to it multplied by time
            futurePosition = hitbox.Position + (velocity * time);

            return futurePosition;
        }

        /// <summary>
        /// Draw method for all PhysicsAgents
        /// </summary>
        public override void Draw()
        {
            DrawAgent();
        }

        /// <summary>
        /// Base Method for drawing that PhysicsAgents can override
        /// </summary>
        protected virtual void DrawAgent()
        {
            Globals.SB.Begin();
            Globals.SB.Draw(
                asset,
                hitbox.ToRectangle,
                Color.White);
            Globals.SB.End();
        }

        /// <summary>
        /// Method to calculate the steering forces of the object: 
        /// Child classes can use this method for determining whether
        /// they are seeking, fleeing, wandering, etc.
        /// </summary>
        protected abstract void CalcSteeringForces();

        /// <summary>
        /// Applies a force to the PhysicsAgent's acceleration
        /// </summary>
        /// <param name="force">Force being applied to an Object</param>
        public void ApplyForce(Vector2 force)
        {
            //Newton's Law is Force = Mass * acceleration
            // so we can calculate acceleration by acceleration = force / mass 
            acceleration += force / mass;
        }

        /// <summary>
        /// Creates an 800x800 box that the PhysicsAgent will remain within
        /// </summary>
        /// <returns>A Vector2 force to keep the Agent within bounds</returns>
        protected Vector2 KeepInBounds()
        {
            Vector2 position = hitbox.Position;

            //checks performed to tell whether the PhysicsAgent is out of bounds
            if (position.Y >= startingPosition.Y + 400 ||
                position.Y <= startingPosition.Y - 400 ||
                position.X >= startingPosition.X + 400 ||
                position.X <= startingPosition.X - 400)
            {
                //if it is then seek the Starting position of the PhysicsAgent
                return Seek(startingPosition);
            }

            //return Vector(0, 0) if the object is within bounds
            return Vector2.Zero;
        }


    }
}
