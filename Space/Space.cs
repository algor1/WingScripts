using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpaceObjects;

public class Space : MonoBehaviour
{
    public GameObject Player;
    private Vector3 zeroPoint = Vector3.zero;

    [SerializeField]
    private GameObject canvasobj;
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
        Debug.Log("Init canvas system...");
        LoadCanvasSystem();
        Debug.Log("Init user command system...");
        GetComponent<CommandManager>().Init(Player);
        mainCamera.GetComponent<MainCamControl>().Init(Player);

        GameManager.onNearestShipData += NearestShipsListUpdate;

    }

    void CreateSolarSystem()
    {

    }
    void LoadCanvasSystem()
    {
        canvasobj.GetComponent<Indicators>().Init(Player);
        canvasobj.GetComponent<ShipEquipmentUI>().Init(Player);

    }

    void CreatePlayer(ShipData shipData)
    {
        Debug.Log("creating player...");
        if (Player == null)
        {
            SetZeroPoint(shipData.Position);
            Debug.Log("player prefub " + shipData.Prefab);
            Player = (GameObject)Instantiate(Resources.Load(shipData.Prefab, typeof(GameObject)), shipData.Position - zeroPoint, shipData.Rotation);
            ShipMotor shipMotor = Player.AddComponent<ShipMotor>() as ShipMotor;
            shipMotor.Init(shipData, this.gameObject);
        }
        GameManager.onPlayerShipData -= InitSpace; //?? 
        GameManager.onPlayerShipData += UpdatePlayer; //?? 
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
        ShipMotor shipMotor = gObj.AddComponent<ShipMotor>() as ShipMotor;

        shipMotor.Init(shipData, this.gameObject);
        //gObj.GetComponent<ShipMotor> ().thisShip.SetTarget (player.GetComponent<ShipMotor> ().thisShip.p.SO);
        //Debug.Log (ship.p.SO.visibleName);
        nearestShips.Add(shipData.Id, gObj);
        canvasobj.GetComponent<Indicators>().AddIndicator_sh(gObj);

    }

    public void DeleteShip(int ship_id)
    {
        canvasobj.GetComponent<Indicators>().DeleteIndicator_sh(ship_id);
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

    void UpdateShip(ShipData shipData)
    {

        nearestShips[shipData.Id].GetComponent<ShipMotor>().thisShip.p = shipData;

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



    void NearestShipsListUpdate(ShipData[] shipsFromServer)
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
    //  ---------------------  SHIPS END  ---------------------------------
}
