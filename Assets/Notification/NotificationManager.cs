using System.Collections.Generic;
using Newtonsoft.Json;
using Unity.Notifications.Android;
using UnityEngine;

namespace MagicOwl.Notification
{
    public class NotificationManager : MonoBehaviour
    {
        private const string NotificationID = "Awesome_Channel";
        [SerializeField] private List<NotificationModel> NotificationModels;
        [SerializeField] private bool AutoClearNotifications;

        void Start()
        {
            Initialize();
            ClearNotifications();
        }

        private void Initialize()
        {
            Debug.Log("notification. Initialize.");
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


        public void SendNotifications()
        {            Debug.Log("notification. send notifications.");

            ClearNotifications();
            foreach (var model in NotificationModels)
            {
                SendNotification(model);
            }
        }

        private void SendNotification(NotificationModel model)
        {
            Debug.Log($"notification. send {JsonConvert.SerializeObject(model)}.");

            var notification = new AndroidNotification
            {
                Title = model.Title,
                Text = model.Text,
                ShowTimestamp = true,
                FireTime = System.DateTime.Now.AddSeconds(model.DelaySecond)
            };

            AndroidNotificationCenter.SendNotification(notification, NotificationID);
        }

        private void ClearNotifications()
        {
            Debug.Log($"notification. clear notifications.");

            if (!AutoClearNotifications) return;

            AndroidNotificationCenter.CancelAllDisplayedNotifications();
            AndroidNotificationCenter.CancelAllScheduledNotifications();
            AndroidNotificationCenter.CancelAllNotifications();
        }
    }
}