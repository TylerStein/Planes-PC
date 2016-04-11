using UnityEngine;
using System.Collections;

public class Module : MonoBehaviour {

    public string[] Tags;

    public ModuleConnector[] GetExits()
    {
        return GetComponentsInChildren<ModuleConnector>();
    }
}
