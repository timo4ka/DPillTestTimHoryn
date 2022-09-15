using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UIWindow : MonoBehaviour
{
    [field: SerializeField] public eUIWindowType WindowType { get; private set; }

    public void Show() => gameObject.SetActive(true);
    public void Hide() => gameObject.SetActive(false);
}

