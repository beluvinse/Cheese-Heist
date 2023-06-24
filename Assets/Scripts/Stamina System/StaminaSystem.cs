using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class StaminaSystem : MonoBehaviour
{
    [SerializeField] int maxStamina = 10;
    [SerializeField] float timeToRecharge = 5f;
    int currentStamina;


    [SerializeField] TextMeshProUGUI staminaText = null;
    [SerializeField] TextMeshProUGUI stamina2Text = null;
    [SerializeField] TextMeshProUGUI timertText = null;
    [SerializeField] TextMeshProUGUI timertText2 = null;

    bool recharging;

    DateTime nextStaminaTime;
    DateTime lastStaminaTime;

    [SerializeField] string notifTitle = "Full Stamina";
    [SerializeField] string notifText = "Tenes la stamina al borde de colapzar, volve al juego";

    int id;

    TimeSpan timer;


    void Start()
    {
        if (!PlayerPrefs.HasKey("currentStamina"))
        {
            PlayerPrefs.SetInt("currentStamina", maxStamina);
        }

        Load();
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
                if (currentStamina >= maxStamina) break;

                currentStamina++;
                staminaAdd = true;
                UpdateStamina();

                DateTime timeToAdd = nextTime;

                if (lastStaminaTime > nextTime)
                    timeToAdd = lastStaminaTime;

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
    }

    void UpdateStamina()
    {
        staminaText.text = currentStamina.ToString() + " / " + maxStamina.ToString();
        staminaText.text = currentStamina.ToString() + " / " + maxStamina.ToString();

        UpdateTimer();
    }

    void Save()
    {
        PlayerPrefs.SetInt("currentStamina", currentStamina);
        PlayerPrefs.SetString("nextStaminaTime", nextStaminaTime.ToString());
        PlayerPrefs.SetString("lastStaminaTime", lastStaminaTime.ToString());
    }

    void Load()
    {
        currentStamina = PlayerPrefs.GetInt("currentStamina");

        nextStaminaTime = StringToDateTime(PlayerPrefs.GetString("nextStaminaTime"));
        lastStaminaTime = StringToDateTime(PlayerPrefs.GetString("lastStaminaTime"));
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
