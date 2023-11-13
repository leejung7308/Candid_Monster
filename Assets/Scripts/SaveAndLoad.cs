using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveData
{
    //public Vector3 playerPos;
    //public Vector3 playerRot;

    public List<int> invenArrayNumber = new List<int>();
    public List<string> invenItemName = new List<string>();
    public List<int> invenItemNumber = new List<int>();

    public string Coin;
}

public class SaveAndLoad : MonoBehaviour
{
    private SaveData saveData = new SaveData();

    private string SAVE_DATA_DIRECTORY;
    private string SAVE_FILENAME = "/SaveFile.txt";

    //private Player thePlayer;
    private Inventory theInventory;

    void Start()
    {
        SAVE_DATA_DIRECTORY = Application.dataPath + "/Save/";

        if (!Directory.Exists(SAVE_DATA_DIRECTORY))
            Directory.CreateDirectory(SAVE_DATA_DIRECTORY);
    }

    public void SaveData()
    {
        //thePlayer = FindObjectOfType<Player>();
        theInventory = FindObjectOfType<Inventory>();

        //saveData.playerPos = thePlayer.transform.position;
        //saveData.playerRot = thePlayer.transform.rotation.eulerAngles;

        saveData.Coin = theInventory.GetCoinText();

        Slot[] slots = theInventory.GetSlots();
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item != null)
            {
                saveData.invenArrayNumber.Add(i);
                saveData.invenItemName.Add(slots[i].item.itemName);
                saveData.invenItemNumber.Add(slots[i].itemCount);
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

            //thePlayer = FindObjectOfType<Player>();
            theInventory = FindObjectOfType<Inventory>();

            //thePlayer.transform.position = saveData.playerPos;
            //thePlayer.transform.eulerAngles = saveData.playerRot;
            theInventory.SetCoinText(-int.Parse(saveData.Coin));

            for (int i = 0; i < saveData.invenItemName.Count; i++)
                theInventory.LoadToInven(saveData.invenArrayNumber[i], saveData.invenItemName[i], saveData.invenItemNumber[i]);
        }
        else
            Debug.Log("none save file");
    }

    public void ResetSaveData()
    {
        saveData.invenArrayNumber.Clear();
        saveData.invenItemName.Clear();
        saveData.invenItemNumber.Clear();
    }
}
