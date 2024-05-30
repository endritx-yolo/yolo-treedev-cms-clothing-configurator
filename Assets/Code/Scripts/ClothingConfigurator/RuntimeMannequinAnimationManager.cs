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
    
    
    public AnimationList AnimList => _animList;

    private void Start()
    {
        var importOptions = new GltfImportOptions
        {
            AutoScale = true,
            AutoScaleSize = 1.0f
        };

        _task = RuntimeGltfImporter.GetImportTask(
            @"C:\Users\winwin.al\Desktop\FemaleAnimations.zip",
            importOptions);

        _task.OnCompleted = OnImportComplete;
        
    }

    public void OnImportComplete(GameObject importedModel)
    {
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
                yield return new WaitForSeconds(3f);
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

    private void Update()
    {
        _task.MoveNext();
    }

    public void GetImportedModel(GameObject importedModel)
    {
        
    }
}