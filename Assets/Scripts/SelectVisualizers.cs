using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SelectVisualizers : MonoBehaviour
{
    // Start is called before the first frame update

    public string nameObj;
    public GameObject manager;

    public int counter = 0;

    public List<string> insideObjNames = new List<string>();
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("nameCube"))
        {
            if (counter < 3)
            {
                switch (nameObj)
                {
                    case "position":
                        manager.GetComponent<VisualizeData>().posNames.Add(other.transform.GetComponentInChildren<TextMeshPro>().text);
                        break;
                    case "rotation":
                        manager.GetComponent<VisualizeData>().rotNames.Add(other.transform.GetComponentInChildren<TextMeshPro>().text);
                        break;
                    case "scale":
                        manager.GetComponent<VisualizeData>().scaleNames.Add(other.transform.GetComponentInChildren<TextMeshPro>().text);
                        break;
                    case "color":
                        manager.GetComponent<VisualizeData>().colorNames.Add(other.transform.GetComponentInChildren<TextMeshPro>().text);
                        break;
                    default:
                        break;
                }
            }
            insideObjNames.Add(other.transform.GetComponentInChildren<TextMeshPro>().text);

            counter++;
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.CompareTag("nameCube"))
        {
            switch (nameObj)
            {
                case "position":
                    manager.GetComponent<VisualizeData>().posNames.Remove(other.transform.GetComponentInChildren<TextMeshPro>().text);
                    break;
                case "rotation":
                    manager.GetComponent<VisualizeData>().rotNames.Remove(other.transform.GetComponentInChildren<TextMeshPro>().text);
                    break;
                case "scale":
                    manager.GetComponent<VisualizeData>().scaleNames.Remove(other.transform.GetComponentInChildren<TextMeshPro>().text);
                    break;
                case "color":
                    manager.GetComponent<VisualizeData>().colorNames.Remove(other.transform.GetComponentInChildren<TextMeshPro>().text);
                    break;
                default:
                    break;
            }

            counter--;

            insideObjNames.Remove(other.transform.GetComponentInChildren<TextMeshPro>().text);

            if (counter == 3)
            {
                switch (nameObj)
                {
                    case "position":
                        manager.GetComponent<VisualizeData>().posNames.Clear();
                        manager.GetComponent<VisualizeData>().posNames = insideObjNames;

                        break;
                    case "rotation":

                        manager.GetComponent<VisualizeData>().rotNames.Clear();
                        manager.GetComponent<VisualizeData>().rotNames = insideObjNames;
                        break;
                    case "scale":

                        manager.GetComponent<VisualizeData>().scaleNames.Clear();
                        manager.GetComponent<VisualizeData>().scaleNames = insideObjNames;
                        break;
                    case "color":

                        manager.GetComponent<VisualizeData>().colorNames.Clear();
                        manager.GetComponent<VisualizeData>().colorNames = insideObjNames;
                        break;
                    default:
                        break;
                }
            }
        }


        

    }


}
