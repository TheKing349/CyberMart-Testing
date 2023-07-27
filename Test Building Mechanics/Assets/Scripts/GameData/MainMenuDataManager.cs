using Newtonsoft.Json;
using Newtonsoft.Json.UnityConverters;
using Newtonsoft.Json.UnityConverters.Math;
using System.IO;
using UnityEngine;

public class MainMenuDataManager : MonoBehaviour
{
    public SettingsDataHandler settingsDataHandlerScript;

    private JsonSerializerSettings serializerSettings;

    [HideInInspector] public string settingsDataFilePath = "";
    [HideInInspector] public string settingsDirectoryPath = "";

    private void Awake()
    {
        serializerSettings = new JsonSerializerSettings
        {
            Converters = new[]
            {
                new Vector3Converter(),
            },
            ContractResolver = new UnityTypeContractResolver(),
        };

        settingsDirectoryPath = Application.persistentDataPath + Path.DirectorySeparatorChar + "Settings" + Path.DirectorySeparatorChar;
        settingsDataFilePath = settingsDirectoryPath + "settings.json";
        CreateDirectoryAndFile(settingsDirectoryPath, settingsDataFilePath);
    }

    private void Start()
    {
        if (File.ReadAllText(settingsDataFilePath) != "")
        {
            settingsDataHandlerScript.LoadSettings();
        }
    }
    public void WriteData()
    {
        string settingsJson = JsonConvert.SerializeObject(settingsDataHandlerScript.settingsData, Formatting.Indented, serializerSettings);
        File.WriteAllText(settingsDataFilePath, settingsJson);
    }

    public void ReadData()
    {
        settingsDataHandlerScript.settingsData = JsonConvert.DeserializeObject<SettingsData>(File.ReadAllText(settingsDataFilePath), serializerSettings);
    }

    public void CreateDirectoryAndFile(string directoryPath, string filePath)
    {
        if ((!File.Exists(filePath) && !Directory.Exists(directoryPath)))
        {
            Directory.CreateDirectory(directoryPath);

            File.Create(filePath);
        }
        else if (!File.Exists(filePath))
        {
            File.Create(filePath);
        }
    }
}
