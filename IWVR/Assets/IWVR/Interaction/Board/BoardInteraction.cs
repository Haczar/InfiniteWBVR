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
            if (hand.GetStandardInteractionButtonDown() || ((hand.controller != null) && hand.controller.GetPressDown(Valve.VR.EVRButtonId.k_EButton_Grip)))
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
        }

        //Marker Stuff
        private void OnMarkerHoverBegin(MarkerTip marker)
        {
            lineNum++; line = new GameObject(lineName + lineNum.ToString());//Increments the lineNum value and then creates a new line object with the lineName and lineNum to gen name.

            line.transform.SetParent(gameObject.transform.Find("LineDump").transform);

            currentLine = line.gameObject.AddComponent<LineRenderer>();

            currentLine.startWidth = width;
            currentLine.endWidth   = width;

            currentLine.material = (Material)Resources.Load("Materials/Marker/markerDefault.mat");

            currentLine.startColor = color;
            currentLine.endColor   = color;

            index = 0;

            lineActive = true;

            this.marker = marker;

            Debug.Log(currentLine.material);
        }

        private void OnMarkerHoverUpdate(MarkerTip marker) { }

        private void OnMarkerHoverEnd(MarkerTip marker)
        { lineActive = false; }

        //-------------------------------------------------
        // Called when this GameObject becomes attached to the hand
        //-------------------------------------------------
        private void OnAttachedToHand(Hand hand)
        { attachTime = Time.time; }


        //-------------------------------------------------
        // Called when this GameObject is detached from the hand
        //-------------------------------------------------
        private void OnDetachedFromHand(Hand hand) {}


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


        void Update()
        {
            if (lineActive)
            {
                currentLine.positionCount = index + 1;

                currentLine.SetPosition(index, new Vector3(marker.transform.position.x, marker.transform.position.y, gameObject.GetComponent<BoxCollider>().transform.position.z - 0.01f));
                //May need rotation for it to be properly latched to the board.

                index++;
            }
        }


        //Public

        //Part of Line Render
        public float width = 0.05f      ;
        public Color color = Color.black;


        //Private
        private bool lineActive;

        private int index   = 0;
        private int lineNum = 0;

        private float attachTime;

        private string lineName = "Line ";  //Part of Line Naming scheme.

        private GameObject line      ;
        private GameObject lineRenObj;

        private Hand.AttachmentFlags attachmentFlags = Hand.defaultAttachmentFlags & (~Hand.AttachmentFlags.SnapOnAttach) & (~Hand.AttachmentFlags.DetachOthers);

        private LineRenderer currentLine;

        private MarkerTip marker;

        private Quaternion oldRotation;

        private Vector3 oldPosition;
    }
}
