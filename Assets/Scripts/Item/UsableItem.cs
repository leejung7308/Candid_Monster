namespace Item
{
    public class UsableItem : Item
    {
        public int healAmount;
        Player player;
        private void Start()
        {
            player = FindObjectOfType<Player>();
        }
        public override void Use()
        {
            player.fatigue -= healAmount;
            if(player.fatigue < 0 ) { player.fatigue = 0; }
        }
        public override int GetData()
        {
            return healAmount;
        }
        public override bool IsEnchanted()
        {
            return false;
        }
        public override void AddData(int data)
        {
            return;
        }
    }
}