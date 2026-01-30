using Unity.Netcode;
using UnityEngine;
using UnityEngine.Networking;
public class PlayerMovement : NetworkBehaviour
{
    public float moveSpeed = 5f;

    private void Update()
    {
        if (!IsOwner || !IsSpawned) return;

        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 move = new Vector3(moveX, 0, moveZ) * moveSpeed * Time.deltaTime;
        transform.Translate(move, Space.World);
    }
}
