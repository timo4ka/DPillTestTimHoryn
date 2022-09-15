using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private eUIWindowType _defaultWindow;

    private Dictionary<eUIWindowType, UIWindow> _windows = new Dictionary<eUIWindowType, UIWindow>();
    private UIWindow _currentWindow;

    private void OnEnable()
    {
        InitWIndows();
        ShowWindow(_defaultWindow);


        EventManager.Subscribe(eEventType.LevelStart, (arg) => ShowWindow(eUIWindowType.Game));
        EventManager.Subscribe(eEventType.LevelLost, (arg) => ShowWindow(eUIWindowType.Lose));
        EventManager.Subscribe(eEventType.LevelComplete, (arg) => ShowWindow(eUIWindowType.Win));

    }


    private void ShowWindow(eUIWindowType windowType)
    {
        if (_currentWindow != null) _currentWindow.Hide();
        if (_windows.TryGetValue(windowType, out _currentWindow))
        {
            _currentWindow.Show();
        }
    }

    private void InitWIndows()
    {
        foreach (Transform child in transform)
        {
            if (child.TryGetComponent(out UIWindow window))
            {
                _windows.Add(window.WindowType, window);
            }
        }
    }




    private void OnDisable()
    {
        EventManager.Unsubscribe(eEventType.LevelStart, (arg) => ShowWindow(eUIWindowType.Game));
        EventManager.Unsubscribe(eEventType.LevelLost, (arg) => ShowWindow(eUIWindowType.Lose));
        EventManager.Unsubscribe(eEventType.LevelComplete, (arg) => ShowWindow(eUIWindowType.Win));

    }


}

