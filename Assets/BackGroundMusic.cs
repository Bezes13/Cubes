using System.Collections;
using System.Collections.Generic;
using Signals;
using UnityEngine;

public class BackGroundMusic : MonoBehaviour
{
    [SerializeField] private AudioClip inGame;
    [SerializeField] private AudioClip menu;
    private AudioSource _audioSource;
    private bool _inGame;
    
    
    void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        Supyrb.Signals.Get<StartGameSignal>().AddListener(StartGameMusic);
        Supyrb.Signals.Get<PlayerDeadSignal>().AddListener(ChangeMusic);
    }

    private void ChangeMusic()
    {
        _inGame = false;
        StartCoroutine(StartMenuMusic());
    }

    private IEnumerator StartMenuMusic()
    {
        _audioSource.Stop();
        yield return new WaitForSeconds(2);
        if (!_inGame)
        {
            _audioSource.clip = menu;
            _audioSource.Play();     
        }
    }

    private void StartGameMusic()
    {
        _inGame = true;
        _audioSource.clip = inGame;
        _audioSource.Play();
    }
}
