using DTO_s;

namespace DataAccessLogic
{
    public interface IListener
    {
        string Command { get; set; }
        DTO_LimitVals DtoLimit { get; set; }
        void ListenLimitValsPC();
        void ListenCommandsPC();

        void SendCommand(string command);

        DTO_LimitVals SendDtoLimitVals(DTO_LimitVals dtoLimit);
        //void StartUp();
    }
}