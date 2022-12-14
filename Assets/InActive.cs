using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InActive : MonoBehaviour
{
    // Start is called before the first frame update
    IEnumerator Start()
    {
        yield return new WaitForSecondsRealtime(0.3f);
        gameObject.SetActive(false);
    }

}
