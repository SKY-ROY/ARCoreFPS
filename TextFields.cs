using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextFields : MonoBehaviour
{   
    [Header("Gameplay Text Elements")]
    public Text scoreText;
    public Text levelCount;
    public Text timePassed;
    public Text highScore;
    public Text coins;
    public Text gems;
    public Text totalCoins;
    public Text totalGems;

    [Header("Game Over Text Elements")]
    public Text _coins;
    public Text _gems;
    public Text _score;
    public Text _totalCoins;
    public Text _totalGems;
    public Text _highScore;

    private GameObject FPSScene;

    private void Start()
    {
        FPSScene = GameObject.Find("FPSSceneControl");
    }
    void Update()
    {
        scoreText.text = "Score: " + FPSScene.GetComponent<FPSSceneControl>().score;//Target.ScoreCount() ;
        highScore.text = "H. Score: " + FPSScene.GetComponent<FPSSceneControl>().highScore;

        coins.text = "Coins: " + FPSScene.GetComponent<FPSSceneControl>().coins;//Target.CoinCount;
        totalCoins.text = "T. Coins: " + FPSScene.GetComponent<FPSSceneControl>().totalCoins;

        gems.text = "Gems: " + FPSScene.GetComponent<FPSSceneControl>().gems;//Target.GemCount();
        totalGems.text = "T. Gems: " + FPSScene.GetComponent<FPSSceneControl>().totalGems;

        levelCount.text = "Level: :)";
        timePassed.text = "Time (s): " + Time.timeSinceLevelLoad.ToString("0");
        
    }
}
