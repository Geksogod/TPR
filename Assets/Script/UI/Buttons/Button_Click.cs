using UnityEngine.UIElements;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Button))]
public class Button_Click : MonoBehaviour
{
    public enum ButtonType
    {
        ChooseResources
    }
    public ButtonType buttonType;
    [SerializeField,Header("Animation Settings")]
    private float animTime = 5f;
    [SerializeField]
    private float coefAnim = 1f;
    private float yourTime;
    private float scale;
    private bool finishHalfAnim =false;
    public bool clicked = false;
    private Vector3 startScale = Vector3.zero;

    private void Start()
    {
        startScale = this.gameObject.transform.localScale;
    }
    public void OnClick()
    {
        if (!clicked)
        {
            StartCoroutine(AnimationCoroutine());
            switch (buttonType)
            {
                case ButtonType.ChooseResources:
                    EventManager.ChangeCurrentEvent(EventManager.Events.ChooseResources);
                    break;
            }
        }
    }

    private IEnumerator AnimationCoroutine()
    {
        clicked = true;
        while (true)
        {
            if (yourTime <= animTime && !finishHalfAnim)
            {
                yield return new WaitForSeconds(Time.deltaTime);
                scale -= coefAnim * Time.deltaTime;
                this.transform.localScale = new Vector3(this.transform.localScale.x + scale, this.transform.localScale.y + scale, this.transform.localScale.z);
                yourTime += Time.deltaTime;
            }
            else if(yourTime >0)
            {
                if (!finishHalfAnim)
                {
                    finishHalfAnim = true;
                    scale = 0;
                }
                yield return new WaitForSeconds(Time.deltaTime);
                scale += coefAnim * Time.deltaTime;
                this.transform.localScale = new Vector3(this.transform.localScale.x + scale, this.transform.localScale.y + scale, this.transform.localScale.z);
                yourTime -= Time.deltaTime;
            }
            else
            {
                this.transform.localScale = startScale;
                scale = 0;
                yourTime = 0;
                finishHalfAnim = false;
                break;
            }
        }
        clicked = false;
        yield break;
    }
}
