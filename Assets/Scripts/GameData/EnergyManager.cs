using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnergyManager : MonoBehaviour
{
    [SerializeField] private TMP_Text _textHearts;
    [SerializeField] private TMP_Text _textHearts2;
    [SerializeField] private TMP_Text _textTimer;
    [SerializeField] private TMP_Text _textTimer2;
    [SerializeField] private int _maxHearts;
    [SerializeField] private int _totalHearts = 0;

    private DateTime _nextHeartTime;
    private DateTime _lastAddedTime;
    private int _restoreDuration = 10; //10 segundos para probar

    public PlayerData playerData;


    bool _restoring = false;

    private void Start()
    {
        Load();
        StartCoroutine(RestoreRoutine());
    }

    public void UseHeart()
    {
        if (_totalHearts == 0)
            return;

        _totalHearts--;
        //UpdateHearts();

        if (!_restoring)
        {
            if (_totalHearts + 1 == _maxHearts)
            {
                _nextHeartTime = AddDuration(DateTime.Now, _restoreDuration);
            }
            StartCoroutine(RestoreRoutine());
        }
    }

    private IEnumerator RestoreRoutine()
    {
        UpdateTimer();
        //UpdateHearts();
        _restoring = true;

        while (_totalHearts < _maxHearts)
        {
            DateTime currentTime = DateTime.Now;
            DateTime counter = _nextHeartTime;
            bool isAdding = false;
            while (currentTime > counter)
            {
                if (_totalHearts < _maxHearts)
                {
                    isAdding = true;
                    _totalHearts++;
                    playerData.AddHearts(1);
                    DateTime timeToAdd = _lastAddedTime > counter ? _lastAddedTime : counter;
                    counter = AddDuration(timeToAdd, _restoreDuration);
                }
                else
                    break;
            }
            if (isAdding)
            {
                _lastAddedTime = DateTime.Now;
                _nextHeartTime = counter;
            }

            //UpdateHearts();
            UpdateTimer();
            Save();
            yield return null;
        }

        _restoring = false;
    }

    private void UpdateTimer()
    {
        if (_totalHearts >= _maxHearts)
        {
            _textTimer.text = "Full";
            _textTimer2.text = "Full!";
            return;
        }
      

         TimeSpan t = _nextHeartTime - DateTime.Now;
        string value = String.Format("{0}:{1:D2}:{2:D2}", (int)t.TotalHours, (int)t.TotalMinutes, (int)t.TotalSeconds);
        _textTimer.text = value;
        _textTimer2.text = "More in " + value;
        
    }

    private void UpdateHearts()
    {
        _textHearts.text = _totalHearts.ToString();
        _textHearts2.text = _totalHearts.ToString() + "/" + _maxHearts;
    }

    private DateTime AddDuration(DateTime time, int duration)
    {
        return time.AddSeconds(duration);
    }

    public void Load()
    {
        //_totalHearts = PlayerPrefs.GetInt("totalHearts");
        _totalHearts = playerData.GetHearts();
        _nextHeartTime = StringToDate(PlayerPrefs.GetString("nextHeartTime"));
        _lastAddedTime = StringToDate(PlayerPrefs.GetString("lastAddedTime"));
    }

    public void Save()
    {
        //PlayerPrefs.SetInt("totalHearts", _totalHearts);
        PlayerPrefs.SetString("nextHeartTime", _nextHeartTime.ToString());
        PlayerPrefs.SetString("lastAddedTime", _lastAddedTime.ToString());
    }

    private DateTime StringToDate(string date)
    {
        if (String.IsNullOrEmpty(date))
            return DateTime.Now.AddSeconds(_restoreDuration);

        return DateTime.Parse(date);
    }
}
