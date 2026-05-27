namespace EFSM_Juego
{
    public class Character
    {
        public string? name { get; set; }

        public float HP = 1.0f;

        public float Speed = 1.0f;

        public float Damage = 1.0f;

        public float CritictRate = 1.0f;

        public enum Type
        {
            KNIGHT,
            WIZARD,
            MERMAID,
            THIEF
        }

        public Type CharacterType { get; set; }
    }
}
