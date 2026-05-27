namespace EFSM_Juego
{
    public class Enemy
    {
        public float healt_max = 1.0f;

        public float HP = 1.0f;

        public float Speed = 0;

        public float Damage = 1.0f;

        public float CritictRate = 1.0f;

        public int Forgiveness = 0;

        public float Probability_Mercy = 0.0f;

        public int XP_DROPPED = 0;

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

        public enum EnemyZone
        {
            DESERT,
            VOLCANIC,
            AQUATIC
        }

        public EnemyZone Zone { get; set; }

        public Enemy()
        {
        }

        public Enemy(
            float healt_max,
            float hp,
            float speed,
            float damage,
            float critictRate,
            int forgiveness,
            float probability_Mercy,
            int XP_DROPPED,
            bool isBoss,
            EnemyType type,
            EnemyZone zone
        )
        {
            this.healt_max = healt_max;
            HP = hp;
            Speed = speed;
            Damage = damage;
            CritictRate = critictRate;
            Forgiveness = forgiveness;
            Probability_Mercy = probability_Mercy;
            this.XP_DROPPED = XP_DROPPED;
            this.isBoss = isBoss;
            Type = type;
            Zone = zone;
        }
    }
}
