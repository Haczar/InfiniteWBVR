using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class MarkerTip : MonoBehaviour {

    public Transform hoverSphereTransform;
    public float hoverSphereRadius = 0.05f;
    public LayerMask hoverLayerMask = -1;
    public float hoverUpdateInterval = 0.1f;

    public bool hoverLocked { get; private set; }

    private Interactable _hoveringInteractable;

    private int prevOverlappingColliders = 0;

    private const int ColliderArraySize = 16;
    private Collider[] overlappingColliders;

    private Player playerInstance;

    private void Awake()
    {
        if (hoverSphereTransform == null)
        {
            hoverSphereTransform = this.transform;
        }
    }
    // Use this for initialization
    void Start () {
        playerInstance = Player.instance;
        // allocate array for colliders
        overlappingColliders = new Collider[ColliderArraySize];
        hoverLocked = false;
	}
	
	// Update is called once per frame
	void Update () {
        UpdateHovering();
	}

    //-------------------------------------------------
    // The Interactable object this Hand is currently hovering over
    //-------------------------------------------------
    public Interactable hoveringInteractable
    {
        get { return _hoveringInteractable; }
        set
        {
            if (_hoveringInteractable != value)
            {
                if (_hoveringInteractable != null)
                {
                    //_hoveringInteractable.SendMessage("OnHandHoverEnd", this, SendMessageOptions.DontRequireReceiver);
                    _hoveringInteractable.SendMessage("OnMarkerHoverEnd", _hoveringInteractable, SendMessageOptions.DontRequireReceiver);

                }

                _hoveringInteractable = value;

                if (_hoveringInteractable != null)
                {
                    //_hoveringInteractable.SendMessage("OnHandHoverBegin", this, SendMessageOptions.DontRequireReceiver);
                    _hoveringInteractable.SendMessage("OnMarkerHoverBegin", _hoveringInteractable, SendMessageOptions.DontRequireReceiver);

                }
            }
        }
    }

    private void UpdateHovering()
    {

        if (hoverLocked)
            return;

        float closestDistance = float.MaxValue;
        Interactable closestInteractable = null;

        // Pick the closest hovering
        float flHoverRadiusScale = playerInstance.transform.lossyScale.x;
        float flScaledSphereRadius = hoverSphereRadius * flHoverRadiusScale;

        // if we're close to the floor, increase the radius to make things easier to pick up
        float handDiff = Mathf.Abs(transform.position.y - playerInstance.trackingOriginTransform.position.y);
        float boxMult = Util.RemapNumberClamped(handDiff, 0.0f, 0.5f * flHoverRadiusScale, 5.0f, 1.0f) * flHoverRadiusScale;

        // null out old vals
        for (int i = 0; i < overlappingColliders.Length; ++i)
        {
            overlappingColliders[i] = null;
        }

        Physics.OverlapBoxNonAlloc(
            hoverSphereTransform.position - new Vector3(0, flScaledSphereRadius * boxMult - flScaledSphereRadius, 0),
            new Vector3(flScaledSphereRadius, flScaledSphereRadius * boxMult * 2.0f, flScaledSphereRadius),
            overlappingColliders,
            Quaternion.identity,
            hoverLayerMask.value
        );

        // DebugVar
        int iActualColliderCount = 0;

        foreach (Collider collider in overlappingColliders)
        {
            if (collider == null)
                continue;

            Interactable contacting = collider.GetComponentInParent<Interactable>();

            // Yeah, it's null, skip
            if (contacting == null)
                continue;

            // Ignore this collider for hovering
            IgnoreHovering ignore = collider.GetComponent<IgnoreHovering>();
            if (ignore != null)
            {
                if (ignore.onlyIgnoreHand == null || ignore.onlyIgnoreHand == this)
                {
                    continue;
                }
            }

            // Best candidate so far...
            float distance = Vector3.Distance(contacting.transform.position, hoverSphereTransform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestInteractable = contacting;
            }
            iActualColliderCount++;
        }

        // Hover on this one
        hoveringInteractable = closestInteractable;

        if (iActualColliderCount > 0 && iActualColliderCount != prevOverlappingColliders)
        {
            prevOverlappingColliders = iActualColliderCount;
        }
    }
}
