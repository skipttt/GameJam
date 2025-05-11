using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private AudioSource _audio;

    private void Awake()
    {
        _audio = GetComponent<AudioSource>();
    }

    public void Pausar()
    {
        _audio?.Pause();
    }

    public void Reanudar()
    {
        _audio?.UnPause();
    }
}
