using UnityEngine;

public class VolumeValueChange : MonoBehaviour
{
  
    public void SetVolume(float vol)
    {
        AudioListener.volume = vol;
    }
}
