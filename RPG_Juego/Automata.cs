using System;
using System.Collections.Generic;

namespace EFSM_Juego
{
    public class Automata
    {
        public List<State> States { get; set; } = new List<State>();

        public State CurrentState { get; set; }

        public void AddState(State state)
        {
            States.Add (state);

            if (States.Count == 1)
            {
                CurrentState = state;
            }
        }
    }
}
