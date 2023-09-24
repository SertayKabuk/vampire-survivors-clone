using System.Collections.Generic;
using UnityEngine;

public class GarlicBehaviour : MeleeWeaponBehaviour
{
    List<GameObject> markedEnemies;

    protected override void Start()
    {
        base.Start();
        markedEnemies = new List<GameObject>();
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(Enums.Tags.Enemy.ToString()) && !markedEnemies.Contains(collision.gameObject))//move to tag enum
        {
            EnemyStats enemy = collision.GetComponent<EnemyStats>();
            enemy.TakeDamage(currentDamage);//allow modification to damage

            markedEnemies.Add(collision.gameObject); //marked enemy to prevent damage same enemy more tahn once
        }
        else if (collision.CompareTag(Enums.Tags.Prop.ToString()) && !markedEnemies.Contains(collision.gameObject))
        {
            if (collision.TryGetComponent<BreakableProps>(out var prop))
            {
                prop.TakeDamage(currentDamage);
                markedEnemies.Add(collision.gameObject); //marked enemy to prevent damage same enemy more tahn once
            }
        }
    }
}
