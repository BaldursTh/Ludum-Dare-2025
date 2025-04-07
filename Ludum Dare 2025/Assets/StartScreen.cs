using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StartScreen : MonoBehaviour
{
    public UnityEvent TriggerGameStart;

    public void StartAnimation()
    {
        GetComponent<Animator>().SetBool("Started", true);
    }

    public void StartGame()
    {
        TriggerGameStart.Invoke();
    }
}
