using System;
using System.Collections;
using System.Collections.Generic;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Engines;
using UnityEngine;
using Vector3 = Piglet.GLTF.Math.Vector3;

public class SwitchMannequin : MonoBehaviour
{
    private ClothingModelImporter[] _clothingModelImporter;
    public List<GameObject> _loadedModels = new();
    private int currentIndex = 0;
    //public Transform spwanpoint1;


    private void Awake()
    {
        _clothingModelImporter = GetComponentsInChildren<ClothingModelImporter>();
        StartCoroutine(FillListWithLoadedModels());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            NextObject();
        }
        else if (Input.GetKeyDown(KeyCode.L))
        {
            PreviousObject();
        }
       
    }

    private IEnumerator FillListWithLoadedModels()
    {
        yield return new WaitForSeconds(5f);
        foreach (var clothingModel in _clothingModelImporter)
        {
            _loadedModels.Add(clothingModel.LoadedModel);
        }
    }

    private void SwitchBetweenMannequins()
    {
        for (int i = 0; i < _loadedModels.Count; i++)
        {
            _loadedModels[i].SetActive(i == currentIndex);
            //_loadedModels[i].transform.position = spwanpoint1.transform.position;
        }
    }
    
    private void NextObject()
    {
        currentIndex = (currentIndex + 1) % _loadedModels.Count;
        SwitchBetweenMannequins();
    }

    private void PreviousObject()
    {
        currentIndex = (currentIndex - 1 + _loadedModels.Count) % _loadedModels.Count;
        SwitchBetweenMannequins();
    }
}
