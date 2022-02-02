namespace AlternateReality.Blocks
{
    public class ExplosiveBlock : BaseBlock
    {
        
        protected override void OnSpawn()
        {
            health = 2;
            score = 5;
        }

        protected override void OnDie()
        {
            
        }
        
        protected override void OnRemove()
        {
            
        }
    }
}