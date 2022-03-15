using UnityEngine;

namespace PressIt
{
    public static class BugHandler
    {
        public static void Log(string message, bool isDebug)
        {
            if (isDebug)
            {
                Debug.Log(message);
            }
        }
    }
}
