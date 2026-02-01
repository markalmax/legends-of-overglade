using UnityEngine;
using Unity.Netcode;
using Unity.Services.Lobbies.Models;

public class SpawnManager : NetworkBehaviour
{
    public GameObject hiderPrefab;
    public GameObject seekerPrefab;

    public override void OnNetworkSpawn()
    {
        if(IsServer)
        {
            NetworkManager.Singleton.OnClientConnectedCallback += OnClientConnected;
        }
    }

    private void OnClientConnected(ulong clientId)
    {
        int totalClients = NetworkManager.Singleton.ConnectedClients.Count;
        if (totalClients % 2 == 0)
        {
            SpawnSeeker(clientId);
        }
        else
        {
            SpawnHider(clientId);
        }
    }

    private void SpawnHider(ulong ownerClientId)
    {
        GameObject hider = Instantiate(hiderPrefab, Vector2.zero, Quaternion.identity);
        hider.GetComponent<NetworkObject>().SpawnWithOwnership(ownerClientId);
    }
    private void SpawnSeeker(ulong ownerClientId)
    {
        GameObject seeker = Instantiate(seekerPrefab, Vector2.zero, Quaternion.identity);
        seeker.GetComponent<NetworkObject>().SpawnWithOwnership(ownerClientId);
    }
}
