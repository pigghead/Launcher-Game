using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI distanceTravelled;
    public TextMeshProUGUI coinsCollected;
    private int totalCoins = 0;
    
    public void UpdateScore(float score)
    {
        distanceTravelled.text = $"Distance: {score}m";
    }

    public void UpdateCoinsCollected()
    {
        totalCoins += 1;
        coinsCollected.text = $"x{totalCoins}";

    }
}
