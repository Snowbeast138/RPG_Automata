namespace EFSM_Juego
{
    public class State
    {
        public string Context { get; set; }

        public string n_State { get; set; }

        public State(string Context, string n_State)
        {
            this.Context = Context;
            this.n_State = n_State;
        }
    }
}
