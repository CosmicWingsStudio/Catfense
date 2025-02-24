using UnityEngine;

public class ExplosionAnimation : MonoBehaviour
{
    public void DestroyParentObject()
    {
        Destroy(transform.parent.gameObject, 1f);
    }

    public void DestroyObject()
    {
        Destroy(gameObject, 1f);
    }
}
