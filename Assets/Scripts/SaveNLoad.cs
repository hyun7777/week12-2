using NUnit.Framework;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    public Vector3 playerPos;
    public Vector3 playerRot;

    public List<int> invenArrayNumber = new List<int>();
    public List<string> invenItemName = new List<string>();
    public List<int> invenItemNumber = new List<int>();
}

public class SaveNLoad : MonoBehaviour
{
    private SaveData saveData = new SaveData();

    private string SAVE_DATA_DIRECTORY;
    private string SAVE_FIELNAME = "/SaveFile.txt";

    private PlayerController thePlayer;
    private Inventory theInven;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SAVE_DATA_DIRECTORY = Application.dataPath + "/Saves/";

        if (!Directory.Exists(SAVE_DATA_DIRECTORY))
            Directory.CreateDirectory(SAVE_DATA_DIRECTORY);
    }

    public void SaveData()
    {
        thePlayer = FindAnyObjectByType<PlayerController>();
        theInven = FindAnyObjectByType<Inventory>();

        saveData.playerPos = thePlayer.transform.position;
        saveData.playerRot = thePlayer.transform.eulerAngles;

        Slot[] slots = theInven.GetSlots();
        for(int i = 0; i < slots.Length; i++)
        {
            if( slots[i] != null )
            {
                saveData.invenArrayNumber.Add(i);
                saveData.invenItemName.Add(slots[i].item.itemName);
                saveData.invenItemNumber.Add(slots[i].itemCount);
            }
        }

        string json = JsonUtility.ToJson(saveData);

        File.WriteAllText(SAVE_DATA_DIRECTORY + SAVE_FIELNAME, json);

        Debug.Log("저장완료");
        Debug.Log(json);
    }

    public void LoadData()
    {
        if (File.Exists(SAVE_DATA_DIRECTORY + SAVE_FIELNAME))
        {
            string loadJson = File.ReadAllText(SAVE_DATA_DIRECTORY);
            saveData = JsonUtility.FromJson<SaveData>(loadJson);

            thePlayer = FindAnyObjectByType<PlayerController>();
            theInven = FindAnyObjectByType<Inventory>();

            thePlayer.transform.position = saveData.playerPos;
            thePlayer.transform.eulerAngles = saveData.playerRot;

            for(int i = 0; i < saveData.invenItemName.Count; i++)
            {
                theInven.LoadToInven(saveData.invenArrayNumber[i], saveData.invenItemName[i], saveData.invenItemNumber[i]);
            }

            Debug.Log("로드 완료");
        }
        else
            Debug.Log("세이브 파일이 없습니다");
    }
}