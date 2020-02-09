using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using System.IO;

public class GlobalSettingsControl : MonoBehaviour
{
    [Header("Setting Variables")]
    public bool musicBool = true;
    public bool sfxBool = true;
    public bool portalBool = true;
    public int qualityLevelIndex;
    public float volumeVal;
    public int highScoreCount;
    public int totalCoinCount;
    public int totalGemCount;

    [Header("Scene Object Reference")]
    public Slider volumeSlider;
    public static int size;
    public Toggle[] toggleArray = new Toggle[size];//Music, SFX, Portal.
    public Dropdown graphicsVal;
    
    [Header("Global Object Reference")]
    public AudioMixer audioMixer;

    private float previousVol;
    
    private void Start()
    {
        Time.timeScale = 1;//flush all pause states and reset to normal time scale
        IsFirstTime();
        LoadState();
        //Debug.Log("Loaded HS: " + highScoreCount);
        //Debug.Log("loaded tcoins: " + totalCoinCount);
        //Debug.Log("loaded tgems: " + totalGemCount);
        if (musicBool && sfxBool)
        {
            GetComponent<AudioSource>().Play();
        }
    }
    private void Update()
    {
        graphicsVal.value = qualityLevelIndex;
        toggleArray[0].isOn = musicBool;
        toggleArray[1].isOn = sfxBool;
        toggleArray[2].isOn = portalBool;
        volumeSlider.value = volumeVal;
    }
    public void TotalCoinsUpdate()
    {
        if (gameObject.GetComponent<FPSSceneControl>())
        {
            totalCoinCount = gameObject.GetComponent<FPSSceneControl>().totalCoins;
            SaveState();
            //Debug.Log("coins: " + coinCount);
        }
    }
    public void TotalGemsUpdate()
    {
        if (gameObject.GetComponent<FPSSceneControl>())
        {
            totalGemCount = gameObject.GetComponent<FPSSceneControl>().totalGems;
            SaveState();
            //Debug.Log("gems: " + gemCount);
        }
    }
    public void HighScoreUpdate()
    {
        if(gameObject.GetComponent<FPSSceneControl>())
        {
            highScoreCount = gameObject.GetComponent<FPSSceneControl>().highScore;
            SaveState();
            //Debug.Log("HS: " + highScoreCount);
        }
    }
    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
        qualityLevelIndex = qualityIndex;
        SaveState();
    }
    public void SetMusic(bool argVal)
    {
        musicBool = argVal;
        if(musicBool)
        {
            GetComponent<AudioSource>().Play();
        }
        else if(!musicBool)
        {
            GetComponent<AudioSource>().Stop();
        }
        SaveState();
    }
    public void SetSFX(bool argVal)
    {
        sfxBool = argVal;
        if(!sfxBool)
        {
            audioMixer.GetFloat("Volume", out previousVol);
            audioMixer.SetFloat("Volume", -80);
            GetComponent<AudioSource>().Stop();
        }
        else if(sfxBool)
        {
            audioMixer.SetFloat("Volume", previousVol);
            GetComponent<AudioSource>().Play();
        }
        SaveState();
    }
    public void SetPortal(bool argVal)
    {
        portalBool = argVal;
        SaveState();
    }
    public void SetVolume(float argvolume)
    {
        audioMixer.SetFloat("Volume", argvolume);
        volumeVal = argvolume;
        SaveState();
    }
    public void SaveState()
    {
        SaveSystem.SaveState(this);
    }
    public void LoadState()
    {
        StateData data = SaveSystem.LoadState();

        sfxBool = data.sfx;
        musicBool = data.music;
        portalBool = data.portal;
        qualityLevelIndex = data.qualityLevel;
        volumeVal = data.volume;
        highScoreCount = data.highScore;
        totalGemCount = data.totalGems;
        totalCoinCount = data.totalCoins;
    }
    public void IsFirstTime()
    {
        string path1 = Application.persistentDataPath + "/this.Sett";
        
        if (!File.Exists(path1))
        {
            Debug.Log("First Time Launch");
            SaveState();
        }
        else
            Debug.Log("Not First Time");
    }
}
