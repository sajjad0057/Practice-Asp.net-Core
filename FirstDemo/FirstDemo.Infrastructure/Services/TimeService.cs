namespace FirstDemo.Infrastructure.Services
{
    public class TimeService : ITimeService
    {

        public DateTime Now
        {
            get => DateTime.Now;
        }
    }
}
