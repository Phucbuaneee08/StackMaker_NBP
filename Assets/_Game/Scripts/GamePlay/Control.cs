using UnityEngine;

public class Control : MonoBehaviour
{
    [SerializeField] private LayerMask layerStack;
    [SerializeField] private float rayLength;
    //[SerializeField] private GameObject smokeVFX;
    private Transform finalStack;
    private Direction direction;

    private bool isSwiping = false;
    private bool isMoving;
    private bool canMove;

    private float swipeAngle;
    private float speed;

    private Vector3 offset;
    private Vector2 touchStartPos;
    private Vector2 touchEndPos;


    public void OnInit()
    {
        offset = new Vector3(0, 0.5f, 0);
        rayLength = 1f;
        speed = 10f;
        isMoving = false;
    }
    void Start()
    {
        OnInit();
    }
    void Update()
    {
        if (!GameManager.Instance.IsState(GameState.GamePlay)) return;

        if (isMoving && finalStack!=null) return;
        if (Input.GetMouseButtonDown(0))
        {
            isSwiping = true;
            touchStartPos = Input.mousePosition;
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (isSwiping)
            {
                touchEndPos = Input.mousePosition;
                isMoving = true;
                CalculateDirection();
                RotatePlayer();
                CheckFinalStack(transform);
            }

            isSwiping = false;
        }
    }
    private void CalculateDirection()
    {
        Vector2 swipeDirection = touchEndPos - touchStartPos;
        swipeAngle = Mathf.Atan2(touchEndPos.y - touchStartPos.y, touchEndPos.x - touchStartPos.x);
        swipeAngle = swipeAngle * 180 / Mathf.PI;

        //Debug.Log("right");
        if (swipeAngle >= -45 && swipeAngle <= 45)
        {
            direction = Direction.Right;
        }
        //Debug.Log("foward");
        if (swipeAngle >= 45 && swipeAngle <= 135)
        {
            direction = Direction.Forward;
        }
        //Debug.Log("backward");
        if (swipeAngle <= -45 && swipeAngle >= -135)
        {

            direction = Direction.Backward;
        }
        //Debug.Log("left");
        if (swipeAngle >= 135 || swipeAngle <= -135)
        {
            direction = Direction.Left;
        }
    }
    private void RotatePlayer()
    {
        switch (direction)
        {
            case Direction.Left:
                transform.rotation = Quaternion.Euler(0, -90, 0);
                break;
            case Direction.Right:
                transform.rotation = Quaternion.Euler(0, 90, 0);
                break;
            case Direction.Forward:
                transform.rotation = Quaternion.Euler(0, 0, 0);
                break;
            case Direction.Backward:
                transform.rotation = Quaternion.Euler(0, 180, 0);
                break;
        }
    }

    private void FixedUpdate()
    {
        if ( isMoving && finalStack!=null && GameManager.Instance.IsState(GameState.GamePlay))
        {
            MoveTo(finalStack);
            CheckDistance(transform, finalStack);
        }
    }
    private void CheckDistance(Transform objectA, Transform objectB)
    {
        if (Vector3.Distance(objectA.position, objectB.position) < 0.01f)
        {
            isMoving = false;
            //smokeVFX.SetActive(false);
        }

    }
    private void MoveTo(Transform target)
    {
        if (target != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.fixedDeltaTime);
            //smokeVFX.SetActive(true);
        }
    }

    private void CheckFinalStack(Transform currentStack)
    {
        Debug.DrawLine(currentStack.position + offset, currentStack.position + offset + transform.forward * rayLength, Color.red);
        RaycastHit hit;
        if (Physics.Raycast(currentStack.position + offset, transform.forward, out hit, rayLength, layerStack))
        {
            finalStack = hit.collider.transform;
            CheckFinalStack(hit.collider.transform);

        }
        //Ray ray = new Ray(transform.position + offset, transform.forward);
        //RaycastHit[] hits = Physics.RaycastAll(ray, rayLength, layerStack);
        //if (hits.Length > 0) finalStack = hits[hits.Length - 1].collider.transform;

    }
}
