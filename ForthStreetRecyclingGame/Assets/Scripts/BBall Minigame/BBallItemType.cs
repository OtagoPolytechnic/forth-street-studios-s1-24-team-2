using UnityEngine;  

public enum BBallItemType
{
    Apple,
    Can
}

// component to hold the enum
public class BBallItem : MonoBehaviour
{
    public BBallItemType itemType;
}