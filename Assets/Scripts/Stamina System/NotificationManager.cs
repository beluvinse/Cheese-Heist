using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.Notifications.Android;

public class NotificationManager : MonoBehaviour
{
    public static NotificationManager Instance { get; private set; }

    AndroidNotificationChannel notifChannel;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        AndroidNotificationCenter.CancelAllDisplayedNotifications();
        AndroidNotificationCenter.CancelAllScheduledNotifications();

        notifChannel = new AndroidNotificationChannel()
        {
            Id = "reminder_notif_channel",
            Name = "Reminder Notifications",
            Description = "This is my description",
            Importance = Importance.High
        };

        AndroidNotificationCenter.RegisterNotificationChannel(notifChannel);

        //DisplayNotification("Te extraño", "Hace tiempo que no te veo, necesito que juegues conmigo", DateTime.Now.AddSeconds(30));
    }

    public int DisplayNotification(string title, string text, DateTime fireTime)
    {
        var notification = new AndroidNotification();
        notification.Title = title;
        notification.Text = text;
        notification.SmallIcon = "icon";
        notification.LargeIcon = "iconn";
        notification.FireTime = fireTime;

        return AndroidNotificationCenter.SendNotification(notification, notifChannel.Id);
    }

    public void CancelNotification(int id)
    {
        AndroidNotificationCenter.CancelScheduledNotification(id);
    }
}
