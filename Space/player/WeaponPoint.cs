using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpaceObjects;
using System;

public class WeaponPoint : MonoBehaviour {
    private GameObject spaceManager;
    private Weapon weapon;


	public void Init(Weapon _weapon,GameObject _spaceManager)
	{
        spaceManager = _spaceManager;
        weapon = _weapon;

        switch (weapon.p.Type)
        {
            case WeaponData.WeaponType.laser:
                gameObject.AddComponent<LaserBeam>();
                break;
			case WeaponData.WeaponType.missile:
				gameObject.AddComponent<MissileLauncher> ();
				break;
        }
        weapon.StartFire += StartFireAnimation;
        weapon.StopFire += StopFireAnimation;
	}


	private void StartFireAnimation (object sender, StartFireEventArgs e)
	{
        Debug.Log("----------------StartFireANIMATION  " + weapon.p.Type +" " + weapon.p.Reload+ "  target " + e.ship_id);
        int ship_id = e.ship_id;
        GameObject target = spaceManager.GetComponent<Space>().GetShip(ship_id);
        Debug.Log("______ target " + target);
        Debug.Log("StartFire  " + weapon.p.Type + "  target " + ship_id);

        switch (weapon.p.Type)
        {
            case WeaponData.WeaponType.laser:
   
                gameObject.GetComponent<LaserBeam>().StartFire(target);
                break;
            case WeaponData.WeaponType.missile:
                gameObject.GetComponent<MissileLauncher>().Fire(target);
                break;
        }
	}

	public void StopFireAnimation (object sender, EventArgs e)
	{
        Debug.Log("----------------StopFireANIMATION  ");
        switch (weapon.p.Type)
        {
            case WeaponData.WeaponType.laser:
                gameObject.GetComponent<LaserBeam>().StopFire();
                break;
            case WeaponData.WeaponType.missile:
                break;
        }
	}
}
