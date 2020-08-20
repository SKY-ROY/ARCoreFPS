using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDisplay : MonoBehaviour
{
    public GameObject[] weaponContainer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void WeaponSwitch(int weaponIndex)
    {
        for(int i = 0; i < weaponContainer.Length; i++)
        {
            if(weaponIndex == i)
            {
                weaponContainer[weaponIndex].SetActive(true);
            }
            else
            {
                weaponContainer[i].SetActive(false);
            }
        }
    }
}
