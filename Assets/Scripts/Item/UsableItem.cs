namespace Item
{
    public class UsableItem : Item
    {

        public UsableItem(int caffeine, int alcohol, int nicotine) : base(caffeine, alcohol, nicotine)
        {
        }
        public override void Use()
        {
        }
        public override float GetData()
        {
            return 0;
        }
    }
}