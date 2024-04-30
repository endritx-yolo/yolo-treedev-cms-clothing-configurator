using System;
using HighlightPlus;
using UnityEngine;

public class PlaceholderHighlighter : MonoBehaviour
{
    private HighlightTrigger _highlightTrigger;

    private void Awake() => _highlightTrigger = GetComponent<HighlightTrigger>();

    private void OnEnable() => Interaction.OnAnyUpdateInteractionRayLength += UpdateHighlightDistance;
    private void OnDisable() => Interaction.OnAnyUpdateInteractionRayLength -= UpdateHighlightDistance;
    
    private void UpdateHighlightDistance(float newRayLength) => _highlightTrigger.maxDistance = newRayLength;
}