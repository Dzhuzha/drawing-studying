using System.Collections.Generic;
using UnityEngine;

public class VoiceoverPlayer : MonoBehaviour
{
    [SerializeField] private AudioSource _audioPlayer;
    
    private List<AudioClip> _motivationSounds = new List<AudioClip>();
    private AudioClip _firstPhraseSound;
    
    public void Init(List<AudioClip> motivationSounds, AudioClip firstPhraseSound)
    {
        _firstPhraseSound = firstPhraseSound;
        _motivationSounds = motivationSounds;
    }

    public void PlayLevelRulesPhrase()
    {
        _audioPlayer.PlayOneShot(_firstPhraseSound);
    }

    public void PlayRandomMotivationPhrase()
    {
        int soundIndex = Random.Range(0, _motivationSounds.Count);
        _audioPlayer.PlayOneShot(_motivationSounds[soundIndex]);
    }
}