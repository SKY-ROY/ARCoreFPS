using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SlotControl : MonoBehaviour
{
    public char Slot;
    public static int Count = 1;//weapon count for respective slots
    public GameObject[] slotWeapons = new GameObject[Count];//array of the weapons in current slot
    public int activeWeaponIndex = 0;//active weapon index to be modified through inventory screen
    public AudioMixerGroup audioMixer;

    private Vector3 weaponSpwanPosition;

    internal bool Bridge_shootCurrent = false;
    
    public Text ammmoTxt;
    GameObject refWeapon, newWeapon;

    private void Start()
    {
        SlotFilter(Slot);
        ChangeSlotWeapon(activeWeaponIndex);
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.GetComponentInChildren<AudioSource>())
        {
            gameObject.GetComponentInChildren<AudioSource>().outputAudioMixerGroup = audioMixer;
        }
        if (!audioMixer)
        {
            return;
        }
    }
    public void Bridge_ClickToShoot(bool shootFlag)
    {
        Bridge_shootCurrent = shootFlag;
    }
    public void ChangeSlotWeapon(int argIndex)
    {
        if (argIndex == -1)
        {
            gameObject.SetActive(false);
        }
        else
        {
            activeWeaponIndex = argIndex;
            refWeapon = this.gameObject.transform.GetChild(0).gameObject;
            newWeapon = Instantiate(slotWeapons[activeWeaponIndex]);

            newWeapon.transform.position = refWeapon.transform.position;
            newWeapon.transform.rotation = refWeapon.transform.rotation;
            newWeapon.transform.parent = refWeapon.transform.parent;

            Destroy(refWeapon);
        }
    }
    private void SlotFilter(char slot)
    {
        switch(slot)
        {
            case 'A':
                activeWeaponIndex = GetComponentInParent<MyInventoryControl>().slotA_index;
                break;
            case 'B':
                activeWeaponIndex = GetComponentInParent<MyInventoryControl>().slotB_index; ;
                break;
            case 'C':
                activeWeaponIndex = GetComponentInParent<MyInventoryControl>().slotC_index; ;
                break;
        }
    }
}
