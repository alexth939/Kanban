// version 18.09.2022

using System;

namespace UnityEngine.SceneManagement
{
    public static class SceneNamesExtensions
    {
        public static string AsString(this SceneName name)
        {
            return Enum.GetName(typeof(SceneName), name);
        }
    }
}
