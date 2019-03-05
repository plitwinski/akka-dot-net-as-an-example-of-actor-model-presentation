namespace Multi.Tenant.Apis
{
    public class Notification
    {
        public Notification(string text)
        {
            Text = text;
        }

        public string Text { get; }
    }
}