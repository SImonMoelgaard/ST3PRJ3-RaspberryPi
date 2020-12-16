using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic
{
    /// <summary>
    /// del ar observer patteren. provider til commands og limitvals
    /// </summary>
    public abstract class UdpProvider 
    {
        
        private static List<IPresentationObserver> _observers = new List<IPresentationObserver>();
        /// <summary>
        ///  del af observerpattern. tilføjer observeren til listen over observers, der skal have at vide, der er sket noget 
        /// </summary>
        /// <param name="observer"></param>
        public static void Attach(IPresentationObserver observer)
        {
            _observers.Add(observer);
        }
        /// <summary>
        /// fjerner observer fra listen over observers - ikke brugt i dette projekt
        /// </summary>
        /// <param name="observer"></param>
        public void Detach(IPresentationObserver observer)
        {
            _observers.Remove(observer);
        }

        /// <summary>
        /// gør observeren (presentationcontrolleren) opmærksom på om der er kommet en ny komando fra UI.
        /// </summary>
        public void Notify()
        {
            foreach (var observer in _observers)
            {
                observer.Update();
            }
        }
        /// <summary>
        /// gør observeren (presentationcontrolleren) opmærksom på om der er kommet en nye limitvals fra UI.
        /// </summary>
        public void NotifyL()
        {
            foreach (var observer in _observers)
            {
                observer.UpdateLimit();
            }
        }

    }
}