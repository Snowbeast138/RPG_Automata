namespace EFSM_Juego
{
    public class Enemy
    {
        public float HP = 1.0f;

        public float Speed = 0;

        public float Damage = 1.0f;

        public float CritictRate = 1.0f;

        public bool isBoss = false;

        public enum EnemyType
        {
            //DESIERTO
            ESCORPION,
            GUSANO_DE_ARENA,
            BUITRE_CARROÑERO,
            LAGARTO_VENENOSO,
            REY_DEL_DESIERTO,
            //FIRE LANDS
            LOBO_DE_FUEGO,
            FUEGO_FATUO,
            LAGARTO_ARDIENTE,
            MAGMA_SLIME,
            DRAGON_SALAMANDER,
            //OCEAN
            TIBURON_MOTOSIERRA,
            CANGREJO_PINZA_ANZUELO,
            PIRATA_FANTASMA,
            ELECTROMEDUSA,
            CHUTULU
        }

        public EnemyType Type { get; set; }

        public Enemy(
            float hp,
            float speed,
            float damage,
            float critictRate,
            bool isBoss,
            EnemyType type
        )
        {
            HP = hp;
            Speed = speed;
            Damage = damage;
            CritictRate = critictRate;
            isBoss = isBoss;
            Type = type;
        }
    }
}
