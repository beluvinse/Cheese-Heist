using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    AudioSource _audioSource;

    [SerializeField] AudioClip chetoSound, trapSound, buttonSound, meowSound, mouseSound;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    private void OnEnable()
    {
        Cheto.OnChetoCollected += PlayChetoSound;
        TrapActiveState.OnTrapActive += PlayTrapSound;
    }

    private void OnDisable()
    {
        Cheto.OnChetoCollected -= PlayChetoSound;
        TrapActiveState.OnTrapActive -= PlayTrapSound;
    }


    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }


    public void PlayChetoSound()
    {
        _audioSource.PlayOneShot(chetoSound);
    }

    public void PlayTrapSound()
    {
        _audioSource.PlayOneShot(trapSound);
    }

    public void PlayButtonSound()
    {
        _audioSource.PlayOneShot(buttonSound);
    }

    public void PlayMeowSound()
    {
        _audioSource.PlayOneShot(meowSound);
    }

    public void PlayMouseSound()
    {
        _audioSource.PlayOneShot(mouseSound);

    }
}
