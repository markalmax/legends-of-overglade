using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;
public class CameraController : MonoBehaviour
{
    [SerializeField]
    public GameObject player;
    public Camera cam;
    public float smoothness = 0.1f;
    public bool following = true;
    public Vector2 offset;

    void Start()
    {
        cam = GetComponent<Camera>();
        
        NetworkObject playerNetworkObject = GetComponentInParent<NetworkObject>();
        
        if (playerNetworkObject != null && !playerNetworkObject.IsOwner)
        {
            cam.enabled = false;
            GetComponent<AudioListener>().enabled = false;
            enabled = false;
        }

        Cursor.lockState = CursorLockMode.Confined;
    }
    
    
    void FixedUpdate()
    {
        if (player == null || !following || !Application.isFocused) return;
        
        Vector2 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        Vector2 playerPos = new Vector2(player.transform.position.x, player.transform.position.y);
        Vector2 betweenPos = (mousePos + playerPos) / 2;
        Vector2 smoothedPos = Vector2.Lerp(new Vector2(transform.position.x, transform.position.y), betweenPos + offset, smoothness);
        transform.position = new Vector3(smoothedPos.x, smoothedPos.y, transform.position.z);
    }
}
