using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Swipe))]
public class GameManager : MonoBehaviour
{

    #region Singleton
    public static GameManager ins;

    void Awake()
    {
        if (ins != null)
        {
            Debug.LogError("More than 1 Game Manager.");
        }
        else
        {
            ins = this;
        }
    }
    #endregion

    [SerializeField] private new GameObject camera;
    [SerializeField] private GameObject playerGO;
    [SerializeField] private Vector3 startPos;
    [SerializeField] private RotateGround ground;
    [SerializeField] private Text scoreText;
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private Text gameOverScore;
    [SerializeField] private GameObject gameOverMenu;
    [SerializeField] private float cameraRotationSpeed = 20f;
    [SerializeField] private GameObject[] hiddenBlocks;
    [SerializeField] private MeshRenderer finishLine;

    private int score;
    private bool isPlayerAlive;
    private bool firstLoad;
    private AudioSource audioSource;
    private Swipe swipe;

    void Start()
    {
        firstLoad = true;
        gameOverMenu.SetActive(false);
        finishLine.enabled = false;
        swipe = GetComponent<Swipe>();
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.LogError("No audio source found.");
        }
    }

    void Update()
    {
        if (!isPlayerAlive)
        {
            RotateMenuCamera();
            if (firstLoad)
            {
                TurnOnMainMenu();
            }
        }
    }

    void SpawnPlayer()
    {
        Debug.Log("[GAMEMANAGER] Spawning Player");
        camera.SetActive(false);
        HideHiddenBlocks();
        GameObject _player = (GameObject)Instantiate(playerGO, startPos, Quaternion.identity);
        swipe.GetPlayerReference(_player);
        SetUI();
        isPlayerAlive = true;
    }

    void StartRotation()
    {
        ground.ResetRotation();
        ground.ToggleRotation();
    }

    void RotateMenuCamera()
    {
        camera.transform.RotateAround(Vector3.zero, Vector3.up, cameraRotationSpeed * Time.deltaTime);
    }

    void TurnOnMainMenu()
    {
        mainMenu.SetActive(true);
        scoreText.enabled = false;
        firstLoad = false;
    }

    void TurnOnGameOverMenu()
    {
        gameOverMenu.SetActive(true);
        scoreText.enabled = false;
        gameOverScore.text = string.Format("Score: {0}", score);
    }

    void SetUI()
    {
        mainMenu.SetActive(false);
        gameOverMenu.SetActive(false);
        scoreText.enabled = true;
        score = 0;
        scoreText.text = "Score: 0";
    }

    void HideHiddenBlocks()
    {
        for (int i = 0; i < hiddenBlocks.Length; i++)
        {
            hiddenBlocks[i].SetActive(false);
        }
    }

    public void IncreaseRotationSpeed()
    {
        ground.IncreaseSpeed();
    }

    public void IncreaseScore()
    {
        score++;
        scoreText.text = string.Format("Score: {0}", score);
        audioSource.PlayOneShot(AudioManager.ins.ScoreSound);
    }

    public void PlayerDied()
    {
        camera.SetActive(true);
        isPlayerAlive = false;
        TurnOnGameOverMenu();
        ground.ToggleRotation();
    }

    public void ShowHiddenBlocks()
    {
        for (int i = 0; i < hiddenBlocks.Length; i++)
        {
            hiddenBlocks[i].SetActive(true);
        }
    }

    public void ShowFinishLine()
    {
        finishLine.enabled = true;
    }

    public void StartGame()
    {
        SpawnPlayer();
        StartRotation();
    }

    public void Exit()
    {
        Debug.Log("Exiting game.");
        Application.Quit();
    }

}
