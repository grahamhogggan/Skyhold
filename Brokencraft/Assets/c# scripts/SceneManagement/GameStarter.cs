using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;

public class GameStarter : MonoBehaviour
{
    [SerializeField] private int sceneIndex;
    [SerializeField] private GameObject continueButton;
    [SerializeField] private Overlord.WorldType type;
    public void LoadLoadingScene()
    {
        Overlord.OverwriteMode = false;

        SceneManager.LoadScene(sceneIndex);
    }
    public void LoadLoadingSceneAndReset()
    {
        Overlord.OverwriteMode = true;
        Overlord.type = type;
        SceneManager.LoadScene(sceneIndex);
    }
    public void LoadRegularScene(int buildIndex)
    {
        SceneManager.LoadScene(buildIndex);
    }
    void Start()
    {
        if(continueButton!=null)
        {
        PlayerData player = JsonUtility.FromJson<PlayerData>(File.ReadAllText(Application.persistentDataPath + "/PlayerData.savedata"));
        if(player.health<=0)
        {
            continueButton.SetActive(false);
        }
        }

    }
}
