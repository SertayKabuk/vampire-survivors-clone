using UnityEngine;

public class PlayerCollector : MonoBehaviour
{
    PlayerStats player;
    CircleCollider2D magnetCollider;
    public float pullSpeed;

    void Start()
    {
        player = FindAnyObjectByType<PlayerStats>();
        magnetCollider = GetComponent<CircleCollider2D>();
    }


    private void Update()
    {
        magnetCollider.radius = player.currentMagnet;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag(Enums.Tags.Collectible.ToString()))
            return;

        if (collision.gameObject.TryGetComponent<ICollectable>(out var collectible))
        {
            //Item pull animation, add force to item to pull that item to player
            Rigidbody2D itemBody = collision.gameObject.GetComponent<Rigidbody2D>();
            Vector2 forceDirection = (transform.position - collision.transform.position).normalized; //item to player vector
            itemBody.AddForce(forceDirection * pullSpeed);

            collectible.Collect();
        }
    }
}