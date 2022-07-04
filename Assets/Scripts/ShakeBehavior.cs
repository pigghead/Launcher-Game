using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeBehavior : MonoBehaviour
{
    private Transform transform;
    private float shakeDuration = 0f;
    private float shakeMagnitude = 0.05f;
    private float dampingSpeed = 1.75f;
    private Vector3 initialPosition;

    void Awake()
    {
        if(transform == null)
            transform = gameObject.GetComponent(typeof(Transform)) as Transform;
    }

    void Update()
    {
        if(shakeDuration > 0)
        {
            transform.localPosition = initialPosition + Random.insideUnitSphere * shakeMagnitude;
            shakeDuration -= Time.deltaTime * dampingSpeed;
        }
        else
        {
            shakeDuration = 0f;
            transform.localPosition = initialPosition;
            transform.position = new Vector3(transform.position.x, 0, transform.position.z);
        }

        StayPositive();
    }

    void OnEnable()
    {
        initialPosition = transform.position;
    }

    void StayPositive()
    {
        float x = transform.position.x < 0 ? 0 : transform.position.x;
        transform.eulerAngles = Vector3.zero;
        transform.position = new Vector3(x, 0, transform.position.z);
    }

    public void TriggerShake()
    {
        //Debug.Log("Shaking screen");
        shakeDuration = 0.8f;
    }

}
