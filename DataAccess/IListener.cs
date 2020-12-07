using DTO_s;

namespace DataAccessLogic
{
    public interface IListener
    {
        DTO_LimitVals ListenLimitValsPC();
        string ListenCommandsPC();
    }
}