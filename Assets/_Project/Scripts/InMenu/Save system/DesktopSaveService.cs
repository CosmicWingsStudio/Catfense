
using Newtonsoft.Json;
using System.IO;

//public class DesktopSaveService : ISaveService
//{
//    readonly RealmsDataHandler RealmsDataHandler;
//    private const string PersistentSaveFileName = "/CDSaveData.json";

//    public DesktopSaveService(RealmsDataHandler realmsDataHandler)
//    {
//        RealmsDataHandler = realmsDataHandler;
//    }
//    public SavedData LoadData()
//    {
//        string path = GetSaveDataPath();
//        SavedData data;
//        if (File.Exists(path))
//        {
//            string stringData = File.ReadAllText(path);
//            if(stringData == string.Empty)
//            {
//                SaveData();
//            }
//        }
//        else
//        {
//            CreateSaveFile(); 
//            SaveData();   
//        }

//        //using (StreamReader streamReader = new StreamReader(path))
//        //{
//        //    string EncryptedJson = streamReader.ReadToEnd();
//        //    string DecryptedJson = EncryptionDecryption(EncryptedJson);
//        //    data = JsonConvert.DeserializeObject<SavedData>(DecryptedJson);
//        //}
//        //UnityEngine.Debug.LogError("!NOT USING ENCRYPRION!");

//        using (StreamReader streamReader = new StreamReader(path))
//        {
//            string json = streamReader.ReadToEnd();
//            data = JsonConvert.DeserializeObject<SavedData>(json);
//        }

//        return data;
//    }

//    public void SaveData()
//    {
//        SavedData savedData = null;

//        if (RealmsDataHandler != null)
//        {
//            if (RealmsDataHandler.IsInitialized == true)
//                savedData = new(RealmsDataHandler.GetDataFromRealmHandlers(), RealmsDataHandler.GetADWatchedStatus());
//            else
//            {
//                RealmsDataHandler.InitializeWithDefaultData();
//                savedData = new(RealmsDataHandler.GetDataFromRealmHandlers(), RealmsDataHandler.GetADWatchedStatus());
//            }
//        }
//        else
//            return;

//        string path = GetSaveDataPath();
//        string json = JsonConvert.SerializeObject(savedData);
//        //string encryptedJson = EncryptionDecryption(json);

//        if (!File.Exists(path))
//        {
//            CreateSaveFile();
//        }

//        //using (StreamWriter streamWriter = new StreamWriter(path))
//        //{
//        //    streamWriter.Write(encryptedJson);
//        //}
//        //UnityEngine.Debug.LogError("!NOT USING ENCRYPTION!");

//        using (StreamWriter streamWriter = new StreamWriter(path))
//        {
//            streamWriter.Write(json);
//        }
//    }

//    public void SaveData(SavedData savedData)
//    {
//        string path = GetSaveDataPath();
//        string json = JsonConvert.SerializeObject(savedData);

//        using (StreamWriter streamWriter = new StreamWriter(path))
//        {
//            streamWriter.Write(json);
//        }
//    }

//    public void SetData(SavedData savedData)
//    {
//        RealmsDataHandler.Initialize(savedData);
//    }

//    private void CreateSaveFile()
//    {
//        File.Create(GetSaveDataPath()).Dispose();
//    }

//    public string GetSaveDataPath()
//    {
//        return UnityEngine.Application.persistentDataPath + PersistentSaveFileName;
//    }

//    //private string EncryptionDecryption(string jsonString)
//    //{
//    //    string keyword = "24082003";
//    //    string result = string.Empty;

//    //    for (int i = 0; i < jsonString.Length; i++)
//    //    {
//    //        result += (char)(jsonString[i] ^ keyword[i % keyword.Length]);
//    //    }

//    //    return result;
//    //} 
//}
