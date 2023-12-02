using System;
using UnityEngine;

public class GlobalInputEvents : MonoBehaviour
{
    public static event Action PointerUp;

    private void Update()
    {
        if(Input.GetMouseButtonUp(0))
            PointerUp?.Invoke();
    }
}
