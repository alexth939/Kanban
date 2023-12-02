// version 19.09.2022

///<summary>
///  Intended to serve as the sole entry point to scene.
///</summary>
///
///<remarks>
///  The built-in "magic functions" are protected to force the IDE to alert
///  in case u'll try to hide inherited members.
///</remarks>
///
///<useage>
///  Attach it to game object in scene.
///  Put all the magic in inherited class.
///</useage>

using System;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace UnityEngine.SceneManagement
{
    [DisallowMultipleComponent]
    public abstract class ScenePresenter: MonoBehaviour
    {
        /// <summary>
        /// auto-invoked at base.Start().
        /// </summary>
        protected abstract void OnEnteringScene();
        protected virtual void OnApplicationAcquiredFocus() { }
        protected virtual void OnApplicationLostFocus() { }
        protected virtual void OnLeavingScene() { }

        protected void SwitchScene(SceneName nextScene)
        {
            OnLeavingScene();
            SceneManager.LoadScene(nextScene.AsString(), LoadSceneMode.Single);
        }

#if UNITY_EDITOR
        protected void OnValidate() => EditorGUIUtility.PingObject(this);
#endif
        protected void Start() => OnEnteringScene();

#pragma warning disable AV1564 // It's builtin fuckup.
        protected void OnApplicationFocus(bool focus)
        {
            Action focusEventHandler = focus ? OnApplicationAcquiredFocus : OnApplicationLostFocus;
            focusEventHandler.Invoke();
        }
#pragma warning restore AV1564 // Parameter in public or internal member is of type bool or bool?
    }
}
