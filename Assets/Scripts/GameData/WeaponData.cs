namespace BBO.BBO.GameData
{
    public static class WeaponData
    {
        public enum Type
        {
            Normal, // weapon that attack object
            IntervalDamage, // weapon that attack for a certain time, then disappear
            LimitAttacksNumber, // weapon that has limit number of attacks
            Protected // weapon for protection
        }

        public enum Weapon
        {
            NoWeapon,
            Baseballbat,
            Nail,
            RubberBand
        }
    }
}
