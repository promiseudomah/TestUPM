using System.Collections;
using System.Collections.Generic;
using TPromise;
using UnityEngine;

public class Example : MonoBehaviour
{
    TestHello test;

    void Start()
    {
        test = new TestHello();
        test.Run();
    }
}
