using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // variable
    bool shakingCam;

    public static CameraController c;

    private void Awake()
    {
        c = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Shake(float duration, float amount, float intensity)
    {
        // if camera is not shaking, then shake the camera
        if (!shakingCam)
            StartCoroutine(ShakeCam(duration, amount, intensity));
    }

    IEnumerator ShakeCam(float dur, float amount, float intensity)
    {
        float t = dur;
        Vector3 originalPos = Camera.main.transform.localPosition;
        Vector3 targetPos = Vector3.zero;
        shakingCam = true;

        while (t > 0.0f)
        {
            if (targetPos == Vector3.zero)
            {
                targetPos = originalPos + (Random.insideUnitSphere * amount);
            }

            // lerp to shake the first point
            Camera.main.transform.localPosition = Vector3.Lerp(Camera.main.transform.localPosition,
                                                                targetPos,
                                                                intensity * Time.deltaTime);

            if (Vector3.Distance(Camera.main.transform.localPosition, targetPos) < 0.02f)
            {
                targetPos = Vector3.zero;
            }

            t -= Time.deltaTime;
            yield return null;
        }

        Camera.main.transform.localPosition = originalPos;  // reset cam postion to original pos
        shakingCam = false;
    }
}
