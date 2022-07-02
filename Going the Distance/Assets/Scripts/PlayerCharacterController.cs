using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacterController : MonoBehaviour
{
    Rigidbody2D rb;
    public Rigidbody2D PlayerRigidBody { get => rb; private set => rb = value; }

    void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }
}
