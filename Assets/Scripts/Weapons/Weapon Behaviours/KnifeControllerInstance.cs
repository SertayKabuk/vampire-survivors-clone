using UnityEngine;

public class KnifeControllerInstance : MonoBehaviour
{
    private static KnifeController _instance;

    public static KnifeController Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindAnyObjectByType<KnifeController>();
            }

            return _instance;
        }
    }
}