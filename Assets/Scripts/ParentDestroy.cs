using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParentDestroy : MonoBehaviour
{
    // Start is called before the first frame update
    void OnEnable()
    {
        Destroy(gameObject, 5.0f);
    }

}
