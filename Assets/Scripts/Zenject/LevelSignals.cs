using UnityEngine;

public class StartRightMoveSignal { }
public class StopRightMoveSignal { }
public class StartLeftMoveSignal { }
public class StopLeftMoveSignal { }
public class JumpSignal { }
public class TryFireSignal { }
public class FireSignal {
    public bool IsRightDir;
    public FireSignal(bool isRightDir) { IsRightDir = isRightDir; }
}
public class BulletHitSignal
{
    public Vector2 Pos;
    public BulletHitSignal(Vector2 pos) { Pos = pos; }
}
public class PauseSignal { }
public class  PlayerDeadSignal{}
public class EnemyCollisionSignal {
    public int Damage;
    public EnemyCollisionSignal(int damage) { Damage = damage;}
}