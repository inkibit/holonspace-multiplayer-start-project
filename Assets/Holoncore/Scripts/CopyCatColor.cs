using System;
using UnityEditor;
using UnityEngine;

public class CopyCatColor : MonoBehaviour
    {
        private bool pressedLastFrame = false;
        private bool clonedLastFrame = false;
        private GameObject prefabToClone;
        private string holonName;

    [SerializeField]
    private Color holonColor;

    public void Awake()
    {
        pressedLastFrame = false;
        clonedLastFrame = false;
        
    }
    public void Start()
    {
        prefabToClone = this.gameObject;
        
    }


    public void Clone()
    {
            
            prefabToClone = this.gameObject;
            GameObject obj = Instantiate(prefabToClone, prefabToClone.transform.position, prefabToClone.transform.rotation);
            obj.transform.localScale = prefabToClone.transform.localScale;
            obj.gameObject.name = holonName + "-" + Time.time;
            obj.layer = LayerMask.NameToLayer("Cloning");
            foreach (var go in obj.GetComponentsInChildren<Transform>())
            {
                go.gameObject.layer = LayerMask.NameToLayer("Cloning");
            }
        }
    }
