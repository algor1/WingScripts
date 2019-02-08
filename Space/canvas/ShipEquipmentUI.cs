using System.Collections;
using System.Collections.Generic;
using SpaceObjects;
using UnityEngine;
using UnityEngine.UI;


public class ShipEquipmentUI : MonoBehaviour
{
    private GameObject player;
    private Weapon[] weapons;
    private Equipment[] equipments;
    [SerializeField]
    private GameObject eqPrefab;






    void Init(GameObject _player)
    {
		player=_player;
        weapons = player.GetComponent<ShipMotor>().thisShip.Weapons;
        equipments = player.GetComponent<ShipMotor>().thisShip.Equipments;
        WeaponButtonInit();
        EqButtonInit();
    }

    private void WeaponButtonInit()
    {
        for (int i = 0; i < weapons.Length; i++)

        {
            GameObject we_button = (GameObject)Instantiate(eqPrefab);
            we_button.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform, false);
            we_button.GetComponent<RectTransform>().position += Vector3.right * (i * -50 - 120);
            //			var ship_obj = player.GetComponent<ShipMotor>().AtackTarget(i) ;
            int _i = i;
            Debug.Log(" we_button.GetComponent<Button>().onClick.AddListener( ()=> { player.GetComponent<ShipMotor>().AtackTarget(_i) ;});");

        }
    }
    private void EqButtonInit()
    {
        for (int i = 0; i < equipments.Length; i++)
        {
            GameObject eq_button = (GameObject)Instantiate(eqPrefab);
            eq_button.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform, false);
            eq_button.GetComponent<RectTransform>().position += Vector3.right * (i * -50 - 300);
            //			var ship_obj = player.GetComponent<ShipMotor>().AtackTarget(i) ;
            int _i = i;
            Debug.Log(" eq_button.GetComponent<Button>().onClick.AddListener( ()=> { player.GetComponent<ShipMotor>().StartEquipment(_i) ;});");
        }
    }

}

