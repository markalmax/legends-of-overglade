using Unity.Netcode;
using UnityEngine;
using Unity.Netcode.Components;
public class PlayerMovement : NetworkBehaviour
{
    public float moveSpeed = 5f;
  

    private void Update()
    {
        if (!IsOwner || !IsSpawned) return;

        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0) * moveSpeed * Time.deltaTime;
        transform.Translate(movement);
    }
}
