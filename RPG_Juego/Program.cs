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

        static Enemy[] enemies = DefiningEnemies(); 

        static void Main()
        {
            WriteLine("Bienvenido al pueblo de Tierras Lejanas " +
            player.name +
            "!");

            ShowMenu();
        }

        public static int SelectingTypeCharacter(int lenghtCharacters)
        {
            WriteLine("Seleccione el tipo de personaje que desea jugar:");
            WriteLine("0 - Caballero");
            WriteLine("1 - Sirena");
            WriteLine("2 - Mago");
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
                player.Speed = characters[indexCharacterSelected].Speed;
                player.Damage = characters[indexCharacterSelected].Damage;
                player.CritictRate =
                    characters[indexCharacterSelected].CritictRate;
                player.CharacterType =
                    characters[indexCharacterSelected].CharacterType;
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
            WriteLine("HP: " + player.HP);
            WriteLine("Velocidad: " + player.Speed);
            WriteLine("Daño: " + player.Damage);
            WriteLine("Probabilidad de Critico: " +
            player.CritictRate * 100 +
            "%");
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
                        HP = 20,
                        Speed = 5,
                        Damage = 10,
                        CritictRate = 0.1f,
                        CharacterType = Character.Type.KNIGHT
                    },
                    new Character {
                        HP = 30,
                        Speed = 1,
                        Damage = 3,
                        CritictRate = 0.15f,
                        CharacterType = Character.Type.MERMAID
                    },
                    new Character {
                        HP = 15,
                        Speed = 10,
                        Damage = 10,
                        CritictRate = 0.2f,
                        CharacterType = Character.Type.WIZARD
                    },
                    new Character {
                        HP = 20,
                        Speed = 15,
                        Damage = 7,
                        CritictRate = 0.25f,
                        CharacterType = Character.Type.THIEF
                    }
                };

            return characters;
        }

        public static Enemy[] DefiningEnemies()
        {
            Enemy[] enemies =
                new Enemy[] {
                    //DESIERTO
                    new Enemy(10, 5, 2, 0.05f, false, Enemy.EnemyType.ESCORPION),
                    new Enemy(15, 3, 4, 0.1f, false, Enemy.EnemyType.GUSANO_DE_ARENA),
                    new Enemy(20, 2, 6, 0.15f, false, Enemy.EnemyType.BUITRE_CARROÑERO),
                    new Enemy(25, 4, 8, 0.2f, false, Enemy.EnemyType.LAGARTO_VENENOSO),
                    //FIRE LANDS
                    new Enemy(30, 7, 10, 0.3f, false, Enemy.EnemyType.LOBO_DE_FUEGO),
                    new Enemy(25, 5, 8, 0.25f, false, Enemy.EnemyType.FUEGO_FATUO),
                    new Enemy(35, 4, 12, 0.3f, false, Enemy.EnemyType.LAGARTO_ARDIENTE),
                    new Enemy(40, 3, 15, 0.35f, false, Enemy.EnemyType.MAGMA_SLIME),
                    //OCEAN
                    new Enemy(20, 6, 5, 0.1f, false, Enemy.EnemyType.TIBURON_MOTOSIERRA),
                    new Enemy(25,7, 7, 0.15f, false, Enemy.EnemyType.CANGREJO_PINZA_ANZUELO),
                    new Enemy(15, 4, 5, 0.3f, false, Enemy.EnemyType.PIRATA_FANTASMA),
                    new Enemy(10, 5, 3, 0.2f, false, Enemy.EnemyType.ELECTROMEDUSA),


                    //BOSSES
                    new Enemy(150, 6, 12, 0.25f, true, Enemy.EnemyType.REY_DEL_DESIERTO),
                    new Enemy(200, 8, 20, 0.4f, true, Enemy.EnemyType.DRAGON_SALAMANDER),
                    new Enemy(300, 6, 25, 0.45f, true, Enemy.EnemyType.CHUTULU),
                    // Agrega más enemigos según sea necesario
                };

            return enemies;
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
            world.AddState(new State("San Marisco", "q10"));
            world.AddState(new State("Castillo Salamander", "q11"));
            world.AddState(new State("Olin Rio", "q12"));




            //Direcciones 
            // q0, Norte -> q1, Sur -> q0, Este -> q4, Oeste -> q7



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
        
    }

    
}
