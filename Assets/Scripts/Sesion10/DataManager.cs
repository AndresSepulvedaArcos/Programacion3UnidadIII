using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;


[System.Serializable]
public class SaveGame
{
    public int level;
    public float xp;

}
 
public class DataManager
{

    public static void SaveData(SaveGame saveGame)
    {
        string path = string.Format("{0}/" + Application.productName +"_savedata.dat" , Application.persistentDataPath);
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        FileStream fileStream;
       
        if(File.Exists(path))
        {
            File.WriteAllText(path, "");
            fileStream = File.Open(path, FileMode.Open);

        }else
        {
            fileStream = File.Create(path); 
        }

        binaryFormatter.Serialize(fileStream, saveGame);

        fileStream.Close();

        Debug.LogFormat("File saved in {0}", path);


    }

   public static SaveGame LoadSave()
    {
        SaveGame save=null;
        string path = string.Format("{0}/" + Application.productName + "_savedata.dat", Application.persistentDataPath);
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        FileStream fileStream;


        if(File.Exists(path))
        {
            
            fileStream = File.Open(path, FileMode.Open);
            //cast 
            save = (SaveGame) binaryFormatter.Deserialize(fileStream);

        }else
        {
            save = new SaveGame();
        }


        return save;
    }
}