using UnityEngine;
using UnityEngine.UI;

namespace TowerDefence
{
    public class NodeUI : MonoBehaviour
    {

        public GameObject ui;

        public Text upgradeCost;

        public Text sellAmount;

        private Node target;

        public void SetTarget(Node t)
        {
            target = t;
        }

        public void Upgrade()
        {
            if (target.isUpgraded) return;
            target.UpgradeTurret();
        }

        public void Max()
        {
            if (target.isMax) return;
            target.MaxTurret();
        }

        public void Sell()
        {
            target.SellTurret();
            BuildManager.instance.DeselectNode();
        }

        private void OnMouseEnter()
        {
            ui.SetActive(true);
            if (target.isUpgraded == true)
                upgradeCost.text = "Upgrade cost: " + target.turretBlueprint.maxCost;
            else if (target.isMax == true)
                upgradeCost.text = "Maxed!";
            else
                upgradeCost.text = "Upgrade cost: " + target.turretBlueprint.upgradeCost;
            sellAmount.text = "Sell price: " + target.turretBlueprint.GetSellAmount();
        }

        private void OnMouseExit()
        {
            ui.SetActive(false);
        }

        private void Update()
        {
            if (!ui.activeInHierarchy) return;

            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                if (target.isUpgraded == false && target.isMax == false)
                {
                    Upgrade();
                }
                else if (target.isUpgraded == true && target.isMax == false)
                {
                    Max();
                }
                else
                {
                    return;
                }
            }
                
            else if (Input.GetKeyDown(KeyCode.Alpha2))
                Sell();
        }
    }
}
