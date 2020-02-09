using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public Text healthCount;
    public Slider healthBar;
    public static int Health { get; set; }
    
    // Start is called before the first frame update
    void Start()
    {
        Health = 100;
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.value = Health;
        healthCount.text = "HP: " + Health;
    }   
}
