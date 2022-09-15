using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollector : MonoBehaviour
{
    [SerializeField] private Transform startPoint;
    [SerializeField] private Stack<Money> colection = new Stack<Money>();
    [SerializeField] private float plyerZborder = -1.35f;
    private int money = 0;
    private string monewyNameInPrefs = "money";
    private void OnEnable()
    {
        money = PlayerPrefs.GetInt(monewyNameInPrefs);
       Invoke("updateUi", 0.1f);
    }
    private void updateUi()
    {
        EventManager.OnEvent(eEventType.UpdateMoney, money);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<Money>(out Money moneyGo))
        {
            if (colection.Count > 0)
            {
                moneyGo.transform.parent = colection.Peek().transform.GetChild(0);
            }
            else
            {
                moneyGo.transform.parent = startPoint;
            }
            colection.Push(moneyGo);

            moneyGo.transform.position = moneyGo.transform.parent.position;

        }
    }

    private void FixedUpdate()
    {
        if (transform.position.z < plyerZborder && colection.Count > 0)
        {
            money += colection.Peek().Count;
            Destroy(colection.Pop().gameObject);
            updateUi();
        }
        PlayerPrefs.SetInt(monewyNameInPrefs, money);
    }

}
