using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class CannonController : MonoBehaviour
{
    public enum CANNON_MODE
    {
        REST = 1,
        ANGLE,
        POWER,
        FIRE
    }

    public InputAction switchMode;
    public GameObject cannonPivot;
    public Transform firePoint;
    public GameObject playerPrefab;
    public ShakeBehavior shakeScreen;
    public float angleIncrement = 0.25f;
    [Header("Cannon Levels")]
    private int cannonFirePowerLevel;
    public int CannonFirePowerLevel 
    {
        get { return cannonFirePowerLevel; }
        set { cannonFirePowerLevel = value; }
    }
    [Header("'Updgradeable' values")]
    public float firePower = 250f;
    private CANNON_MODE mode = CANNON_MODE.ANGLE;
    private bool up = true;

    void Start()
    {
        StartCoroutine(PivotCannon());
        switchMode.performed += OnSwitchMode;
        switchMode.canceled += GarbageAction;
    }

    void OnEnable()
    {
        switchMode.Enable();
    }

    void OnDisable()
    {
        switchMode.Disable();
    }

    void OnSwitchMode(InputAction.CallbackContext ctx)
    {
        var isPressed = ctx.performed;
        
        if(isPressed)
            mode += 1;

        if((int)mode == 5)
            mode = CANNON_MODE.ANGLE;

        // Check cannon mode and start appropriate coroutine
        switch (mode)
        {
            case CANNON_MODE.ANGLE:
                StartCoroutine(PivotCannon());
                break;
            case CANNON_MODE.POWER:
                StartCoroutine(AdjustPower());
                break;
            case CANNON_MODE.FIRE:
                FireCannon();
                break;
            default:
                break;
        }

        //Debug.Log(mode);
    }

    void GarbageAction(InputAction.CallbackContext ctx)
    {

    }

    #region CANNON UPGRADE METHODS

    public void IncreaseFirePower()
    {
        firePower += 10f;
    }

    #endregion

    #region CANNON MODE HANDLERS

    IEnumerator PivotCannon()
    {
        while(mode == CANNON_MODE.ANGLE)
        {
            if(up)
            {
                cannonPivot.transform.Rotate(0, 0, angleIncrement);
                if (cannonPivot.transform.eulerAngles.z >= 70) up = false;
            }
            else if (up == false)
            {
                cannonPivot.transform.Rotate(0, 0, -angleIncrement);
                if (cannonPivot.transform.eulerAngles.z <= 19) up = true;
            }

            yield return new WaitForFixedUpdate();
        }
    }

    IEnumerator AdjustPower()
    {
        while(mode == CANNON_MODE.POWER) 
        {
            //Debug.Log("adjust power");
            yield return new WaitForSeconds(1f);
        }
    }

    void FireCannon()
    {
        //Debug.Log("spawn");
        var tempChar = Instantiate(playerPrefab, firePoint.position, cannonPivot.transform.rotation);
        PlayerCharacterController player = tempChar.GetComponent<PlayerCharacterController>();
        shakeScreen.TriggerShake();

        // cache the angle of the cannon to apply as the Y-direction force
        float zAngle = cannonPivot.transform.eulerAngles.z;
        Vector3 dir = Quaternion.AngleAxis(zAngle, Vector3.forward) * Vector3.right;
        //Debug.Log($"dir :: {dir}");
        player.PlayerRigidBody.AddForce(dir*firePower);
    }

    #endregion
}
