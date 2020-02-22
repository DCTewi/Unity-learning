using UnityEngine;
using UnityEngine.AddressableAssets;

public class AssetAync : MonoBehaviour
{
    private SpriteRenderer _sprite;

    [SerializeField] private string _spritePath;
    [SerializeField] private Sprite _breadSprite;

    private void Awake()
    {
        _spritePath = "bread";

        _sprite = gameObject.AddComponent<SpriteRenderer>();
    }

    private async void OnEnable()
    {
        _breadSprite = await Addressables.LoadAssetAsync<Sprite>(_spritePath).Task;
        _sprite.sprite = _breadSprite;
    }

    private void OnDisable()
    {
        Debug.Log("Released!");
        // Seems not work:
        Addressables.Release(_breadSprite);

        // If need (usually called at Loading scene):
        // Resources.UnloadUnusedAssets();
    }
}
