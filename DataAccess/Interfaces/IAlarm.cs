namespace DataAccessLogic
{
    public interface IAlarm
    {
        void Mute();
        void StopHighAlarm();
        void StopMediumAlarm();
        void StartHighAlarm();
        void StartMediumAlarm();
    }
}