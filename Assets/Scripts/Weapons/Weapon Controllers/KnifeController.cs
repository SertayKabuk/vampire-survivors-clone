using UnityEngine;

public class KnifeController : WeaponController
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void Attack()
    {
        base.Attack();
        GameObject spawnedKnife = Instantiate(weaponData.Prefab);
        spawnedKnife.transform.position = transform.position; //player position
        spawnedKnife.GetComponent<KnifeBehaviour>().DirectionChecker(playerMovement.lastMovedVector); // set weapon direction
    }
}
