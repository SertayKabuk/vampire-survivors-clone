using UnityEngine;

public class PassiveItem : MonoBehaviour
{
    protected PlayerStats player;
    public PassiveItemScriptableObject passiveItemData;

    // Start is called before the first frame update
    void Start()
    {
        player = FindAnyObjectByType<PlayerStats>();
        ApplyModifier();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected virtual void ApplyModifier()
    {
        //apply modifiers
    }
}