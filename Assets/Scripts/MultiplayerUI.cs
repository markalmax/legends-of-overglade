using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.UI;

public class MultiplayerUI : MonoBehaviour
{
    [SerializeField] Button hostBtn, joinBtn;
    [SerializeField] TMPro.TMP_InputField ipInput, portInput, nameInput;
    
    GameObject cm;

    void Awake()
    {
        cm = GameObject.FindWithTag("Chat Manager");
        hostBtn.onClick.AddListener( delegate { NetworkManager.Singleton.StartHost(); } );
        joinBtn.onClick.AddListener( delegate { NetworkManager.Singleton.StartClient(); } );
        ipInput.onEndEdit.AddListener( delegate { NetworkManager.Singleton.GetComponent<Unity.Netcode.Transports.UTP.UnityTransport>().SetConnectionData(ipInput.text, ushort.Parse(portInput.text)); } );
        portInput.onEndEdit.AddListener( delegate { NetworkManager.Singleton.GetComponent<Unity.Netcode.Transports.UTP.UnityTransport>().SetConnectionData(ipInput.text, ushort.Parse(portInput.text)); } );
        nameInput.onEndEdit.AddListener( delegate { cm.GetComponent<ChatManager>().SetPlayerName(nameInput.text); } );
    }

}
