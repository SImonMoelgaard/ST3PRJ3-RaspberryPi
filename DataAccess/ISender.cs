using System;
using System.Collections.Generic;
using System.Text;
using DTO_s;

namespace DataAccessLogic
{
    public interface ISender
    {
        void SendDTO_Calculated(DTO_Calculated dtoCalculated);
        void SendDTO_Raw(List<DTO_Raw> dtoRaw);
        void SendDouble(double meanVal);
    }
}
