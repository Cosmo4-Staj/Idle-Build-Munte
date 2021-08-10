using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        //Instantiate(Levels[PlayerPrefs.GetInt("LevelNo")], transform.position, Quaternion.identity);
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
        //buraya sonra tekrardan bak

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
}



















/*using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    void Start()
    {
        moneyManager = FindObjectOfType<MoneyManager>();
        diggerSpawner = FindObjectOfType<DiggerSpawner>();
        stoneworkerSpawner = FindObjectOfType<StoneworkerSpawner>();

        diggerSpawner.DiggerSpawn();
        stoneworkerSpawner.StoneworkerSpawn();

        LevelsText.text = "Level " + (levelCount + 1);
        StartScreen.SetActive(true);
        GameScreen.SetActive(false);
        WinScreen.SetActive(false);
        StartGame();
    }

    void Update()
    {
        //LevelsText.GetComponent<TextMeshProUGUI>().SetText("Level: " + (levelCount + 1).ToString());
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void AddStrength()
    {
        spawnStartTime -= 0.1f;
        spawnRepeatTime -= 0.1f;
        moneyManager.SubtractMoney(15);
    }

    public void StartGame()
    {

        if (isGameRestarted)
        {
            StartScreen.SetActive(false);
            GameScreen.SetActive(true);
        }
        levelCount = PlayerPrefs.GetInt("levelCount", levelCount);

        Debug.Log("level uretildi");
        if (levelCount < 0 || levelCount >= Levels.Count)
        {
            levelCount = 0;
            Debug.Log(levelCount);
            PlayerPrefs.SetInt("levelCount", levelCount);
        }

        CreateLevel(levelCount);
    }

    public void CreateLevel(int Levelindex)
    {
        Instantiate(Levels[Levelindex]);
    }

    public void StartTheGame()
    {
        StartScreen.SetActive(false);
        GameScreen.SetActive(true);
        isGameStarted = true;
    }

    public void NextLevel()
    {
        levelCount++;
        PlayerPrefs.SetInt("LevelNo", levelCount);
        isGameEnded = false;
        isGameRestarted = true;
        isGameStarted = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void RestartLevel()
    {
        isGameRestarted = true;
        isGameStarted = true;
        isGameEnded = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void OnLevelStarted()
    {
        isGameRestarted = true;
        isGameStarted = true;
        StartScreen.SetActive(false);
    }

    public void OnLevelCompleted()
    {
        isGameEnded = true;
        GameScreen.SetActive(false);
        WinScreen.SetActive(true);
    }
}*/