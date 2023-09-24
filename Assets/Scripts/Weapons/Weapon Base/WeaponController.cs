using UnityEngine;

/// <summary>
/// Base weapon script
/// </summary>
public class WeaponController : MonoBehaviour
{
    [Header("Weapon Stats")]
    public WeaponScriptableObject weaponData;
    float currentCooldown;

    protected PlayerMovement playerMovement;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        playerMovement = FindAnyObjectByType<PlayerMovement>();
        currentCooldown = weaponData.CooldownDuration;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        currentCooldown -= Time.deltaTime;

        if (currentCooldown < 0f)
        {
            Attack();
        }
    }

    protected virtual void Attack()
    {
        currentCooldown = weaponData.CooldownDuration;
    }
}
