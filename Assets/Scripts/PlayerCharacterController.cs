using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerCharacterController : MonoBehaviour
{
    Rigidbody2D rb;
    public Rigidbody2D PlayerRigidBody { get => rb; private set => rb = value; }
    public float scoreLine;
    private ScoreManager sm;
    public float distanceTravelled = 0;

    void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        sm = GameObject.FindGameObjectWithTag("ScoreManager").GetComponent<ScoreManager>();
    }

    void Update()
    {
        if (gameObject.transform.position.y <= -5)
        {
            gameObject.transform.position = new Vector3(transform.position.x, -5, 0);
            rb.velocity = Vector2.zero;
        }

        distanceTravelled = Mathf.Floor(gameObject.transform.position.x - scoreLine);
        sm.UpdateScore(distanceTravelled);
        //scoreIndicator.text = $"";
    }
}
