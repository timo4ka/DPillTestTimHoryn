using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiHP : MonoBehaviour
{
    [SerializeField] private Text text;
    [SerializeField] private Image bar;

    private void OnEnable()
    {
        EventManager.Subscribe(eEventType.PlayerDamag, (act) => SetHP((int)act));
    }

    private void SetHP(int hp)
    {
        text.text = hp.ToString();
        bar.fillAmount = hp/100f;
    }
}
