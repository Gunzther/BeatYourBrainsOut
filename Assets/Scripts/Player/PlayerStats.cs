using BBO.BBO.GameData;

namespace BBO.BBO.PlayerManagement
{
    public class PlayerStats
    {
        public int Health => health;
        public int DamageDealScore => damageDealScore;
        public int DamageReceivedScore => damageReceivedScore;
        public int CraftingDoneScore => craftingDoneScore;
        public int SummaryScore => damageDealScore + damageReceivedScore + craftingDoneScore;

        private readonly int playerID = default;
        private int health = default;
        private int damageDealScore = default;
        private int damageReceivedScore = default;
        private int craftingDoneScore = default;

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

        public void UpdateDamageDealScore(int value)
        {
            damageDealScore += value;
        }

        public void UpdateDamageReceivedScore(int value)
        {
            damageReceivedScore -= value;
        }

        public void UpdateCraftingDoneScore(int value)
        {
            craftingDoneScore += value;
        }
    }
}
