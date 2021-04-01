using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class GUITableExample : MonoBehaviour {
	
    [System.Serializable]
    public class Vegetation
    {
        public GameObject prefab;
        public float minHeight = 0.1f;
        public float maxHeight = 0.2f;
        public float minSlope = 0;
        public float maxSlope = 1;
        public float overlap = 0.01f;
        public float feather = 0.05f;
        public float density = 0.5f;
        public bool remove = false;
    }

    public List<Vegetation> vegetation = new List<Vegetation>()
    {
        new Vegetation()
    };

    public void AddRow()
    {
        vegetation.Add(new Vegetation());
    }

    public void RemoveRow()
    {
        List<Vegetation> keptVegetation = new List<Vegetation>();
        for (int i = 0; i < vegetation.Count; i++)
        {
            if (!vegetation[i].remove)
            {
                keptVegetation.Add(vegetation[i]);
            }
        }
        if (keptVegetation.Count == 0) //don't want to keep any
        {
            keptVegetation.Add(vegetation[0]); //add at least 1
        }
        vegetation = keptVegetation;
    }


    // Use this for initialization
	void Awake () {
        vegetation.Add(new Vegetation());
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
