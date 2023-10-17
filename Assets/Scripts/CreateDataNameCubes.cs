using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CreateDataNameCubes : MonoBehaviour
{

    public GameObject cubePrefab;

    GameObject[] menuCubes;
    // Start is called before the first frame update
    void Start()
    {
        List<string> cubeNames = GetComponent<VisualizeData>().columnNames;

        menuCubes = new GameObject[cubeNames.Count];

        for (int i = 0; i < cubeNames.Count; i++)
        {
            menuCubes[i] = Instantiate(cubePrefab, new Vector3(Random.Range(3f,5f), 1f, Random.Range(3f, 5f)), Quaternion.identity);
            menuCubes[i].GetComponentInChildren<TextMeshPro>().text = cubeNames[i];

        }
    }

}
