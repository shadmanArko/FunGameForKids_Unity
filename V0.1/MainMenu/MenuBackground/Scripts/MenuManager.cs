using UnityEngine;

public class MenuManager : MonoBehaviour
{
    private void Awake()
    {
        FindObjectOfType<SceneLoader>().loadSceneAsAdditive("MainMenu");
    }
}
