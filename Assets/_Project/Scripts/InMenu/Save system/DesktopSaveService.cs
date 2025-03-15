
using Newtonsoft.Json;
using System.IO;

public class DesktopSaveService : ISaveService
{
    readonly RealmsDataHandler RealmsDataHandler;
    private const string PersistentSaveFileName = "/CDSaveData.json";

    public DesktopSaveService(RealmsDataHandler realmsDataHandler)
    {
        RealmsDataHandler = realmsDataHandler;
    }

    public SavedData LoadData()
    {
        string path = GetSaveDataPath();
        SavedData data;
        if (File.Exists(path))
        {
            string stringData = File.ReadAllText(path);
            if(stringData == string.Empty)
            {
                SaveData();
            }
        }
        else
        {
            CreateSaveFile(); 
            SaveData();   
        }

        using (StreamReader streamReader = new StreamReader(path))
        {
            string json = streamReader.ReadToEnd();
            data = JsonConvert.DeserializeObject<SavedData>(json);
        }
        
        return data;
    }

    public void SaveData()
    {
        //типа дата поток хуёк тут в файл преобразуется
        SavedData savedData;
        
        if (RealmsDataHandler.IsInitialized == true)
            savedData = new(RealmsDataHandler.GetDataFromRealmHandlers());
        else
        {
            RealmsDataHandler.InitializeWithDefaultData();
            savedData = new(RealmsDataHandler.GetDataFromRealmHandlers());
        }

        string path = GetSaveDataPath();
        string json = JsonConvert.SerializeObject(savedData);

        if (!File.Exists(path))
        {
            CreateSaveFile();
        }

        using (StreamWriter streamWriter = new StreamWriter(path))
        {
            streamWriter.Write(json);
        }
    }

    public void SaveData(SavedData savedData)
    {
        string path = GetSaveDataPath();
        string json = JsonConvert.SerializeObject(savedData);

        using (StreamWriter streamWriter = new StreamWriter(path))
        {
            streamWriter.Write(json);
        }
    }

    public void SetData(SavedData savedData)
    {
        //типа готовый объект его поля суем куда надо
        UnityEngine.Debug.Log(savedData.Username);
        RealmsDataHandler.Initialize(savedData);
    }

    private void CreateSaveFile()
    {
        File.Create(GetSaveDataPath()).Dispose();
    }

    public string GetSaveDataPath()
    {
        return UnityEngine.Application.persistentDataPath + PersistentSaveFileName;
    }
}
