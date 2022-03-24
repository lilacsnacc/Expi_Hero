using UnityEngine;

public class FollowParent : MonoBehaviour {
    Transform followTarget;
    Vector3 offset;
    
    void Start() {
        offset = transform.position - transform.parent.position;
        followTarget = transform.parent;
        transform.parent = null;
    }

    void Update() {
        transform.position = followTarget.position + offset;
    }
}
