using Chat;
using DarkRift;
using DarkRift.Client;
using DarkRiftTags;
using Launcher;
using UnityEngine;
using SpaceObjects;

//namespace Game
//{
public class GameManager : MonoBehaviour
{
    //[SerializeField]
    //private GameObject ShowEnv;
    #region Events

    public delegate void NearestShipsDataEventHandler(ShipData[] nearestShipData);
    public delegate void NearestSpaceObjectEventHandler(SpaceObject[] nearestSpaceObject);

    public delegate void PlayerShipDataEventHandler(ShipData shipData);



    public static event NearestShipsDataEventHandler onNearestShipData;
    public static event NearestSpaceObjectEventHandler onNearestSpaceObject;

    public static event PlayerShipDataEventHandler onPlayerShipData;





    #endregion

    private void Start()
    {
        Debug.Log("Game started");
        GameControl.Client.MessageReceived += OnDataHandler;
    }

    private void OnDestroy()
    {
        if (GameControl.Client == null)
            return;

        GameControl.Client.MessageReceived -= OnDataHandler;
    }

    #region Network Calls

    public static void SendPlayerShipCommands(ShipCommand command, int target_id,int point_id)
    {
        using (var writer = DarkRiftWriter.Create())
        {
            writer.Write((int)command);
            writer.Write(target_id);
            writer.Write(point_id);

            using (var msg = Message.Create(GameTags.PlayerCommand, writer))
            {
                GameControl.Client.SendMessage(msg, SendMode.Reliable);
            }
        }
    }
    public static void SendPlayerInit()
    {
        using (var msg = Message.CreateEmpty(GameTags.InitPlayer))
        {
            GameControl.Client.SendMessage(msg, SendMode.Reliable);
        }
    }
   
    #endregion

    private static void OnDataHandler(object sender, MessageReceivedEventArgs e)
    {

        using (var message = e.GetMessage())
        {
            Debug.Log("Tag  "+message.Tag);
            // Check if message is meant for this plugin
            if (message.Tag < Tags.TagsPerPlugin * Tags.Game || message.Tag >= Tags.TagsPerPlugin * (Tags.Game + 1))
                return;

            switch (message.Tag)
            {
                case GameTags.NearestShips:
                    {
                        using (var reader = message.GetReader())
                        {
                            ShipData[] nearestShipDataArray = reader.ReadSerializables<ShipData>();

                            //Debug.Log($"Nearest ships" + nearestShipDataArray.Length);
                            //ChatManager.ServerMessage(friendName + " wants to add you as a friend!", MessageType.Info);

                            onNearestShipData?.Invoke(nearestShipDataArray);
                        }
                        break;
                    }
                case GameTags.PlayerShipData:
                    {
                        using (var reader = message.GetReader())
                        {
                            ShipData playerShipData = reader.ReadSerializable<ShipData>();

                            Debug.Log($"Player ship data recieved  " + playerShipData.VisibleName);
                            //ChatManager.ServerMessage(friendName + " wants to add you as a friend!", MessageType.Info);
                            Debug.Log(onPlayerShipData.GetInvocationList().Length);
                            onPlayerShipData?.Invoke(playerShipData);
                        }
                        break;
                    }
                case GameTags.NearestSpaceObjects:
                    {
                        using (var reader = message.GetReader())
                        {
                            SpaceObject[] nearestSpaceObjectArray = reader.ReadSerializables<SpaceObject>();

                            //Debug.Log($"Nearest so " + nearestSpaceObjectArray.Length);
                            //ChatManager.ServerMessage(friendName + " wants to add you as a friend!", MessageType.Info);

                            onNearestSpaceObject?.Invoke(nearestSpaceObjectArray);
                        }
                        break;
                    }
            }
        }
    }
}
//}