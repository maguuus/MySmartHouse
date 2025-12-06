namespace MySmartHome
{
    public class EventLogger
    {
        private readonly List<string> _log = [];

        public void Log(string message)
        {
            string entry = $"{DateTime.Now:MM/dd/yyyy HH:mm:ss}: {message}";
            _log.Add(entry);
            // Implement adding a message to the log with a timestamp.
        }

        public void ShowLog()
        {
            Console.WriteLine($"Event log:");
            if (_log.Count == 0)
            {
                return;
            }
            foreach (var entry in _log)
            {
                Console.WriteLine(entry);
            }
            // Implement displaying the log entries in the console.
        }
    }
}