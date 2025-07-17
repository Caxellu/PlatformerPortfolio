public class PlayerModel 
{
    public float MaxSpeed { get; }
    public int MaxHP { get; }
    public Health Health { get; private set; }
    public float JumpForce { get; }
    public PlayerModel(float maxSpeed, float jumpForce, int maxHP)
    {
        MaxSpeed = maxSpeed;
        JumpForce = jumpForce;
        MaxHP = maxHP;
        Health = new Health(maxHP);
    }
}
