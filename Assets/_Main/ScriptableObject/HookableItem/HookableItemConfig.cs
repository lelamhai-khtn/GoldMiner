using UnityEngine;

[CreateAssetMenu(fileName = "HookableItemConfig", menuName = "GoldMiner/HookableItemConfig")]
public class HookableItemConfig : ScriptableObject
{
    public string itemName;

    public Sprite sprite;
    public WeightType weightType;

    [Tooltip("Giá trị vàng / điểm")]
    public int value;

    [Tooltip("Hệ số nặng. Càng lớn kéo càng chậm")]
    public float weightFactor = 1f;

    [Tooltip("Có thể kéo được hay không")]
    public bool canBePulled = true;

    [Header("Hiệu ứng")]
    public AudioClip grabSfx;
    public AudioClip draggingSfx;
    public AudioClip dropSfx;
}