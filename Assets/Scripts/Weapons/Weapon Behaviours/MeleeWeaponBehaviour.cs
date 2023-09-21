using UnityEngine;
/// <summary>
/// Base for all melee weapons
/// </summary>
public class MeleeWeaponBehaviour : MonoBehaviour
{
    public float destroyAfterSeconds;
    
    // Start is called before the first frame update
    protected virtual void Start()
    {
        Destroy(gameObject, destroyAfterSeconds);
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        
    }
}
