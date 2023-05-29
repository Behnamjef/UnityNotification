using System;

namespace MagicOwl.Notification
{
    [Serializable]
    public class NotificationModel
    {
        public string Title;
        public string Text;
        public int DelaySecond;
    }
}