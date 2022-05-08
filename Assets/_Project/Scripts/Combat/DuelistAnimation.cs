using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class DuelistAnimation : MonoBehaviour
{
    [SerializeField] private Image _leftEye;
    [SerializeField] private Image _rightEye;
    [SerializeField] private Image _nose;
    [SerializeField] private Image _mouth;
    [SerializeField] private Image _body;
    [SerializeField] private float _shakeDuration;
    [SerializeField] private float _shakeStrength;

    private DuelistController _duelistController;

    private void Awake()
    {
        _duelistController = GetComponentInParent<DuelistController>();
        GetComponent<RectTransform>().DOScaleY(1.025f, 1f).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo);
    }

    private void OnEnable()
    {
        _duelistController.OnTakeDamage += ShakeCharacter;
    }

    private void OnDisable()
    {
        _duelistController.OnTakeDamage -= ShakeCharacter;
    }

    public void SetVisualFeatures(Sprite eye, Sprite nose, Sprite mouth, Sprite body)
    {
        _leftEye.sprite = eye;
        _rightEye.sprite = eye;
        _nose.sprite = nose;
        _mouth.sprite = mouth;
        _body.sprite = body;
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
}
