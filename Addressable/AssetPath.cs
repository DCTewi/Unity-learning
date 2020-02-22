using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement;
using UnityEngine.ResourceManagement.AsyncOperations;

public class AssetPath : MonoBehaviour
{
    private SpriteRenderer _sprite;
    [SerializeField]
    private string _spritePath;

    private void Awake()
    {
        _spritePath = "lemon";

        _sprite = gameObject.AddComponent<SpriteRenderer>();

        Addressables.LoadAssetAsync<Sprite>(_spritePath).Completed += OnLoaded;
    }

    private void OnLoaded(AsyncOperationHandle<Sprite> obj)
    {
        _sprite.sprite = obj.Result;
    }
}
