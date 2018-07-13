using System;

namespace SoapUIClone.Common
{
    public static class Action
    {
        public static System.Action Empty()
        {
            return new System.Action(() => { });
        }
    }
}
