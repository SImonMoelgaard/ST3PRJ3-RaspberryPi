﻿using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic
{
    public abstract class LimitProvider
    {
        private static List<IPresentationObserver> _observers = new List<IPresentationObserver>();
        public static void Attach(IPresentationObserver observer)
        {
            _observers.Add(observer);
        }

        public void Detach(IPresentationObserver observer)
        {
            _observers.Remove(observer);
        }

        public void NotifyL()
        {
            foreach (var observer in _observers)
            {
                observer.UpdateLimit();
            }
        }

    }
}