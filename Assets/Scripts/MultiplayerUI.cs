using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.UI;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using UnityEngine.Rendering;

public class MultiplayerUI : MonoBehaviour
{
    [SerializeField] Button hostBtn, joinBtn, dscBtn;
    [SerializeField] TMPro.TMP_InputField ipInput, portInput, nameInput;
    [SerializeField] string setIP;
    GameObject cm;

    void Awake()
    {
        cm = GameObject.FindWithTag("Chat Manager");
        hostBtn.onClick.AddListener( delegate { NetworkManager.Singleton.StartHost(); } );
        joinBtn.onClick.AddListener( delegate { NetworkManager.Singleton.StartClient(); } );
        dscBtn.onClick.AddListener( delegate { NetworkManager.Singleton.Shutdown(); } );
        ipInput.onEndEdit.AddListener( delegate { NetworkManager.Singleton.GetComponent<Unity.Netcode.Transports.UTP.UnityTransport>().SetConnectionData(ipInput.text, ushort.Parse(portInput.text)); } );
        portInput.onEndEdit.AddListener( delegate { NetworkManager.Singleton.GetComponent<Unity.Netcode.Transports.UTP.UnityTransport>().SetConnectionData(ipInput.text, ushort.Parse(portInput.text)); } );
        nameInput.onEndEdit.AddListener( delegate { cm.GetComponent<ChatManager>().SetPlayerName(nameInput.text); } );
    }
    void Start()
    {
        ipInput.text =  GetLocalIPAddress(); 
    }
    public string GetLocalIPAddress()
    {
        if(setIP != "")
        {
            return setIP;
        }
        var host = Dns.GetHostEntry(Dns.GetHostName());
        foreach (var ip in host.AddressList)
        {
            if (ip.AddressFamily == AddressFamily.InterNetwork)
            {
                return ip.ToString();
            }
        }
        throw new System.Exception("No network adapters with an IPv4 address in the system!");
    }
}

