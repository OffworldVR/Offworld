using UnityEngine;
using UnityEditor;
using System.Collections;
// CopyComponents - by Michael L. Croswell for Colorado Game Coders, LLC
// March 2010

public class ResizeObjects : ScriptableWizard
{
    public GameObject ObjectsToResize;
    public float resize_factor;

    [MenuItem("Custom/Resize Objects")]


    static void CreateWizard()
    {
        ScriptableWizard.DisplayWizard("Resize GameObjects", typeof(ResizeObjects), "Resize");
    }

    void OnWizardCreate()
    {
        Transform[] Resizes;
        Resizes = ObjectsToResize.GetComponentsInChildren<Transform>();

        foreach (Transform t in Resizes)
        {
            t.localScale *= resize_factor;
        }
    }
}
