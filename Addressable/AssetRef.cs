using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class AssetRef : MonoBehaviour
{
    [SerializeField]
    private AssetReference _assetRef;
    private SpriteRenderer _sprite;

    private void Awake()
    {
        _sprite = gameObject.AddComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        _assetRef.LoadAssetAsync<Sprite>().Completed += OnLoaded;
    }

    //private async void OnEnable()
    //{
    //    _sprite.sprite = await _assetRef.LoadAssetAsync<Sprite>().Task;
    //}

    private void OnDisable()
    {
        // 释放资源
        _assetRef.ReleaseAsset();
    }

    private void OnLoaded(AsyncOperationHandle<Sprite> obj)
    {
        Debug.Log(obj.Result.ToString());
        _sprite.sprite = obj.Result;
    }
}
