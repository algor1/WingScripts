﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using SpaceObjects;

[RequireComponent(typeof(Rigidbody))]

public class ShipMotor : MonoBehaviour {

	[SerializeField]
	private GameObject spaceManager;
	public Ship thisShip;




	private bool _flagWarp;
	public float _warpPower;
	public float back_thrust;


	public float thrust;

	[SerializeField]
	private GameObject burner;	



	[SerializeField]
	private List<GameObject> weaponPoints;




	//public enum ShipEvenentsType{spawn,warp,warmwarp,move,stop,land,hide,reveal};




	public void Init(ShipData _shipData, GameObject _spaceManager)
	{

		thisShip = new Ship(_shipData);
        SignOnShipEvents();
		Debug.Log (this);
        InitWeapons();
		
		spaceManager = _spaceManager;
		Debug.Log ("----init   " + thisShip.p.VisibleName+"  id   " +thisShip.p.Id);
	}

    private void InitWeapons()
    {
        //if (thisShip.weapons.Count <= weaponPoints.Count)
        //{
        //    for (int i = 0; i < thisShip.weapons.Count; i++)
        //    {
        //        thisShip.weapons[i].SetWeaponPoint(weaponPoints[i]);
        //        weaponPoints[i].GetComponent<WeaponPoint>().Init(thisShip.weapons[i].p.type);
        //        Debug.Log(thisShip.p.visibleName + " init weapon point " + thisShip.weapons[i].p.type + i);
        //    }
        //}
    }
    private void SignOnShipEvents() { 
    }






	void FixedUpdate () {
		thisShip.Tick ();
		//Warp();
		//Atack();
        //Equipment();
		PerformRotateQ ();

		PerformMove ();

	}



	void PerformRotateQ(){
		transform.rotation = thisShip.p.Rotation;


	}

	void PerformMove(){
		transform.position = thisShip.p.Position-spaceManager.GetComponent<Space>().GetZeroPoint();
	}



    #region UserCommands

    //   public void SetTarget(SpaceObject tg)
    //{
    //	thisShip.SetTarget(tg);
    //}


    //public void GoTotarget()
    //   {
    //	thisShip.GoToTarget ();
    //	Server_GO.GetComponent<Server> ().PlayerControl(0,Server.Command.MoveTo,0);

    //   }
    //   public void WarpTotarget()

    //   {
    //	thisShip.WarpToTarget ();
    //	Server_GO.GetComponent<Server> ().PlayerControl(0,Server.Command.WarpTo,0);
    //   }

    //   public void StartEquipment(int weaponnum)
    //   {
    //       thisShip.StartEquipment();
    //	Server_GO.GetComponent<Server>().PlayerControl(0, Server.Command.Equipment,weaponnum);
    //   }
    //public void AtackTarget(int weaponnum)
    //{
    //	thisShip.Atack_target(weaponnum);
    //	Server_GO.GetComponent<Server>().PlayerControl(0, Server.Command.Atack,weaponnum);
    //}

    //   public void LandTotarget()
    //   {
    //	Server_GO.GetComponent<Server> ().PlayerControl (0, Server.Command.LandTo,0);
    //   }
    //public void OpenTarget()
    //{
    //	Server_GO.GetComponent<Server> ().PlayerControl (0, Server.Command.Open,0);
    //	thisShip.OpenTarget ();
    //}
    #endregion

    #region Actions

    //    private void Spawn()//backlink from Ship
    //	{
    //		//		set zero
    //		Debug.Log("set zeropoint");
    //		Debug.Log("zeropoint was  " +spaceManager.GetComponent<ShowEnv>().GetZeroPoint());
    //		spaceManager.GetComponent<ShowEnv>().SetZeroPoint(thisShip.p.position);
    //		Debug.Log("zeropoint now  " +spaceManager.GetComponent<ShowEnv>().GetZeroPoint());
    //		Debug.Log ("move ship To 0,0,0");
    //		transform.position = transform.position * 0;
    //	}
    //public void Thrust(float _thrust)
    //{
    //    thisShip.p.SpeedNew = thisShip.p.SpeedMax * _thrust;
    //}
    //	private void Warp()
    //	{
    //		if ( thisShip.moveCommand == Ship.MoveType.warp)
    //		{
    //			if (thisShip.Rotate())
    //			{
    //				if (!thisShip.warpCoroutineStarted)
    //				{
    //					StartCoroutine(thisShip.Warpdrive());
    //				}
    //				//else
    //				//{
    //				//    ship.p.position = Vector3.MoveTowards (ship.p.position,ship.p. Time.deltaTime * ship.p.warpSpeed;
    //				//}
    //			}
    //		}
    //	}

    //	private void Atack()
    //	{


    //			for (int i = 0; i < thisShip.weapons.Count; i++)
    //			{

    //			if (thisShip.weapons[i].fire && !thisShip.weapons[i].activated)
    //				{
    //					thisShip.weapons[i].atack_co= StartCoroutine(thisShip.weapons[i].Attack());
    //				Debug.Log ("*************      atack_co    " + thisShip.weapons [i].atack_co.GetHashCode());

    //				}
    //			}
    ////		}
    //}
    //private void StopAtacking()
    //{
    //	for (int i = 0; i < thisShip.weapons.Count; i++)
    //	{
    //		if (thisShip.weapons[i].activated)
    //		{
    //			Debug.Log ("stopping atack couroutine id " + thisShip.weapons [i].atack_co.GetHashCode ());
    //			StopCoroutine (thisShip.weapons [i].atack_co);
    //		}
    //	}
    //}

    //   private void Equipment()
    //   {
    //       for (int i = 0; i < thisShip.equipments.Count; i++)
    //       {
    //           if (thisShip.equipments[i].activate && !thisShip.equipments[i].coroutineStarted)
    //           {
    //			thisShip.equipments [i].use_co=StartCoroutine(thisShip.equipments[i].UseEq());
    //           }
    //       }
    //   }
    //private void StopEquipments()
    //{
    //	for (int i = 0; i < thisShip.equipments.Count; i++)
    //	{
    //		if (thisShip.equipments[i].coroutineStarted)
    //		{
    //			StopCoroutine (thisShip.equipments [i].use_co);
    //		}
    //	}
    //}
    #endregion

    //    public void Events(Ship.ShipEvenentsType shipEvent, Ship ship)
    //	{
    //		if (ship == thisShip) {
    //			switch (shipEvent) {
    //			case Ship.ShipEvenentsType.stop:
    //				Debug.Log ("motor " + thisShip.p.visibleName + " ship stopped");
    //				burner.GetComponent<BurnControl> ().EngineStop ();
    //				break;
    //			case Ship.ShipEvenentsType.move:
    //				Debug.Log ("motor " + thisShip.p.visibleName + " ship accelerated");
    //				burner.GetComponent<BurnControl> ().EngineStart ();
    //				break;
    //			case Ship.ShipEvenentsType.warmwarp:
    //				Debug.Log ("motor " + thisShip.p.visibleName + " ship preparing to warp");
    //				burner.GetComponent<BurnControl> ().WarpStart ();
    //				break;
    //			case Ship.ShipEvenentsType.warp:

    //				Debug.Log ("motor " + thisShip.p.visibleName + " ship warping....");
    ////			if player
    //				spaceManager.GetComponent<Effects> ().PlayerWarp (thisShip.targetToMove.position, thisShip.p.warpSpeed);
    //				break;
    //			case Ship.ShipEvenentsType.spawn:
    //				Debug.Log ("motor " + thisShip.p.visibleName + " ship spawn");
    //				spaceManager.GetComponent<Effects> ().PlayerWarpStop ();
    //				burner.GetComponent<BurnControl> ().WarpStop ();
    //				Spawn ();
    //				break;
    //			case Ship.ShipEvenentsType.land:
    //				Debug.Log ("motor " + thisShip.p.visibleName + " ship landing");
    //				Server_GO.GetComponent<LandingServer> ().Landing (thisShip.p.id, thisShip.targetToMove.id);
    //				thisShip.landed = true;
    //				break;
    //			case Ship.ShipEvenentsType.destroyed:
    //				Debug.Log ("motor " + thisShip.p.visibleName + " ship destroyed");
    //				StopAtacking ();
    //				StopEquipments ();
    ////			thisShip = null;
    //		//var detonator = gameObject.AddComponent<Detonator> ();
    //		//detonator.duration = 4;
    //		//detonator.destroyTime = 4;
    //		//detonator.size = 40;
    //		//detonator.autoCreateSmoke = false;
    //		//detonator.autoCreateHeatwave = false;
    //		//detonator.color = Color.white;
    //		//detonator.destroyObj = false;
    //		//detonator.Explode ();
    //		thisShip.p.destroyed = true;
    //		Debug.Log ("********************       explosion");
    //		spaceManager.GetComponent<ShowEnv> ().DestroyShip (thisShip.p.id);
    //		break;
    //	case Ship.ShipEvenentsType.open:
    //		GameObject canvasObj = GameObject.Find ("Canvas");
    //		canvasObj.GetComponent<ShowMenus> ().ShowInventory (thisShip.p.id, thisShip.targetToMove.id);
    //		break;
    //	}
    //}
    //}

}	