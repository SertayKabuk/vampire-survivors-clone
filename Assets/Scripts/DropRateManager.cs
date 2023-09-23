using System;
using System.Collections.Generic;
using UnityEngine;

public class DropRateManager : MonoBehaviour
{
    [Serializable]
    public class Drop
    {
        public string name;
        public GameObject itemPrefab;
        public float dropRate;
    }

    public List<Drop> drops;

    private void OnDestroy()
    {
        float randomNumber = UnityEngine.Random.Range(0f, 100f);

        List<Drop> possibleDrops = new();

        foreach (Drop drop in drops)
        {
            if (randomNumber <= drop.dropRate)
            {
                possibleDrops.Add(drop);
            }
        }

        if (possibleDrops.Count > 0)
        {
            var selectedDrop = possibleDrops[UnityEngine.Random.Range(0, possibleDrops.Count - 1)];
            Instantiate(selectedDrop.itemPrefab, transform.position, Quaternion.identity);
        }
    }
}