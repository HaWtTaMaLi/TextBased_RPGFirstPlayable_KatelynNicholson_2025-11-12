using System;

namespace TextBased_RPGFirstPlayable_KatelynNicholson_2025_11_12
{
    internal class Program
    {
        //Store the map in a text file
        //Reads it as an array
        //MAP
        //walls/boundaries
        //player cannot move through bounds
        //Enemy cannot move through bounds
        //map with only borders is not enough make it visually nice

        //Player
        //moves WASD/ arrow keys input
        //attacks enemies by moving into them
        //can be attacked and killed (by enemy moving into them)

        //Enemy
        //movies via simple ai
        //attacks players via moving into them
        //can be attacked and killed by moving into them

        //refer to Health Systems / and rpg map

        static void Main()
        {
            //draw map
            Map(0);

            //place player and set health
            Player();
            PlayerHealth();

            //place enemy and set health
            Enemy();
            EnemyHealth();

        }

        static void Map()
        {

        }

        static void Map(int scale)
        {

        }

        static void PlayerHealth()
        {

        }

        static void EnemyHealth()
        {

        }

        static void Player()
        {

        }

        static void Enemy()
        {

        }
    }
}

