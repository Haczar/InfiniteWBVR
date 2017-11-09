using UnityEngine               ;
using Valve.VR.InteractionSystem;

public class MarkerTip : MonoBehaviour
{
    private void Awake()
    { if (hoverSphereTransform == null) { hoverSphereTransform = this.transform; } }


    void Start ()
    {
        playerInstance = Player.instance;

        overlappingColliders = new Collider[ColliderArraySize];   //Allocate array for colliders

        hoverLocked = false;
	}
	
	// Update is called once per frame
	void Update () { UpdateHovering(); }

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
                    _hoveringInteractable.SendMessage("OnMarkerHoverEnd", this, SendMessageOptions.DontRequireReceiver);
                }

                _hoveringInteractable = value;

                if (_hoveringInteractable != null)
                {
                    //_hoveringInteractable.SendMessage("OnHandHoverBegin", this, SendMessageOptions.DontRequireReceiver);
                    _hoveringInteractable.SendMessage("OnMarkerHoverBegin", this, SendMessageOptions.DontRequireReceiver);

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
        float flHoverRadiusScale   = playerInstance.transform.lossyScale.y * 2.315f;
        float flScaledSphereRadius = hoverSphereRadius;

        // if we're close to the floor, increase the radius to make things easier to pick up
        float handDiff = Mathf.Abs(transform.position.y - playerInstance.trackingOriginTransform.position.y);
        float boxMult = Util.RemapNumberClamped(handDiff, 0.0f, 0.5f * flHoverRadiusScale, 5.0f, 1.0f) * flHoverRadiusScale;

        // null out old vals
        for (int i = 0; i < overlappingColliders.Length; ++i)
            overlappingColliders[i] = null;

        Physics.OverlapSphereNonAlloc(hoverSphereTransform.position,
                                      flScaledSphereRadius                                                                                    ,
                                      overlappingColliders                                                                                    ,
                                      hoverLayerMask.value                                                                                     );

        int iActualColliderCount = 0; // DebugVar

        foreach (Collider collider in overlappingColliders)
        {
            if (collider == null)
                continue;

            Interactable contacting = collider.GetComponentInParent<Interactable>();

            if (contacting == null)   // Yeah, it's null, skip
                continue;

            
            IgnoreHovering ignore = collider.GetComponent<IgnoreHovering>();   // Ignore this collider for hovering
            if (ignore != null)
                if (ignore.onlyIgnoreHand == null || ignore.onlyIgnoreHand == this)
                    continue;

            // Best candidate so far...
            float distance = Vector3.Distance(contacting.transform.position, hoverSphereTransform.position);

            if (distance < closestDistance && !collider.Equals(this.GetComponentInParent<CapsuleCollider>()))
            {
                closestDistance     = distance  ;
                closestInteractable = contacting;
            }

            iActualColliderCount++;
        }

        // Hover on this one
        hoveringInteractable = closestInteractable;

        if (iActualColliderCount > 0 && iActualColliderCount != prevOverlappingColliders)
            prevOverlappingColliders = iActualColliderCount;
    }


    //Public
    public bool hoverLocked { get; private set; }

    public float hoverSphereRadius   = 0.05f;
    public float hoverUpdateInterval = 0.1f ;

    public LayerMask hoverLayerMask = -1;

    public Transform hoverSphereTransform;

    //Private
    private int prevOverlappingColliders = 0;

    private const int ColliderArraySize = 16;

    private Collider[] overlappingColliders;

    private Interactable _hoveringInteractable;

    private Player playerInstance;
}
