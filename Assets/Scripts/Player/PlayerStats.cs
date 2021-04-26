using BBO.BBO.GameData;

namespace BBO.BBO.PlayerManagement
{
    public class PlayerStats
    {
        public int Health => health;
        public int DamageDealScore => damageDealScore;
        public int DamageReceivedScore => damageReceivedScore;
        public int CraftingDoneScore => craftingDoneScore;
        public int JukingDoneScore => jukingDoneScore;
        public int HealingDoneScore => healingDoneScore;
        public int SummaryScore => summaryScore;
        
        private readonly int playerID = default;
        private int health = default;
        private int damageDealScore = default;
        private int damageReceivedScore = default;
        private int craftingDoneScore = default;
        private int jukingDoneScore = default;
        private int healingDoneScore = default;
        private int summaryScore = default;

        public PlayerStats(int playerID)
        {
            this.playerID = playerID;
            Reset();
        }

        public void Reset()
        {
            health = PlayerData.DefaultHealth;
        }

        public void SetPlayerHealth(int value)
        {
            health = value;
        }

        public void DecreasePlayerHealth(int value)
        {
            health -= value;
        }

        public void IncreasePlayerHealth(int value)
        {
            health += value;
        }
        
        public void IncreaseDamageDealScore(int value)
        {
            damageDealScore += value;
        }

        public void IncreaseCraftingDoneScore(int value)
        {
            craftingDoneScore += value;
        }

        public void IncreaseDamageReceivedScore(int value)
        {
            damageReceivedScore += value;
        }

        public void IncreaseJukingDoneScore(int value)
        {
            jukingDoneScore += value;
        }

        public void IncreaseHealingDoneScore(int value)
        {
            healingDoneScore += value;
        }

        public float GetSummaryScore()
        {
            // TODO : put some weighting for each score before return
            summaryScore = damageDealScore + damageReceivedScore + craftingDoneScore + jukingDoneScore +
                           healingDoneScore;
            return summaryScore;
        }
    }
}
