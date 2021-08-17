using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
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
    public bool isTouched = false;

    public GameObject speedImage;
    public ParticleSystem finishParticles;
    public AudioClip finishSound;

    void Start()
    {
        isGameStarted = false;
        isGameEnded = false;
        LoadLevel();

        LevelsText.GetComponent<TextMeshProUGUI>().SetText("Level " + (levelCount + 1).ToString());
    }

    void StartGame()
    {
        moneyManager = FindObjectOfType<MoneyManager>();
        diggerSpawner = FindObjectOfType<DiggerSpawner>();
        stoneworkerSpawner = FindObjectOfType<StoneworkerSpawner>();

        speedImage.gameObject.SetActive(false);
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
        speedImage.gameObject.SetActive(true);
        spawnStartTime -= 0.1f;
        spawnRepeatTime -= 0.1f;
        moneyManager.StrengthFee();
        StartCoroutine(SpeedUp());
    }

    IEnumerator SpeedUp()
    {
        yield return new WaitForSeconds(1f);
        speedImage.gameObject.SetActive(false);
    }

    void Update()
    {
        if (!GameManager.isGameStarted || GameManager.isGameEnded)
        {
            return;
        }
        
        if(!GameManager.isGameEnded){
            //to speed up game in each tap to screen
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                if (Input.GetTouch(0).position.y > 342)
                {
                    Time.timeScale = 2;
                    isTouched = true;
                    StartCoroutine(RevertTime(0.5f));
                }
            }
        }
        
        
    }

    //reverting the time to normal after each touch
    IEnumerator RevertTime(float time)
    {
        yield return new WaitForSeconds(time);
        isTouched = false;
        Time.timeScale = 1;
    }

    public void NextLevel()
    {
        SaveWorkers();
        OnLevelCompleted();
        levelCount++;
        PlayerPrefs.SetInt("level", levelCount);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void LoadLevel()
    {
        levelCount = PlayerPrefs.GetInt("level");

        if (levelCount >= Levels.Count || levelCount < 0)
        {
            levelCount = 0;
            PlayerPrefs.SetInt("level", levelCount);
        }
        Instantiate(Levels[levelCount], Vector3.zero, Quaternion.identity);

        //if level is 5 or 10, superworker button is active based on its price and percentage of building
        if (!(PlayerPrefs.GetInt("level") == 4 || PlayerPrefs.GetInt("level") == 9))
        {
            superworkerButton.gameObject.SetActive(false);
        }

        if (PlayerPrefs.GetInt("level") == 4 || PlayerPrefs.GetInt("level") == 9)
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

        moneyManager.CheckMoney();
    }

    public void OnLevelCompleted()
    {
        StartCoroutine(FinishParticles());
        isGameEnded = true;
        StarCount();
        StartScreen.SetActive(false);
        GameScreen.SetActive(false);
        WinScreen.SetActive(true);
    }

    IEnumerator FinishParticles()
    {
        AudioSource.PlayClipAtPoint(finishSound, transform.position, 1);
        Instantiate(finishParticles);
        yield return new WaitForSeconds(1f);
        Time.timeScale = 0;
    }

    public void SaveWorkers()
    {
        //saving current total money for next levels
        PlayerPrefs.SetInt("TotalMoney", moneyManager.currentMoney);

        PlayerPrefs.SetInt("DiggersCount", diggerSpawner.diggers.Count);
        PlayerPrefs.SetInt("StoneworkersCount", stoneworkerSpawner.stoneworkers.Count);
    }
    public void LoadWorkers()
    {
        //FOR DIGGERS
        diggersCount = PlayerPrefs.GetInt("DiggersCount");

        for (int i = 0; i < diggersCount / 3 - 1; i++)
        {
            diggerSpawner.DiggerSpawn();
        }

        //FOR STONEWORKERS
        stoneworkersCount = PlayerPrefs.GetInt("StoneworkersCount");
        for (int i = 0; i < stoneworkersCount / 3 - 1; i++)
        {
            stoneworkerSpawner.StoneworkerSpawn();
        }
    }

    //Next Level screen -> to calculate star numbers based on earned money
    public void StarCount()
    {
        star1.gameObject.SetActive(false);
        star2.gameObject.SetActive(false);
        star3.gameObject.SetActive(false);

        int starMoney = moneyManager.currentMoney;

        if (starMoney < 100)
        {
            star1.gameObject.SetActive(true);
        }
        else if (starMoney >= 100 && starMoney < 300)
        {
            star1.gameObject.SetActive(true);
            star2.gameObject.SetActive(true);
        }
        else if (starMoney >= 300)
        {
            star1.gameObject.SetActive(true);
            star2.gameObject.SetActive(true);
            star3.gameObject.SetActive(true);
        }
    }

}