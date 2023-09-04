using UnityEngine;

[CreateAssetMenu(fileName = "New Quest", menuName = "Quest System/Quest")]
public class Quest : ScriptableObject
{
    public enum QuestType
    {
        NpcToNpc,
        Kill,
        Gather
    }

    public QuestType questType;
    public GameObject turnInNPC;
    public int currencyReward;

    public enum ItemRewardType
    {
        PredefinedItems,
        ChooseItems
    }

    public ItemRewardType itemRewardType;

    // Use UnityEngine.Object instead of Item
    public UnityEngine.Object[] predefinedItemRewards; // Predefined items

    public int itemRewardCount; // Number of items to choose if itemRewardType is ChooseItems

    public Objective[] objectives;

    [System.Serializable]
    public class Objective
    {
        public enum ObjectiveType
        {
            Kill,
            Gather
        }

        public ObjectiveType type;
        public int targetID; // NPC ID for Kill, Item ID for Gather
        public int targetAmount;
    }
}
