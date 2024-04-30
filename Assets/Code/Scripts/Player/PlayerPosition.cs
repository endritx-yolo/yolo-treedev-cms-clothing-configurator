using System;
using System.Collections;
using UnityEngine;

public class PlayerPosition : MonoBehaviour
{
    private WaitForSeconds _wait;

    private string _posx = "posx";
    private string _posy = "posy";
    private string _posz = "posz";
    
    private string _rotx = "rotx";
    private string _roty = "roty";
    private string _rotz = "rotz";

    private void Awake() => LoadData();

    private void Start()
    {
        _wait = new WaitForSeconds(.5f);
        StartCoroutine(SaveStats());
    }

    private IEnumerator SaveStats()
    {
        Transform playerTransform = transform;
        while (true)
        {
            Vector3 currPosition = playerTransform.localPosition;
            Vector3 currRotation = playerTransform.localRotation.eulerAngles;

            PlayerPrefs.SetFloat(_posx, currPosition.x);
            PlayerPrefs.SetFloat(_posy, currPosition.y);
            PlayerPrefs.SetFloat(_posz, currPosition.z);

            PlayerPrefs.SetFloat(_rotx, currRotation.x);
            PlayerPrefs.SetFloat(_roty, currRotation.y);
            PlayerPrefs.SetFloat(_rotz, currRotation.z);

            yield return _wait;
            PlayerPrefs.Save();
        }
    }

    private void LoadData()
    {
        if (!PlayerPrefs.HasKey(_posx)) return;

        float xPos = 0f;
        float yPos = 0f;
        float zPos = 0f;
        float xRot = 0f;
        float yRot = 0f;
        float zRot = 0f;

        AssignValue(_posx, out xPos);
        AssignValue(_posy, out yPos);
        AssignValue(_posz, out zPos);
        AssignValue(_rotx, out xRot);
        AssignValue(_roty, out yRot);
        AssignValue(_rotx, out zRot);

        transform.localPosition = new Vector3(xPos, yPos, zPos);
        transform.localRotation = Quaternion.Euler(xRot, yRot, zRot);
    }

    private void AssignValue(string key, out float value)
    {
        if (PlayerPrefs.HasKey(key))
        {
            value = PlayerPrefs.GetFloat(key);
        }
        else
        {
            value = 0f;
        }
    }
}