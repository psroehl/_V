using UnityEngine;
using UnityEngine.UI;
namespace TowerDefence
{
    public class Shop : MonoBehaviour
    {

        public TurretBlueprint standardTurret;
        public TurretBlueprint missileLauncher;
        public TurretBlueprint laserBeamer;
        public TurretBlueprint shieldTurret;

        public Button[] turretButtons;
        public Text money;

        BuildManager buildManager;

        void Start()
        {
            buildManager = BuildManager.instance;
            buildManager.SelectTurretToBuild(standardTurret);
            SetButton(1);
        }

        public void SelectStandardTurret()
        {
            Debug.Log("Standard Turret Selected");
            buildManager.SelectTurretToBuild(standardTurret);
        }

        public void SelectMissileLauncher()
        {
            Debug.Log("Missile Launcher Selected");
            buildManager.SelectTurretToBuild(missileLauncher);
        }

        public void SelectLaserBeamer()
        {
            Debug.Log("Laser Beamer Selected");
            buildManager.SelectTurretToBuild(laserBeamer);
        }

        public void SelectShieldTurret()
        {
            Debug.Log("Shield Turret Selected");
            buildManager.SelectTurretToBuild(shieldTurret);
        }



        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                buildManager.SelectTurretToBuild(standardTurret);
                SetButton(1);
            }             
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                buildManager.SelectTurretToBuild(missileLauncher);
                SetButton(2);
            }               
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                buildManager.SelectTurretToBuild(laserBeamer);
                SetButton(3);
            }
            // else if (Input.GetKeyDown(KeyCode.Alpha4))
            // {
            //     buildManager.SelectTurretToBuild(shieldTurret);
            //     SetButton(4);
            // }
            money.text = "$: "+PlayerStats.Money.ToString();
        }

        void SetButton(int i)
        {
            foreach (var item in turretButtons)
            {
                item.interactable = false;
            }

            turretButtons[i - 1].interactable = true;
        }
    }
}
