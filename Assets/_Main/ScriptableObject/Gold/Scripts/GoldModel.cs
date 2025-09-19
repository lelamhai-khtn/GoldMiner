using UnityEngine;

[System.Serializable]
public class GoldModel
{
    public int Id;
    public string Name;
    public Transform Prefab;
    public Sprite Avatar;
    public int Price;
    public float Weight;

    [TextArea(5, 500)]
    public string Description;
}
