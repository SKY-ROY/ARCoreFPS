using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatsControl : MonoBehaviour
{
    public Text highScore;
    public Text totalGems;
    public Text totalCoins;
    
    private GameObject settings;
    // Start is called before the first frame update
    void Start()
    {
        settings = GameObject.Find("StartSceneControl");//for future refernce in the script
    }

    // Update is called once per frame
    void Update()
    {
        highScore.text = "High Score: " + settings.GetComponent<GlobalSettingsControl>().highScoreCount.ToString();
        totalCoins.text = "T. Coins: " + settings.GetComponent<GlobalSettingsControl>().totalCoinCount.ToString();
        totalGems.text = "T. Gems: " + settings.GetComponent<GlobalSettingsControl>().totalGemCount.ToString();
    }
}
