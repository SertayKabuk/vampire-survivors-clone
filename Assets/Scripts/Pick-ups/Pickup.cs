using UnityEngine;

public class Pickup : MonoBehaviour
{
    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(Enums.Tags.Player.ToString()))
        {
            Destroy(gameObject);
        }
    }
}
