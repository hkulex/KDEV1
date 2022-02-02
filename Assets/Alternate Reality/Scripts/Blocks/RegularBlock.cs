namespace AlternateReality.Blocks
{
    public class RegularBlock : BaseBlock
    {
        protected override void OnDie()
        {
            
        }

        protected override void OnSpawn()
        {
            health = 1;
            score = 1;
        }

        protected override void OnRemove()
        {
            
        }
    }
}