using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DarkRift.Client.Unity;
using UnityEngine.SceneManagement;

public class ipinput : MonoBehaviour
{
    [SerializeField]
    private GameObject gamecontroler;
    [SerializeField]
    private InputField portInput;
    private UnityClient _client;

    public void Connect()
    {
        _client = gamecontroler.GetComponent<UnityClient>();
        string ip = GetComponent<InputField>().text ;
        System.Net.IPAddress _ip;
        _ip = System.Net.IPAddress.Parse(ip);
        int port = int.Parse(portInput.text);
        _client.Connect(_ip, port, DarkRift.IPVersion.IPv4);

        SceneManager.LoadScene("Login");

    }

}
