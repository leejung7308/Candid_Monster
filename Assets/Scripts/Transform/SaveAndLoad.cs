using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveData
{
    public List<int> invenArrayNumber = new List<int>();
    public List<int> invenItemCode = new List<int>();
    public List<int> invenItemNumber = new List<int>();
    public List<int> equipArrayNumber = new List<int>();
    public List<int> equipItemCode = new List<int>();

    public string Coin;
}

public class SaveAndLoad : MonoBehaviour
{
    private SaveData saveData = new SaveData();

    private string SAVE_DATA_DIRECTORY;
    private string SAVE_FILENAME = "/SaveFile.txt";

    private Inventory theInventory;
    private Equipment theEquipment;

    void Start()
    {
        SAVE_DATA_DIRECTORY = Application.dataPath + "/Save/";

        if (!Directory.Exists(SAVE_DATA_DIRECTORY))
            Directory.CreateDirectory(SAVE_DATA_DIRECTORY);
    }

    public void SaveData()
    {
        theInventory = FindObjectOfType<Inventory>();
        theEquipment = FindObjectOfType<Equipment>();
        saveData.Coin = theInventory.GetCoinText();

        Slot[] slots = theInventory.GetSlots();
        Slot[] equipSlots = theEquipment.GetSlots();

        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item != null)
            {
                saveData.invenArrayNumber.Add(i);
                saveData.invenItemCode.Add(slots[i].item.itemCode);
                saveData.invenItemNumber.Add(slots[i].itemCount);
            }
        }
        for(int i = 0;i < equipSlots.Length; i++)
        {
            if (equipSlots[i].item != null)
            {
                saveData.equipArrayNumber.Add(i);
                saveData.equipItemCode.Add(equipSlots[i].item.itemCode);
            }
        }

        string json = JsonUtility.ToJson(saveData);

        File.WriteAllText(SAVE_DATA_DIRECTORY + SAVE_FILENAME, json);

        Debug.Log(json);
    }

    public void LoadData()
    {
        if (File.Exists(SAVE_DATA_DIRECTORY + SAVE_FILENAME))
        {
            string loadJson = File.ReadAllText(SAVE_DATA_DIRECTORY + SAVE_FILENAME);
            saveData = JsonUtility.FromJson<SaveData>(loadJson);

            theInventory = FindObjectOfType<Inventory>();
            theEquipment = FindObjectOfType<Equipment>();

            theInventory.SetCoinText(-int.Parse(saveData.Coin));

            for (int i = 0; i < saveData.invenItemCode.Count; i++)
                theInventory.LoadToInven(saveData.invenArrayNumber[i], saveData.invenItemCode[i], saveData.invenItemNumber[i]);
            for(int i = 0; i < saveData.equipItemCode.Count; i++)
            {
                theEquipment.LoadToEquipment(saveData.equipArrayNumber[i], saveData.equipItemCode[i]);
            }
        }
        else
            Debug.Log("none save file");
    }

    public void ResetSaveData()
    {
        saveData.invenArrayNumber.Clear();
        saveData.invenItemCode.Clear();
        saveData.invenItemNumber.Clear();
    }
}
