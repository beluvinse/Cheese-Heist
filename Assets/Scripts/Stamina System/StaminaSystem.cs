using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class StaminaSystem : MonoBehaviour
{
    [SerializeField] int maxStamina = 10;
    [SerializeField] float timeToRecharge = 5f;
    [SerializeField] int currentStamina;

    //[SerializeField] TextMeshProUGUI staminaText = null;
    //[SerializeField] TextMeshProUGUI staminaText2 = null;
    [SerializeField] TextMeshProUGUI timertText = null;
    [SerializeField] TextMeshProUGUI timertText2 = null;

    bool recharging;

    DateTime nextStaminaTime;
    DateTime lastStaminaTime;

    [SerializeField] string notifTitle = "Full Stamina";
    [SerializeField] string notifText = "Tenes la stamina al borde de colapsar, volve al juego";

    int id;

    TimeSpan timer;

    public UIManager uiManager;

    void Awake()
    {
        uiManager = FindObjectOfType<UIManager>();
    }

    void Start()
    {
        Load();
        UpdateTimer();

        nextStaminaTime = StringToDateTime(PlayerPrefs.GetString("nextStaminaTime"));
        lastStaminaTime = StringToDateTime(PlayerPrefs.GetString("lastStaminaTime"));

        Debug.Log(nextStaminaTime);
        Debug.Log(lastStaminaTime);

        StartCoroutine(RechargeStamina());

        if (currentStamina < maxStamina)
        {
            timer = nextStaminaTime - DateTime.Now;
            id = NotificationManager.Instance.DisplayNotification(notifTitle, notifText,
                AddDuration(DateTime.Now, ((maxStamina - (currentStamina) + 1) * timeToRecharge) + 1 + (float)timer.TotalSeconds));
        }
    }

    public bool HasEnoughStamina(int stamina) => currentStamina - stamina >= 0;

    IEnumerator RechargeStamina()
    {
        UpdateTimer();
        recharging = true;

        while (currentStamina < maxStamina)
        {
            DateTime currentTime = DateTime.Now;
            DateTime nextTime = nextStaminaTime;

            bool staminaAdd = false;

            while (currentTime > nextTime)
            {
                if (currentStamina >= maxStamina)
                {
                    UpdateTimer();
                    break;
                }
                

                currentStamina++;
                staminaAdd = true;
                PlayerData.Instance.AddHearts(1);
                UpdateStamina();

                DateTime timeToAdd = nextTime;

                if (lastStaminaTime > nextTime)
                {
                    timeToAdd = lastStaminaTime;
                    Debug.Log("entre al if que corrige las cosas!!!");
                }

                nextTime = AddDuration(timeToAdd, timeToRecharge);
            }

            if (staminaAdd)
            {
                nextStaminaTime = nextTime;
                lastStaminaTime = DateTime.Now;
            }

            UpdateTimer();
            UpdateStamina();
            Save();


            yield return new WaitForEndOfFrame();
        }

        NotificationManager.Instance.CancelNotification(id);
        recharging = false;
    }

    DateTime AddDuration(DateTime date, float duration)
    {
        return date.AddSeconds(duration);
    }

    public void UseStamina(int staminaToUse)
    {
        if (currentStamina - staminaToUse >= 0)
        {
            currentStamina -= staminaToUse;
            UpdateStamina();

            NotificationManager.Instance.CancelNotification(id);
            id = NotificationManager.Instance.DisplayNotification(notifTitle, notifText,
                AddDuration(DateTime.Now, ((maxStamina - (currentStamina) + 1) * timeToRecharge) + 1 + (float)timer.TotalSeconds));

            if (!recharging)
            {
                nextStaminaTime = AddDuration(DateTime.Now, timeToRecharge);
                StartCoroutine(RechargeStamina());
            }

            Debug.Log("Voy al nivel x");
        }
        else
        {
            Debug.Log("No tengo Stamina!");
        }
    }

    void UpdateTimer()
    {
        if (currentStamina >= maxStamina)
        {
            timertText.text = "Full";
            timertText2.text = "Full!";
            return;
        }

        timer = nextStaminaTime - DateTime.Now;

        timertText.text = timer.Minutes.ToString("00") + ":" + timer.Seconds.ToString("00");
        timertText2.text = timer.Minutes.ToString("00") + ":" + timer.Seconds.ToString("00");
    }

    void UpdateStamina()
    {
        //staminaText.text = currentStamina.ToString();
        //staminaText2.text = currentStamina.ToString() + " / " + maxStamina.ToString();

        uiManager.UpdateHearts();
        
    }

    private void OnEnable()
    {
        SaveWithJson.OnDeletedFile += Load;
    }

    private void OnDisable()
    {
        SaveWithJson.OnDeletedFile -= Load;
    }

    void Save()
    {
        currentStamina = PlayerData.Instance.GetHearts();
        PlayerPrefs.SetInt("currentStamina", currentStamina);
        PlayerPrefs.SetString("nextStaminaTime", nextStaminaTime.ToString());
        PlayerPrefs.SetString("lastStaminaTime", lastStaminaTime.ToString());
    }
    
    private void Load()
    {
        currentStamina = PlayerPrefs.GetInt("currentStamina");
        PlayerData.Instance.SetHearts(currentStamina);
        Debug.Log("currentStamina:");
        Debug.Log(currentStamina);
        uiManager.UpdateCheetos();
        UpdateStamina();
    }


    DateTime StringToDateTime(string date)
    {
        if (string.IsNullOrEmpty(date))
        {
            return DateTime.Now;
        }
        else
        {
            return DateTime.Parse(date);
        }
    }

    private void OnApplicationQuit()
    {
        Save();
    }

    private void OnApplicationPause(bool pause)
    {
        if (pause) Save();
    }


}
