using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenLink : MonoBehaviour
{
    public void GitHub()
    {
        Debug.Log("laad git hub");
        Application.OpenURL("https://github.com/jorisklijen/Slootwater-1");
    }
}
