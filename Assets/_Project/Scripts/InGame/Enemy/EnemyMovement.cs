using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public bool CanMove { get; set; } = true;

    private void Update()
    {
        if (!CanMove)
            return;

        Vector2 vec2 = transform.position;
        vec2.x += -Time.deltaTime;
        transform.position = vec2;
    }
}
