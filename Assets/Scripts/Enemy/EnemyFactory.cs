using System.Collections.Generic;
using System.Numerics;

public class EnemyFactory 
{
    private List<EnemySO> enemyList;
    public void Initialize(List<EnemySO> list)
    {
        enemyList = list;
    }
   public EnemySO Get(EnemyType type)
    {
        return enemyList.Find(x=>x.enemyType==type);
    }
}
