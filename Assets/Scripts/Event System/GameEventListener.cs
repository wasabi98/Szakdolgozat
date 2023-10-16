using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameEventListener : MonoBehaviour
{
    public GameEvent GameEvent;
    public UnityEvent response;
    public void OnEnable()
    {
        GameEvent.RegisterListener(this);
    }
    public void OnDisable()
    {
        GameEvent.UnregisterListener(this);
    }
    public void OnEventRaised()
    {
        response.Invoke();
    }
}
