using UnityEngine;
using Unity.Netcode;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private float gameDuration = 300f; // 5 minutes in seconds
    
    private float gameTimer = 0f;
    private int hidersRemaining = 0;
    private int seekersRemaining = 0;
    private bool gameEnded = false;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        gameTimer = gameDuration;
        CountPlayers();
    }

    private void Update()
    {
        if (gameEnded) return;

        gameTimer -= Time.deltaTime;

        if (gameTimer <= 0f)
        {
            HidersWin("Time ran out!");
            return;
        }

        // Check if all seekers are gone
        if (seekersRemaining <= 0)
        {
            HidersWin("All seekers are gone!");
            return;
        }

        // Check if all hiders are gone
        if (hidersRemaining <= 0)
        {
            SeekersWin("All hiders caught!");
            return;
        }
    }

    private void CountPlayers()
    {
        Hider[] hiders = FindObjectsByType<Hider>(FindObjectsSortMode.None);
        Seeker[] seekers = FindObjectsByType<Seeker>(FindObjectsSortMode.None);
        
        hidersRemaining = hiders.Length;
        seekersRemaining = seekers.Length;
    }

    public void HiderCaught()
    {
        hidersRemaining--;
        
        if (hidersRemaining <= 0)
        {
            SeekersWin("All hiders caught!");
        }
    }

    public void SeekerDisconnected()
    {
        seekersRemaining--;
        
        if (seekersRemaining <= 0)
        {
            HidersWin("All seekers are gone!");
        }
    }

    private void HidersWin(string reason)
    {
        gameEnded = true;
        Debug.Log("HIDERS WIN! " + reason);
        EndGameAndReturnToMenu();
    }

    private void SeekersWin(string reason)
    {
        gameEnded = true;
        Debug.Log("SEEKERS WIN! " + reason);
        EndGameAndReturnToMenu();
    }

    private void EndGameAndReturnToMenu()
    {
        if (NetworkManager.Singleton != null)
        {
            NetworkManager.Singleton.Shutdown();
        }
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }

    public float GetRemainingTime()
    {
        return Mathf.Max(0f, gameTimer);
    }

    public int GetHidersRemaining()
    {
        return hidersRemaining;
    }

    public int GetSeekersRemaining()
    {
        return seekersRemaining;
    }
}
