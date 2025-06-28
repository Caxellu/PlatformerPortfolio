public class StartRightMoveSignal { }
public class StopRightMoveSignal { }
public class StartLeftMoveSignal { }
public class StopLeftMoveSignal { }
public class JumpSignal { }
public class FireSignal { }
public class PauseSignal { }
public class  PlayerDeadSignal{}
public class EnemyCollisionSignal {
    public int Damage;
    public EnemyCollisionSignal(int damage) { Damage = damage;}
}