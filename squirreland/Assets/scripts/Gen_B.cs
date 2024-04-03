using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

public class Gen_B : MonoBehaviour
{
    public GameObject[] objectToSpawn;
    [SerializeField]
    private float[] wait;
    [SerializeField]
    private float[] speed;
    [SerializeField]
    public float varriable_speed = 17f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Create_dots());
    }

    IEnumerator Create_dots()
    {
        for (int i = 0; i < wait.Length; i++)
        {
            yield return new WaitForSeconds(wait[i]);
            foreach (GameObject obj in objectToSpawn)
            {
                if (!obj.activeSelf)
                {
                    obj.transform.position = transform.position;
                    obj.GetComponent<Move_obj>().speed = speed[i]*varriable_speed;
                    obj.SetActive(true);

                    var sr = obj.GetComponent<Image>();
                    Color color = sr.color;
                    color.a = 1f;
                    sr.color = color;

                    break;
                }
            }
         

        }

    }

}
