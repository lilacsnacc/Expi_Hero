using UnityEngine;

public class CameraController : MonoBehaviour
{
    Camera myCamera;
    Transform lerpTarget;
    LTDescr positionTween;
    LTDescr rotationTween;
    bool continuousFollow;

    void Start() {
      myCamera = this.GetComponent<Camera>();
      transform.parent = null;
    }

    void FixedUpdate() {
        if (!LeanTween.isTweening(gameObject) && continuousFollow)
            transform.position = Vector3.Lerp (transform.position, lerpTarget.position, 5f * Time.deltaTime);
    }

    public LTDescr SetLerpTarget(Transform lerpTarget, float transitionTime = 0.5f, bool continuousFollow = false) {
        bool wasAlreadyTarget = this.lerpTarget == lerpTarget;

        this.lerpTarget = lerpTarget;
        this.continuousFollow = continuousFollow;

        LeanTween.cancel(gameObject);

        return Lerp(wasAlreadyTarget ? 0 : transitionTime);
    }

    LTDescr Lerp(float transitionTime = 0.5f) {
        positionTween = LeanTween.move(gameObject, lerpTarget.position, transitionTime);
        rotationTween = LeanTween.rotate(gameObject, lerpTarget.eulerAngles, transitionTime);

        return rotationTween;
    }

    public Transform GetLerpTarget() {
        return lerpTarget;
    }

    public Camera GetCamera() {
      return myCamera;
    }
}
