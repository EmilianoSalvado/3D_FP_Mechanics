using System;
using System.Collections;
using UnityEngine;

public class AuxMethods : MonoBehaviour
{
    public static AuxMethods Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null) Destroy(this);
        Instance = this;
    }

    public void AuxStartCoroutine(Func<IEnumerator> routine)
    {
        StartCoroutine(routine());
    }
}