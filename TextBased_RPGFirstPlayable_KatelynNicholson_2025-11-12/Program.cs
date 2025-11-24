using System;
using System.IO;
using System.Net;
using System.Threading;

namespace TextBased_RPGFirstPlayable_KatelynNicholson_2025_11_12
{
    internal class Program
    {
        //ENEMY
        static int enemyX = 70;
        static int enemyY = 15;
        static char enemyChar = 'X';
        static int enemyHealth;
        static int enemyMaxHealth = 100;
        static int sightDistance = 20;
        enum EnemyState
        {
            Patrol,
            Chase,
            Attack
        }
        static EnemyState currentState = EnemyState.Patrol;
        static int enemyCoolDown = 0;
        static int enemyMoveRate = 1;
        static Random random = new Random();

        static int coinX;
        static int coinY;
        static int coinValue = 10;
        static char coinChar = 'O';
        static int coinsCollected = 0;

        //PLAYER
        static int playerX = 93; //93
        static int playerY = 22; //22
        static char playerChar = '@';
        static int playerMoney = 0;
        static int currentHealth;
        static int maxHealth = 100;
        static int currentShield;
        static int maxShield = 20;
        static int lives = 1;

        static char[,] map = new char[,]
        {
            {'⌠','▓','▓','⌠','▓','▓','▓','⌠','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','⌠','⌠','⌠','⌠','⌠','⌠','⌠','⌠','⌠','⌠','⌠','⌠','⌠','⌠','⌠','⌠','⌠','⌠','⌠','⌠','⌠','⌠','⌠','⌠','⌠','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','⌠','⌠','⌠','⌠','⌠','⌠','⌠','⌠','⌠'},
            {'▓','▓','⌠','▓','⌠','▓','⌠','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','⌠','⌠','⌠','⌠','▓','▓','▓','⌠','▓','▓','⌠','⌠','⌠','⌠','▓','⌠','⌠','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','⌠','⌠','⌠','⌠','⌠','⌠','⌠'},
            {'▓','⌠','▓','▓','⌠','⌠','▓','▓','⌠','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','⌠','▓','▓','▓','▓','▓','▓','▓','⌠','⌠','⌠','⌠','▓','▓','▓','▓','▓','▓','⌠','⌠','⌠','⌠','⌠','⌠','⌠','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','~','~','▓','▓','▓','▓','▓','▓','▓','▓','▓','⌠','⌠','⌠','⌠','⌠','⌠'},
            {'▓','▓','⌠','⌠','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','⌠','⌠','⌠','▓','▓','▓','▓','▓','⌠','▓','⌠','⌠','▓','▓','▓','▓','▓','▓','▓','⌠','⌠','⌠','⌠','⌠','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','~','~','~','▓','▓','▓','▓','▓','▓','▓','▓','▓','⌠','⌠','⌠','⌠','⌠','⌠'},
            {'▓','▓','⌠','▓','▓','⌠','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','⌠','⌠','⌠','▓','▓','▓','▓','▓','⌠','⌠','⌠','⌠','⌠','⌠','⌠','▓','▓','▓','▓','▓','▓','⌠','⌠','⌠','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','~','~','~','~','~','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','⌠','⌠','⌠'},
            {'⌠','▓','⌠','▓','⌠','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','⌠','⌠','▓','▓','▓','▓','▓','⌠','⌠','⌠','⌠','⌠','⌠','⌠','▓','▓','▓','▓','▓','⌠','⌠','⌠','⌠','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','~','~','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','⌠','⌠','⌠','⌠'},
            {'▓','⌠','▓','▓','⌠','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','⌠','⌠','⌠','▓','▓','▓','▓','▓','⌠','⌠','⌠','⌠','⌠','⌠','⌠','⌠','⌠','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','~','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','░'},
            {'▓','⌠','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','⌠','⌠','⌠','⌠','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','⌠','⌠','⌠','⌠','▓','⌠','⌠','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','░','▒'},
            {'▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','⌠','⌠','⌠','░','░','░','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','⌠','⌠','⌠','⌠','⌠','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▒','▒'},
            {'⌠','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','⌠','⌠','⌠','░','░','░','░','░','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','⌠','⌠','⌠','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','░','░','▒','░'},
            {'▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','⌠','⌠','⌠','░','░','░','░','░','░','░','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','⌠','⌠','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','░','▒','▒','▒','▒'},
            {'▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','⌠','░','░','░','░','░','░','░','░','░','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','⌠','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','⌠','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','░','▒','▒','▒','▒','▒'},
            {'▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','░','░','░','░','░','░','░','░','░','░','░','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','⌠','⌠','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','░','▒','▒','▒','▒','▒'},
            {'▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','░','░','░','░','░','░','░','░','░','░','░','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','⌠','⌠','⌠','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','░','▒','▒','▒','▒','▒','▒'},
            {'▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','░','░','░','░','░','░','░','░','░','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','⌠','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','⌠','▒','▒','▒','▒','░','▒','▒'},
            {'▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','░','░','░','░','░','░','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','⌠','⌠','⌠','░','▒','▒','▒','▒','▒','▒','▒'},
            {'▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','⌠','⌠','░','░','░','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','░','░','░','░','░','░','▒','▒','▒','▒','▒','▒','▒','░'},
            {'▓','▓','▓','▓','▓','▓','▓','▓','░','░','⌠','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','⌠','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','⌠','▓','▓','▓','▓','▓','▓','▓','░','▒','▒','▒','▒','▒','▒','▒','▒','▒','▒','▒','▒','░','░'},
            {'▓','▓','▓','▓','▓','▓','⌠','░','░','░','░','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','⌠','⌠','⌠','▓','▓','▓','▓','▓','░','▒','▒','▒','▒','▒','▒','▒','▒','▒','▒','▒','▒','▒','░','▒'},
            {'▓','▓','▓','▓','▓','⌠','░','░','░','░','░','░','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','⌠','⌠','⌠','⌠','⌠','▓','▓','▓','⌠','▒','▒','▒','▒','▒','▒','▒','▒','░','▒','▒','▒','▒','░','▒','░'},
            {'▓','▓','▓','▓','⌠','░','░','░','░','░','░','⌠','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','⌠','▓','▓','▓','▓','⌠','⌠','⌠','⌠','⌠','⌠','▓','▓','▓','░','▒','▒','▒','░','▒','▒','▒','▒','▒','▒','▒','▒','░','▒','░','▒'},
            {'▓','▓','▓','▓','▓','⌠','⌠','░','░','░','⌠','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','⌠','⌠','⌠','▓','▓','⌠','⌠','⌠','⌠','⌠','⌠','⌠','⌠','▓','░','▒','▒','▒','▒','▒','▒','▒','▒','▒','▒','▒','▒','▒','▒','░','▓','▓'}, //safe zone
            {'▓','▓','▓','▓','▓','▓','▓','⌠','▓','░','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','⌠','⌠','⌠','⌠','⌠','⌠','⌠','⌠','⌠','⌠','⌠','⌠','⌠','⌠','⌠','░','▒','▒','▒','▒','▒','▒','▒','▒','▒','▒','░','▒','▒','▒','░','▓','▓','▓'},//< this island is player spawnPoint
        };

        static void Main()
        {
            currentHealth = maxHealth;
            currentShield = maxShield;
            enemyHealth = enemyMaxHealth;
            SpawnCoin();

            while (true) //WASD
            {

                Console.Clear();
                Map();
                CoinsCollected();
                PlayerHUD(currentHealth, currentShield, lives);
                EnemyHUD(enemyHealth);

                //Player Input
                ConsoleKey key = Console.ReadKey(true).Key;

                int newX = playerX;
                int newY = playerY;

                switch (key)
                {
                    case ConsoleKey.W: newY--; break;
                    case ConsoleKey.S: newY++; break;
                    case ConsoleKey.A: newX--; break;
                    case ConsoleKey.D: newX++; break;
                }

                //Boundaries 
                if (newY >= 0 && newY < map.GetLength(0) &&
                newX >= 0 && newX < map.GetLength(1))
                {
                    playerX = newX;
                    playerY = newY;
                }

                if (playerX == coinX && playerY == coinY)
                {
                    coinsCollected += coinValue;
                    Console.Clear();
                    SpawnCoin();
                }

                EnemyTakeDamage(20);
                EnvironmentalDamage();

                //Update Enemy
                Update();

                Thread.Sleep(300);
            }
        }

        static void Update()
        {
            int directionX = playerX - enemyX;
            int directionY = playerY - enemyY;
            int distance = Math.Abs(directionX) + Math.Abs(directionY);

            //Decide State
            if (distance == 0)
                currentState = EnemyState.Attack;
            else if (distance <= sightDistance)
                currentState = EnemyState.Chase;
            else
                currentState = EnemyState.Patrol;

            //Act State
            switch (currentState)
            {
                case EnemyState.Patrol:
                    EnemyPatrol(); //Enemy RandomMovement
                    break;
                case EnemyState.Chase:
                    EnemyChase(); //Enemy Chase
                    break;
                case EnemyState.Attack:
                    PlayerTakeDamage(); //EnemyAttack
                    break;
            }
        }

        static void SpawnCoin()
        {

            coinX = random.Next(0, map.GetLength(1));
            coinY = random.Next(0, map.GetLength(0));
        }

        static void EnemyPatrol() //EenemyState.Patrol
        {
            if (enemyCoolDown > 0)
            {
                enemyCoolDown--;
                return;
            }

            enemyCoolDown = enemyMoveRate;

            //Enemy RandomMovement
            int moveDirection = random.Next(1, 5);
            int moveX = enemyX;
            int moveY = enemyY;

            switch (moveDirection)
            {
                case 1: moveY--; break;
                case 2: moveY++; break;
                case 3: moveX--; break;
                case 4: moveX++; break;
            }

            //Enemy Boundaries
            if (moveY >= 0 && moveY < map.GetLength(0) &&
                moveX >= 0 && moveX < map.GetLength(1) &&
                map[moveY, moveX] != '▒') //Safe area = ▒
            {
                enemyY = moveY;
                enemyX = moveX;
            }

        }

        static void EnemyChase() //EnemyState.Chase
        {
            if (enemyCoolDown > 0)
            {
                enemyCoolDown--;
                return;
            }

            enemyCoolDown = enemyMoveRate;

            int moveX = enemyX;
            int moveY = enemyY;

            if (moveX != playerX)
                moveX += Math.Sign(playerX - enemyX);
            if (moveY != enemyY)
                moveY += Math.Sign(playerY - enemyY);

            //Boundaries 
            if (moveY >= 0 && moveY < map.GetLength(0) &&
                    moveX >= 0 && moveX < map.GetLength(1) &&
                    map[moveY, moveX] != '░')
            {
                enemyX = moveX;
                enemyY = moveY;
            }
        }

        static void PlayerTakeDamage() //EnemyState.Attack
        {
            int damage = random.Next(1, 51); //Generates a random damage between 1-20

            if (currentShield > 0)
            {
                if (damage <= currentShield)
                {
                    currentShield -= damage;
                    damage = 0;
                }
                else
                {
                    damage -= currentShield;
                    currentShield = 0;
                }
            }

            if (damage > 0)
            {
                currentHealth -= damage;
                if (currentHealth < 0) currentHealth = 0;

                if (currentHealth <= 0)
                {
                    if (lives > 0)
                    {
                        lives--;
                        currentHealth = maxHealth;
                        playerX = 93;
                        playerY = 22;
                    }
                    else
                    {
                        GameOver();
                    }
                }
            }
        }

        static void EnvironmentalDamage()
        {
            if (map[playerY, playerX] == '~')
            {
                int lavaDMG = random.Next(1, 51); //Generates a random damage between 1-20

                if (currentShield > 0)
                {
                    if (lavaDMG <= currentShield)
                    {
                        currentShield -= lavaDMG;
                        lavaDMG = 0;
                    }
                    else
                    {
                        lavaDMG -= currentShield;
                        currentShield = 0;
                    }
                }

                if (lavaDMG > 0)
                {
                    currentHealth -= lavaDMG;
                    if (currentHealth < 0) currentHealth = 0;

                    if (currentHealth <= 0)
                    {
                        if (lives > 0)
                        {
                            lives--;
                            currentHealth = maxHealth;
                            playerX = 93;
                            playerY = 22;
                        }
                        else
                        {
                            GameOver();
                        }
                    }
                }
            }
        }

        static void CoinsCollected()
        {
            int HUD = 97;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.SetCursorPosition(HUD, 10);
            Console.WriteLine($"Coins: ${coinsCollected}");
        }

        static void PlayerHUD(int currentHealth, int currentShield, int lives)
        {
            int HUD = 97;
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.BackgroundColor = ConsoleColor.Cyan;
            Console.SetCursorPosition(HUD, 20);
            Console.WriteLine("-----PLAYER-----");
            Console.SetCursorPosition(HUD, 21);
            Console.WriteLine($"Health: {currentHealth}     ");
            Console.SetCursorPosition(HUD, 22);
            Console.WriteLine($"Shield: {currentShield}      ");
            Console.SetCursorPosition(HUD, 23);
            Console.WriteLine($"Lives: {lives}        ");
            Console.ResetColor();

        }

        static void EnemyHUD(int enemyHealth)
        {
            int HUD = 97;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.BackgroundColor = ConsoleColor.Red;
            Console.SetCursorPosition(HUD, 1);
            Console.WriteLine("-----ENEMY-----");
            Console.SetCursorPosition(HUD, 2);
            Console.Write($"Health: {enemyHealth}    ");
            Console.ResetColor();

        }

        static void EnemyTakeDamage(int damage)
        {

            if (playerX == enemyX && playerY == enemyY)
            {
                enemyHealth -= damage;
                if (enemyHealth < 0) enemyHealth = 0;
                if (enemyHealth == 0)
                {
                    YouWon();
                }
            }
        }

        static void Map()
        {
            //Walls = - && | 
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("X");//Top Left Corner
            for (int rowBoarder = 0; rowBoarder < map.GetLength(1); rowBoarder++)
            {
                Console.Write("-"); //Top Boarder
            }
            Console.WriteLine("X");//Top Right Corner

            for (int row = 0; row < map.GetLength(0); row++) //top to bottom
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("|");//Left Boarder

                for (int col = 0; col < map.GetLength(1); col++) //left to right
                {

                    if (row == playerY && col == playerX)
                    {
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        Console.BackgroundColor = ConsoleColor.Cyan;
                        Console.Write(playerChar);
                    }
                    else if (row == enemyY && col == enemyX)
                    {
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.BackgroundColor = ConsoleColor.Red;
                        Console.Write(enemyChar);
                    }
                    else if (row == coinY && col == coinX)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.BackgroundColor = ConsoleColor.Green;
                        Console.Write(coinChar);
                    }
                    else
                    {
                        char tile = map[row, col];

                        switch (tile)
                        {
                            case '▓': //Grass
                                Console.ForegroundColor = ConsoleColor.DarkGreen;
                                Console.BackgroundColor = ConsoleColor.Green;
                                break;
                            case '░': //Ocean
                                Console.ForegroundColor = ConsoleColor.White;
                                Console.BackgroundColor = ConsoleColor.Blue;
                                break;
                            case '▒': //Lake
                                Console.ForegroundColor = ConsoleColor.DarkBlue;
                                Console.BackgroundColor = ConsoleColor.Black;
                                break;
                            case '⌠': //Tree
                                Console.ForegroundColor = ConsoleColor.Black;
                                Console.BackgroundColor = ConsoleColor.Green;
                                break;
                            case '~': //Lava
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.BackgroundColor = ConsoleColor.DarkRed;
                                break;
                        }
                        Console.Write(tile);
                    }
                }
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("|");//Right Boarder
            }
            Console.Write("X");//Bottom Left Corner
            for (int rowBoarder = 0; rowBoarder < map.GetLength(1); rowBoarder++)
            {
                Console.Write("-"); //Bottom Boarder
            }
            Console.WriteLine("X");//Bottom Right Corner
            Console.ResetColor();

            //Legend
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.BackgroundColor = ConsoleColor.Green;
            Console.Write("▓");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.WriteLine(" = Grass");

            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.Write("░");
            Console.BackgroundColor = ConsoleColor.Black;
            Console.WriteLine(" = Lake");

            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Write("▒");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.WriteLine(" = Ocean");

            Console.ForegroundColor = ConsoleColor.Black;
            Console.BackgroundColor = ConsoleColor.Green;
            Console.Write("⌠");
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(" = Trees");

            //ResetColors
            Console.ResetColor();
        }

        static void GameOver()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Black;
            Console.BackgroundColor = ConsoleColor.Red;
            Console.WriteLine(@"
__     ______  _    _   _      ____   _____ ______ 
\ \   / / __ \| |  | | | |    / __ \ / ____|  ____|
 \ \_/ / |  | | |  | | | |   | |  | | (___ | |__   
  \   /| |  | | |  | | | |   | |  | |\___ \|  __|  
   | | | |__| | |__| | | |___| |__| |____) | |____ 
   |_|  \____/ \____/  |______\____/|_____/|______|
");
            Console.WriteLine("You have fallen in battle...");
            Console.WriteLine("Press any key to exit.");

            Console.ResetColor();
            Console.ReadKey();
            Environment.Exit(0);
        }

        static void YouWon()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.BackgroundColor = ConsoleColor.Cyan;
            Console.WriteLine(@"
__     ______  _    _  __          __   __  __ 
\ \   / / __ \| |  | | \ \        / /(_)| \ | |
 \ \_/ / |  | | |  | |  \ \  /\  / / | ||  \| |
  \   /| |  | | |  | |   \ \/  \/ /  | ||     |
   | | | |__| | |__| |    \  /\  /   | || |\  |
   |_|  \____/ \____/      \/  \/    |_||_| \_|
");
            Console.WriteLine("Victory is yours!");
            Console.WriteLine("Press any key to exit.");

            Console.ResetColor();
            Console.ReadKey();
            Environment.Exit(0);
        }
    }
}