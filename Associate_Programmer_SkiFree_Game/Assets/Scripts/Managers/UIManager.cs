using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    #region Init
    static UIManager UI;
    public static UIManager _Instance
    {
        get
        {
            if (UI == null)
            {
                Debug.LogError("UIManager is NULL");
            }
            return UI;
        }
    }

    private void Awake()
    {
        UI = this;
        GameOver.Initialize();
    }
    #endregion

    public GameObject mainPanel, winMessaga, loseMessage;
    [SerializeField] TextMeshProUGUI UITimerText, UILivesText;
    [SerializeField] Player playerScript;

    Timer timer;

    int playerHealth;
    public float time;

    public UnityAction<int> reduseHealthEvent;
    public UnityAction runOutOfTimeEvent;
    private void Start()
    {
        //Timer Init
        timer = GetComponent<Timer>();
        timer.Duration = time;
        timer.Run();

        //Setting properties
        playerHealth = playerScript.playerStats.lives;

        reduseHealthEvent += ReduseHealth;
    }

    private void Update()
    {
        //Timer
        TimerCountdown();
    }

    void TimerCountdown()
    {
        UITimerText.text = "Time Left: " + (int)timer.SecondsLeft;
        if(timer.SecondsLeft <= 0)
        {
            GameOver.Lose();
        }
    }
    void ReduseHealth(int damage)
    {
        playerHealth -= damage;
        UILivesText.text = "Lives: " + playerHealth.ToString();

        if(playerHealth == 0)
        {
            GameOver.Lose();
        }
    }



    #region Buttons
    public void OnButtonClickNextRace()
    {
        //Go to next level
    }

    public void OnButtonClickRetry()
    {
        Time.timeScale = 1;
        Scene scene = SceneManager.GetActiveScene(); 
        SceneManager.LoadScene(scene.name);
    }

    public void OnButtonClickQuit()
    {
        Application.Quit();
    }
    #endregion
}
