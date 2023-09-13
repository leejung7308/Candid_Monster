namespace Item
{
    public class Weapon: Item
    {
        public float damage;
        public Weapon(float caffeine, float alcohol, float nicotine, float damage): base(caffeine, alcohol, nicotine)
        {
            damage = damage;
        }
    }
}