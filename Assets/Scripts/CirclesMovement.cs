using UnityEngine;

public class CirclesMovement : MonoBehaviour {
    public GameObject leftCircle;
    public GameObject rightCircle;
    public GameObject parentCircle;
    public Camera mainCamera;
    private GameObject container;
    private GameObject lastTag;
    private Touch firstTouch;
    private bool needToGoBack, needToSwap;
    public static float speed;
 
	void Start () {
        lastTag = new GameObject
        {
            tag = "PinkBlue"
        };
        needToGoBack = false;
        needToSwap = false;
        speed = 3.0f;
	}

	void Update () {
        
		if (Input.touchCount > 0)
        {
            if (Input.touches[0].phase == TouchPhase.Began)
            {
                firstTouch = Input.touches[0];
            }
            if (Input.touches[0].position.x - firstTouch.position.x >= 100)
            {
                if (parentCircle.transform.position.x < 3.0f)
                {
                    parentCircle.transform.position = new Vector3(parentCircle.transform.position.x + 0.6f, parentCircle.transform.position.y);

                    if (parentCircle.tag == "PinkBlue")
                    {
                        parentCircle.tag = "ThirdPink";
                    }
                    else if (parentCircle.tag == "BluePink")
                    {
                        parentCircle.tag = "ThirdBlue";
                    }
                }
            }
            if (Input.touches[0].position.x - firstTouch.position.x <= -100)
            {
                if (parentCircle.transform.position.x > -3.0f)
                {
                    parentCircle.transform.position = new Vector3(parentCircle.transform.position.x - 0.6f, parentCircle.transform.position.y);

                    if (parentCircle.tag == "PinkBlue")
                    {
                        parentCircle.tag = "BlueThird";
                    }
                    else if (parentCircle.tag == "BluePink")
                    {
                        parentCircle.tag = "PinkThird";
                    }

                }
            }
            if (Input.touches[0].phase == TouchPhase.Ended && parentCircle.transform.position.x == 0.0f)
            {
                needToSwap = true;
                parentCircle.GetComponent<CircleCollider2D>().enabled = false;
                parentCircle.GetComponent<CircleCollider2D>().radius = 0.8f;
            }
            if (Input.touches[0].phase == TouchPhase.Ended && parentCircle.transform.position.x != 0.0f)
            {
                needToGoBack = true;
            }
            
        }
        if (needToGoBack)
        {
            if (parentCircle.transform.position.x > 0.0f)
            {
                parentCircle.transform.position = new Vector3(parentCircle.transform.position.x - 0.6f, parentCircle.transform.position.y);
                if (parentCircle.tag == "ThirdPink")
                {
                    parentCircle.tag = "PinkBlue";
                }
                else if (parentCircle.tag == "ThirdBlue")
                {
                    parentCircle.tag = "BluePink";
                }
            }
            if (parentCircle.transform.position.x < 0.0f)
            {
                parentCircle.transform.position = new Vector3(parentCircle.transform.position.x + 0.6f, parentCircle.transform.position.y);
                if (parentCircle.tag == "PinkThird")
                {
                    parentCircle.tag = "BluePink";
                }
                else if (parentCircle.tag == "BlueThird")
                {
                    parentCircle.tag = "PinkBlue";
                }
            }
            if (parentCircle.transform.position.x == 0.0f)
            {
                needToGoBack = false;
            }
        }
        if (needToSwap)
        {
            if (leftCircle.transform.position.x < 1.5f)
            {
                if (parentCircle.tag != "Swapping")
                {
                    lastTag.tag = parentCircle.tag;
                }
                parentCircle.tag = "Swapping";
                leftCircle.transform.position = new Vector3(leftCircle.transform.position.x + 0.6f, leftCircle.transform.position.y);
                rightCircle.transform.position = new Vector3(rightCircle.transform.position.x - 0.6f, rightCircle.transform.position.y);
            }
            else
            {
                container = leftCircle;
                leftCircle = rightCircle;
                rightCircle = container;
                if (lastTag.tag == "PinkBlue")
                {
                    parentCircle.tag = "BluePink";
                } else
                {
                    parentCircle.tag = "PinkBlue";
                }
                needToSwap = false;
                parentCircle.GetComponent<CircleCollider2D>().enabled = false;
                parentCircle.GetComponent<CircleCollider2D>().radius = 0.4f;
            }
        }
        parentCircle.transform.position = new Vector3(parentCircle.transform.position.x, parentCircle.transform.position.y + speed * Time.deltaTime);
        mainCamera.transform.position = new Vector3(mainCamera.transform.position.x, mainCamera.transform.position.y + speed * Time.deltaTime, -10);
        speed += 0.001f;
    }
}
