using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
public class ScoreCounter : MonoBehaviour
{
    [SerializeField] private TMP_Text score;

    // Update is called once per frame
    void Update()
    {
        score.text = $"{GameManager.instance.Score}m";
    }
}
