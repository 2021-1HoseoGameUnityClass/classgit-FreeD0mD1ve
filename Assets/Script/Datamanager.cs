using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// 시스템에서 파일을 생성하기 위한 DLL
using System.IO;

using System.Runtime.Serialization.Formatters.Binary;

public class Datamanager : MonoBehaviour
{
    private static Datamanager _instance = null;
    public static Datamanager instance { get { return _instance; } }

    public int playerHP = 3;
    public string currentScene = "Level1";

    private void Awake()
    {
        _instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        Load();
    }

   public void Save()
    {
        SaveData saveData = new SaveData();
        saveData.sceneName = currentScene;
        saveData.playerHP = playerHP;

        FileStream file = File.Create(Application.persistentDataPath + "/save.dat");

        Debug.Log("저장 파일 생성");

        BinaryFormatter binaryFormatter = new BinaryFormatter();
        binaryFormatter.Serialize(file, saveData);
        file.Close();
    }

    public void Load()
    {
        if(File.Exists(Application.persistentDataPath + "/save.dat") == true)
        {
            FileStream file = File.Open(Application.persistentDataPath + "/save.dat", FileMode.Open);

            if(file != null && file.Length > 0)
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                SaveData savedata = (SaveData)binaryFormatter.Deserialize(file);
                playerHP = savedata.playerHP;
                UImanager.instance.PlayerHP();
                currentScene = savedata.sceneName;

                file.Close();
            }
        }
    }
}
