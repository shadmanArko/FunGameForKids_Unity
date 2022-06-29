using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    public RectTransform mainMenu, settingsMenu, leaderboardMenu;
    public GameObject setting_panel;
    [SerializeField]
    private Sprite[] _sound_btn_back;
    [SerializeField]
    private Image _sound_btn;
    [SerializeField]
    private Slider _slider;
    private bool _sound_on = true;
    private SFXManager _sfx_manager;
    void Start()
    {
        mainMenu.DOAnchorPos(Vector2.zero, 3f);
        _sfx_manager = SFXManager.Instance;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    public void settingsButton()
    {
        mainMenu.DOAnchorPos(new Vector2(-2000, 0), 3f);
        settingsMenu.DOAnchorPos(new Vector2(0,0), 3f);
    }
    public void closesettingsButton()
    {
        mainMenu.DOAnchorPos(new Vector2(0, 0), 3f);
        settingsMenu.DOAnchorPos(new Vector2(0,2000), 3f);
    }
    public void leaderBoardButton()
    {
        mainMenu.DOAnchorPos(new Vector2(-2000, 0), 3f);
        leaderboardMenu.DOAnchorPos(new Vector2(0, 0), 3f).SetDelay(0.3f);
    }
    public void closeleaderBoardButton()
    {
        mainMenu.DOAnchorPos(new Vector2(0, 0), 3f);
        leaderboardMenu.DOAnchorPos(new Vector2(0, -2000), 3f).SetDelay(0.3f);
    }
    public void open_settings_panel()
    {
        setting_panel.SetActive(true);
        _slider.value = _sfx_manager.GetVolume();
        if (_sfx_manager.GetVolume() == -40f)
        {
            _sound_on = false;
            _sound_btn.sprite = _sound_btn_back[1];
        }
        else
        {
            _sound_on = true;
            _sound_btn.sprite = _sound_btn_back[0];
        }
    }
    public void close_settings_panel()
    {
        setting_panel.SetActive(false);
    }
    public void OnVolumeChange(Slider slider)
    {
        float volume = slider.value;
        _sfx_manager.VolumeChange(volume);
    }
    public void SoundOnOff()
    {
        if (_sound_on)
        {
            _sound_on = false;
            _sound_btn.sprite = _sound_btn_back[1];
            _sfx_manager.SoundOff();
            _slider.value = _sfx_manager.GetVolume();
        }
        else
        {
            _sound_on = true;
            _sound_btn.sprite = _sound_btn_back[0];
            _sfx_manager.SoundOn();
            _slider.value = _sfx_manager.GetVolume();
        }

    }
}
