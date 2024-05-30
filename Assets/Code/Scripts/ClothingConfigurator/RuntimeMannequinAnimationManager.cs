using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Piglet;

public class RuntimeMannequinAnimationManager : MonoBehaviour
{
    private GltfImportTask _task;
    private Animation _anim;
    private AnimationList _animList;
    private bool _isPlayingFirstAnimation = true;
    private int _secondAnimationCount;

    [SerializeField] private GameObject importedModel;

    private ClothingModelImporter _clothingModelImporter;
    public AnimationList AnimList => _animList;


    private void Awake()
    {
        _clothingModelImporter = GetComponent<ClothingModelImporter>();
    }

    private void OnEnable()
    {
        if (_clothingModelImporter != null)
            _clothingModelImporter.OnGetModelReference += ClothingModelImporterOnOnGetModelReference;
    }


    private void ClothingModelImporterOnOnGetModelReference(GameObject modelReference)
    {
        importedModel = modelReference;
        _anim = importedModel.GetComponent<Animation>();
        _animList = importedModel.GetComponent<AnimationList>();

        StartCoroutine(PlayAnimationSequence());
    }


    public IEnumerator PlayAnimationSequence()
    {
        while (true)
        {
            if (_isPlayingFirstAnimation)
            {
                _anim.Play(_animList.Clips[0].name);
                yield return new WaitForSeconds(6f);
                _isPlayingFirstAnimation = false;
            }
            else
            {
                _anim.Play(_animList.Clips[1].name);
                yield return new WaitForSeconds(5f);
                _isPlayingFirstAnimation = true;
                _secondAnimationCount++;
                if (_secondAnimationCount >= 1)
                {
                    break;
                }
            }
        }

        _anim.Play(_animList.Clips[0].name);
    }
}