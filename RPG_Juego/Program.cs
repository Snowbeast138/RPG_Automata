using System;
using System.Collections.Generic;
using static System.Console;

namespace EFSM_Juego
{
    class Program
    {
        static Character[] characters = DefiningCharacters();

        static int indexCharacterSelected = SelectingTypeCharacter(characters.Length);

        static Character player = CreatingPlayer(characters, indexCharacterSelected);

        static Automata world = DefiningWorld();

        static List<Enemy> enemies = new List<Enemy>();

        static Enemy enemy = null;

        // Variables para el Autómata del Jefe
        static bool bossIsDodging = false;
        static int bossTurnCount = 0;

        static void Main()
        {
            DefiningEnemies();

            WriteLine("Bienvenido al pueblo de Tierras Lejanas " + player.name + ".");

            ShowMenu();
        }

        public static int SelectingTypeCharacter(int lenghtCharacters)
        {
            WriteLine("Seleccione el tipo de personaje que desea jugar:");
            WriteLine("0 - Caballero");
            WriteLine("1 - Mago");
            WriteLine("2 - Sirena");
            WriteLine("3 - Ladron");

            int indexCharacterSelected = -1;
            do
            {
                string? input = ReadLine();
                if (!int.TryParse(input, out indexCharacterSelected))
                {
                    WriteLine("El valor ingresado no es un numero valido, por favor ingrese un numero entre 0 y " + (lenghtCharacters - 1));
                    indexCharacterSelected = -1;
                }
                else if (indexCharacterSelected < 0 || indexCharacterSelected > 3)
                {
                    WriteLine("El indice del personaje seleccionado no es valido, ingrese un valor entre 0 y " + (lenghtCharacters - 1));
                    indexCharacterSelected = -1;
                }
            }
            while (indexCharacterSelected < 0 || indexCharacterSelected > 3);

            return indexCharacterSelected;
        }

        public static Character CreatingPlayer(Character[] characters, int indexCharacterSelected)
        {
            Character player = new Character();
            do
            {
                Write("Ingrese el Nombre del Jugador:");
                player.name = ReadLine();
                if (player.name == null || player.name == "")
                {
                    WriteLine("El nombre del jugador no puede ser nulo, por favor ingrese un nombre valido.");
                }
            }
            while (player.name == null);

            if (indexCharacterSelected >= 0 && indexCharacterSelected < characters.Length)
            {
                player.HP = characters[indexCharacterSelected].HP;
                player.healt_max = characters[indexCharacterSelected].healt_max;
                player.Speed = characters[indexCharacterSelected].Speed;
                player.Damage = characters[indexCharacterSelected].Damage;
                player.CritictRate = characters[indexCharacterSelected].CritictRate;
                player.CharacterType = characters[indexCharacterSelected].CharacterType;
                player.Level = characters[indexCharacterSelected].Level;
                player.XP_STORAGED = characters[indexCharacterSelected].XP_STORAGED;
                player.HealLevel = characters[indexCharacterSelected].HealLevel;
            }
            else
            {
                do
                {
                    WriteLine("El indice del personaje seleccionado no es valido, ingrese un valor entre 0 y " + (characters.Length - 1));
                    string? input = ReadLine();
                    if (!int.TryParse(input, out indexCharacterSelected))
                    {
                        WriteLine("El valor ingresado no es un numero valido, por favor ingrese un numero entre 0 y " + (characters.Length - 1));
                        indexCharacterSelected = -1;
                    }
                    indexCharacterSelected = int.Parse(input);
                }
                while (indexCharacterSelected < 0 || indexCharacterSelected > characters.Length - 1);
            }

            WriteLine("Personaje creado exitosamente.");
            ShowPlayerInfo(player);

            return player;
        }

        public static void ShowPlayerInfo(Character player)
        {
            WriteLine("------------------------------");
            WriteLine("Nombre del Jugador: " + player.name);
            WriteLine("Tipo de Personaje: " + player.CharacterType);
            WriteLine("HP: " + player.HP + "/" + player.healt_max);
            WriteLine("Velocidad: " + player.Speed);
            WriteLine("Daño: " + player.Damage);
            WriteLine("Probabilidad de Critico: " + player.CritictRate * 100 + "%");
            WriteLine("Nivel: " + player.Level);
            WriteLine("Experiencia Acumulada: " + player.XP_STORAGED + "/100");
            WriteLine("Nivel de Curación: " + player.HealLevel);
            WriteLine("------------------------------");
        }

        public static void ShowWorldInfo(Automata world)
        {
            WriteLine("------------------------------");
            WriteLine("Lugares del Mundo:");
            foreach (State state in world.States)
            {
                WriteLine("- " + state.Context);
            }
            WriteLine("Lugar Actual del Personaje: " + world.CurrentState.Context);
            WriteLine("------------------------------");
        }

        public static Character[] DefiningCharacters()
        {
            Character[] characters = new Character[] {
                new Character {
                    HP = 60,
                    healt_max = 60,
                    Speed = 5,
                    Damage = 20,
                    CritictRate = 0.25f,
                    Level = 1,
                    XP_STORAGED = 0,
                    HealLevel = 5.0f,
                    CharacterType = Character.Type.KNIGHT
                },
                new Character {
                    HP = 100,
                    healt_max = 100,
                    Speed = 8,
                    Damage = 15,
                    CritictRate = 0.2f,
                    Level = 1,
                    XP_STORAGED = 0,
                    HealLevel = 10.0f,
                    CharacterType = Character.Type.WIZARD
                },
                new Character {
                    HP = 80,
                    healt_max = 80,
                    Speed = 3,
                    Damage = 5,
                    CritictRate = 0.2f,
                    Level = 1,
                    XP_STORAGED = 0,
                    HealLevel = 20.0f,
                    CharacterType = Character.Type.MERMAID
                },
                new Character {
                    HP = 40,
                    healt_max = 40,
                    Speed = 15,
                    Damage = 10,
                    CritictRate = 0.4f,
                    Level = 1,
                    XP_STORAGED = 0,
                    HealLevel = 10.0f,
                    CharacterType = Character.Type.THIEF
                }
            };

            return characters;
        }

        public static void DefiningEnemies()
        {
            enemies.Add(new Enemy(10, 10, 5, 2, 0.05f, 0, 0.15f, 10, false, Enemy.EnemyType.ESCORPION, Enemy.EnemyZone.DESERT));
            enemies.Add(new Enemy(15, 15, 3, 4, 0.1f, 0, 0.15f, 15, false, Enemy.EnemyType.GUSANO_DE_ARENA, Enemy.EnemyZone.DESERT));
            enemies.Add(new Enemy(20, 20, 2, 6, 0.15f, 0, 0.15f, 20, false, Enemy.EnemyType.BUITRE_CARROÑERO, Enemy.EnemyZone.DESERT));
            enemies.Add(new Enemy(25, 25, 4, 8, 0.2f, 0, 0.15f, 25, false, Enemy.EnemyType.LAGARTO_VENENOSO, Enemy.EnemyZone.DESERT));
            enemies.Add(new Enemy(30, 30, 7, 10, 0.3f, 0, 0.05f, 30, false, Enemy.EnemyType.LOBO_DE_FUEGO, Enemy.EnemyZone.VOLCANIC));
            enemies.Add(new Enemy(25, 25, 5, 8, 0.25f, 0, 0.1f, 25, false, Enemy.EnemyType.FUEGO_FATUO, Enemy.EnemyZone.VOLCANIC));
            enemies.Add(new Enemy(35, 35, 4, 12, 0.3f, 0, 0.05f, 35, false, Enemy.EnemyType.LAGARTO_ARDIENTE, Enemy.EnemyZone.VOLCANIC));
            enemies.Add(new Enemy(40, 40, 3, 15, 0.35f, 0, 0.1f, 40, false, Enemy.EnemyType.MAGMA_SLIME, Enemy.EnemyZone.VOLCANIC));
            enemies.Add(new Enemy(30, 30, 6, 5, 0.1f, 0, 0.05f, 50, false, Enemy.EnemyType.TIBURON_MOTOSIERRA, Enemy.EnemyZone.AQUATIC));
            enemies.Add(new Enemy(50, 50, 7, 7, 0.15f, 0, 0.2f, 45, false, Enemy.EnemyType.CANGREJO_PINZA_ANZUELO, Enemy.EnemyZone.AQUATIC));
            enemies.Add(new Enemy(20, 20, 4, 5, 0.3f, 0, 0.05f, 25, false, Enemy.EnemyType.PIRATA_FANTASMA, Enemy.EnemyZone.AQUATIC));
            enemies.Add(new Enemy(60, 60, 5, 3, 0.2f, 0, 0.3f, 80, false, Enemy.EnemyType.ELECTROMEDUSA, Enemy.EnemyZone.AQUATIC));
            enemies.Add(new Enemy(150, 150, 6, 12, 0.25f, 0, 0.0f, 150, true, Enemy.EnemyType.REY_DEL_DESIERTO, Enemy.EnemyZone.DESERT));
            enemies.Add(new Enemy(200, 200, 8, 20, 0.4f, 0, 0.0f, 200, true, Enemy.EnemyType.DRAGON_SALAMANDER, Enemy.EnemyZone.VOLCANIC));
            enemies.Add(new Enemy(300, 300, 6, 25, 0.45f, 0, 0.0f, 300, true, Enemy.EnemyType.CHUTULU, Enemy.EnemyZone.AQUATIC));
        }

        public static Automata DefiningWorld()
        {
            Automata world = new Automata();

            world.AddState(new State("Pueblo Inicial", "q0"));
            world.AddState(new State("Dunas Deserticas", "q1"));
            world.AddState(new State("Piramide Invertida", "q2"));
            world.AddState(new State("Oasis Paradisaco", "q3"));
            world.AddState(new State("Ruinas Volcanicas", "q4"));
            world.AddState(new State("Bosque de Las Cenizas", "q5"));
            world.AddState(new State("Kakacatepetl", "q6"));
            world.AddState(new State("Jardin Oceaico", "q7"));
            world.AddState(new State("Campo de Las Merlusas", "q8"));
            world.AddState(new State("Ruinas Oceanicas", "q9"));
            world.AddState(new State("San Marisco", "q10"));
            world.AddState(new State("Castillo Salamander", "q11"));
            world.AddState(new State("Olin Oir", "q12"));

            return world;
        }

        public static void WriteCentered(string text)
        {
            int windowWidth = Console.WindowWidth;
            int padding = (windowWidth - text.Length) / 2;
            padding = Math.Max(0, padding);
            WriteLine(text.PadLeft(text.Length + padding));
        }

        public static void ShowMenu()
        {
            int optionSelected = -1;
            do
            {
                do
                {
                    WriteCentered("------------------------------------------");
                    WriteCentered("¿Qué acción desea realizar?");
                    WriteCentered("1 - Caminar");
                    WriteCentered("2 - Verificar Información del Personaje");
                    WriteCentered("3 - Verificar Información del Mundo");
                    WriteCentered("4 - Salir del Juego");
                    WriteCentered("------------------------------------------");

                    WriteLine();
                    Write("Opcion:");

                    string? input = ReadLine();
                    if (!int.TryParse(input, out optionSelected))
                    {
                        WriteLine("El valor ingresado no es un numero valido, por favor ingrese un numero entre 1 y 4.");
                        optionSelected = -1;
                    }
                    else if (optionSelected < 1 || optionSelected > 4)
                    {
                        WriteLine("La opcion seleccionada no es valida, por favor ingrese un numero entre 1 y 4.");
                        optionSelected = -1;
                    }

                    switch (optionSelected)
                    {
                        case 1:
                            Walkthrough();
                            break;
                        case 2:
                            ShowPlayerInfo(player);
                            break;
                        case 3:
                            ShowWorldInfo(world);
                            break;
                        case 4:
                            WriteLine("Finalizando juego.");
                            break;
                    }
                }
                while (optionSelected < 1 || optionSelected > 4);
            }
            while (player.HP <= 0 || optionSelected != 4);

            if (player.HP <= 0)
            {
                WriteLine("El personaje ha muerto. Fin del juego.");
            }
        }

        public static void Walkthrough()
        {
            WriteLine("En que dirreccion desea caminar? (Norte:1, Sur:2, Este:3, Oeste:4)");
            int direction = -1;
            do
            {
                string? input = ReadLine();
                if (!int.TryParse(input, out direction))
                {
                    WriteLine("El valor ingresado no es un numero valido, por favor ingrese un numero entre 1 y 4.");
                    direction = -1;
                }
                else if (direction < 1 || direction > 4)
                {
                    WriteLine("La direccion ingresada no es valida, por favor ingrese un numero entre 1 y 4.");
                    direction = -1;
                }
            }
            while (direction < 1 || direction > 4);

            WalkthroughResult(direction);
        }

        public static void WalkthroughResult(int direction)
        {
            string directionName = direction switch
            {
                1 => "Norte",
                2 => "Sur",
                3 => "Este",
                4 => "Oeste",
                _ => "dirección desconocida"
            };

            WriteLine($"Camina hacia el {directionName}...");

            State previousState = world.CurrentState;
            string currentStateId = previousState.n_State;

            string nextStateId = (currentStateId, direction) switch
            {
                ("q0", 1) => "q1",
                ("q0", 3) => "q4",
                ("q0", 4) => "q7",
                
                ("q1", 1) => "q3",
                ("q1", 2) => "q0",
                ("q1", 3) => "q2",
                
                ("q2", 1) => "q12",
                ("q2", 2) => "q1",
                ("q2", 3) => "q3",
                
                ("q3", 1) => "q12",
                ("q3", 2) => "q1",
                ("q3", 4) => "q2",
                
                ("q4", 1) => "q11",
                ("q4", 2) => "q0",
                ("q4", 3) => "q6",
                ("q4", 4) => "q5",
                
                ("q5", 1) => "q11",
                ("q5", 2) => "q4",
                ("q5", 3) => "q6",
                
                ("q6", 1) => "q11",
                ("q6", 2) => "q4",
                ("q6", 4) => "q5",
                
                ("q7", 1) => "q8",
                ("q7", 2) => "q0",
                ("q7", 4) => "q9",
                
                ("q8", 1) => "q10",
                ("q8", 2) => "q7",
                ("q8", 4) => "q9",
                
                ("q9", 1) => "q10",
                ("q9", 2) => "q7",
                ("q9", 3) => "q8",
                
                ("q10", 2) => "q8",
                
                ("q11", 2) => "q5",
                
                ("q12", 2) => "q2",
                
                _ => "-"
            };

            if (nextStateId == "-")
            {
                WriteLine($"Movimiento no permitido hacia el {directionName} desde {previousState.Context}. Límite del mundo alcanzado.");
            }
            else
            {
                world.CurrentState = world.States.Find(state => state.n_State == nextStateId)!;
                WriteLine($"Desplazamiento registrado: [{previousState.Context}] -> [{world.CurrentState.Context}].");

                if (CheckBossSpawn())
                {
                    WriteLine("Presencia anómala detectada.");
                    SpawnBoss();
                }
                else if (isSpawningEnemy())
                {
                    SpawnEnemy();
                }

                CheckEnvironmentalEffects();
            }
        }

        public static void CheckEnvironmentalEffects()
        {
            string currentStateId = world.CurrentState.n_State;
            Character.Type type = player.CharacterType;

            if (currentStateId == "q1" || currentStateId == "q2" || currentStateId == "q3" || currentStateId == "q12")
            {
                if (type == Character.Type.MERMAID || type == Character.Type.WIZARD)
                {
                    player.HP -= 3;
                    WriteLine("Penalización por entorno desértico. Reducción de 3 HP.");
                }
                
                if (player.Speed < 5)
                {
                    player.HP -= 1;
                    WriteLine("Penalización adicional por velocidad baja en entorno desértico. Reducción de 1 HP extra.");
                }
            }
            else if (currentStateId == "q4" || currentStateId == "q5" || currentStateId == "q6" || currentStateId == "q11")
            {
                if(type != Character.Type.WIZARD)
                {
                    player.HP -= 6;
                    WriteLine("Daño por calor volcánico. Reducción de 6 HP.");
                }
            }
            else if (currentStateId == "q7" || currentStateId == "q8" || currentStateId == "q9" || currentStateId == "q10" )
            {
                if (type == Character.Type.KNIGHT || type == Character.Type.THIEF)
                {
                    player.HP -= 4;
                    WriteLine("Penalización por movilidad en entorno acuático. Reducción de 4 HP.");
                }
            }
        }

        public static bool CheckBossSpawn()
        {
            if (player.Level >= 10)
            {
                return true;
            }
            else if (player.Level >= 5)
            {
                Random random = new Random();
                int roll = random.Next(1, 101);
                return roll <= 25;
            }
            return false;
        }

        public static Enemy.EnemyZone GetCurrentZone()
        {
            string id = world.CurrentState.n_State;
            if (id == "q1" || id == "q2" || id == "q3" || id == "q12") return Enemy.EnemyZone.DESERT;
            if (id == "q4" || id == "q5" || id == "q6" || id == "q11") return Enemy.EnemyZone.VOLCANIC;
            return Enemy.EnemyZone.AQUATIC;
        }

        public static void SpawnBoss()
        {
            Enemy.EnemyZone currentZone = GetCurrentZone();
            List<Enemy> bossList = enemies.FindAll(e => e.Zone == currentZone && e.isBoss == true);
            
            if (bossList.Count > 0)
            {
                Random random = new Random();
                int index = random.Next(bossList.Count);
                enemy = bossList[index];
                bossTurnCount = 0;
                bossIsDodging = false;
                
                ShowEnemyInfo();
                do
                {
                    ShowCombatMenu();
                } while (enemy != null && player.HP > 0 && (enemy.HP > 0 || enemy.Forgiveness == 0));
            }
        }

        public static bool isSpawningEnemy()
        {
            int spawnChance = 30;
            Random random = new Random();
            int roll = random.Next(1, 101);
            if (roll > spawnChance)
            {
                WriteLine("Zona libre de hostilidades. Ningún enemigo detectado.");
                return false;
            }
            else
            {
                return true;
            }
        }

        public static void SpawnEnemy()
        {
            Random random = new Random();
            switch(world.CurrentState.n_State)
            {
                case "q1":
                case "q2":
                case "q3":
                case "q12":
                    List<Enemy> desertEnemies = enemies.FindAll(enemy => enemy.Zone == Enemy.EnemyZone.DESERT && enemy.isBoss == false);
                    if (desertEnemies.Count > 0)
                    {
                        int index = random.Next(desertEnemies.Count);
                        enemy = desertEnemies[index];
                    }
                    break;
                case "q4":
                case "q5":
                case "q6":
                case "q11":
                    List<Enemy> volcanicEnemies = enemies.FindAll(enemy => enemy.Zone == Enemy.EnemyZone.VOLCANIC && enemy.isBoss == false);
                    if (volcanicEnemies.Count > 0)
                    {
                        int index = random.Next(volcanicEnemies.Count);
                        enemy = volcanicEnemies[index];
                    }
                    break;
                case "q7":
                case "q8":
                case "q9":
                case "q10":
                    List<Enemy> aquaticEnemies = enemies.FindAll(enemy => enemy.Zone == Enemy.EnemyZone.AQUATIC && enemy.isBoss == false);
                    if (aquaticEnemies.Count > 0)
                    {
                        int index = random.Next(aquaticEnemies.Count);
                        enemy = aquaticEnemies[index];
                    }
                    break;
            }
            if (enemy != null)
            {
                ShowEnemyInfo();
                do
                {
                    ShowCombatMenu();
                } while (enemy != null && player.HP > 0 && (enemy.HP > 0 || enemy.Forgiveness == 0));
            }
        }

        public static void ShowEnemyInfo()
        {
            WriteLine("------------------------------");
            WriteLine("Encuentro hostil.");
            WriteLine("Tipo de Enemigo: " + enemy.Type);
            WriteLine("HP: " + enemy.HP + "/" + enemy.healt_max);
            WriteLine("Velocidad: " + enemy.Speed);
            WriteLine("Daño: " + enemy.Damage);
            WriteLine("Probabilidad de Critico: " + enemy.CritictRate * 100 + "%");
            WriteLine("Probabilidad de Misericordia: " + enemy.Probability_Mercy * 100 + "%");
            if (enemy.isBoss)
            {
                WriteLine("Advertencia: Entidad de nivel jefe detectada.");
            }
            WriteLine("------------------------------");
        } 

        public static void ShowCombatMenu()
        {
            WriteCentered("------------------------------------------");
            WriteCentered("¿Qué acción desea realizar?");
            WriteCentered("1 - Atacar");
            WriteCentered("2 - Pedir Misericordia");
            WriteCentered("3 - Curarse");
            WriteCentered("4 - Informacion del Enemigo");
            WriteCentered("5 - Informacion del Personaje");
            WriteCentered("6 - Huir");
            WriteCentered("------------------------------------------");

            WriteLine();
            Write("Opcion:");

            int optionSelected = -1;
            do
            {
                string? input = ReadLine();
                if (!int.TryParse(input, out optionSelected))
                {
                    WriteLine("El valor ingresado no es un numero valido, por favor ingrese un numero entre 1 y 6.");
                    optionSelected = -1;
                }
                else if (optionSelected < 1 || optionSelected > 6)
                {
                    WriteLine("La opcion seleccionada no es valida, por favor ingrese un numero entre 1 y 6.");
                    optionSelected = -1;
                }
            } while (optionSelected < 1 || optionSelected > 6);

            switch (optionSelected)
            {
                case 1:
                    CombatRoutine();
                    break;
                case 2:
                    PrayMercy();
                    break;
                case 3:
                    HealRoutine();
                    break;
                case 4:
                    ShowEnemyInfo();
                    break;
                case 5:
                    ShowPlayerInfo(player);
                    break;
                case 6:
                    Flee();
                    break;
            }
            
            isLevelingUp();
        }

        public static float AttackBar()
        {
            int barLength = 30;
            int pos = 0;
            int direction = 1;
            
            CursorVisible = false;

            while (KeyAvailable) ReadKey(true);

            WriteLine("Presione ESPACIO para determinar precisión.");
            
            while (true)
            {
                if (KeyAvailable)
                {
                    if (ReadKey(true).Key == ConsoleKey.Spacebar) break;
                }

                string bar = "\r[";
                for (int i = 0; i < barLength; i++)
                {
                    if (i == barLength / 2) 
                        bar += (i == pos) ? "█" : "I";
                    else 
                        bar += (i == pos) ? "█" : "-";
                }
                bar += "]";
                Write(bar);

                pos += direction;
                
                if (pos <= 0 || pos >= barLength - 1) direction *= -1;

                System.Threading.Thread.Sleep(30); 
            }

            CursorVisible = true;
            WriteLine();

            float center = barLength / 2.0f;
            float distance = Math.Abs(center - pos);
            
            float accuracy = 1.0f - (distance / center);
            if (accuracy < 0.2f) accuracy = 0.2f;

            return accuracy;
        }

        public static float HealBar()
        {
            int barLength = 30;
            int pos = 0;
            int direction = 1;
            
            CursorVisible = false;

            while (KeyAvailable) ReadKey(true);

            WriteLine("Presione ESPACIO para determinar precisión de curación.");
            
            while (true)
            {
                if (KeyAvailable)
                {
                    if (ReadKey(true).Key == ConsoleKey.Spacebar) break;
                }

                string bar = "\r[";
                for (int i = 0; i < barLength; i++)
                {
                    if (i == barLength / 2) 
                        bar += (i == pos) ? "H" : "+"; 
                    else 
                        bar += (i == pos) ? "H" : "-"; 
                }
                bar += "]";
                Write(bar);

                pos += direction;
                
                if (pos <= 0 || pos >= barLength - 1) direction *= -1;

                System.Threading.Thread.Sleep(15); 
            }

            CursorVisible = true;
            WriteLine();

            float center = barLength / 2.0f;
            float distance = Math.Abs(center - pos);
            
            float accuracy = 1.0f - (distance / center);
            if (accuracy < 0.2f) accuracy = 0.2f;

            return accuracy;
        }

        public static void PlayerAttack()
        {
            float accuracyMultiplier = AttackBar();

            if (bossIsDodging)
            {
                WriteLine("El objetivo evadió el ataque.");
                bossIsDodging = false;
                return;
            }

            float finalDamage = player.Damage * accuracyMultiplier;

            Random rand = new Random();
            bool isCritical = rand.NextDouble() <= player.CritictRate;

            if (isCritical)
            {
                finalDamage *= 2.0f;
                WriteLine("Impacto crítico registrado.");
            }

            int damageApplied = (int)Math.Round(finalDamage);
            enemy.HP -= damageApplied;

            WriteLine($"Precisión calculada: {Math.Round(accuracyMultiplier * 100)}%.");
            WriteLine($"Daño ejecutado: {damageApplied}.");
        }

        public static void PlayerHeal()
        {
            float accuracyMultiplier = HealBar();

            int healApplied = (int)Math.Round(player.HealLevel * accuracyMultiplier);
            player.HP += healApplied;

            if (player.HP > player.healt_max)
            {
                player.HP = player.healt_max;
            }

            WriteLine($"Eficacia de curación: {Math.Round(accuracyMultiplier * 100)}%.");
            WriteLine($"Recuperación: {healApplied} HP. Salud actual: {player.HP}/{player.healt_max}.");
        }

        public static void EnemyAttack()
        {
            Random rand = new Random();
            bool isCritical = rand.NextDouble() <= enemy.CritictRate;
            if(isCritical)
            {
                player.HP -= (int)(enemy.Damage * 2.0f);
                WriteLine("El enemigo ha ejecutado un impacto crítico.");
                WriteLine($"Daño recibido: {(int)(enemy.Damage * 2.0f)}.");
            }
            else
            {
                player.HP -= (int)enemy.Damage;
                WriteLine($"Ataque enemigo recibido. Daño: {(int)enemy.Damage}.");
            }
        }

        public static void BossAutomatonTurn()
        {
            bossTurnCount++;
            string nextState = "q1";

            if (bossTurnCount % 4 == 0) 
            {
                nextState = "q4";
            }
            else
            {
                Random rand = new Random();
                int roll = rand.Next(1, 101);
                if (roll <= 25) nextState = "q3";
                else if (roll <= 60) nextState = "q2";
                else nextState = "q1";
            }

            switch (nextState)
            {
                case "q1":
                    WriteLine($"[{enemy.Type}] ejecuta patrón de ataque estándar.");
                    EnemyAttack();
                    break;

                case "q2":
                    WriteLine($"[{enemy.Type}] analiza vulnerabilidad de clase {player.CharacterType} y ataca.");
                    float damageMultiplier = 1.0f;
                    switch (player.CharacterType)
                    {
                        case Character.Type.KNIGHT: damageMultiplier = 1.5f; break;
                        case Character.Type.WIZARD: damageMultiplier = 1.8f; break;
                        case Character.Type.THIEF: damageMultiplier = 1.2f; break;
                        case Character.Type.MERMAID: damageMultiplier = 1.4f; break;
                    }
                    int classDamage = (int)(enemy.Damage * damageMultiplier);
                    player.HP -= classDamage;
                    WriteLine($"Daño crítico por afinidad de clase recibido: {classDamage}.");
                    break;

                case "q3":
                    WriteLine($"[{enemy.Type}] asume postura de evasión. El siguiente ataque será neutralizado.");
                    bossIsDodging = true;
                    break;

                case "q4":
                    WriteLine($"[{enemy.Type}] alcanza carga máxima y ejecuta habilidad definitiva.");
                    int ultiDamage = (int)(enemy.Damage * 2.5f);
                    player.HP -= ultiDamage;
                    WriteLine($"Impacto máximo recibido. Daño: {ultiDamage}.");
                    break;
            }
        }

        public static void CombatRoutine()
        {
            WriteLine("------------------------------");
            WriteLine($"Iniciando fase de combate contra {enemy.Type}.");

            if (player.Speed >= enemy.Speed)
            {
                WriteLine("Ventaja de velocidad. Turno aliado.");
                PlayerAttack();
                
                if (enemy.HP > 0) 
                {
                    if (enemy.isBoss) { BossAutomatonTurn(); }
                    else { EnemyAttack(); }
                }
            }
            else
            {
                WriteLine("Desventaja de velocidad. Turno enemigo.");
                if (enemy.isBoss) { BossAutomatonTurn(); }
                else { EnemyAttack(); }
                
                if (player.HP > 0) 
                {
                    PlayerAttack();
                }
            }
            
            if (enemy.HP <= 0)
            {
                WriteLine("Objetivo eliminado.");
                player.XP_STORAGED += enemy.XP_DROPPED;
                enemy = null;
            }
            WriteLine("------------------------------");
        }

        public static void HealRoutine()
        {
            WriteLine("------------------------------");

            if (player.Speed >= enemy.Speed)
            {
                PlayerHeal();
                
                if (enemy.HP > 0) 
                {
                    if (enemy.isBoss) { BossAutomatonTurn(); }
                    else { EnemyAttack(); }
                }
            }
            else
            {
                WriteLine("Desventaja de velocidad. Turno enemigo.");
                if (enemy.isBoss) { BossAutomatonTurn(); }
                else { EnemyAttack(); }
                
                if (player.HP > 0) 
                {
                    PlayerHeal();
                }
            }
            WriteLine("------------------------------");
        }

        public static void isLevelingUp()
        {
            if (player.XP_STORAGED >= 100)
            {
                player.Level++;
                player.XP_STORAGED -= 100; 
                player.HP = player.healt_max; 
                WriteLine($"Nivel incrementado: {player.Level}.");
            }
        }

        public static void PrayMercy()
        {
            Random rand = new Random();
            if (rand.NextDouble() <= enemy.Probability_Mercy)
            {
                WriteLine("Petición de cese al fuego aceptada por la entidad enemiga. Combate finalizado.");
                enemy = null;
            }
            else
            {
                WriteLine("Petición rechazada. El combate prosigue.");
                if (enemy.isBoss) { BossAutomatonTurn(); }
                else { EnemyAttack(); }
            }
        }

        public static void Flee()
        {
            Random rand = new Random();
            if (rand.NextDouble() <= 0.05)
            {
                WriteLine("Evasión de combate exitosa.");
                enemy = null;
            }
            else
            {
                WriteLine("Fallo en intento de evasión. El enemigo contraataca.");
                if (enemy.isBoss) { BossAutomatonTurn(); }
                else { EnemyAttack(); }
            }
        }
    }
}