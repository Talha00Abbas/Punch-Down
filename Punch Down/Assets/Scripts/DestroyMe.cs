using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyMe : MonoBehaviour
{
    public float destroyTime = 11f;
    // Update is called once per frame
    void Update()
    {
        Debug.Log("Destroyed" + name);
        Destroy(this.gameObject, destroyTime);
    }
}
