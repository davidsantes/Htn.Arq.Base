namespace Htn.Arq.Base.WorkerService.Workers
{
    public class TimeService : ITimeService
    {
        public DateTime GetDateTime() => DateTime.Now;
    }
}