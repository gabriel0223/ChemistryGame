using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class SpaceshipController : MonoBehaviour
{
    [SerializeField] private RectTransform _rotationOrigin;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private Vector2 _ellipseRadius;

    //ordered from straight right by clockwise rotation
    [SerializeField] private Sprite[] _spaceshipSprites;

    private float _currentAngle;
    private RectTransform _rectTransform;
    private Image _image;

    private void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
        _image = GetComponent<Image>();
    }

    private void Update()
    {
        _currentAngle += _rotationSpeed * Time.deltaTime;
        float posX = _rotationOrigin.anchoredPosition.x + Mathf.Cos(_currentAngle) * _ellipseRadius.x;
        float posY = _rotationOrigin.anchoredPosition.y + Mathf.Sin(_currentAngle) * _ellipseRadius.y;

        _rectTransform.anchoredPosition = new Vector3(posX, posY);

        Vector2 dir = (_rectTransform.anchoredPosition - _rotationOrigin.anchoredPosition).normalized;
        float angle = 180 + Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        Debug.Log(angle);
        
        //transform.rotation 
        
        //UpdateSpaceshipSprite(angle);
    }

    private void UpdateSpaceshipSprite(float angle)
    {
        if (angle > 5 && angle < 40)
        {
            _image.sprite = _spaceshipSprites[1]; 
        }
        else if (angle > 40 && angle < 140)
        {
            _image.sprite = _spaceshipSprites[2]; 
        }
        else if (angle > 140 && angle < 170)
        {
            _image.sprite = _spaceshipSprites[3]; 
        }
        else if (angle > 170 && angle < 190)
        {
            _image.sprite = _spaceshipSprites[4]; 
        }
        else if (angle > 190 && angle < 225)
        {
            _image.sprite = _spaceshipSprites[5]; 
        }
        else if (angle > 225 && angle < 310)
        {
            _image.sprite = _spaceshipSprites[6]; 
        }
        else if (angle > 310 && angle < 345)
        {
            _image.sprite = _spaceshipSprites[7]; 
        }
        else
        {
            _image.sprite = _spaceshipSprites[0];
        }
    }
}
