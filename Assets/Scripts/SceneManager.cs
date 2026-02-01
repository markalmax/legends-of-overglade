using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager : NetworkBehaviour
{
    public void StartGame()
    {
        if (!IsHost)return;

        GoToGameSceneServerRpc();
    }

    [ServerRpc]
    private void GoToGameSceneServerRpc()
    {
        GoToGameSceneClientRpc();
    }

    [ClientRpc]
    private void GoToGameSceneClientRpc()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Game");
    }
}
