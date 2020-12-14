using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace BusinessLogic
{
    public interface IPresentationObserver
    {
        public void Update();
        public void UpdateLimit();
    }
}