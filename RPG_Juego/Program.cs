using System;
using System.Collections.Generic;

using static System.Console;

namespace EFSM_Juego
{
    class Program
    {
        static Character[] characters = DefiningCharacters();

        static int
            indexCharacterSelected = SelectingTypeCharacter(characters.Length);

        static Character
            player = CreatingPlayer(characters, indexCharacterSelected);

        static Automata world = DefiningWorld();

        static List<Enemy> enemies = new List<Enemy>();

        static Enemy enemy= null;

        static void Main()
        {
            DefiningEnemies();

            WriteLine("Bienvenido al pueblo de Tierras Lejanas " +
            player.name +
            "!");

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
                    WriteLine("El valor ingresado no es un numero valido, por favor ingrese un numero entre 0 y " +
                    (lenghtCharacters - 1));
                    indexCharacterSelected = -1; // Reiniciar el índice para continuar el bucle
                }
                else if (
                    indexCharacterSelected < 0 || indexCharacterSelected > 3
                )
                {
                    WriteLine("El indice del personaje seleccionado no es valido, ingrese un valor entre 0 y " +
                    (lenghtCharacters - 1));
                    indexCharacterSelected = -1; // Reiniciar el índice para continuar el bucle
                }
            }
            while (indexCharacterSelected < 0 || indexCharacterSelected > 3);

            return indexCharacterSelected;
        }

        public static Character
        CreatingPlayer(Character[] characters, int indexCharacterSelected)
        {
            Character player = new Character();
            do
            {
                Write("Ingrese el Nombre del Jugador:");
                player.name = ReadLine();
                if (player.name == null || player.name == "")
                {
                    WriteLine("El nombre del jugador no puede ser nulo, por favor ingrese un nombre valido");
                }
            }
            while (player.name == null);

            if (
                indexCharacterSelected >= 0 &&
                indexCharacterSelected < characters.Length
            )
            {
                //Cargamos con las stats predefinidas del personaje seleccionado
                player.HP = characters[indexCharacterSelected].HP;
                player.healt_max = characters[indexCharacterSelected].healt_max;
                player.Speed = characters[indexCharacterSelected].Speed;
                player.Damage = characters[indexCharacterSelected].Damage;
                player.CritictRate =
                    characters[indexCharacterSelected].CritictRate;
                player.CharacterType =
                    characters[indexCharacterSelected].CharacterType;
                player.Level = characters[indexCharacterSelected].Level;
                player.XP_STORAGED = characters[indexCharacterSelected].XP_STORAGED;
                player.HealLevel = characters[indexCharacterSelected].HealLevel;
            }
            else
            {
                do
                {
                    WriteLine("El indice del personaje seleccionado no es valido, ingrese un valor entre 0 y " +
                    (characters.Length - 1));
                    string? input = ReadLine();
                    if (!int.TryParse(input, out indexCharacterSelected))
                    {
                        WriteLine("El valor ingresado no es un numero valido, por favor ingrese un numero entre 0 y " +
                        (characters.Length - 1));
                        indexCharacterSelected = -1;
                    }
                    indexCharacterSelected = int.Parse(input);
                }
                while (indexCharacterSelected < 0 ||
                    indexCharacterSelected > characters.Length - 1
                );
            }

            WriteLine("Personaje creado exitosamente!");

            ShowPlayerInfo (player);

            return player;
        }

        public static void ShowPlayerInfo(Character player)
        {
            WriteLine("------------------------------");
            WriteLine("Nombre del Jugador: " + player.name);
            WriteLine("Tipo de Personaje: " + player.CharacterType);
            WriteLine("HP: " + player.HP+"/"+ player.healt_max);
            WriteLine("Velocidad: " + player.Speed);
            WriteLine("Daño: " + player.Damage);
            WriteLine("Probabilidad de Critico: " +
            player.CritictRate * 100 +
            "%");
            WriteLine("Nivel: " + player.Level);
            WriteLine("Experiencia Acumulada: " + player.XP_STORAGED+"/100");
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
            WriteLine("Lugar Actual del Personaje: " +
            world.CurrentState.Context);
            WriteLine("------------------------------");
        }

        public static Character[] DefiningCharacters()
        {
            //Definimos los atributos de cada personaje y los enumeramos para diferenciar cada tipo con un valor numerico
            //0 == Knight
            //1 == Mermaid
            //2== Wizard
            //3 == Thief
            Character[] characters =
                new Character[] {
                    new Character {
                        HP = 60, //health
                        healt_max = 60, //health_max
                        Speed = 5, //speed
                        Damage = 20, //damage
                        CritictRate = 0.25f, //critictRate
                        Level = 1, //level
                        XP_STORAGED = 0, //xp_storaged
                        HealLevel = 5.0f, //healLevel
                        CharacterType = Character.Type.KNIGHT //characterType
                    },
                    new Character {
                        HP = 100, //health
                        healt_max = 100, //health_max
                        Speed = 8, //speed
                        Damage = 15, //damage
                        CritictRate = 0.2f, //critictRate
                        Level = 1, //level
                        XP_STORAGED = 0, //xp_storaged
                        HealLevel = 10.0f, //healLevel
                        CharacterType = Character.Type.WIZARD //characterType
                    },
                    new Character {
                        HP = 80, //health
                        healt_max = 80, //health_max
                        Speed = 3, //speed
                        Damage = 5, //damage
                        CritictRate = 0.2f, //critictRate
                        Level = 1, //level
                        XP_STORAGED = 0, //xp_storaged
                        HealLevel = 20.0f, //healLevel
                        CharacterType = Character.Type.MERMAID //characterType
                    },
                    new Character {
                        HP = 40, //health
                        healt_max = 40, //health_max
                        Speed = 15, //speed
                        Damage = 10, //damage
                        CritictRate = 0.4f, //critictRate
                        Level = 1, //level
                        XP_STORAGED = 0, //xp_storaged
                        HealLevel = 10.0f, //healLevel
                        CharacterType = Character.Type.THIEF //characterType
                    },
    
                };

            return characters;
        }

        public static void DefiningEnemies()
        {

            enemies.Add( new Enemy(10,10, 5, 2, 0.05f, 0, 0.15f, 10, false, Enemy.EnemyType.ESCORPION, Enemy.EnemyZone.DESERT) );
            enemies.Add( new Enemy(15,15, 3, 4, 0.1f, 0, 0.15f, 15, false, Enemy.EnemyType.GUSANO_DE_ARENA, Enemy.EnemyZone.DESERT) );
            enemies.Add( new Enemy(20,20, 2, 6, 0.15f, 0, 0.15f, 20, false, Enemy.EnemyType.BUITRE_CARROÑERO, Enemy.EnemyZone.DESERT) );
            enemies.Add( new Enemy(25,25, 4, 8, 0.2f, 0, 0.15f, 25, false, Enemy.EnemyType.LAGARTO_VENENOSO, Enemy.EnemyZone.DESERT) );
            enemies.Add( new Enemy(30,30, 7, 10, 0.3f, 0, 0.05f, 30, false, Enemy.EnemyType.LOBO_DE_FUEGO, Enemy.EnemyZone.VOLCANIC) );
            enemies.Add( new Enemy(25,25, 5, 8, 0.25f, 0, 0.1f, 25, false, Enemy.EnemyType.FUEGO_FATUO, Enemy.EnemyZone.VOLCANIC) );
            enemies.Add( new Enemy(35,35, 4, 12, 0.3f, 0, 0.05f, 35, false, Enemy.EnemyType.LAGARTO_ARDIENTE, Enemy.EnemyZone.VOLCANIC) );
            enemies.Add( new Enemy(40,40, 3, 15, 0.35f, 0, 0.1f, 40, false, Enemy.EnemyType.MAGMA_SLIME, Enemy.EnemyZone.VOLCANIC) );
            enemies.Add( new Enemy(30,30, 6, 5, 0.1f, 0, 0.05f, 50, false, Enemy.EnemyType.TIBURON_MOTOSIERRA, Enemy.EnemyZone.AQUATIC) );
            enemies.Add( new Enemy(50,50,7, 7, 0.15f, 0, 0.2f, 45, false, Enemy.EnemyType.CANGREJO_PINZA_ANZUELO,Enemy.EnemyZone.AQUATIC) );
            enemies.Add( new Enemy(20,20, 4, 5, 0.3f, 0, 0.05f, 25, false, Enemy.EnemyType.PIRATA_FANTASMA,Enemy.EnemyZone.AQUATIC) );
            enemies.Add( new Enemy(60,60, 5, 3, 0.2f, 0, 0.3f, 80, false,Enemy.EnemyType.ELECTROMEDUSA,Enemy.EnemyZone.AQUATIC) ) ;
            enemies.Add( new Enemy(150,150, 6, 12, 0.25f, 0, 0.0f, 150, true, Enemy.EnemyType.REY_DEL_DESIERTO,Enemy.EnemyZone.DESERT)) ;
            enemies.Add( new Enemy(200,200, 8, 20, 0.4f, 0, 0.0f, 200, true, Enemy.EnemyType.DRAGON_SALAMANDER,Enemy.EnemyZone.VOLCANIC));
            enemies.Add( new Enemy(300,300, 6, 25, 0.45f, 0, 0.0f, 300, true, Enemy.EnemyType.CHUTULU,Enemy.EnemyZone.AQUATIC));
            

         
        }

        public static Automata DefiningWorld()
        {
            //Inicializamos el objeto Automata para el mundo
            Automata world = new Automata();

            //Definimos el Estado y Su Contexto
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
            //Bossfight oceano
            world.AddState(new State("San Marisco", "q10"));
            //Bossfight volcan
            world.AddState(new State("Castillo Salamander", "q11"));
            //Bossfight desierto
            world.AddState(new State("Olin Oir", "q12"));




            return world;
        }

        public static void WriteCentered(string text)
        {
            // Obtenemos el ancho total de la ventana de la consola
            int windowWidth = Console.WindowWidth;

            // Calculamos cuántos espacios necesitamos a la izquierda para centrar el texto
            int padding = (windowWidth - text.Length) / 2;

            // Aseguramos que el padding no sea negativo (por si el texto es más largo que la consola)
            padding = Math.Max(0, padding);

            // Imprimimos el texto con los espacios añadidos
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
                        WriteLine("El valor ingresado no es un numero valido, por favor ingrese un numero entre 1 y 4");
                        optionSelected = -1;
                    }
                    else if (optionSelected < 1 || optionSelected > 4)
                    {
                        WriteLine("La opcion seleccionada no es valida, por favor ingrese un numero entre 1 y 4");
                        optionSelected = -1;
                    }

                    switch (optionSelected)
                    {
                        case 1:
                            Walkthrough();
                            break;
                        case 2:
                            ShowPlayerInfo (player);
                            break;
                        case 3:
                            ShowWorldInfo (world);
                            break;
                        case 4:
                            WriteLine("Gracias por jugar. ¡Hasta la próxima!");
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
                    WriteLine("El valor ingresado no es un numero valido, por favor ingrese un numero entre 1 y 4");
                    direction = -1;
                }
                else if (direction < 1 || direction > 4)
                {
                    WriteLine("La direccion ingresada no es valida, por favor ingrese un numero entre 1 y 4");
                    direction = -1;
                }
            }
            while (direction < 1 || direction > 4);

            WalkthroughResult (direction);
        }

        public static void WalkthroughResult(int direction)
        {
            // 1. Determinamos el nombre de la dirección
            string directionName = direction switch
            {
                1 => "Norte",
                2 => "Sur",
                3 => "Este",
                4 => "Oeste",
                _ => "dirección desconocida"
            };

            WriteLine($"Caminas hacia el {directionName}...");

            // Guardamos el estado inicial para poder mostrar desde dónde partió
            State previousState = world.CurrentState;
            string currentStateId = previousState.n_State;

            // 2. Evaluamos la tabla de transiciones usando Tuple Pattern Matching
            // Si la combinación no está declarada, cae en el caso por defecto "_" que devuelve "-"
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
                
                _ => "-" // Movimiento inválido (cualquier combinación que sea '-')
            };

            // 3. Aplicamos el resultado de la tabla
            if (nextStateId == "-")
            {
                WriteLine($"No puedes caminar hacia el {directionName} desde {previousState.Context}. Has llegado al límite del mundo.");
            }
            else
            {
                // Actualizamos el estado buscando la ID resultante en la lista de estados del mundo
                world.CurrentState = world.States.Find(state => state.n_State == nextStateId)!;
                
                // Imprimimos de dónde a dónde viajó
                WriteLine($"Has viajado desde [{previousState.Context}] hacia [{world.CurrentState.Context}].");

                if (isSpawningEnemy())
                {
                    SpawnEnemy();
                }

                CheckEnvironmentalEffects();
            }
        }

        public static void CheckEnvironmentalEffects()
        {
            // Obtenemos el ID del lugar actual y el tipo de personaje para no escribir tanto
            string currentStateId = world.CurrentState.n_State;
            Character.Type type = player.CharacterType;

            // --- ZONAS DE DESIERTO (q1: Dunas, q2: Pirámide, q3: Oasis, q12: Olin Rio) ---
            if (currentStateId == "q1" || currentStateId == "q2" || currentStateId == "q3" || currentStateId == "q12")
            {
                // Condicional por tipo
                if (type == Character.Type.MERMAID || type == Character.Type.WIZARD) //La sirena y el mago pierden vida en el desierto
                {
                    player.HP -= 3;
                    WriteLine("Sientes una sed insaciable... Has perdido 3 de HP.");
                }
                
                // Ejemplo extra: Condicional combinando otro atributo del Character (Speed)
                if (player.Speed < 5)
                {
                    player.HP -= 1;
                    WriteLine("El sol implacable te agota debido a tu baja velocidad... Pierdes 1 de HP extra.");
                }
            }

            // --- ZONAS VOLCÁNICAS (q4: Ruinas Volcánicas, q5: Bosque Cenizas, q6: Kakacatepetl, q11: Castillo Salamander) ---
            else if (currentStateId == "q4" || currentStateId == "q5" || currentStateId == "q6" || currentStateId == "q11")
            {
                // Todos pierden HP sin importar la clase
                if(type != Character.Type.WIZARD) //El mago es inmune al calor volcánico por su afinidad con el elemento fuego, mientras que la sirena es resistente pero no inmune
                {
                    player.HP -= 6;
                    WriteLine("Sientes cómo el calor abrazador del area consume tu cuerpo... Has perdido 6 de HP.");
                }
            }

            // --- ZONAS DE AGUA (q7: Jardín, q8: Campo, q9: Ruinas, q10: San Marisco) ---
            else if (currentStateId == "q7" || currentStateId == "q8" || currentStateId == "q9" || 
                    currentStateId == "q10" )
            {
                // Solo Caballero o Ladrón reciben penalización
                if (type == Character.Type.KNIGHT || type == Character.Type.THIEF)
                {
                    player.HP -= 4;
                    WriteLine("El agua dificulta el movimiento de tu armadura/equipo y te ahogas un poco... Has perdido 4 de HP.");
                }
            }
        }

        public static bool isSpawningEnemy()
        {
            int spawnChance = 30; // Probabilidad de que aparezca un enemigo (30%)
            Random random = new Random();
            int roll = random.Next(1, 101); // Genera un número entre 1 y 100
            if (roll > spawnChance)
            {
                // No aparece enemigo
                WriteLine("El camino está despejado. No hay enemigos a la vista.");
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
                //Cargamos la lista de enemigos del desierto
                    List<Enemy> desertEnemies = enemies.FindAll(enemy => enemy.Zone == Enemy.EnemyZone.DESERT && enemy.isBoss == false);
                    if (desertEnemies.Count > 0)                    {
                        int index = random.Next(desertEnemies.Count);
                        enemy = desertEnemies[index];
                    }
                    break;
                case "q4":
                case "q5":
                case "q6":
                case "q11":
                    List<Enemy> volcanicEnemies = enemies.FindAll(enemy => enemy.Zone == Enemy.EnemyZone.VOLCANIC && enemy.isBoss == false);
                    if (volcanicEnemies.Count > 0)                    {
                        int index = random.Next(volcanicEnemies.Count);
                        enemy = volcanicEnemies[index];
                    }
                    break;
                case "q7":
                case "q8":
                case "q9":
                case "q10":
                    List<Enemy> aquaticEnemies = enemies.FindAll(enemy => enemy.Zone == Enemy.EnemyZone.AQUATIC && enemy.isBoss == false);
                    if (aquaticEnemies.Count > 0)                    {
                        int index = random.Next(aquaticEnemies.Count);
                        enemy = aquaticEnemies[index];
                    }
                    break;
            }
            if (enemy != null)
            {
                ShowEnemyInfo();
            }
        }

        public static void ShowEnemyInfo()
        {
            WriteLine("------------------------------");
            WriteLine("¡Un enemigo salvaje aparece!");
            WriteLine("Tipo de Enemigo: " + enemy.Type);
            WriteLine("HP: " + enemy.HP + "/" + enemy.healt_max);
            WriteLine("Velocidad: " + enemy.Speed);
            WriteLine("Daño: " + enemy.Damage);
            WriteLine("Probabilidad de Critico: " +
            enemy.CritictRate * 100 +
            "%");
            WriteLine("Probabilidad de Misericordia: " +
            enemy.Probability_Mercy * 100 +
            "%");
            if (enemy.isBoss)
            {
                WriteLine("¡Cuidado! Este enemigo es un jefe.");
            }
            WriteLine("------------------------------");
        }   
    }

    
}
