using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[System.Serializable]
public struct Rows
{
   public string[] columns;

}
[System.Serializable]
public class JsonObject
{
    public int level;
    public float timeElapsed;
    public string playerName;
}


public class ReadFile : MonoBehaviour
{
    public TextAsset textAsset;
    public string[] rows;
    public List<Rows> table = new List<Rows>();

    [Header("Json Object")]
    public JsonObject jsonObject;
    [Header("Save Object")]
    public SaveGame save;

    [Header("Scriptable")]
    public ScritableDatabase database;
    // Start is called before the first frame update
    void Start()
    {
        ReadFileIO();
        ReadCSV();
        ReadJson();
        SaveGame();

        LoadSave();
    }

    void SaveGame()
    {
        SaveGame saveGame = new SaveGame();
        saveGame.level = 3;

        DataManager.SaveData(saveGame);

    }

    void LoadSave()
    {
        save = DataManager.LoadSave();
    }
 
    void ReadFileIO()
    {
        string path = "Assets/Resources/ExampleText.txt";

        if (!File.Exists(path))
        {
            File.WriteAllText(path,"");

        }
         
       Debug.Log(  File.ReadAllText(path));
         
    }

    void ReadCSV()
    {
        string path = "Assets/Resources/SampleCSVFile_2kb.csv";

        if (!File.Exists(path)) return;

        string allCsvData= File.ReadAllText(path);

        rows= allCsvData.Split('\n');

        for (int i = 0; i < rows.Length; i++)
        {
            Rows row = new Rows();
            row.columns = rows[i].Split(',') ;
            table.Add(row);

        }
        database.table = table;

        Debug.Log(allCsvData);


    }

    void ReadJson()
    {
        string path = "Assets/Resources/sample2.json";
        if (!File.Exists(path)) return;
        string jsonString = File.ReadAllText(path);

        jsonObject = new JsonObject();
        JsonUtility.FromJsonOverwrite(jsonString, jsonObject);



    }

}
