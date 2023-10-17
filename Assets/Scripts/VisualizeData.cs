using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEditor.PlayerSettings;
using static UnityEngine.EventSystems.EventTrigger;

public class VisualizeData : MonoBehaviour
{

    public float posScaling = 10f;
    public float scaleScaling = 1.0f;
    public GameObject dataPointPrefab;
    public GameObject holderObj;

    public bool showRotation = false;
    public bool showScale = false;
    public bool showColor = false;

    private Dictionary<string, Vector2> minMax = new Dictionary<string, Vector2>();
    float[] minTempArr;
    float[] maxTempArr;

    string[] namesArray;

    List<Dictionary<string, object>> data;
    public List<string> columnNames;

    GameObject[] dataPoints;


    //public string[] posNames = { "energy", "key", "loudness" };
    //public string[] rotNames = { "speechiness", "acousticness", "instrumentalness" };
    //public string[] scaleNames = { "liveness", "valence", "tempo" };
    //public string[] colorNames = { "duration_ms", "time_signature", "mode" };

    public List<string> posNames;
    public List<string> rotNames;
    public List<string> scaleNames;
    public List<string> colorNames;


    private void Awake()
    {
        posNames = new List<string>();
        rotNames = new List<string>();
        scaleNames = new List<string>();
        colorNames = new List<string>();


        data = CSVReader.Read("spotify_data");

        columnNames = new List<string>();
        foreach (string key in data[0].Keys)
        {

            columnNames.Add(key);
        }
    }

    void Start()
    {
        dataPoints = new GameObject[data.Count];

        int numCols = columnNames.Count;

        minTempArr = new float[numCols];
        maxTempArr = new float[numCols];


        for (int i = 0; i < numCols; i++)
        {
            minTempArr[i] = float.MaxValue;
            maxTempArr[i] = float.MinValue;
        }

        for (var i = 0; i < data.Count; i++)
        {
            //print("danceability " + data[i]["danceability"] + " " +
            //       "energy " + data[i]["energy"] + " " +
            //       "key " + data[i]["key"] + " " +
            //       "loudness " + data[i]["loudness"] + " " +
            //       "mode " + data[i]["mode"] + " " +
            //       "speechiness " + data[i]["speechiness"] + " " +
            //       "acousticness " + data[i]["acousticness"] + " " +
            //       "instrumentalness " + data[i]["instrumentalness"] + " " +
            //       "liveness " + data[i]["liveness"] + " " +
            //       "valence " + data[i]["valence"] + " " +
            //       "tempo " + data[i]["tempo"] + " " +
            //       "duration_ms " + data[i]["duration_ms"] + " " +
            //       "time_signature " + data[i]["time_signature"] + " " +
            //       "liked " + data[i]["liked"]);

            dataPoints[i] = Instantiate(dataPointPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            dataPoints[i].transform.parent = holderObj.transform;

            for (int j = 0; j < numCols; j++)
            {
                
                if (Convert.ToSingle(data[i][columnNames[j]]) < minTempArr[j])
                {
                    minTempArr[j] = Convert.ToSingle(data[i][columnNames[j]]);
                }

                if (Convert.ToSingle(data[i][columnNames[j]]) > maxTempArr[j])
                {
                    maxTempArr[j] = Convert.ToSingle(data[i][columnNames[j]]);
                }
            }


        }

        //for (int i = 0; i < minTempArr.Length; i++)
        //{
        //    Debug.Log($"For {columnNames[i]} min is {minTempArr[i]} and max is {maxTempArr[i]}" );
        //}

        for (int i = 0; i < columnNames.Count; i++)
        {
            minMax.Add(columnNames[i], new Vector2(minTempArr[i], maxTempArr[i]));
            Debug.Log($"For {columnNames[i]} min is {minTempArr[i]} and max is {maxTempArr[i]}");
        }




        
        //spreadPoints(posNames, rotNames, scaleNames, colorNames);

    }


    void spreadPoints(List<string> posNames, List<string> rotNames = null, List<string> scaleNames = null, List<string> colorNames = null)
    {
        float newMinPos = 0.0f;
        float newMaxPos = 1.0f * posScaling;

        float newMinRot = 0.0f;
        float newMaxRot = 1.0f;

        float newMinScale = 0.1f;
        float newMaxScale = 1.0f * scaleScaling;

        float newMinColor = 0.0f;
        float newMaxColor = 1.0f;

        for (var i = 0; i < data.Count; i++)
        {
            List<float> newPos = new List<float>();
            foreach (string posName in posNames)
            {

                float oldValue = Convert.ToSingle(data[i][posName]);
                float oldMin = minMax[posName][0];
                float oldMax = minMax[posName][1];

                float scaledValue = (oldValue - oldMin) / (oldMax - oldMin) * (newMaxPos - newMinPos) + newMinPos;


                //Debug.Log(scaledValue);
                newPos.Add(scaledValue);


            }

            Quaternion newRot = Quaternion.identity;
            if (showRotation) 
            {
                List<float> newOrient = new List<float>();
                foreach (string rotName in rotNames)
                {

                    float oldValue = Convert.ToSingle(data[i][rotName]);
                    float oldMin = minMax[rotName][0];
                    float oldMax = minMax[rotName][1];

                    float scaledValue = (oldValue - oldMin) / (oldMax - oldMin) * (newMaxRot - newMinRot) + newMinRot;


                    //Debug.Log(scaledValue);
                    newOrient.Add(scaledValue);


                }

                newRot = Quaternion.LookRotation(new Vector3(newOrient[0], newOrient[1], newOrient[2]));
            }


            Vector3 newScale = Vector3.one;
            if (showScale)
            {
                List<float> newScaleL = new List<float>();
                foreach (string scaleName in scaleNames)
                {

                    float oldValue = Convert.ToSingle(data[i][scaleName]);
                    float oldMin = minMax[scaleName][0];
                    float oldMax = minMax[scaleName][1];

                    float scaledValue = (oldValue - oldMin) / (oldMax - oldMin) * (newMaxScale - newMinScale) + newMinScale;


                    //Debug.Log(scaledValue);
                    newScaleL.Add(scaledValue);


                }

                newScale = new Vector3(newScaleL[0], newScaleL[1], newScaleL[2]);
            }



            Color newColor = Color.white;
            if (showColor)
            {
                List<float> newColorL = new List<float>();
                foreach (string colorName in colorNames)
                {

                    float oldValue = Convert.ToSingle(data[i][colorName]);
                    float oldMin = minMax[colorName][0];
                    float oldMax = minMax[colorName][1];

                    float scaledValue = (oldValue - oldMin) / (oldMax - oldMin) * (newMaxColor - newMinColor) + newMinColor;


                    //Debug.Log(scaledValue);
                    newColorL.Add(scaledValue);


                }

                newColor = new Color(newColorL[0], newColorL[1], newColorL[2]);
            }




            dataPoints[i].transform.position = new Vector3(newPos[0], newPos[1], newPos[2]) + holderObj.transform.position;
            dataPoints[i].transform.rotation = newRot;

            dataPoints[i].transform.localScale = newScale;
            dataPoints[i].transform.GetComponent<Renderer>().material.color = newColor;
        }



    }


    // Update is called once per frame
    void Update()
    {
        showRotation = (rotNames.Count == 3) ? true : false;
        showScale = (scaleNames.Count == 3) ? true : false;
        showColor = (colorNames.Count == 3) ? true : false;



        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (posNames.Count == 3) 
            {
                spreadPoints(posNames, rotNames, scaleNames, colorNames);
            }
            else
            {
                Debug.Log("Not enough positions");
            }
            
        }

        //int[] originalArray = { /* Your original array values here */ };
        //int[] mappedArray = new int[originalArray.Length];

        //double oldMin = -10;
        //double oldMax = 16;
        //double newMin = 5;
        //double newMax = 20;

        //for (int i = 0; i < originalArray.Length; i++)
        //{
        //    double oldValue = originalArray[i];
        //    double newValue = (oldValue - oldMin) / (oldMax - oldMin) * (newMax - newMin) + newMin;
        //    mappedArray[i] = (int)newValue; // If you want integers, otherwise use double
        //}
    }
}
