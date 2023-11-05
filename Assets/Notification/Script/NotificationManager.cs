using System;
using System.Collections.Generic;
using Unity.Notifications.Android;
using UnityEngine;
using UnityEngine.Android;

namespace MagicOwl.Notification
{
    public class NotificationManager : MonoBehaviour
    {
        private const string NotificationID = "Awesome_Channel";
        private const string PermissionID = "android.permission.POST_NOTIFICATIONS";
        
        [SerializeField] private List<NotificationModel> NotificationModels;

        private void Start()
        {
            Initialize();
            ClearNotifications();
        }

        private void Initialize()
        {
            CheckPermission();
            var channel = new AndroidNotificationChannel()
            {
                Id = NotificationID,
                Name = "Routine Channel",
                Description = "Generic notifications",
                CanShowBadge = true,
                Importance = Importance.High,
                LockScreenVisibility = LockScreenVisibility.Public
            };
            AndroidNotificationCenter.RegisterNotificationChannel(channel);
        }

        private void CheckPermission()
        {
            var hasPermission = Permission.HasUserAuthorizedPermission(PermissionID);
            if (hasPermission) return;
            Permission.RequestUserPermission(PermissionID);
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

            AndroidNotificationCenter.SendNotification(notification, NotificationID);
        }

        private void ClearNotifications()
        {
            AndroidNotificationCenter.CancelAllDisplayedNotifications();
            AndroidNotificationCenter.CancelAllScheduledNotifications();
            AndroidNotificationCenter.CancelAllNotifications();
        }

        private void OnApplicationQuit()
        {
            ClearNotifications();
            SendNotifications();
        }
    }
}
