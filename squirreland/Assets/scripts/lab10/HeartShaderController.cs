using UnityEngine;

public class HeartShaderController : MonoBehaviour
{
    public Material heartMaterial;
    public float blinkSpeed = 1f;

    /*void Update()
    {
        if (heartMaterial != null)
        {
            heartMaterial.SetFloat("_TimeValue", Time.time * blinkSpeed);
        }
    }*/
    void Update()
    {
        heartMaterial.SetFloat("_TimeValue", Time.time);
    }
}
