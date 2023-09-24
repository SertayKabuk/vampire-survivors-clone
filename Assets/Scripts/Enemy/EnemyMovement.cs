using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    Transform player;
    EnemyStats enemy;

    // Start is called before the first frame update
    void Start()
    {
        player = FindAnyObjectByType<PlayerMovement>().transform;
        enemy = GetComponent<EnemyStats>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, enemy.currentMoveSpeed * Time.deltaTime);
    }
}
