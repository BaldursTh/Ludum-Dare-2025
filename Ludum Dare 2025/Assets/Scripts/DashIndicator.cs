using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.UI;

public class DashIndicator : MonoBehaviour
{
    [SerializeField] PlayerMovement player;
    [SerializeField] GameObject indicator1, indicator2, indicator3;

    // Update is called once per frame
    void Update()
    {
        indicator1.SetActive(player.CurrentDashes > 0);
        indicator2.SetActive(player.CurrentDashes > 1);
        indicator3.SetActive(player.CurrentDashes > 2);
    }
}
