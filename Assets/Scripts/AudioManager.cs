using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    AudioSource _audioSource;

    [SerializeField] AudioClip chetoSound;

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
    }

    private void OnDisable()
    {
        Cheto.OnChetoCollected -= PlayChetoSound;
    }


    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }


    public void PlayChetoSound()
    {
        _audioSource.PlayOneShot(chetoSound);
    }
}
