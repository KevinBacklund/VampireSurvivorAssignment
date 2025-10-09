using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class DroprateManager : MonoBehaviour
{
    [System.Serializable]
    public class DropItem
    {
        public string dropName;
        public GameObject itemPrefab;
        [UnityEngine.Range(0f, 1f)]
        public float dropChance;
    }

    public List<DropItem> dropItems;

    public void DropItems()
    {
        float randomValue = Random.Range(0f, 1f);

        foreach (DropItem rate in dropItems)
        {
            if (randomValue <= rate.dropChance)
            {
                Instantiate(rate.itemPrefab, transform.position, Quaternion.identity);
            }
        }
    }
}
