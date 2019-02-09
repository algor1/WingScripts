using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpaceObjects;

public class CommandManager : MonoBehaviour
{
    private GameObject player;
    bool initialized;

    public void Init(GameObject _player)
    {
        player = _player;

    }
    public void SendUserCommand(Command command,GameObject target=null,int point_id=-1)
    {
        int target_id = (target != null) ? target.GetComponent<ShipMotor>().thisShip.p.Id : -1;
        var targetShipdata= (target != null) ? target.GetComponent<ShipMotor>().thisShip.p : null;
        player.GetComponent<ShipMotor>().thisShip.GetCommand(command,targetShipdata,point_id);
        GameManager.SendPlayerShipCommands(command, target_id, point_id);
    }
    public void GoTo()
    {
        SendUserCommand(Command.MoveTo);
    }
}
