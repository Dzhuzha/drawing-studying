using System.Collections.Generic;
using UnityEngine;

public class VoiceoverPlayer : MonoBehaviour
{
    [SerializeField] private AudioSource _audioPlayer;
    [SerializeField] private LevelConfig _levelConfig;

    private List<AudioClip> _motivationSounds = new List<AudioClip>();
    private AudioClip _firstPhraseSound;
    private float _rulesTimer;

    public void SubscribeToLevelComplete(SpellChecker spellChecker)
    {
        spellChecker.SymbolCompleted += PlayRandomMotivationPhrase;
    }

    private void Update()
    {
        CheckPlayerActivity();
    }

    private void CheckPlayerActivity()
    {
        RunTimer();

        if (_rulesTimer > 0) return;

        _rulesTimer = _levelConfig.TimeToRepeatRules;
        PlayLevelRulesPhrase();
    }

    private void RunTimer()
    {
        if (Input.anyKey)
        {
            _rulesTimer = _levelConfig.TimeToRepeatRules;
        }
        else
        {
            _rulesTimer -= Time.deltaTime;
        }
    }

    private void Start()
    {
        _firstPhraseSound = _levelConfig.StartPhrase;
        _motivationSounds = _levelConfig.MotivationPhrases;
    }

    private void PlayLevelRulesPhrase()
    {
        _audioPlayer.PlayOneShot(_firstPhraseSound);
    }

    private void PlayRandomMotivationPhrase(SpellChecker spellChecker)
    {
        spellChecker.SymbolCompleted -= PlayRandomMotivationPhrase;
        int soundIndex = Random.Range(0, _motivationSounds.Count);
        _audioPlayer.PlayOneShot(_motivationSounds[soundIndex]);
    }
}