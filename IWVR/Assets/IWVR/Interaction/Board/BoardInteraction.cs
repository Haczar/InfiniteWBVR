using UnityEngine               ;
using Valve.VR.InteractionSystem;

namespace IWVR
{
    [RequireComponent(typeof(Interactable))]
    public class BoardInteraction : MonoBehaviour
    {
        //-------------------------------------------------
        void Awake() {}


        //-------------------------------------------------
        // Called when a Hand starts hovering over this object
        //-------------------------------------------------
        private void OnHandHoverBegin(Hand hand) {}

        //-------------------------------------------------
        // Called when a Hand stops hovering over this object
        //-------------------------------------------------
        private void OnHandHoverEnd(Hand hand) {}

        //-------------------------------------------------
        // Called every Update() while a Hand is hovering over this object
        //-------------------------------------------------
        private void HandHoverUpdate(Hand hand)
        {
            if (hand.GetStandardInteractionButtonDown() /*|| ((hand.controller != null) && hand.controller.GetPressDown(Valve.VR.EVRButtonId.k_EButton_Grip))*/)
            {
                if (hand.currentAttachedObject != gameObject)
                {
                    // Save our position/rotation so that we can restore it when we detach
                    oldPosition = transform.position;
                    oldRotation = transform.rotation;

                    // Call this to continue receiving HandHoverUpdate messages,
                    // and prevent the hand from hovering over anything else
                    hand.HoverLock(GetComponent<Interactable>());

                    // Attach this object to the hand
                    hand.AttachObject(gameObject, attachmentFlags);
                }
                else
                {
                    // Detach this object from the hand
                    hand.DetachObject(gameObject);

                    // Call this to undo HoverLock
                    hand.HoverUnlock(GetComponent<Interactable>());

                    // Restore position/rotation
                    //transform.position = oldPosition;
                    //transform.rotation = oldRotation;
                }
            }
            else if ((hand.controller != null) && hand.controller.GetPressDown(Valve.VR.EVRButtonId.k_EButton_ApplicationMenu))
            {
                //Debug.Log("Clearing board.");

                Transform lineDumpTrans = lineDump.transform;

                Destroy(lineDump);

                lineDump = new GameObject("LineDump");

                lineDump.transform.SetParent(gameObject.transform);

                lineDump.transform.localPosition = lineDumpTrans.localPosition;
                lineDump.transform.localRotation = lineDumpTrans.localRotation;
                lineDump.transform.localScale    = lineDumpTrans.localScale   ;
            }
        }

        //-------------------------------------------------
        // Called when this GameObject becomes attached to the hand
        //-------------------------------------------------
        private void OnAttachedToHand(Hand hand)
        {
            attachTime = Time.time;

            Destroy(gameObject.GetComponent<FixedJoint>());
        }


        //-------------------------------------------------
        // Called when this GameObject is detached from the hand
        //-------------------------------------------------
        private void OnDetachedFromHand(Hand hand)
        {
            Util.FindOrAddComponent<FixedJoint>(gameObject);

            gameObject.GetComponent<FixedJoint>().connectedBody = floor.GetComponent<Rigidbody>();
        }


        //-------------------------------------------------
        // Called every Update() while this GameObject is attached to the hand
        //-------------------------------------------------
        private void HandAttachedUpdate(Hand hand) {}


        //-------------------------------------------------
        // Called when this attached GameObject becomes the primary attached object
        //-------------------------------------------------
        private void OnHandFocusAcquired(Hand hand) {}


        //-------------------------------------------------
        // Called when another attached GameObject becomes the primary attached object
        //-------------------------------------------------
        private void OnHandFocusLost(Hand hand) {}

        void Start()
        {
            oriRotation = Quaternion.Euler(0, 180, 0);
        }

        void Update()
        {
            
        }

        //Marker Stuff
        public void OnMarkerHoverBegin(MarkerTip markerTip)
        {
            lineNum++; line = new GameObject(lineName + lineNum.ToString());//Increments the lineNum value and then creates a new line object with the lineName and lineNum to gen name.

            line.transform.SetParent(gameObject.transform.Find("LineDump").transform);

            currentLine = line.gameObject.AddComponent<LineRenderer>();


            currentLine.startWidth = marker.GetComponent<MarkerInteraction>().width;
            currentLine.endWidth = marker.GetComponent<MarkerInteraction>().width;

            currentLine.alignment = LineAlignment.Local;
            currentLine.colorGradient.mode = GradientMode.Fixed;
            currentLine.textureMode = LineTextureMode.Stretch;
            currentLine.motionVectorGenerationMode = MotionVectorGenerationMode.ForceNoMotion;

            currentLine.useWorldSpace = false;

            currentLine.material.shader = Shader.Find("Sprites/Default");

            currentLine.startColor = marker.GetComponent<MarkerInteraction>().color;
            currentLine.endColor = marker.GetComponent<MarkerInteraction>().color;

            currentLine.transform.localScale = new Vector3(0.125f, 0.5f, 99.95f);

            currentLine.transform.localRotation = Quaternion.Euler(0, 0, 0);
            currentLine.transform.localPosition = Vector3.zero;

            index = 0;

            lineActive = true;

            this.markerTip = markerTip;

            Debug.Log(currentLine.material);
        }

        public void OnMarkerHoverUpdate(MarkerTip marker)
        {
            if (lineActive)
            {
                currentLine.positionCount = index + 1;

                Vector3 worldMarkerPos = marker.transform.position;

                Vector3 localMarkerPos = currentLine.transform.InverseTransformPoint(worldMarkerPos);

                Vector3 clippedZPos = new Vector3(localMarkerPos.x, localMarkerPos.y, -0.00759f);

                currentLine.SetPosition(index, clippedZPos);

                //lineCollider.sharedMesh.

                index++;
            }
        }

        public void OnMarkerHoverEnd(MarkerTip marker)
        {
            lineActive = false;

            lineCollider = line.gameObject.AddComponent<MeshCollider>();

            line.AddComponent<LineInteraction>();

            line.AddComponent<EraserHoverInteraction>();
        }


        //Transform.getPositionLocalToGameObject(board, marker.transfrom.position.x, marker.transform.position.y, marker.transform.position.z);

        //Public

        //Part of Line Render
        public GameObject floor   ;
        public GameObject lineDump;
        public GameObject marker  ;

        //Private
        private bool lineActive;

        private int index   = 0;
        private int lineNum = 0;

        private float attachTime;

        private string lineName = "Line ";  //Part of Line Naming scheme.

        private MeshCollider lineCollider;

        private GameObject line;

        private Hand.AttachmentFlags attachmentFlags = Hand.defaultAttachmentFlags & (~Hand.AttachmentFlags.SnapOnAttach) & (~Hand.AttachmentFlags.DetachOthers);

        private LineRenderer currentLine;

        private MarkerTip markerTip;

        private Quaternion oldRotation;
        private Quaternion oriRotation;

        private Vector3 oldPosition;
    }
}
