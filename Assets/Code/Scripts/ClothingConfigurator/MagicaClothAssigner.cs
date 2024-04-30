using Lean.Pool;
using MagicaCloth2;
using NaughtyAttributes;
using UnityEngine;
using System.Collections.Generic;

public class MagicaClothAssigner : MonoBehaviour
{
    [SerializeField] private MagicaCloth _trenchCoatCloth;

    [SerializeField] private List<ColliderComponent> _magicaColliderList = new List<ColliderComponent>();

    [SerializeField] private SkinnedMeshRenderer _skinnedMeshRenderer;

    [Button]
    private void TestClothAssign()
    {
        MagicaCloth spawnedCloth = LeanPool.Spawn(_trenchCoatCloth, transform);

        spawnedCloth.SerializeData.sourceRenderers.Clear();
        spawnedCloth.SerializeData.sourceRenderers.Add(_skinnedMeshRenderer);
        
        spawnedCloth.SerializeData.colliderCollisionConstraint.colliderList.Clear();
        spawnedCloth.SerializeData.colliderCollisionConstraint.colliderList = _magicaColliderList;
        spawnedCloth.BuildAndRun();
    }
}
