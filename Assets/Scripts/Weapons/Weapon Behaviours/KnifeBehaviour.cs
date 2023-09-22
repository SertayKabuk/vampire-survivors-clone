using UnityEngine;

public class KnifeBehaviour : ProjectileWeaponBehaviour
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += weaponData.Speed * Time.deltaTime * direction;
    }
}
