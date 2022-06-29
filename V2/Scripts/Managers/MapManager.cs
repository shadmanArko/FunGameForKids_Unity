using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(SceneLoader))]
public class MapManager : MonoBehaviour
{
 
    void Start()
    {
        SceneManager.sceneLoaded += SceneManager_sceneLoaded;
        GetComponent<SceneLoader>().loadSceneAsAdditive("CommonMenu");
        SFXManager.Instance.PlayBGM(0);
    }

    private void SceneManager_sceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        FindObjectOfType<CommonMenuManager>()?.OverRideBackButtonScene("MainMenu");
        SceneManager.sceneLoaded -= SceneManager_sceneLoaded;
    }
}
