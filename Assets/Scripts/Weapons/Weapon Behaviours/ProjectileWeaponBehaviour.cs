using Unity.VisualScripting;
using UnityEngine;
/// <summary>
/// Base for projectile weapons
/// </summary>
public class ProjectileWeaponBehaviour : MonoBehaviour
{
    public WeaponScriptableObject weaponData;
    protected Vector3 direction;
    public float destroyAfterSeconds;

    //Current stats
    protected float currentDamage;
    protected float currentSpeed;
    protected float currentCooldownDuration;
    protected float currentPierce;

    private void Awake()
    {
        currentDamage = weaponData.Damage;
        currentSpeed = weaponData.Speed;
        currentCooldownDuration = weaponData.CooldownDuration;
        currentPierce = weaponData.Pierce;
    }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        Destroy(gameObject, destroyAfterSeconds);
    }

    public void DirectionChecker(Vector3 dir)
    {
        direction = dir;

        float dirX = direction.x;
        float dirY = direction.y;

        Vector3 scale = transform.localScale;
        Vector3 rotation = transform.rotation.eulerAngles;

        if (dirX < 0 && dirY == 0) // left
        {
            scale.x *= -1;
            scale.y *= -1;
        }
        else if (dirX == 0 && dirY < 0) // down
        {
            scale.y *= -1;
        }
        else if (dirX == 0 && dirY > 0) // up
        {
            scale.x *= -1;
        }
        else if (dirX > 0 && dirY > 0) // right up
        {
            rotation.z = 0f;
        }
        else if (dirX > 0 && dirY < 0) // right down
        {
            rotation.z = -90f;
        }
        else if (dirX < 0 && dirY > 0) // left up
        {
            scale.x *= -1;
            scale.y *= -1;
            rotation.z = -90f;
        }
        else if (dirX < 0 && dirY < 0) // left down
        {
            scale.x *= -1;
            scale.y *= -1;
            rotation.z = 0f;
        }

        transform.localScale = scale;
        transform.rotation = Quaternion.Euler(rotation);
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        //deal damage to enemy
        if (collision.CompareTag(Enums.Tags.Enemy.ToString()))
        {
            EnemyStats enemy = collision.GetComponent<EnemyStats>();
            enemy.TakeDamage(currentDamage);//allow modification to damage
            ReducePierce();
        }
        else if (collision.CompareTag(Enums.Tags.Prop.ToString()))
        {
            if (collision.TryGetComponent<BreakableProps>(out var prop))
            {
                prop.TakeDamage(currentDamage);
                ReducePierce();
            }
        }

    }

    private void ReducePierce()
    {
        currentPierce--;

        if (currentPierce <= 0)
            Destroy(gameObject);
    }
}
