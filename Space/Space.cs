using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpaceObjects;

public class Space : MonoBehaviour
{
    [SerializeField]
    private GameObject player;
    private Vector3 zeroPoint =  Vector3.zero;


    void Start()
    {
        GameManager.onPlayerShipData += CreatePlayer;
        GameManager.SendPlayerInit();
        Debug.Log("spase started");

    }

    void CreateSolarSystem()
    { }

    void CreatePlayer(ShipData shipData)
    {
        Debug.Log("creating player...");
        if (player == null)
        {
            Debug.Log("player prefub " + shipData.Prefab);
            player = (GameObject)Instantiate(Resources.Load(shipData.Prefab, typeof(GameObject)), shipData.Position - zeroPoint, shipData.Rotation);
            ShipMotor shipMotor = player.AddComponent<ShipMotor>() as ShipMotor;
            shipMotor.Init(shipData, this.gameObject);
            //gObj.GetComponent<ShipMotor>().thisShip.SetTarget(player.GetComponent<ShipMotor>().thisShip.p.SO);
            //Debug.Log(ship.p.SO.visibleName);
            //nearestShips.Add(ship.p.SO.id, gObj);
            //canvasobj.GetComponent<Indicators>().AddIndicator_sh(gObj);
        }
    }
      public void SetZeroPoint(Vector3 newZeroPoint)
      {
          zeroPoint = newZeroPoint;
      }

      public Vector3 GetZeroPoint()
      {
            return zeroPoint;
      }
}
