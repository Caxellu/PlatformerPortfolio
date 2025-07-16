using UnityEngine;

[RequireComponent(typeof(HitBulletLifeCycle))]
public class HitView : MonoBehaviour
{
    private IBulletFactory _bulletFactory;
    public HitBulletLifeCycle LifeCycle { get; private set; }

    private void Awake()
    {
        LifeCycle = GetComponent<HitBulletLifeCycle>();
    }
    public void Construct( IBulletFactory bulletFactory)
    {
        _bulletFactory= bulletFactory;
    }
    public void Deactivate()
    {
        gameObject.SetActive(false);
        _bulletFactory.ReturnToPool(this);
    }
    public void Activate()
    {
        gameObject.SetActive(true);
    }
}
