using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EraserHoverInteraction : MonoBehaviour
{
    private void OnEraserHoverBegin (EraseSide eraseSide) { onEraserHoverBegin .Invoke(); }
    private void OnEraserHoverUpdate(EraseSide eraseSide) { onEraserHoverUpdate.Invoke(); }
    private void OnEraserHoverEnd   (EraseSide eraseSide) { onEraserHoverEnd   .Invoke(); }

    public UnityEvent onEraserHoverBegin ;
    public UnityEvent onEraserHoverUpdate;
    public UnityEvent onEraserHoverEnd   ;
}
