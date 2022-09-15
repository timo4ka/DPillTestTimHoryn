using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyUI : MonoBehaviour
{
    [SerializeField] private Text text;

    private void OnEnable()
    {
        EventManager.Subscribe(eEventType.UpdateMoney, (act) => SetMoney((int)act));
    }

    private void SetMoney(int hp)
    {
        text.text = hp.ToString();
    }
}
