using DTO_s;

namespace DataAccessLogic
{
    public interface IListener
    {
        void ReceiveSystemOn(bool systemOn);
        string Command { get; set; }
        DTO_LimitVals DtoLimit { get; set; }
        void ListenLimitValsPC();
        void ListenCommandsPC();

        void AddToQueueCommand(string command);

        void AddToQueueDtoLimitVals(DTO_LimitVals dtoLimit);
        
    }
}