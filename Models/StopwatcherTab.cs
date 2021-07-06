namespace ProteiTestApp.Models
{
    public class StopwatcherTab
    {
        public StopwatcherTab(StopwatchCounter counter)
        {
            Counter = counter;
        }
        public StopwatchCounter Counter { get; private set; }
    }
}
