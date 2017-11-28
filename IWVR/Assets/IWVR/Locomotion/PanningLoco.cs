using UnityEngine;


public class PanningLoco : MonoBehaviour
{
    //From Point and Move all in one script.
    void applyTranslation()
    {
        //Vector3 newPos = new Vector3(direction.x * velocity + playArea.transform.position.x,
        //                             direction.y * velocity + playArea.transform.position.y,
        //                                                      playArea.transform.position.z );   /*direction.z * velocity + playArea.transform.position.z*/

        Vector3 newPos = new Vector3(0, 0, 0);

        playArea.gameObject.transform.position = newPos;
    }

    void setDirection()  //Old
    {
        Vector3 calculatedDirection = (locomotionController != null ? locomotionController.gameObject.transform.rotation : Quaternion.identity) * Vector3.forward;

        direction = calculatedDirection;
    }

    void sticky()
    { velocity = Vector3.Distance(previousPos, currentPos) * sensitivity; applyTranslation(); }
    //----------------------------------------------------------------

    //Panning Function
    void pan()
    {
        float horiDistance = currentPos.x - previousPos.x;
        float vertDistance = currentPos.y - previousPos.y;

        Vector3 newPos = new Vector3(sensitivity * -horiDistance + playArea.gameObject.transform.position.x, 
                                     sensitivity * -vertDistance + playArea.gameObject.transform.position.y,
                                                                   playArea.gameObject.transform.position.z);

        playArea.gameObject.transform.position = newPos;
    }


    // Use this for initialization
    void Start () 
    {
        if (active == true)
            playArea.transform.position = new Vector3(boardObj.gameObject.transform.position.x, 
                                                                                             0, 
                                                      boardObj.gameObject.transform.position.z - 1f);
	}


    // Update is called once per frame
    void Update()
    {
        currentPos = locomotionController.gameObject.transform.position - playArea.gameObject.transform.position;

        if (locomotionController.triggerPressed)
            pan();

        previousPos = currentPos;
    }


    //Public
    public enum controllers { left, right };
   
    public bool active;

    public controllers locoController ;
    public controllers boardController;

    public float sensitivity;

    public GameObject boardObj;

    public SteamVR_PlayArea playArea;

    public SteamVR_TrackedController locomotionController;


    //Private
    private uint counter;

    private float velocity;

    private Vector3 direction  ;
    private Vector3 currentPos ;
    private Vector3 previousPos;
}