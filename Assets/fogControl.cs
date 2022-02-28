using UnityEngine;
using System.Collections;

public class fogControl : MonoBehaviour
{

    public Color fogColorSet;
    public float fogDensitySet;

    private void Update()
    {
        RenderSettings.fogColor = fogColorSet;
        RenderSettings.fogDensity = fogDensitySet;

    }
}