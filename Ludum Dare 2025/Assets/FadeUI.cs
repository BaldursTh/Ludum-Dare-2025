using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeUI : MonoBehaviour
{
    public Animator anim;
    public Animator DeathScreen;

    public void EndGame()
    {
        anim.SetBool("FadeOut", true);
        DeathScreen.SetBool("FadeOut",true);
    }
}
