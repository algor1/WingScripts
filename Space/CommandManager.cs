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
        int target_id = -1;
        switch (command)
        {
            case Command.SetTargetShip:
                target_id = (target != null) ? target.GetComponent<ShipMotor>().thisShip.p.Id : -1;
                var targetShipData = (target != null) ? target.GetComponent<ShipMotor>().thisShip.p : null;
                player.GetComponent<ShipMotor>().thisShip.GetCommand(command, targetShipData, point_id);

                break;
            case Command.SetTarget:
                target_id = (target != null) ? target.GetComponent<SOParametres>().thisServerObject.Id : -1;
                var targetData = (target != null) ? target.GetComponent<SOParametres>().thisServerObject : null;
                player.GetComponent<ShipMotor>().thisShip.GetCommand(command, targetData, point_id);
                break;
            default:
                player.GetComponent<ShipMotor>().thisShip.GetCommand(command,null, point_id);
                break;
        }
        Debug.Log(command+" "+target_id);
        GameManager.SendPlayerShipCommands(command, target_id, point_id);
    }
    public void GoTo()
    {
        SendUserCommand(Command.MoveTo);
    }
}
