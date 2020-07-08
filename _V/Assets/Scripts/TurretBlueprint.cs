using UnityEngine;
using System.Collections;

namespace TowerDefence
{
    [System.Serializable]
    public class TurretBlueprint
    {

        public GameObject prefab;
        public int cost;

        public GameObject upgradedPrefab;
        public int upgradeCost;

        public GameObject maxPrefab;
        public int maxCost;

        public int GetSellAmount()
        {
            return cost / 2;
        }

    }
}
