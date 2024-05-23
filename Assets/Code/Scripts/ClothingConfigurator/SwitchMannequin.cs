using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchMannequin : MonoBehaviour
{
    private ClothingModelImporter[] _clothingModelImporter;
    public List<GameObject> _loadedModels = new();
    private GameObject _selectedModel;
    private int currentIndex = 0;


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
        else if(Input.GetKeyDown(KeyCode.M))
        {
            EnableAllMannequins();
        }
    }

    private IEnumerator FillListWithLoadedModels()
    {
        yield return new WaitForSeconds(7f);
        foreach (var clothingModel in _clothingModelImporter)
        {
            _loadedModels.Add(clothingModel.LoadedModel);
        }
        
    }


    private void EnableAllMannequins()
    {
        for (var i = 0; i < _loadedModels.Count; i++)
        {
            
            foreach (var loadedModel in _loadedModels) _loadedModels[i].SetActive(loadedModel);
        }
    }
    
    private void SwitchBetweenMannequins()
    {
        for (var i = 0; i < _loadedModels.Count; i++)
        {
            _loadedModels[i].SetActive(i == currentIndex);
           //_loadedModels[i].transform.position = _initialPositions[i];
        }
    }
    
   
    private void NextObject()
    {
        currentIndex = (currentIndex + 1) % _loadedModels.Count;
        SwitchBetweenMannequins();
        Debug.Log("Next");
    }

    private void PreviousObject()
    {
        currentIndex = (currentIndex - 1 + _loadedModels.Count) % _loadedModels.Count;
        SwitchBetweenMannequins();
        Debug.Log("Previous");
    }
}
