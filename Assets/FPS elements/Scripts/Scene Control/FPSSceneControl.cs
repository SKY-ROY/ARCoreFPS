using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FPSSceneControl : MonoBehaviour
{
    public GameObject portalPrefabParticleEffect;
    public GameObject[] activeScreens;//ShooterSccreen, PauseScreen, GameOverScreen
    public Button slotAButton;
    public Button slotBButton;
    public Button slotCButton;
    
    [Header("Game Over Stats")]
    public Text scoreText;
    public Text coinsText;
    public Text gemsText;
    public Text highScoreText;
    public Text totalCoinsText;
    public Text totalGemsText;

    public static bool collect = true;
    public int highScore, score, totalCoins, coins, totalGems, gems; 
    public float timeSinceLevelStart;
    public static int staticCoinsFlag, staticGemsFlag;// stCoins, stGems;
    public static bool isGameOverFlag = false;

    public Animator camAnim;

    private bool portalBool;
    private static bool isCoinUpdate = false, isGemUpdate = false;

    // Update is called once per frame
    private void Start()
    {
        DefaultState();
        GameObject weaponHolder = GameObject.Find("WeaponHolder");
        
        if(weaponHolder.GetComponent<MyInventoryControl>().slotA_index == -1)
        {
            slotAButton.interactable = false;
        }
        if(weaponHolder.GetComponent<MyInventoryControl>().slotB_index == -1)
        {
            slotBButton.interactable = false;
        }
        if(weaponHolder.GetComponent<MyInventoryControl>().slotC_index == -1)
        {
            slotCButton.interactable = false;
        }
    }
    void Update()
    {
        timeSinceLevelStart = Time.timeSinceLevelLoad;

        portalBool = GetComponent<GlobalSettingsControl>().portalBool;
        portalPrefabParticleEffect.SetActive(portalBool);

        highScore = GetComponent<GlobalSettingsControl>().highScoreCount;
        totalCoins = GetComponent<GlobalSettingsControl>().totalCoinCount;
        totalGems = GetComponent<GlobalSettingsControl>().totalGemCount;

        score = Target.GetScoreCount();
        coins = Target.GetCoinCount();
        gems = Target.GetGemCount();

        if (score >= highScore)
        {
            highScore = score;
            gameObject.GetComponent<GlobalSettingsControl>().HighScoreUpdate();//save current high score value
        }

        if (/*staticCoinsFlag != totalCoins && */isCoinUpdate)
        {
            totalCoins += staticCoinsFlag;
            gameObject.GetComponent<GlobalSettingsControl>().TotalCoinsUpdate();//save current total coins value
            isCoinUpdate = false;
        }

        if (/*staticGemsFlag != totalGems && */isGemUpdate)
        {
            totalGems += staticGemsFlag;
            gameObject.GetComponent<GlobalSettingsControl>().TotalGemsUpdate();//save current total gems value
            isGemUpdate = false;
        }
        
        if (PlayerController.Health <= 0 && !isGameOverFlag)
        {
            GameOver();
            isGameOverFlag = true;
        }
        
    }
    public static void CoinCountUpdate(int arg)
    {
        staticCoinsFlag = arg;
        isCoinUpdate = true;
    }
    public static void GemCountUpdate(int arg)
    {
        staticGemsFlag = arg;
        isGemUpdate = true;
    }
    public void PauseResume(bool pauseState)
    {
        if(pauseState)
        {
            Time.timeScale = 0;
        }
        else if(!pauseState)
        {
            Time.timeScale = 1;
        }
    }
    public void DefaultState()
    {
        MonsterController.ResetParameters();

        Target.SetScoreCount(0);//reset score
        Target.SetCoinCount(0);//reset coins
        Target.SetGemCount(0);//reset gems

        highScore = transform.GetComponent<GlobalSettingsControl>().highScoreCount;//load high score from saved file
        totalCoins = transform.GetComponent<GlobalSettingsControl>().totalCoinCount;//load total coins from saved file
        totalGems = transform.GetComponent<GlobalSettingsControl>().totalGemCount;//load total gems from saved file

        Debug.Log("default state");
        Debug.Log("tc: " + totalCoins);
        Debug.Log("tg: " + totalGems);

        isGameOverFlag = false;
    }
    public void Restart()
    {
        DefaultState();
        GameObject.Find("ScreenController").GetComponent<LevelLoader>().LoadLevel("FpsScene1");
        //SceneManager.LoadScene("FpsScene1");
        Time.timeScale = 1;
    }
    public void GameOver()
    {
        activeScreens[2].SetActive(true);
        activeScreens[0].SetActive(false);
        activeScreens[1].SetActive(false);

        scoreText.text = "Score: " + Target.GetScoreCount().ToString();
        coinsText.text = "Coins: " + Target.GetCoinCount().ToString();
        gemsText.text = "Gems: " + Target.GetGemCount().ToString();

        highScoreText.text = "H. Score: " + highScore;
        totalCoinsText.text = "T. Coins: " + totalCoins;
        totalGemsText.text = "T. Gems: " + totalGems;
        
        Time.timeScale = 0;
    }
    public void CurrencyCollect()
    {
        totalCoins += staticCoinsFlag;
        totalGems += staticGemsFlag;

        gameObject.GetComponent<GlobalSettingsControl>().TotalCoinsUpdate();//save current total coins value
        gameObject.GetComponent<GlobalSettingsControl>().TotalGemsUpdate();//save current total gems value
    }
    public void CamShake()
    {
        int rand = Random.Range(0, 3);
        if(rand == 0)
        {
            camAnim.SetTrigger("Shake1");
        }
        else if(rand == 1)
        {
            camAnim.SetTrigger("Shake2");
        }
        else if(rand == 2)
        {
            camAnim.SetTrigger("Shake3");
        }
    }
}