﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// 对象池
/// </summary>
namespace CEngine
{
    public class ObjectPool<T> where T : new()
    {
        private List<T> _objects = new List<T>();

        public T Get()
        {
            if (0 != _objects.Count)
            {
                var obj = _objects[0];
                _objects.RemoveAt(0);
                return obj;
            }
            return new T();
        }

        public void Release(T obj)
        {
            _objects.Add(obj);
        }
    }
}