using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveStorage
{
    public List<int> storageArrayNumber = new List<int>();
    public List<int> storageItemCode = new List<int>();
    public List<int> storageItemNumber = new List<int>();
}

public class SaveAndLoadStorage : MonoBehaviour
{
    private SaveStorage saveData = new SaveStorage();

    private string SAVE_DATA_DIRECTORY;
    private string SAVE_FILENAME = "/SaveStorageFile.txt";

    private Storage theStorage;

    void Start()
    {
        SAVE_DATA_DIRECTORY = Application.dataPath + "/Save/";

        if (!Directory.Exists(SAVE_DATA_DIRECTORY))
            Directory.CreateDirectory(SAVE_DATA_DIRECTORY);
    }

    public void SaveStorage()
    {
        theStorage = FindObjectOfType<Storage>();

        StorageSlot[] theSlot = theStorage.GetStorageSlots();
        for (int i = 0; i < theSlot.Length; i++)
        {
            if (theSlot[i].item != null)
            {
                saveData.storageArrayNumber.Add(i);
                saveData.storageItemCode.Add(theSlot[i].item.itemCode);
                saveData.storageItemNumber.Add(theSlot[i].itemCount);
            }
        }

        string json = JsonUtility.ToJson(saveData);

        File.WriteAllText(SAVE_DATA_DIRECTORY + SAVE_FILENAME, json);

        Debug.Log(json);
    }

    public void LoadStorage()
    {
        if (File.Exists(SAVE_DATA_DIRECTORY + SAVE_FILENAME))
        {
            string loadJson = File.ReadAllText(SAVE_DATA_DIRECTORY + SAVE_FILENAME);
            saveData = JsonUtility.FromJson<SaveStorage>(loadJson);

            theStorage = FindObjectOfType<Storage>();

            for (int i = 0; i < saveData.storageItemCode.Count; i++)
                theStorage.LoadToStorage(saveData.storageArrayNumber[i], saveData.storageItemCode[i], saveData.storageItemNumber[i]);
        }
        else
            Debug.Log("none save file");
    }

    public void ResetSaveStorage()
    {
        saveData.storageArrayNumber.Clear();
        saveData.storageItemCode.Clear();
        saveData.storageItemNumber.Clear();
    }
}
