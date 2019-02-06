using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpaceObjects;

public class CommandManager : MonoBehaviour
{
    private GameObject player;
    private void Start()
    {
        player = GetComponent<Space>().Player;

    }
    public void SendUserCommand(Command command,GameObject target=null,int point_id=-1)
    {

        player.GetComponent<ShipMotor>().thisShip.GetCommand(command,target.GetComponent<ShipMotor>().thisShip.p,point_id);
        GameManager.SendPlayerShipCommands(command, target.GetComponent<ShipMotor>().thisShip.p.Id, point_id);
    }
}
