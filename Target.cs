using UnityEngine;

public class Target : MonoBehaviour
{
    public float health = 20f;
    
    private static int killScore = 0, killCoins = 0, killGems = 0, killCount = 0;

    private int awardedScore, awardedCoins, awardedGems;

    private GameObject FPSSceneController;
    private void Start()
    {
        FPSSceneController = GameObject.Find("FPSSceneControl");
    }
    public void TakeDamage(float DamageAmount)
    {
        health -= DamageAmount;
        if(health <= 0f)
        {
            Die();
        }
    }
    void Die()
    {
        killCount++;
        if(GetComponent<MonsterController>())
        {
            ScoreMultiplier(GetComponent<MonsterController>().creatureType);
        }
        Destroy(gameObject);
        //Debug.Log("scored: " + killScore);
    }
    void ScoreMultiplier(char thisType)
    {
        switch(thisType)
        {
            case 'A':
                awardedScore = 1;
                awardedCoins = 2;
                awardedGems = 1;
                break;
            case 'B':
                awardedScore = 5;
                awardedCoins = 4;
                awardedGems = 2;
                break;
            case 'C':
                awardedScore = 10;
                awardedCoins = 6;
                awardedGems = 3;
                break;
            case 'D':
                awardedScore = 50;
                awardedCoins = 8;
                awardedGems = 4;
                break;
            default:
                awardedScore = 0;
                awardedCoins = 0;
                awardedGems = 0;
                break;
        }
        killScore += awardedScore ;
        killCoins += awardedCoins;
        killGems += awardedGems;
        FPSSceneControl.CoinCountUpdate(awardedCoins);
        FPSSceneControl.GemCountUpdate(awardedGems);
    }
    public static int GetScoreCount()
    {
        return killScore;
    }
    public static void SetScoreCount(int arg)
    {
        killScore = arg;
    }
    public static int GetCoinCount()
    {
        return killCoins;
    }
    public static void SetCoinCount(int arg)
    {
        killCoins = arg;
    }
    public static int GetGemCount()
    {
        return killGems;
    }
    public static void SetGemCount(int arg)
    {
        killGems = arg;
    }
}
