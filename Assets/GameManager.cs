using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Security.Cryptography.X509Certificates;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private int[] locksStartNumbers = new int[3];
    private int[] locks = new int[3];
    [SerializeField]
    private TMP_Text[] lockLabels = new TMP_Text[3];

    [SerializeField]
    private float maxTime = 60;
    [SerializeField]
    private TMP_Text timerText;
    private float timeLeft;

    [SerializeField]
    private int unlockNumber = 5;

    [SerializeField]
    private int[] drillPower = { 0, -2, 1 };
    [SerializeField]
    private int[] hammerPower = { 2, -1, -1 };
    [SerializeField]
    private int[] lockpickPower = { -1, 2, 0 };

    [SerializeField]
    private GameObject loseWindow;
    [SerializeField]
    private GameObject winWindow;

    private void Start()
    {
        Restart();
    }

    // Update is called once per frame
    private void Update()
    {
        timeLeft-= Time.deltaTime;
        timerText.text = Mathf.Round(timeLeft).ToString();

        if(timeLeft <= 0)
        {
            Time.timeScale = 0;
            loseWindow.SetActive(true);
        }
    }
    
    public void UseDrillButton()
    {
        for (int i = 0; i < locks.Length; i++)
        {
            locks[i] += drillPower[i];
            if (locks[i] < 0)
                locks[i] = 0;
            else if (locks[i] > 10)
                locks[i] = 10;
        }
        UpdateLockLabels();
    }

    public void UseHammerButton()
    {
        for (int i = 0; i < locks.Length; i++)
        {
            locks[i] += hammerPower[i];
            if (locks[i] < 0)
                locks[i] = 0;
            else if (locks[i] > 10)
                locks[i] = 10;
        }
        UpdateLockLabels();
    }

    public void UseLockpickButton()
    {
        for (int i = 0; i < locks.Length; i++)
        {
            locks[i] += lockpickPower[i];

            if (locks[i] < 0)
                locks[i] = 0;
            else if (locks[i] > 10)
                locks[i] = 10;
        }
        UpdateLockLabels();
    }

    public void UpdateLockLabels()
    {
        for(int i = 0; i< locks.Length; i++)
            lockLabels[i].text = locks[i].ToString();
        CheckWin();
    }

    public void CheckWin() 
    {
        foreach(int num in locks)
        {
            if(num != unlockNumber)
            {
                return;
            }
        }

        Time.timeScale = 0;
        winWindow.SetActive(true);
    }

    public void Restart()
    {
        Time.timeScale = 1;
        timeLeft = maxTime;

        loseWindow.SetActive(false);
        winWindow.SetActive(false);

        for (int i = 0; i < locks.Length; i++)
        {
            locks[i] = locksStartNumbers[i];
        }
        UpdateLockLabels();

    }
}
