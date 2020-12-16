using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace BusinessLogic
{
    /// <summary>
    /// del af observeren interface til at forbinde businesscontrolleren fra udpprovider til presentationscontrolleren
    /// </summary>
    public interface IPresentationObserver
    {
        /// <summary>
        /// update bliver kaldt når der er kommet nye komandoer
        /// </summary>
        public void Update();
        /// <summary>
        /// updateLimit bliver kaldt når der er kommet nye grænseværdier
        /// </summary>
        public void UpdateLimit();
    }
}