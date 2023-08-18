using UnityEngine;
using ModTool;
using System.IO;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class ModHandler : MonoBehaviour
{
    public GameObject modMenuCanvas;
    public GameObject exampleModGameObject;
    public GameObject modScrollViewContent;
    public GameObject modErrorPrompt;

    private string modsFolderString;

    private void Start()
    {
        modsFolderString = Application.persistentDataPath + Path.DirectorySeparatorChar + "Mods";
        if (!Directory.Exists(modsFolderString))
        {
            Directory.CreateDirectory(modsFolderString);
        }
        ModManager.RemoveSearchDirectory(ModManager.defaultSearchDirectory);
        ModManager.AddSearchDirectory(modsFolderString);

        ModManager.ModFound += InstantiateModGameObject;
        ModManager.ModRemoved += DestroyModGameObject;
        ModManager.ModLoaded += LoadModContents;
    }

    #region ModGameObjectInstantiation
    public void InstantiateModGameObject(Mod currentMod)
    {
        GameObject currentModGameObject = Instantiate(exampleModGameObject, modScrollViewContent.transform);
        currentModGameObject.name = currentMod.name;

        currentModGameObject.GetComponentInChildren<Toggle>().isOn = currentMod.isEnabled;

        if (currentMod.isEnabled)
        {
            currentMod.Load();
            LoadModContents(currentMod);

            TMP_Text[] allTexts = currentModGameObject.GetComponentsInChildren<TMP_Text>();
            foreach (TMP_Text text in allTexts)
            {
                if (text.name == "ModNameText")
                {
                    text.text = currentMod.modInfo.name;
                }
                else if (text.name == "ContentTypeText")
                {
                    text.text = currentMod.modInfo.content.ToString();
                }
                else if (text.name == "AuthorNameText")
                {
                    text.text = "By: " + currentMod.modInfo.author;
                }
                else if (text.name == "VersionNumberText")
                {
                    text.text = "Version: " + currentMod.modInfo.version;
                }
            }
        }
    }

    public void DestroyModGameObject(Mod currentMod)
    {
        Destroy(GameObject.Find(currentMod.name));
    }

    public void LoadModContents(Mod currentMod)
    {
        //"Scenes" refers to a different map that is included in a different scene
        if ((currentMod.contentType.ToString() == "Scene") || (currentMod.contentType.ToString() == "Scenes"))
        {
            //SceneManager.LoadScene(currentMod.sceneNames[0]);
        }
        //"Asset" refers to a prefab to be loaded into a list(to build)
        else if ((currentMod.contentType.ToString() == "Asset") || (currentMod.contentType.ToString() == "Assets"))
        {
            foreach (GameObject prefab in currentMod.prefabs)
            {
                //prefabListScript.prefabsList.Add(prefab);
            }
        }
        //"Code" refers to a mod that requires scripting
        else if (currentMod.contentType.ToString() == "Code")
        {

        }
        else
        //Error loading the mod
        {
            ModError(currentMod);
        }
    }

    public void ModError(Mod currentMod)
    {
        modErrorPrompt.SetActive(true);
        TMP_Text errorText = modErrorPrompt.GetComponentInChildren<TMP_Text>();
        errorText.text.Replace("MOD_NAME", currentMod.modInfo.name);
        errorText.text.Replace("AUTHOR_NAME", currentMod.modInfo.author);
        currentMod.Unload();
    }

    public void RemoveModContents(Mod currentMod)
    {
        //"Scenes" refers to a different map that is included in a different scene
        if ((currentMod.contentType.ToString() == "Scene") || (currentMod.contentType.ToString() == "Scenes"))
        {
            SceneManager.LoadScene(0);
        }
        //"Asset" refers to a prefab to be loaded into a list(to build)
        else if ((currentMod.contentType.ToString() == "Asset") || (currentMod.contentType.ToString() == "Assets"))
        {
            foreach (GameObject prefab in currentMod.prefabs)
            {
                //prefabListScript.prefabsList.Remove(prefab);
            }
        }
        //"Code" refers to a mod that requires scripting
        else if (currentMod.contentType.ToString() == "Code")
        {

        }
        //Error loading the mod
        else
        {
            ModError(currentMod);
        }

    }
    #endregion

    #region ModLoader
    public void LoadSelectedMods()
    {
        GameObject[] modGameObjects = GameObject.FindGameObjectsWithTag("ModGameObject");

        for (int i = 0; i < modGameObjects.Length; i++)
        {
            Mod currentMod = ModManager.mods[i];
            if (modGameObjects[i].GetComponentInChildren<Toggle>().isOn)
            {
                currentMod.isEnabled = true;
                currentMod.Load();
            }
            else
            {
                RemoveModContents(currentMod);
                currentMod.isEnabled = false;
                currentMod.Unload();
            }
        }
    }
    #endregion

    public void RefreshMods()
    {
        ModManager.Refresh();
    }
}