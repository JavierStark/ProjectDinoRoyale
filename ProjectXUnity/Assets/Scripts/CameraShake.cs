using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour {

	public static CameraShake instance;

	public Transform camTransform;

	public float shakeDuration = 0f;

	public float shakeAmount = 0.2f;
	public float decreaseFactor = 1.0f;

	Vector3 originalPos;

	void Awake() {
		instance = this;
		camTransform = GetComponent<Transform>();		
	}

	void OnEnable() {
		originalPos = camTransform.localPosition;
	}

	void Update() {
		if (shakeDuration > 0) {
			camTransform.localPosition = originalPos + Random.insideUnitSphere * shakeAmount;

			shakeDuration -= Time.deltaTime * decreaseFactor;
		}
		else {
			shakeDuration = 0f;
			camTransform.localPosition = originalPos;
		}
	}

	public void Shake(float dur) {
		shakeDuration = dur;
    }
}