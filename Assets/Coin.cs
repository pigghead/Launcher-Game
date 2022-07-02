using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    Collider2D coinCollider;
    ScoreManager sm;
    void Awake()
    {
        coinCollider = GetComponent<Collider2D>();
        sm = GameObject.FindGameObjectWithTag("ScoreManager").GetComponent<ScoreManager>();
    }

    void OnTriggerEnter2D()
    {
        sm.UpdateCoinsCollected();
        Destroy(this.gameObject);
    }
}
