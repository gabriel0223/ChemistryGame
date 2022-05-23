using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class DuelistAnimation : MonoBehaviour
{
    private enum DuelistMood
    {
        Normal, Happy, Angry
    }

    [SerializeField] private Image _leftEye;
    [SerializeField] private Image _rightEye;
    [SerializeField] private Image _nose;
    [SerializeField] private Image _mouth;
    [SerializeField] private Image _body;
    [SerializeField] private float _shakeDuration;
    [SerializeField] private float _shakeStrength;

    private DuelistController _duelistController;
    private DuelistController _playerDuelistController;
    private DuelistMood _duelistMood;
    private DuelistEyes _duelistEyes;
    private DuelistMouth _duelistMouth;

    private const float MinBlinkDelay = 3f;
    private const float MaxBlinkDelay = 4f;
    private const float ClosedEyesTime = 0.1f;
    private const float HealthReactionThreshold = 0.15f;

    private void Start()
    {
        Initialize();
    }

    private void OnDisable()
    {
        _duelistController.OnTakeDamage -= ReactToDamageTaken;
        _duelistController.OnDie -= ReactToLosing;
        
        _playerDuelistController.OnTakeDamage -= ReactToDamageDone;
        _playerDuelistController.OnDie -= ReactToWinning;
    }

    private void Initialize()
    {
        _playerDuelistController = _duelistController.DuelistOpponent;
        
        GetComponent<RectTransform>().DOScaleY(1.025f, 1f).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo);
        
        StartCoroutine(BlinkingCoroutine());
        
        _duelistController.OnTakeDamage += ReactToDamageTaken;
        _duelistController.OnDie += ReactToLosing;
        
        _playerDuelistController.OnTakeDamage += ReactToDamageDone;
        _playerDuelistController.OnDie += ReactToWinning;
    }

    public void SetDuelistController(DuelistController duelistController)
    {
        _duelistController = duelistController;
    }

    public void SetVisualFeatures(DuelistEyes eyes, Sprite nose, DuelistMouth mouth, Sprite body)
    {
        _duelistEyes = eyes;
        _duelistMouth = mouth;
        
        SetMood(DuelistMood.Normal);
        
        _nose.sprite = nose;
        _body.sprite = body;
    }

    private void SetMood(DuelistMood newMood)
    {
        _duelistMood = newMood;
        Sprite newMouth = GetMoodMouth();
        Sprite newEye = GetMoodEye();
        
        _leftEye.sprite = newEye;
        _rightEye.sprite = newEye;
        _mouth.sprite = newMouth;
    }

    private Sprite GetMoodEye()
    {
        return _duelistMood switch
        {
            DuelistMood.Normal => _duelistEyes.NormalEye,
            DuelistMood.Happy => _duelistEyes.HappyEye,
            DuelistMood.Angry => _duelistEyes.AngryEye,
            _ => throw new ArgumentOutOfRangeException(nameof(_duelistEyes), _duelistEyes, null)
        };
    }
    
    private Sprite GetMoodMouth()
    {
        return _duelistMood switch
        {
            DuelistMood.Normal => _duelistMouth.NormalMouth,
            DuelistMood.Happy => _duelistMouth.HappyMouth,
            DuelistMood.Angry => _duelistMouth.AngryMouth,
            _ => throw new ArgumentOutOfRangeException(nameof(_duelistMouth), _duelistMouth, null)
        };
    }

    private void ReactToDamageTaken()
    {
        ShakeCharacter();

        bool isLifeBelowThreshold = _duelistController.HealthPercentage < (_playerDuelistController.HealthPercentage - HealthReactionThreshold);
        bool isLifeAboveThreshold = _duelistController.HealthPercentage > (_playerDuelistController.HealthPercentage + HealthReactionThreshold);
        
        if (isLifeBelowThreshold)
        {
            SetMood(DuelistMood.Angry);
        }
        else if (!isLifeAboveThreshold)
        {
            SetMood(DuelistMood.Normal);
        }
    }
    
    private void ReactToDamageDone()
    {
        bool isLifeBelowThreshold = _duelistController.HealthPercentage < (_playerDuelistController.HealthPercentage - HealthReactionThreshold);
        bool isLifeAboveThreshold = _duelistController.HealthPercentage > (_playerDuelistController.HealthPercentage + HealthReactionThreshold);
        
        if (isLifeAboveThreshold)
        {
            SetMood(DuelistMood.Happy);
        }
        else if (!isLifeBelowThreshold)
        {
            SetMood(DuelistMood.Normal);
        }
    }

    private void ReactToWinning()
    {
        SetMood(DuelistMood.Happy);
    }

    private void ReactToLosing()
    {
        SetMood(DuelistMood.Angry);
    }

    public void SetNewHue(float newHue)
    {
        Material mat = GetComponent<Image>().material;
        mat.SetFloat("_HsvShift", newHue);
    }

    private void ShakeCharacter()
    {
        transform.DOShakePosition(_shakeDuration, _shakeStrength);
    }

    private IEnumerator BlinkingCoroutine()
    {
        yield return new WaitForSeconds(Random.Range(MinBlinkDelay, MaxBlinkDelay));

        _leftEye.sprite = _duelistEyes.ClosedEye;
        _rightEye.sprite = _duelistEyes.ClosedEye;
        
        yield return new WaitForSeconds(ClosedEyesTime);

        Sprite newEye = GetMoodEye();
        _leftEye.sprite = newEye;
        _rightEye.sprite = newEye;

        StartCoroutine(BlinkingCoroutine());
    }
}
