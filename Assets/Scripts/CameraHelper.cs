using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHelper : MonoBehaviour {
    public float screenshakeMagnitude;
    public float screenshakeInterval;
    public AnimationCurve screenshakeCurve;
    private Vector3 m_CameraPos;

    private Bounds m_CameraBounds;

    void Awake() {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        m_CameraPos = transform.position;
    }

    void Start() {
        Vector3 cameraCenter = transform.position;
        cameraCenter.z = 0;

        Vector3 viewportSize = new Vector3 ( );
        viewportSize.y = GetComponent<Camera> ( ).orthographicSize;
        viewportSize.x = viewportSize.y * ( ( float ) Screen.width / ( float ) Screen.height );
        viewportSize.z = 10;

        m_CameraBounds = new Bounds ( cameraCenter, viewportSize * 2 );
    }

    public bool IsOutside(Bounds bounds, out Vector3 depenetration) {
        Bounds cameraBounds = m_CameraBounds;
        cameraBounds.size -= bounds.size;

        if ( cameraBounds.Contains ( bounds.center ) ) {
            depenetration = Vector3.zero;
            return false;
        }

        depenetration = cameraBounds.ClosestPoint ( bounds.center ) - bounds.center;
        return true;
    }

    public void ShakeScreen(float duration) {
        StopAllCoroutines ( );
        StartCoroutine ( ScreenShakeRoutine ( duration ) );
    }

    IEnumerator ScreenShakeRoutine(float duration) {
        float startTime = Time.time;

        while(Time.time - startTime < duration ) {
            float mag = 1 - ( (Time.time - startTime) / duration );
            Vector3 shakeVector = Vector3.Normalize ( new Vector3 ( Random.Range ( -1f, 1f ), Random.Range ( -1f, 1f ) ) );
            Vector3 shakePos = m_CameraPos + shakeVector * screenshakeCurve.Evaluate(mag) * screenshakeMagnitude;

            float shakeTime = Time.time;
            while(Time.time - shakeTime < screenshakeInterval ) {
                transform.position = Vector3.Lerp ( shakePos, m_CameraPos, ( Time.time - shakeTime ) / screenshakeInterval );
                yield return null;
            }

            yield return null;
        }

        transform.position = m_CameraPos;
    }
}
