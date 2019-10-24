using Unity.Collections;
using UnityEngine;

public class CameraTransform : MonoBehaviour
{
    [Header("Zoom Settings"),SerializeField, Range(1, 20)]
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
    [SerializeField]
    private float trashHold;


    [SerializeField, Header("Mouse Position")]
    private float Mouse1X;
    [SerializeField]
    private float Mouse1Y;

    private Camera cameraMain;

    private void Awake()
    {
        cameraMain = Camera.main;
    }

    private void Start()
    {
        scrollValue = MinZoom;
    }

    private void FixedUpdate()
    {
        if (Input.mouseScrollDelta.y != 0)
            ZoomChange(Input.mouseScrollDelta.y);
        if (Input.GetMouseButtonDown(2)|| Input.GetMouseButtonDown(1))
        {
            Mouse1X = Input.mousePosition.x;
            Mouse1Y = Input.mousePosition.y;
        }
        if (Input.GetMouseButton(2))
        {
            ChangeRotationCamera();
        }
        if (Input.GetMouseButton(1))
        {
            ChangeX();
            //ChangeY();
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
        if (Mouse1X < Input.mousePosition.x)
        {
            cameraMain.transform.Rotate(Vector3.up, rotationYSpeed * Time.deltaTime, Space.World);
        }
        else if (Mouse1X > Input.mousePosition.x)
        {
            cameraMain.transform.Rotate(Vector3.up, -1 * (rotationYSpeed * Time.deltaTime), Space.World);
        }
        Mouse1X = Input.mousePosition.x;
    }
    private void ChangeX()
    {
        Vector3 cameraPos = cameraMain.transform.position;
        float xCamera = cameraMain.transform.position.x;
        float zCamera = cameraMain.transform.position.z;
        if (Mouse1X < Input.mousePosition.x)
        {
            cameraPos += (Vector3.left* positionSpeed * Time.deltaTime);
        }
        else if (Mouse1X > Input.mousePosition.x)
        {
            cameraPos += (Vector3.right * positionSpeed * Time.deltaTime);
        }
        if (Mouse1Y > Input.mousePosition.y)
        {
            cameraPos += (Vector3.forward * positionSpeed * Time.deltaTime);
        }
        else if (Mouse1Y < Input.mousePosition.y)
        {
            cameraPos += (Vector3.back * positionSpeed * Time.deltaTime);
        }
        cameraMain.transform.position = cameraPos;
    }
    
}
