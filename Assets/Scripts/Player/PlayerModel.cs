using UnityEngine;

public class PlayerModel 
{
    public float MaxSpeed { get; }
    public Health Health { get; private set; }
    public float JumpForce { get; }
    public Vector3 SpawnPos { get; }
    public PlayerModel(float maxSpeed, float jumpForce, int maxHP, Vector3 vector3)
    {
        MaxSpeed = maxSpeed;
        JumpForce = jumpForce;
        Health = new Health(maxHP);
        SpawnPos = vector3;
    }
    public void ResetHP()
    {
        Health.SetMaxHp();
    }
}
