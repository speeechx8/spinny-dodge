using UnityEngine;

public class Swipe : MonoBehaviour
{
    private bool tap;
    private bool swipeLeft;
    private bool swipeRight;
    private bool swipeUp;
    private bool swipeDown;

    private float dragDistance;

    private Vector3 startTouch;
    private Vector3 endTouch;

    private GameObject playerObject;
    private Player player;

    public bool SwipeLeft { get { return swipeLeft; } }
    public bool SwipeRight { get { return swipeRight; } }
    public bool SwipeUp { get { return swipeUp; } }
    public bool SwipeDown { get { return swipeDown; } }

    void Start()
    {
        dragDistance = Screen.width * 15 / 100;
    }

    void Update()
    {
        tap = false;
        swipeLeft = false;
        swipeRight = false;
        swipeUp = false;
        swipeDown = false;

        #region Standalone Input

        if (Input.GetMouseButtonDown(0)) // User has mouse button pressed
        {
            Debug.Log("[SWIPE] Touch begin");
            startTouch = Input.mousePosition;
            endTouch = Input.mousePosition;
        }
        else if (Input.GetMouseButton(0)) // Update the last position based on where they moved
        {
            endTouch = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0)) // User released mouse button
        {
            Debug.Log("[SWIPE] Touch finish");
            endTouch = Input.mousePosition;

            CalculateDrag();
            Reset();
        }

        #endregion

        #region Mobile Input

        if (Input.touchCount == 1) // User is touching screen with one touch
        {
            Touch _touch = Input.GetTouch(0); // Get the touch
            if (_touch.phase == TouchPhase.Began) // Check for the first touch
            {
                Debug.Log("[SWIPE] Touch begin");
                startTouch = _touch.position;
                endTouch = _touch.position;
            }
            else if (_touch.phase == TouchPhase.Moved) // Update the last position based on where they moved
            {
                endTouch = _touch.position;
            }
            else if (_touch.phase == TouchPhase.Ended) // Check if the finger is removed from the screen
            {
                Debug.Log("[SWIPE] Touch finish");
                endTouch = _touch.position;

                CalculateDrag();
                Reset();
            }
        }

        #endregion

        #region Old calculate drag
        // // Calculate distance being dragged
        // swipeDelta = Vector2.zero;
        // if (isDragging)
        // {
        //     Debug.Log("[SWIPE] Beginning position: " + startTouch);
        //     Debug.Log("[SWIPE] Dragging");
        //     // Mobile
        //     if (Input.touches.Length > 0)
        //     {
        //         endTouch = Input.touches[0].position;
        //         swipeDelta = endTouch - startTouch;
        //     }
        //     // Standalone
        //     else if (Input.GetMouseButton(0))
        //     {
        //         endTouch = Input.mousePosition;
        //         swipeDelta = endTouch - startTouch;
        //     }
        //     Debug.Log("[SWIPE] End mouse position: " + endTouch);
        //     Debug.Log("[SWIPE] Swipe Delta is: " + swipeDelta);
        // }

        // // Did we cross the deadzone?
        // if (swipeDelta.magnitude > dragDistance)
        // {
        //     Debug.Log("Crossed deadzone");
        //     passedZone = true;
        //     Debug.Log("[SWIPE] Passed Zone: " + passedZone);

        //     // Which direction?
        //     float _x = swipeDelta.x;
        //     float _y = swipeDelta.y;

        //     if (Mathf.Abs(_x) > Mathf.Abs(_y))
        //     {
        //         // Left or right
        //         if (_x < 0)
        //         {
        //             swipeLeft = true;
        //             Debug.Log("Swipe left");

        //             player.MoveLeft();
        //         }
        //         else
        //         {
        //             swipeRight = true;
        //             Debug.Log("Swipe right");

        //             player.MoveRight();
        //         }
        //     }
        //     else
        //     {
        //         // Up or down
        //         if (_y < 0)
        //         {
        //             swipeDown = true;
        //             Debug.Log("Swipe down");
        //         }
        //         else
        //         {
        //             swipeUp = true;
        //             Debug.Log("Swipe up");
        //         }
        //     }
        // }
        #endregion
    }

    void CalculateDrag()
    {
        if (Mathf.Abs(endTouch.x - startTouch.x) > dragDistance || Mathf.Abs(endTouch.y - startTouch.y) > dragDistance)
        {
            // It's a drag
            // Check if vertical or horizontal
            if (Mathf.Abs(endTouch.x - startTouch.x) > Mathf.Abs(endTouch.y - startTouch.y))
            {
                // Horizontal movement is greater than vertical movement
                if (endTouch.x > startTouch.x)
                {
                    Debug.Log("[SWIPE] Swipe right");
                    swipeRight = true;

                    player.MoveRight();
                }
                else
                {
                    Debug.Log("[SWIPE] Swipe left");
                    swipeLeft = true;

                    player.MoveLeft();
                }
            }
            else
            {
                // Vertical movement is greater than horizontal movement
                if (endTouch.y > startTouch.y)
                {
                    Debug.Log("[SWIPE] Swipe up");
                    swipeUp = true;
                }
                else
                {
                    Debug.Log("[SWIPE] Swipe down");
                    swipeDown = true;
                }
            }
        }
        else
        {
            // It's a tap
            // The distance dragged was less than the dragDistance
            Debug.Log("[SWIPE] Tap");
            tap = true;
        }
    }

    void Reset()
    {
        startTouch = Vector3.zero;
        endTouch = Vector3.zero;
    }

    public void GetPlayerReference(GameObject _player)
    {
        playerObject = _player;
        player = playerObject.GetComponent<Player>();
        Debug.Log("[SWIPE] This player reference: " + player);
    }

}
