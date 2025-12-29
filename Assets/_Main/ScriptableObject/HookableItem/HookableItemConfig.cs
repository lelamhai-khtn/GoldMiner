using UnityEngine;

[CreateAssetMenu(fileName = "HookableItemConfig", menuName = "GoldMiner/HookableItemConfig")]
public class HookableItemConfig : ScriptableObject
{
    public string itemName;

    public Sprite sprite;
    public WeightType weightType;
    public int value;
    public bool canBePulled = true;
    public float weightFactor = 1f;

    [Header("Sound")]
    public AudioClip grabSfx;
    public AudioClip draggingSfx;
    public AudioClip dropSfx;

    private void OnValidate()
    {
        weightFactor = WeightTable.Factor[weightType];
    }
}