namespace Item
{
    public class Weapon: Item
    {
        public float damage;
        public Weapon(float caffeine, float alcohol, float nicotine, float damage): base(caffeine, alcohol, nicotine)
        {
            this.damage = damage;
        }

        public override DamageHolder GetDamageHolder()
            => new DamageHolder(
                this.caffeine,
                this.alcohol,
                this.nicotine,
                this.damage
            );
    }
}