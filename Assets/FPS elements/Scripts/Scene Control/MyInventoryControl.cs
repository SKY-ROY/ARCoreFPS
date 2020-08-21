using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class MyInventoryControl : MonoBehaviour
{
    public int slotA_index, slotB_index, slotC_index;

    private void Start()
    {
        FirstInventoryVisit();
        LoadState();
    }
    public void SwitchWeaponA(int weaponIndex)
    {
        slotA_index = weaponIndex;
        SaveState();
    }
    public void SwitchWeaponB(int weaponIndex)
    {
        slotB_index = weaponIndex;
        SaveState();
    }
    public void SwitchWeaponC(int weaponIndex)
    {
        slotC_index = weaponIndex;
        SaveState();
    }
    public void SaveState()
    {
        SaveSystem.SaveInventory(this);
    }
    public void LoadState()
    {
        StateData data = SaveSystem.LoadInventory();

        slotA_index = data.slotA_val;
        slotB_index = data.slotB_val;
        slotC_index = data.slotC_val;
    }
    void FirstInventoryVisit()
    {
        string path2 = Application.persistentDataPath + "/this.Inv";

        if (!File.Exists(path2))
        {
            Debug.Log("First Inventory Visit");
            SaveState();
        }
        else
            Debug.Log("Not First Inventory Visit");
    }
}
