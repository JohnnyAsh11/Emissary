using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emissary
{
    /// <summary>
    /// Manager class for enemies within the game
    /// </summary>
    public class EnemyManager
    {

        //Fields:
        private List<Enemy> enemies;

        //Properties:

        //Constructors:
        /// <summary>
        /// Default constructor for the EnemyManager class
        /// </summary>
        public EnemyManager()
        {
            enemies = new List<Enemy>();
            Spawn();
        }

        //Methods:
        /// <summary>
        /// Per frame update method for all of the enemies in the EnemyManager
        /// </summary>
        public void Update()
        {
            foreach (Enemy enemy in enemies)
            {
                enemy.Update();
            }
        }

        /// <summary>
        /// Draw method for all of the enemies within the EnemyManager
        /// </summary>
        public void Draw()
        {
            foreach (Enemy enemy in enemies)
            {
                enemy.Draw();
            }
        }

        /// <summary>
        /// Test Method for enemy instantiation
        /// </summary>
        private void Spawn()
        {
            for (int i = 0; i < 5; i++)
            {
                enemies.Add(
                    new Enemy(
                        new FloatRectangle(800, 380, 50, 50),
                        Globals.GameTextures["GoblinWalk"],
                        10));
            }
        }
    }
}
