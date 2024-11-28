using UnityEngine;
using Zenject;

public class EnemyFactory : PlaceholderFactory<Enemy>
{
    public override Enemy Create()
    {
        return base.Create();
    }
}
