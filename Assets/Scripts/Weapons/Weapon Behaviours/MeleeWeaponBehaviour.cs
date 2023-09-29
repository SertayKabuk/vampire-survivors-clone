using UnityEngine;
/// <summary>
/// Base for all melee weapons
/// </summary>
public class MeleeWeaponBehaviour : MonoBehaviour
{
    public WeaponScriptableObject weaponData;
    public float destroyAfterSeconds;
    PlayerStats player;

    //Current stats
    protected float currentDamage;
    protected float currentSpeed;
    protected float currentCooldownDuration;
    protected float currentPierce;

    private void Awake()
    {
        player = FindAnyObjectByType<PlayerStats>();
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

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        //deal damage to enemy
        if (collision.CompareTag(Enums.Tags.Enemy.ToString()))
        {
            EnemyStats enemy = collision.GetComponent<EnemyStats>();
            enemy.TakeDamage(GetCurrentDamage());//allow modification to damage
        }
        else if (collision.CompareTag(Enums.Tags.Prop.ToString()))
        {
            if (collision.TryGetComponent<BreakableProps>(out var prop))
            {
                prop.TakeDamage(GetCurrentDamage());
            }
        }
    }

    protected float GetCurrentDamage()
    {
        return currentDamage *= player.currentMight;
    }
}