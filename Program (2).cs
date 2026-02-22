using System;
using System.Collections.Generic;

class MarioRPGAdvanced
{
    // Player stats
    static string playerName = "Mario";
    static int playerHP = 100;
    static int playerMaxHP = 100;
    static int playerAttack = 20;
    static int coins = 0;

    // Allies
    static bool hasGeno = false;
    static bool hasMallow = false;

    // Inventory
    static List<string> inventory = new List<string>();

    // Map locations
    static string[] locations = {
        "Mushroom Kingdom",
        "Star Road",
        "Bowser Castle",
        "Marsh Cave",
        "Forest Maze"
    };
    static int currentLocation = 0;

    static void Main()
    {
        Console.Title = "Mario RPG Terminal - Mundo Completo";
        ShowIntro();
        GameLoop();
    }

    static void ShowIntro()
    {
        Console.WriteLine("🎮 Bem-vindo ao Mario RPG Terminal Avançado!");
        Console.WriteLine("Explore o Reino dos Cogumelos, encontre aliados e recupere as estrelas!");
        Console.WriteLine("Pressione qualquer tecla para começar...");
        Console.ReadKey();
        Console.Clear();
    }

    static void GameLoop()
    {
        while (true)
        {
            ShowLocation();
            Console.WriteLine("O que deseja fazer?");
            Console.WriteLine("1 - Explorar");
            Console.WriteLine("2 - Ver Status/Inventário");
            Console.WriteLine("3 - Mover para outro local");
            Console.WriteLine("4 - Sair do jogo");
            Console.Write("Escolha: ");
            string choice = Console.ReadLine();
            Console.Clear();

            switch (choice)
            {
                case "1":
                    Explore();
                    break;
                case "2":
                    ShowStatus();
                    break;
                case "3":
                    MoveLocation();
                    break;
                case "4":
                    Console.WriteLine("Saindo do jogo... Até a próxima!");
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Escolha inválida!");
                    break;
            }
        }
    }

    static void ShowLocation()
    {
        Console.WriteLine($"🌍 Você está em: {locations[currentLocation]}");
        switch (currentLocation)
        {
            case 0:
                Console.WriteLine("O Mushroom Kingdom está em paz, mas Bowser ainda é uma ameaça!");
                break;
            case 1:
                Console.WriteLine("Star Road leva a lugares misteriosos e inimigos poderosos.");
                break;
            case 2:
                Console.WriteLine("Bowser Castle! Prepare-se para chefes perigosos!");
                break;
            case 3:
                Console.WriteLine("Marsh Cave: Mallow se junta à aventura!");
                if (!hasMallow) hasMallow = true;
                break;
            case 4:
                Console.WriteLine("Forest Maze: Geno aparece para ajudar!");
                if (!hasGeno) hasGeno = true;
                break;
        }
    }

    static void Explore()
    {
        Random rand = new Random();
        int eventRoll = rand.Next(0, 5); // 0-2 batalha, 3 moedas, 4 item

        if (eventRoll <= 2)
        {
            RandomEnemy();
        }
        else if (eventRoll == 3)
        {
            int foundCoins = rand.Next(5, 31);
            coins += foundCoins;
            Console.WriteLine($"🪙 Você encontrou {foundCoins} moedas!");
        }
        else if (eventRoll == 4)
        {
            string[] items = { "Mushroom", "Fire Flower", "Star", "Potion" };
            string itemFound = items[rand.Next(items.Length)];
            inventory.Add(itemFound);
            Console.WriteLine($"🎁 Você encontrou um item: {itemFound}!");
        }

        WaitKey();
    }

    static void RandomEnemy()
    {
        string[] enemies = { "Goomba", "Koopa", "Boo", "Hammer Bro" };
        Random rand = new Random();
        string enemy = enemies[rand.Next(enemies.Length)];
        int enemyHP = rand.Next(30, 61);
        int enemyAttack = rand.Next(5, 16);

        Battle(enemy, enemyHP, enemyAttack);
    }

    static void Battle(string enemy, int enemyHP, int enemyAttack)
    {
        Console.WriteLine($"⚔ Um {enemy} apareceu! (HP: {enemyHP})");

        while (enemyHP > 0 && playerHP > 0)
        {
            Console.WriteLine("\nEscolha ação:");
            Console.WriteLine("1 - Atacar");
            if (hasMallow) Console.WriteLine("2 - Mallow Magia (Thunder)");
            if (hasGeno) Console.WriteLine("3 - Geno Ataque (Star Beam)");
            Console.WriteLine("4 - Fugir");
            Console.Write("Ação: ");
            string action = Console.ReadLine();
            Console.Clear();

            if (action == "1")
            {
                enemyHP -= playerAttack;
                Console.WriteLine($"Você atacou {enemy} e causou {playerAttack} de dano!");
            }
            else if (action == "2" && hasMallow)
            {
                int damage = 25;
                enemyHP -= damage;
                Console.WriteLine($"Mallow usa Thunder e causa {damage} de dano!");
            }
            else if (action == "3" && hasGeno)
            {
                int damage = 30;
                enemyHP -= damage;
                Console.WriteLine($"Geno usa Star Beam e causa {damage} de dano!");
            }
            else if (action == "4")
            {
                if (new Random().Next(0, 2) == 0)
                {
                    Console.WriteLine("Você conseguiu fugir!");
                    return;
                }
                else
                {
                    Console.WriteLine("Você não conseguiu fugir!");
                }
            }
            else
            {
                Console.WriteLine("Ação inválida!");
                continue;
            }

            if (enemyHP <= 0)
            {
                int rewardCoins = new Random().Next(10, 31);
                coins += rewardCoins;
                Console.WriteLine($"🎉 Você derrotou {enemy}! Ganhou {rewardCoins} moedas!");
                return;
            }

            playerHP -= enemyAttack;
            Console.WriteLine($"{enemy} atacou você e causou {enemyAttack} de dano!");

            if (playerHP <= 0)
            {
                Console.WriteLine("💀 Você morreu! Fim de jogo.");
                Environment.Exit(0);
            }
        }
    }

    static void ShowStatus()
    {
        Console.WriteLine($"👤 Jogador: {playerName}");
        Console.WriteLine($"💖 HP: {playerHP}/{playerMaxHP}");
        Console.WriteLine($"⚔ Ataque: {playerAttack}");
        Console.WriteLine($"🪙 Moedas: {coins}");
        Console.WriteLine("🤝 Aliados: " + (hasMallow ? "Mallow " : "") + (hasGeno ? "Geno" : ""));
        Console.WriteLine("🎒 Inventário: " + (inventory.Count > 0 ? string.Join(", ", inventory) : "Vazio"));
        WaitKey();
    }

    static void MoveLocation()
    {
        Console.WriteLine("Para onde deseja ir?");
        for (int i = 0; i < locations.Length; i++)
        {
            Console.WriteLine($"{i + 1} - {locations[i]}");
        }
        Console.Write("Escolha: ");
        string choice = Console.ReadLine();
        Console.Clear();

        if (int.TryParse(choice, out int loc) && loc >= 1 && loc <= locations.Length)
        {
            currentLocation = loc - 1;
            ShowLocation();
        }
        else
        {
            Console.WriteLine("Local inválido!");
        }
        WaitKey();
    }

    static void WaitKey()
    {
        Console.WriteLine("\nPressione qualquer tecla para continuar...");
        Console.ReadKey();
        Console.Clear();
    }
}