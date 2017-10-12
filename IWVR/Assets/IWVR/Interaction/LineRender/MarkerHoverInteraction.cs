using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MarkerHoverInteraction : MonoBehaviour {

    public UnityEvent onMarkerHoverBegin;
    public UnityEvent onMarkerHoverUpdate;
    public UnityEvent onMarkerHoverEnd;

    private void OnMarkerHoverBegin(MarkerTip marker)
    {
        onMarkerHoverBegin.Invoke();
    }

    private void OnMarkerHoverUpdate(MarkerTip marker)
    {
        onMarkerHoverUpdate.Invoke();
    }

    private void OnMarkerHoverEnd(MarkerTip marker)
    {
        onMarkerHoverEnd.Invoke();
    }
}
