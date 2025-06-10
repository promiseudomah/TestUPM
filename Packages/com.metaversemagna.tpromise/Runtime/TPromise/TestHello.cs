using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TPromise
{
    public class TestHello 
    {
        public void Run()
        {
            Debug.Log($"Hello World! ");
            Camera.main.backgroundColor = Color.yellow;
            Debug.Log($"V 2! ");
        }
    }
}
