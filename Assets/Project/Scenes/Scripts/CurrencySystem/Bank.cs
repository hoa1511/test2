using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Bank : MonoBehaviour
{
    [SerializeField] private int startingBalance = 0;
    [SerializeField] private int currentBalance;
    [SerializeField] private TextMeshProUGUI displayBalance;
    public static Bank Instance;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }    
        else
        {
            Destroy(this);
        }
    }
    private void Start()
    {
        currentBalance = startingBalance;
        
    }

    private void Update()
    {
        UpdateDisplayCoin();
    }

    public void Deposit(int amount)
    {
        currentBalance += amount;
    }

    public void Withdraw(int amount)
    {
        currentBalance -= amount;
        if(currentBalance < 0)
        {
            currentBalance = 0;
            Debug.Log("You don't have enough coin");
        }
    }

    public void UpdateDisplayCoin()
    {
        displayBalance.text = currentBalance.ToString();
    }
}
