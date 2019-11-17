using UnityEngine;

public class MouseMonitor : MonoBehaviour
{
    private ITouchListener touchListener;
    private Camera mainCamera;
    private bool colliderEnter;
    public GameObject currentGameObject;
    public GameObject lastChoseGameObject;
    RaycastHit hit;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void LateUpdate()
    {
        RaycastHit hit;
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject.GetComponent<ITouchListener>() != null)
            {
                touchListener = hit.collider.gameObject.GetComponent<ITouchListener>();
                if (hit.collider.gameObject != currentGameObject)
                {
                    colliderEnter = false;
                }
                if (!colliderEnter)
                {
                    touchListener.MouseEnter();
                    currentGameObject = hit.collider.gameObject;
                    colliderEnter = true;
                }
                if (Input.GetButtonDown("Fire1") && colliderEnter)
                {
                    touchListener.MouseDown();
                    lastChoseGameObject = hit.collider.gameObject;
                }


            }
            else if(colliderEnter)
            {
                colliderEnter = false;
                touchListener.MouseExit();
                touchListener = null;
                currentGameObject = null;
            }
            else if (hit.collider==null&&colliderEnter)
            {
                colliderEnter = false;
                touchListener.MouseExit();
                touchListener = null;
                currentGameObject = null;
            }
        }

    }
}
public interface ITouchListener
{
    void MouseEnter();
    void MouseDown();
    void MouseExit();
}