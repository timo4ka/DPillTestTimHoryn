using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ContonueButton : BaseButton
{
    protected override void OnClick()
    {
        if(SceneManager.GetActiveScene().buildIndex==1)
        {
            SceneManager.LoadScene(0);
            return;
        }
        SceneManager.LoadScene(1);
    }
}
