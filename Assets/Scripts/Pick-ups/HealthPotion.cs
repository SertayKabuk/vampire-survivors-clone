using UnityEngine;

public class HealthPotion : Pickup, ICollectable
{
    public int healthToRestore;
    public void Collect()
    {
        PlayerStats player = FindAnyObjectByType<PlayerStats>();
        player.RestoreHealth(healthToRestore);
    }

}