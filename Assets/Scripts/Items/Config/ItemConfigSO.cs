using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemConfig", menuName = "Entity Config/Item Config")]
public class ItemConfigSO : ScriptableObject
{
    [Tooltip("Initial item type")]
    [SerializeField] private GameItemType _itemType;
    [Tooltip("Initial item animation frames")]
    [SerializeField] private Sprite[] _itemSprites;

    public GameItemType ItemType => _itemType;
    public Sprite[] ItemSprites => _itemSprites;
}
