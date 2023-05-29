using System;
using System.Collections.Generic;
using Unity.Notifications.Android;
using UnityEngine;

namespace MagicOwl.Notification
{
    public class NotificationManager : MonoBehaviour
    {
        private const string NotificationID = "Awesome_Channel";
        [SerializeField] private List<NotificationModel> NotificationModels;

        private void Start()
        {
            Initialize();
            ClearNotifications();
        }

        private void Initialize()
        {
            Debug.Log("Initializing notification.");
            
            var channel = new AndroidNotificationChannel()
            {
                Id = NotificationID,
                Name = "Awesome Channel",
                Description = "Generic notifications",
                CanShowBadge = true,
                Importance = Importance.High,
                LockScreenVisibility = LockScreenVisibility.Public
            };
            AndroidNotificationCenter.RegisterNotificationChannel(channel);
        }


        private void SendNotifications()
        {
            ClearNotifications();
            foreach (var model in NotificationModels)
            {
                SendNotification(model);
            }
        }

        private void SendNotification(NotificationModel model)
        {

            var notification = new AndroidNotification
            {
                Title = Application.productName,
                Text = model.Text,
                ShowTimestamp = true,
                FireTime = DateTime.Now.AddSeconds(model.DelaySecond)
            };
            
            Debug.Log($"Send notification: {notification.Title}----{notification.Text}");

            AndroidNotificationCenter.SendNotification(notification, NotificationID);
        }

        private void ClearNotifications()
        {
            Debug.Log($"Clear previous notifications.");

            AndroidNotificationCenter.CancelAllDisplayedNotifications();
            AndroidNotificationCenter.CancelAllScheduledNotifications();
            AndroidNotificationCenter.CancelAllNotifications();
        }

        private void OnApplicationFocus(bool hasFocus)
        {
            if(!hasFocus)
                SendNotifications();
            else
                ClearNotifications();
        }
    }
}