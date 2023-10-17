using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    public static CanvasManager ins;
    public Canvas canvas;
    private void Awake()
    {
        ins = this;
    }
}
