namespace Item
{
    public class Weapon: Item
    {
        public float damage;
        public Weapon(int caffeine, int alcohol, int nicotine, float damage): base(caffeine, alcohol, nicotine)
        {
            this.damage = damage;
        }

        public override DamageHolder GetDamageHolder()
            => new DamageHolder(
                caffeine,
                alcohol,
                nicotine,
                damage
            );
    }
}