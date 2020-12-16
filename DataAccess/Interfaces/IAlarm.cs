namespace DataAccessLogic
{
    /// <summary>
    /// interface til alarm
    /// </summary>
    public interface IAlarm
    {
        /// <summary>
        /// muter alarmen
        /// </summary>
        void Mute();
        /// <summary>
        /// stopper alarm med høj prioritet
        /// </summary>
        void StopHighAlarm();
        /// <summary>
        /// stopper alarm med mellem prioritet
        /// </summary>
        
        void StopMediumAlarm();
        /// <summary>
        /// starter alarm med høj prioritet
        /// </summary>
        void StartHighAlarm();
        /// <summary>
        /// starter alarm med mellem prioritet
        /// </summary>
        void StartMediumAlarm();
    }
}