using UnityEngine;

public class PlayerCollector : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(Enums.Tags.Collectible.ToString()))
        {
            if (collision.gameObject.TryGetComponent<ICollectable>(out var collectible))
            {
                collectible.Collect();
            }
        }
    }
}
