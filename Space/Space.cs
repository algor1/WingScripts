﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpaceObjects;

public class Space : MonoBehaviour
{
    public GameObject Player;
    private Vector3 zeroPoint = Vector3.zero;

    [SerializeField]
    private GameObject canvas;
    [SerializeField]
    private GameObject mainCamera;
    private Dictionary<int, GameObject> nearestShips;
    //    //public GameObject station;
    //    private Vector3 zeroPoint;
    ////    [SerializeField]
    //  //public Waypoints wp;
    //  //[SerializeField]
    //  //private GameObject wpprefab;
    public List<GameObject> wpList;
    //  public GameObject player;
    //  private int player_id;
    //  private Ship player_SO;

    ////    public List<FlyObject> allFlyObject;
    //  public List<Ship> shipsFromServer;
    //    //public Dictionary<int,FlyObject> nearestFlyObject;
    //  public Dictionary<int,GameObject> nearestShips;
    //  public List<SpaceObject> serverSOlist;
      public Dictionary<int,GameObject> nearestSOs;
    //  Coroutine shipcoroutine;
    //  Coroutine socoroutine;
    //  private IEnumerator coroutineSH;
    //  private IEnumerator coroutineSO;
    //  [SerializeField]
    //  private GameObject serverObj;
    //  [SerializeField]
    //  private GameObject serverSOObj;
    //    [SerializeField]
    //  private GameObject serverLandObj;




    void Awake()
    {
        nearestShips = new Dictionary<int, GameObject>();
        nearestSOs = new Dictionary<int, GameObject>();
        GameManager.onPlayerShipData += InitSpace; //?? 
        GameManager.SendPlayerInit();
    }
    void InitSpace(ShipData playerShipData)
    {
        Debug.Log("Init solar system...");
        CreateSolarSystem();
        Debug.Log("Init player ...");
        CreatePlayer(playerShipData);
        Debug.Log("Init user command system...");
        GetComponent<CommandManager>().Init(Player);
        Debug.Log("Init canvas system...");
        LoadCanvasSystem();
        mainCamera.GetComponent<MainCamControl>().Init(Player);
        
        GameManager.onNearestShipData += ShipsListUpdate;
        GameManager.onNearestSpaceObject += SOListUpdate;

    }

    void CreateSolarSystem()
    {

    }
    void LoadCanvasSystem()
    {
        canvas.GetComponent<Indicators>().Init();
        canvas.GetComponent<ShipEquipmentUI>().Init(Player,gameObject);

    }

    void CreatePlayer(ShipData shipData)
    {
        Debug.Log("creating player...");
        if (Player == null)
        {
            SetZeroPoint(shipData.Position);
            Debug.Log("player prefub " + shipData.Prefab);
            Player = (GameObject)Instantiate(Resources.Load(shipData.Prefab, typeof(GameObject)), shipData.Position - zeroPoint, shipData.Rotation);
            //ShipMotor shipMotor = Player.AddComponent<ShipMotor>() as ShipMotor;
            Player.GetComponent<ShipMotor>().Init(shipData, this.gameObject);
        }
        GameManager.onPlayerShipData -= InitSpace; //?? 
        GameManager.onPlayerShipData += UpdatePlayer; //?? 
        GameManager.onNearestShipCommand += SendShipCommand;
        GameManager.onShipDestroyed += DestroyShipCallFromServer;
    }
    void UpdatePlayer(ShipData shipData)
    {

        Player.GetComponent<ShipMotor>().thisShip.p = shipData;

    }
    public void SetZeroPoint(Vector3 newZeroPoint)
    {
        zeroPoint = newZeroPoint;
    }

    public Vector3 GetZeroPoint()
    {
        return zeroPoint;
    }

    public void ClearAll()
    {
        // Debug.Log("coroutin stopped");

        // StopCoroutine(coroutineSH);
        // StopCoroutine(coroutineSO);
        foreach (int key in nearestShips.Keys)
        {
            DeleteShip(key);
        }
        //foreach (int key in nearestSOs.Keys)
        //{
            //DeleteSO(key);
        //}
    }
    public void UpdateAll()
    {
            //send request to server for nearest ship
    }



    //    ---------------------  SHIPS ---------------------------------
    void AddShip(ShipData shipData)
    {
        Debug.Log("prefub " + shipData.Prefab);
        GameObject gObj = (GameObject)Instantiate(Resources.Load(shipData.Prefab, typeof(GameObject)), shipData.Position - zeroPoint, shipData.Rotation);
        //ShipMotor shipMotor = gObj.AddComponent<ShipMotor>() as ShipMotor;

        gObj.GetComponent<ShipMotor>().Init(shipData, this.gameObject);
        //gObj.GetComponent<ShipMotor> ().thisShip.SetTarget (player.GetComponent<ShipMotor> ().thisShip.p.SO);
        //Debug.Log (ship.p.SO.visibleName);
        nearestShips.Add(shipData.Id, gObj);
        canvas.GetComponent<Indicators>().AddIndicator_sh(gObj);

    }

    public void DeleteShip(int ship_id)
    {
        canvas.GetComponent<Indicators>().DeleteIndicator_sh(ship_id);
        if (nearestShips.ContainsKey(ship_id))
        {
            //            if (!nearestShips[ship_id].GetComponent<ShipMotor>().thisShip.p.destroyed)
            {

                nearestShips[ship_id].GetComponent<ShipMotor>().thisShip.BeforeDestroy();
                nearestShips[ship_id].GetComponent<ShipMotor>().thisShip = null;
                Destroy(nearestShips[ship_id]);
                Debug.Log("***************************   destroy");
                nearestShips.Remove(ship_id);
            }
        }

    }
    public void DestroyShip(int ship_id)
    {
        DeleteShip(ship_id);
    }
    public void DestroyShipCallFromServer(int ship_id)
    {
        if (nearestShips.ContainsKey(ship_id))
        {
            DestroyEventArgs args = new DestroyEventArgs();
            args.ship_id = ship_id;
            nearestShips[ship_id].GetComponent<ShipMotor>().thisShip.Destroyed();
        }
    }

    void UpdateShip(ShipData shipData)
    {

        nearestShips[shipData.Id].GetComponent<ShipMotor>().thisShip.p.Update( shipData);

    }


    void ChangePlayer()
    {
        //player_SO=serverObj.GetComponent<Server>().GetPlayer(player_id);//player id=0       
        //player.GetComponent<ShipMotor>().thisShip.p.SO.position=player_SO.p.SO.position;
        //player.GetComponent<ShipMotor>().thisShip.p.SO.rotation=player_SO.p.SO.rotation;
        //if (player_SO.landed)
        //{
        //  int station_id = serverObj.GetComponent<LandingServer>().FindShipLocation(player_SO.p.SO.id);
        //  SceneManager.LoadScene ("station");
        //}

    }



    void ShipsListUpdate(ShipData[] shipsFromServer)
    {

        //Создаем копию dict nearestShips чтобы узнать какие нужно удалить
        Dictionary<int, int> shipsForDeletionList = new Dictionary<int, int>();
        foreach (int key in nearestShips.Keys)
        {
            shipsForDeletionList.Add(key, key);
            //                Debug.Log (shipsForDeletionList);
            //                print (key);
        }

        //        Debug.Log (shipsFromServer.Count);
        for (int i = 0; i < shipsFromServer.Length; i++)
        {
            //print(shipsFromServer[i].p.id);
            if (shipsForDeletionList.Remove(shipsFromServer[i].Id))
            {
                UpdateShip(shipsFromServer[i]);
                //print ("4");
            }
            else
            {
                print("add ship");
                AddShip(shipsFromServer[i]);
                UpdateShip(shipsFromServer[i]);
            }

        }
        //удаляем корабли которых небыло в списке
        foreach (int key in shipsForDeletionList.Keys)
        {
            DeleteShip(key);


        }



    }

    public GameObject GetShip(int ship_id)
    {
        if (nearestShips.ContainsKey(ship_id)) return nearestShips[ship_id];
        return null;
    }
    private void SendShipCommand(int ship_id, ShipCommand command, int target_id, int point)
    {
        if (nearestShips.ContainsKey(ship_id))
        {
            SpaceObject target=null;
            if (target_id != -1)
            {
                //TODO может случится что цель не находится в поле видимости игрока must найти варианты решения
                if (nearestShips.ContainsKey(target_id)) target = nearestShips[target_id].GetComponent<ShipMotor>().thisShip.p;
                if (nearestSOs.ContainsKey(target_id)) target = nearestSOs[target_id].GetComponent<SOParametres>().thisServerObject;
            }
            nearestShips[ship_id].GetComponent<ShipMotor>().thisShip.Command(command,target,point);
        }
    }


    //  ---------------------  SHIPS END ---------------------------------

    //  ---------------------  SO (Space Objects) ---------------------------------

    void AddSO(SpaceObject so)
    {
        Debug.Log("prefub " + so.Prefab);

        GameObject SObj = (GameObject)Instantiate(Resources.Load(so.Prefab, typeof(GameObject)), so.Position - zeroPoint, so.Rotation);
        SObj.AddComponent<SOParametres>();
        SObj.GetComponent<SOParametres>().Init(so, this.gameObject);
        //      Debug.Log (so.visibleName);
        nearestSOs.Add(so.Id, SObj);
        Debug.Log("add SO id " + so.Id);
        canvas.GetComponent<Indicators>().AddIndicator_so(SObj);

    }

    void DeleteSO(int so_id)
    {
        canvas.GetComponent<Indicators>().DeleteIndicator_so(so_id);
        if (nearestSOs.ContainsKey(so_id))
        {
            Destroy(nearestSOs[so_id]);
            nearestSOs.Remove(so_id);
        }
    }

    void UpdateSO(SpaceObject so)
    {

        nearestSOs[so.Id].transform.position = so.Position - zeroPoint;
        nearestSOs[so.Id].transform.rotation = so.Rotation;

    }






    void SOListUpdate(SpaceObject[] soFromServer)
    {

        //          Debug.Log(" забираем инфу SO с сервера ???");   
        //      print ("1");
        //Создаем копию dict nearestShips чтобы узнать какие нужно удалить
        Dictionary<int, int> SOsForDeletionList = new Dictionary<int, int>();
        foreach (int key in nearestSOs.Keys)
        {
            SOsForDeletionList.Add(key, key);
            //              Debug.Log (shipsForDeletionList);
            //              print (key);
        }

        //      Debug.Log (serverShipslist.Count);
        for (int i = 0; i < soFromServer.Length; i++)
        {
            //print(serverShipslist[i].p.id);
            if (SOsForDeletionList.Remove(soFromServer[i].Id))
            {
                UpdateSO(soFromServer[i]);
                //print ("4");
            }
            else
            {
                Debug.Log("add SO " + soFromServer[i].VisibleName);
                AddSO(soFromServer[i]);
                UpdateSO(soFromServer[i]);
            }

        }
        //удаляем корабли которых небыло в списке
        foreach (int key in SOsForDeletionList.Keys)
        {
            DeleteSO(key);


        }
        //          Debug.Log ("!!!!!!!!!!!!!!!! ended !!!!!!!!!!!!!!!!");
        
    }

    //  ---------------------  SO END ---------------------------------

}
