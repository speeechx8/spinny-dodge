using UnityEngine;

public class RotateGround : MonoBehaviour
{

    [SerializeField] private float turnSpeed = 5f;

    private Vector3 startRotation;
    private bool isRotating;

    void Start()
    {
        startRotation = transform.eulerAngles;
    }

    void Update()
    {
        if (isRotating)
        {
            transform.Rotate(Vector3.left * Time.deltaTime * turnSpeed);
        }
    }

    public void IncreaseSpeed()
    {
        turnSpeed++;
    }

    public void ToggleRotation()
    {
        isRotating = !isRotating;
    }

    public void ResetRotation()
    {
        transform.eulerAngles = startRotation;
    }
}
