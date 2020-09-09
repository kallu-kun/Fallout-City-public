using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour {

    public GameObject[] objectPool;
    
    public void PoolObjects(GameObject obj, int amountToPool) {
        objectPool = new GameObject[amountToPool];

        for (int i = 0; i < amountToPool; i++) {
            objectPool[i] = (GameObject) Instantiate(obj);
            obj.SetActive(false);
        }
    }

    public GameObject GetObject() {
        GameObject result = null;

        for (int i = 0; i < objectPool.Length; i++) {
            if (!objectPool[i].activeInHierarchy) {
                result = objectPool[i];
            }
        }

        return result;
    }
}
