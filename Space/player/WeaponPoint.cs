using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpaceObjects;

public class WeaponPoint : MonoBehaviour {
    public WeaponData.WeaponType type;
    public Component weapon;
    private GameObject spaceManager;

	public void Init(WeaponData.WeaponType _type,GameObject _spaceManager)
	{
        spaceManager = _spaceManager;
		type = _type;
        switch (type)
        {
            case WeaponData.WeaponType.laser:
                weapon = gameObject.AddComponent<LaserBeam>();
                break;
			case WeaponData.WeaponType.missile:
				weapon = gameObject.AddComponent<MissileLauncher> ();
				break;
        }
	}


	public void StartFire (Ship target)
	{
//        Debug.Log("StartFire  "+ type);

        GameObject shipObject=
        switch (type)
        {
            case WeaponData.WeaponType.laser:
   
                gameObject.GetComponent<LaserBeam>().StartFire(target);
                break;
            case WeaponData.WeaponType.missile:
                gameObject.GetComponent<MissileLauncher>().Fire(target.host);
                break;
        }
	}

	public void StopFire ()
	{
        switch (type)
        {
            case WeaponData.WeaponType.laser:
                gameObject.GetComponent<LaserBeam>().StopFire();
                break;
            case WeaponData.WeaponType.missile:
                break;
        }
		
	}
}
