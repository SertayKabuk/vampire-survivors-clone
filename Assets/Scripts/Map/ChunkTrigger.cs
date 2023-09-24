using UnityEngine;

public class ChunkTrigger : MonoBehaviour
{
    MapController mapController;
    public GameObject targetMap;

    // Start is called before the first frame update
    void Start()
    {
        mapController = FindAnyObjectByType<MapController>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag(Enums.Tags.Player.ToString()))
        {
            mapController.currnetChunk = targetMap;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag(Enums.Tags.Player.ToString()) && mapController.currnetChunk == targetMap)
        {
            mapController.currnetChunk = null;
        }
    }
}
