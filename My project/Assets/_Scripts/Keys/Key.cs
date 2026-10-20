using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    public int keyType;
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            switch (keyType)
            {
                case 0: 
                    KeyManager.Instance.purpleKeyImage.SetActive(true);
                    Keys.hasPurpleKey = true;
                    break;
                case 1: 
                    KeyManager.Instance.redKeyImage.SetActive(true);
                    Keys.hasRedKey = true;
                    break;
                case 2: 
                    KeyManager.Instance.blueKeyImage.SetActive(true);
                    Keys.hasBlueKey = true;;
                    break;
                default:
                    break;
            }
            Destroy(gameObject);
        }
    }
}
