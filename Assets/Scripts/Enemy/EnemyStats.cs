using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public EnemyScriptableObject enemyData;
    EnemySpawner enemySpawner;

    //current stats
    [HideInInspector]
    public float currentMoveSpeed;
    [HideInInspector]
    public float currentHealth;
    [HideInInspector]
    public float currentDamage;

    public float despawnDistance = 20f;
    Transform player;

    private void Awake()
    {
        currentMoveSpeed = enemyData.MoveSpeed;
        currentHealth = enemyData.MaxHealth;
        currentDamage = enemyData.Damage;
    }

    void Start()
    {
        enemySpawner = FindAnyObjectByType<EnemySpawner>();
        player = FindAnyObjectByType<PlayerStats>().transform;
    }

    void Update()
    {
        if (Vector2.Distance(transform.position, player.position) >= despawnDistance)
        {
            ReturnEnemy();
        }
    }

    public void TakeDamage(float dmg)
    {
        currentHealth -= dmg;

        if (currentHealth <= 0)
        {
            Kill();
        }
    }

    public void Kill()
    {
        Destroy(gameObject);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(Enums.Tags.Player.ToString()))
        {
            PlayerStats player = collision.gameObject.GetComponent<PlayerStats>();

            player.TakeDamage(currentDamage);
        }
    }

    private void ReturnEnemy()
    {
        var relativeEnemyPosition = enemySpawner.relativeEnemySpawnPoints[UnityEngine.Random.Range(0, enemySpawner.relativeEnemySpawnPoints.Count - 1)].position; //get random position

        transform.position = player.position + relativeEnemyPosition;
    }

    private void OnDestroy()
    {
        enemySpawner.OnEnemyKilled();
    }
}