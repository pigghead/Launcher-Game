using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class OpenShop : MonoBehaviour
{
    public InputAction openShop;
    public GameObject shopUi;
    public Button firePowerBttn;
    public CannonController cannon;

    void Awake()
    {
        openShop.performed += OnOpenShop;

        firePowerBttn.onClick.AddListener(UpgradeFirePower);
    }

    #region ShopButton Methods

    void UpgradeFirePower()
    {
        cannon.IncreaseFirePower();
    }

    #endregion

    #region OnOpenShop

    void OnOpenShop(InputAction.CallbackContext ctx)
    {
        var isPressed = ctx.performed;
        bool shopActivated = shopUi.activeSelf;

        if(isPressed)
        {
            shopUi.SetActive(!shopActivated);
            if(shopActivated)
                Time.timeScale = 1f;
            else   
                Time.timeScale = 0f;
        }
    }

    #endregion
    
    #region Enable/Disable

    void OnEnable()
    {
        openShop.Enable();
    }

    void OnDisable()
    {
        openShop.Disable();
    }

    #endregion
}
