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
            if (NetworkManager.Singleton.ConnectedClients.Count % 2 == 0)
            {
                SpawnSeeker();
            }
            else
            {
                SpawnHider();
            }
        }
    }

    private void SpawnHider()
    {
        GameObject hider = Instantiate(hiderPrefab, Vector2.zero, Quaternion.identity);
        hider.GetComponent<NetworkObject>().Spawn();
    }
    private void SpawnSeeker()
    {
        GameObject seeker = Instantiate(seekerPrefab, Vector2.zero, Quaternion.identity);
        seeker.GetComponent<NetworkObject>().Spawn();
    }
}
