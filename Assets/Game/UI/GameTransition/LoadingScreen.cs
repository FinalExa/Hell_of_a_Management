using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingScreen : MonoBehaviour
{
    public static LoadingScreen loadingScreenInstance;
    void Start()
    {
        DontDestroyOnLoad(this);

        if (loadingScreenInstance == null)
            loadingScreenInstance = this;
        else
            Destroy(gameObject);
    }
}
