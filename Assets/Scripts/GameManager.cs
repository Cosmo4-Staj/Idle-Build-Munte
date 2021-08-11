using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public float spawnStartTime = 5f;
    public float spawnRepeatTime = 5f;

    public static bool isGameStarted = false;
    public static bool isGameEnded = false;
    public static bool isGameRestarted = false;

    public GameObject StartScreen;
    public GameObject GameScreen;
    public GameObject WinScreen;

    public TextMeshProUGUI LevelsText;
    public List<GameObject> Levels = new List<GameObject>();
    public int levelCount = 0;

    MoneyManager moneyManager;
    DiggerSpawner diggerSpawner;
    StoneworkerSpawner stoneworkerSpawner;

    private int diggersCount;
    private int stoneworkersCount;

    public Image star1;
    public Image star2;
    public Image star3;

    //Superworker
    public Button superworkerButton;

    void Start()
    {
        isGameStarted = false;
        isGameEnded = false;
        LoadLevel();
    }

    void StartGame()
    {
        moneyManager = FindObjectOfType<MoneyManager>();
        diggerSpawner = FindObjectOfType<DiggerSpawner>();
        stoneworkerSpawner = FindObjectOfType<StoneworkerSpawner>();

        diggerSpawner.DiggerSpawn();
        stoneworkerSpawner.StoneworkerSpawn();

    }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    //Power Button
    public void AddStrength()
    {
        spawnStartTime -= 0.1f;
        spawnRepeatTime -= 0.1f;
        moneyManager.StrengthFee();
    }

    void Update()
    {
        if (!GameManager.isGameStarted || GameManager.isGameEnded)
        {
            return;
        }

        LevelsText.GetComponent<TextMeshProUGUI>().SetText("Level " + (levelCount + 1).ToString());
    }
    public void NextLevel()
    {
        SaveWorkers();
        OnLevelCompleted();
        levelCount++;
        PlayerPrefs.SetInt("LevelNo", levelCount);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void LoadLevel()
    {
        
        levelCount = PlayerPrefs.GetInt("LevelNo", 0);

        if (levelCount > Levels.Count - 1 || levelCount < 0)
        {
            levelCount = 0;
            PlayerPrefs.SetInt("LevelNo", levelCount);
        }
        Instantiate(Levels[levelCount], Vector3.zero, Quaternion.identity);


        //if level is 5 or 10, superworker button is active based on its price and percentage of building

        if (!(PlayerPrefs.GetInt("LevelNo") == 4 || PlayerPrefs.GetInt("LevelNo") == 9))
        {
            superworkerButton.gameObject.SetActive(false);
        }

        if (PlayerPrefs.GetInt("LevelNo") == 4 || PlayerPrefs.GetInt("LevelNo") == 9)
        {
            superworkerButton.gameObject.SetActive(true);
            superworkerButton.interactable = false;
        }
    }

    public void OnLevelStarted()
    {
        StartGame();
        LoadWorkers();
        //isGameRestarted = true;
        isGameStarted = true;
        StartScreen.SetActive(false);
        GameScreen.SetActive(true);
        WinScreen.SetActive(false);
    }

    public void OnLevelCompleted()
    {
        StarCount();
        Time.timeScale = 0;
        StartScreen.SetActive(false);
        GameScreen.SetActive(false);
        WinScreen.SetActive(true);
        //isGameEnded=true;
    }

    public void SaveWorkers()
    {
        PlayerPrefs.SetInt("DiggersCount",diggerSpawner.diggers.Count);
        PlayerPrefs.SetInt("StoneworkersCount", stoneworkerSpawner.stoneworkers.Count);
    }
    public void LoadWorkers()
    {
        //FOR DIGGERS
        diggersCount = PlayerPrefs.GetInt("DiggersCount");
        if (diggersCount - 1 < 3)
        {
            for (int i = 0; i < diggersCount - 1; i++)
            {
                diggerSpawner.DiggerSpawn();
            }
        }
        else
        {
            for (int i = 0; i < diggersCount / 3 - 1; i++)
            {
                diggerSpawner.DiggerSpawn();
            }
        }

        //FOR STONEWORKERS
        stoneworkersCount = PlayerPrefs.GetInt("StoneworkersCount");
        if (stoneworkersCount - 1 < 3)
        {
            for (int i = 0; i < stoneworkersCount - 1; i++)
            {
                stoneworkerSpawner.StoneworkerSpawn();
            }
        }
        else
        {
            for (int i = 0; i < stoneworkersCount / 3 - 1; i++)
            {
                stoneworkerSpawner.StoneworkerSpawn();
            }
        }
    }

    //Next Level screen -> to calculate star numbers based on earned money
    public void StarCount()
    {
        star1.gameObject.SetActive(false);
        star2.gameObject.SetActive(false);
        star3.gameObject.SetActive(false);

        int starMoney = moneyManager.currentMoney;

        if (starMoney < 50)
        {
            star1.gameObject.SetActive(true);
        }
        else if (starMoney >= 50 && starMoney <100)
        {
            star1.gameObject.SetActive(true);
            star2.gameObject.SetActive(true);
        }
        else if (starMoney >= 100)
        {
            star1.gameObject.SetActive(true);
            star2.gameObject.SetActive(true); 
            star3.gameObject.SetActive(true);
        }
    }  
   
}