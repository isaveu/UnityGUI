﻿using Gameframe.GUI.Utility;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Gameframe.GUI.Camera.UI
{

  public class UIEventManager : MonoBehaviour
  {

    static UIEventManager _current;
    public static UIEventManager Current
    {
      get
      {
        return _current;
      }
    }

    public static void LockCurrent()
    {
      if ( _current != null )
      {
        _current.Lock();
      }
    }

    public static void UnlockCurrent()
    {
      if ( _current != null )
      {
        _current.Unlock();
      }
    }
    
    public static bool IsPointerOverUI()
    {
      return EventSystem.current != null && EventSystem.current.IsPointerOverGameObject();
    }

    EventSystem eventSystem;

    [SerializeField]
    CountLock inputLock = new CountLock();

    protected virtual void Awake()
    {
      eventSystem = GetComponent<EventSystem>();
      _current = this;
      inputLock.onUnlocked.AddListener( this.OnUnlock );
      inputLock.onLocked.AddListener( this.OnLock );
    }

    public void Lock()
    {
      inputLock.Lock();
    }

    public void Unlock()
    {
      inputLock.Unlock();
    }

    void OnLock()
    {
      if ( eventSystem != null )
      {
        eventSystem.enabled = false;
      }
    }

    void OnUnlock()
    {
      if ( eventSystem != null )
      {
        eventSystem.enabled = true;
      }
    }

  }

}