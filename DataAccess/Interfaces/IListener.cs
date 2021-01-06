using DTO_s;

namespace DataAccessLogic
{
    /// <summary>
    /// interface til listneren
    /// </summary>
    public interface IListener
    {
        /// <summary>
        /// bool der indikere om systemet er tændt
        /// </summary>
        /// <param name="systemOn">bool der indikere om systemet er tændt</param>
        void ReceiveSystemOn(bool systemOn);
        /// <summary>
        /// propperty til den kommando der kommer ind i systemet
        /// </summary>
        string Command { get; set; }
        /// <summary>
        /// propperty til den dto med grænseværdier, der kommer ind i systemet
        /// </summary>
       // DTO_LimitVals DtoLimit { get; set; }
        /// <summary>
        /// metode, der lytter efter grænseværdier
        /// </summary>
        void ListenLimitValsPC();
        //DTO_LimitVals ListenLimitValsPC();
        /// <summary>
        /// metode, der lytter efter komandoer
        /// </summary>
        void ListenCommandsPC();
        /// <summary>
        /// metode, der tilføjer komandoen til datakøen for komandoer
        /// </summary>
        /// <param name="command">komandoen fra UI, der sætter RPi igang</param>
        void AddToQueueCommand(string command);
        /// <summary>
        /// metode, der tilføjer limitvals til datakøen for komandoer
        /// </summary>
        /// <param name="dtoLimit">dto bestående af øvre og nedre grænse for sys, dia, middel blodtryk samt nulpunktjustering og calkibrationsværdi</param>
        void AddToQueueDtoLimitVals(DTO_LimitVals dtoLimit);
        
    }
}