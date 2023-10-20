namespace Hacienda.WorkerService.Workers
{
    public class TimeService : ITimeService
    {
        public DateTime GetDateTime() => DateTime.Now;
    }
}