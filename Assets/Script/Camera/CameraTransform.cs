using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraTransform : MonoBehaviour
{
    [Header("Zoom Settings"), SerializeField, Range(1, 20)]
    private float ZoomSpeed;
    [SerializeField, Range(10, 50)]
    private float MaxZoom;
    [SerializeField, Range(3, 10)]
    private float MinZoom;
    [Min(0)]
    public float scrollValue = 0;

    [Header("Rotation Settings"), SerializeField, Range(1, 100)]
    private float rotationYSpeed;
    [Header("Change Position Settings"), SerializeField, Range(1, 100)]
    private float positionSpeed;
    private float myPositionSpeed;
    [SerializeField]
    private float trashHold;
    private float xAxis;

    [SerializeField, Header("Mouse Position")]
    private float MouseX;
    [SerializeField]
    private float MouseY;
    private float ScreenEdgeBorderThickness = 5.0f;

    private Camera cameraMain;

    private void Awake()
    {
        cameraMain = Camera.main;
    }

    private void Start()
    {
        scrollValue = MinZoom;
        myPositionSpeed = positionSpeed;
    }

    private void LateUpdate()
    {
        if (Input.mouseScrollDelta.y != 0)
            ZoomChange(Input.mouseScrollDelta.y);

        MouseX = Input.mousePosition.x;
        MouseY = Input.mousePosition.y;
        if (Input.GetMouseButton(1))
        {
            ChangeRotationCamera();
            ChangeCameraPosition();
        }
    }

    private void ZoomChange(float ScrollDelta)
    {
        float yCamera = cameraMain.transform.position.y;
        if (ScrollDelta > 0 && scrollValue <= MaxZoom)
        {
            yCamera += ScrollDelta * (ZoomSpeed * Time.deltaTime);
        }
        else if (ScrollDelta < 0 && scrollValue >= MinZoom)
        {
            yCamera -= Mathf.Abs(ScrollDelta) * (ZoomSpeed * Time.deltaTime);
        }
        scrollValue = yCamera;
        cameraMain.transform.position = new Vector3(Camera.main.transform.position.x, yCamera, Camera.main.transform.position.z);

    }

    private void ChangeRotationCamera()
    {
       // transform.Translate(new Vector3(MouseX, MouseY, 0f).normalized * Time.deltaTime);
        if (xAxis < Input.mousePosition.x)
        {
            cameraMain.transform.Rotate(Vector3.up, rotationYSpeed * Time.deltaTime, Space.World);

            xAxis = Input.mousePosition.x;
        }
        else if (xAxis > Input.mousePosition.x)
        {
            cameraMain.transform.Rotate(Vector3.up, -1 * (rotationYSpeed * Time.deltaTime), Space.World);
            xAxis = Input.mousePosition.x;
        }
        
    }
    private void ChangeCameraPosition()
    {
        Vector3 panMovement = Vector3.zero;
        if (Input.GetKey(KeyCode.W) || Input.mousePosition.y >= Screen.height - ScreenEdgeBorderThickness)
        {
            panMovement += Vector3.up;
            panMovement += Vector3.forward;
        }
        if (Input.GetKey(KeyCode.S) || Input.mousePosition.y <= ScreenEdgeBorderThickness)
        {
            panMovement += Vector3.down;
            panMovement += Vector3.back;
        }
        
        if (Input.GetKey(KeyCode.A) || Input.mousePosition.x <= ScreenEdgeBorderThickness)
        {
            panMovement += Vector3.left*2;
        }
        if (Input.GetKey(KeyCode.D) || Input.mousePosition.x >= Screen.width - ScreenEdgeBorderThickness)
        {
            panMovement += Vector3.right * 2;
        }
        if (panMovement != Vector3.zero)
        {

           myPositionSpeed += positionSpeed * Time.deltaTime / 10;
        }
        else
        {
            myPositionSpeed = positionSpeed;
        }
        transform.Translate(panMovement.normalized* myPositionSpeed * Time.deltaTime,Space.Self);
    }

}
