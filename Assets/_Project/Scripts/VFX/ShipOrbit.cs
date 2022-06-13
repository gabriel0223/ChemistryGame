using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class ShipOrbit : MonoBehaviour
{
    [SerializeField] private float _rotationSpeed;

    private float _currentRotation;

    private void Update()
    {
        _currentRotation += _rotationSpeed * Time.deltaTime;
        
        transform.localRotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, _currentRotation);
    }
}
