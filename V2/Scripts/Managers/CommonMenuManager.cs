using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(SceneLoader))]
public class CommonMenuManager : MonoBehaviour
{

    [SerializeField] private Button Backbutton;

    private SFXManager _sfx_manager;
    void Awake()
    {
        _sfx_manager =SFXManager.Instance;
        OverRideBackButtonScene("Map");
    }


    public void SoundOnOff(bool _sound_on)
    {
        if (!_sound_on)
        {
            _sfx_manager.SoundOff();
        }
        else
        {
            _sfx_manager.SoundOn();
        }
        
    }

    public void OverRideBackButtonScene(string secenName)
    {
        SceneLoader sceneLoader = GetComponent<SceneLoader>();
        Backbutton.onClick.RemoveAllListeners();
        Backbutton.onClick.AddListener(()=>sceneLoader.LoadSceneAsSingle(secenName));
    }

    

}
