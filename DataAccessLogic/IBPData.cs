﻿using System;
using System.Collections.Generic;
using System.Text;
using DTO_s;

namespace PresentationLogic
{
    /// <summary>
    /// Dette er et Interface, der gør det nemmere at skifte mellem Physionets måling og fra ADC'en
    /// Senere vil denne også gøre det nemmere at teste klasserne
    /// </summary>
    public interface IBPData
    {
        public DTO_Raw MeassureSignal();

    }
}
