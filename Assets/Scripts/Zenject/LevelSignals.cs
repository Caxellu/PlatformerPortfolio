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
public class RestartSignal { }
public class UnPauseSignal { }
public class  PlayerDeadSignal{}
public class LevelCompleteSignal{}
public class FreezeSignal { }
public class UnFreezeSignal { }