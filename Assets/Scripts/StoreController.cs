using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreController : MonoBehaviour
{
    public float currentMoney;
    public Transform stockSpawnPoint;


    // Start is called before the first frame update
    void Start()
    {
        UIController.Instance.UpdateMoney(currentMoney);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AddMoney(float amountToAdd)
    {
        currentMoney += amountToAdd;
        UIController.Instance.UpdateMoney(currentMoney);

    }

    public void SpendMoney(float amountToSpend)
    {
        currentMoney -= amountToSpend;

        if (currentMoney < 0)
        {
            currentMoney = 0;
        }

        UIController.Instance.UpdateMoney(currentMoney);
    }

    public bool CheckMoneyAvailable(float amountToCheck)
    {
        if (currentMoney >= amountToCheck)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
