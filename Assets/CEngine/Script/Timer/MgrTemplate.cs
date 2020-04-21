﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MgrTemplate<T> where T: MgrTemplate<T>, new()
{
    private static T _inst;

    public static T instance
    {
        get
        {
            if (null == _inst)
            {
                _inst = new T();
                _inst.OnAwake();
            }
            return _inst;
        }
    }

    public void Init()
    {
        OnInit();
    }

    protected virtual void OnAwake()
    {
    }

    protected virtual void OnInit()
    {
    }

    public void Dispose()
    {
        OnClear();

        _inst = null;
    }

    protected virtual void OnClear()
    {
    }
}