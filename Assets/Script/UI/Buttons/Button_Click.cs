using UnityEngine.UI;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Button))]
public class Button_Click : MonoBehaviour
{
    [SerializeField,Header("Animation Settings")]
    private float animTime = 5f;
    [SerializeField]
    private float coefAnim = 1f;
    private float yourTime;
    private float scale;
    private bool finishHalfAnim =false;


    public void OnClick()
    {

        StartCoroutine(AnimationCoroutine());

    }

    private IEnumerator AnimationCoroutine()
    {
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
                this.transform.localScale = new Vector3(100, 100, 100);
                scale = 0;
                yourTime = 0;
                finishHalfAnim = false;
                Debug.Log("dasd");
                break;
            }
        }
        yield break;
    }
}
