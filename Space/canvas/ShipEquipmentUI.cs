using System.Collections;
using System.Collections.Generic;
using SpaceObjects;
using UnityEngine;
using UnityEngine.UI;
using SpaceObjects;


public class ShipEquipmentUI : MonoBehaviour
{
    private GameObject player;
    private GameObject spaceManager;
    private Weapon[] weapons;
    private Equipment[] equipments;
    [SerializeField]
    private GameObject eqPrefab;






    public void Init(GameObject _player,GameObject _spaceManager)
    {
        spaceManager = _spaceManager;
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
            int _i = weapons[i].p.Point;
            GameObject we_button = (GameObject)Instantiate(eqPrefab);
            
            we_button.transform.SetParent( gameObject.transform, false);
            we_button.GetComponent<RectTransform>().position += Vector3.right * (_i * -50 - 120);
            we_button.GetComponent<Button>().onClick.AddListener( ()=> { spaceManager.GetComponent<CommandManager>().SendUserCommand(ShipCommand.Atack, null,_i) ;});

        }
    }
    private void EqButtonInit()
    {
        for (int i = 0; i < equipments.Length; i++)
        {
            GameObject eq_button = (GameObject)Instantiate(eqPrefab);
            eq_button.transform.SetParent(gameObject.transform, false);
            eq_button.GetComponent<RectTransform>().position += Vector3.right * (i * -50 - 300);
            int _i = i;
            eq_button.GetComponent<Button>().onClick.AddListener(() => { spaceManager.GetComponent<CommandManager>().SendUserCommand(ShipCommand.Equipment, null, _i); });
        }
    }

}

