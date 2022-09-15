using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Money : MonoBehaviour
{
    public int Count => count;
    private int count;

    private void OnEnable()
    {
        count = Random.Range(1, 10);
    }
}
