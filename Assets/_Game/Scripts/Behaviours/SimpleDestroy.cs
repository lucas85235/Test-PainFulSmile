using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleDestroy : MonoBehaviour
{
    public float destroyTimer = 1;

    private void Start()
    {
        Destroy(gameObject, destroyTimer);
    }
}
