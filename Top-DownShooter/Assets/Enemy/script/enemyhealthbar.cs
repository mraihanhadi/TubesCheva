using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    public Transform enemy;
    public Vector3 offset;
    private void Update()
    {
        transform.rotation = Quaternion.identity; // Keep the health bar from rotating
    }
}