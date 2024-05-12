using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour

{
    [SerializeField] Transform followingTarget;
    [SerializeField, Range(0f, 1f)] float parallaxSttength = 0.1f;
    [SerializeField] bool disableVeticalParallax;
    Vector3 targetPreviousPosition;
    // Start is called before the first frame update
    void Start()
    {
        if (!followingTarget) {
            followingTarget = Camera.main.transform;
        }
        targetPreviousPosition = followingTarget.position;
    }

    // Update is called once per frame
    void Update()
    {
        var delta = followingTarget.position - targetPreviousPosition;

        if(disableVeticalParallax) {
            delta.y = 0;
        }
        targetPreviousPosition = followingTarget.position;
        transform.position += delta * parallaxSttength;
    }
}
