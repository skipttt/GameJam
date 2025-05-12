using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private AudioSource _audio;
    private bool fuePausado = false;

    private void Awake()
    {
        _audio = GetComponent<AudioSource>();
    }

    public void Pausar()
    {
        if (_audio.isPlaying)
        {
            _audio.Pause();
            fuePausado = true;
        }
    }

    public void Reanudar()
    {
        if (fuePausado)
        {
            _audio.UnPause();
            fuePausado = false;
        }
    }
}
