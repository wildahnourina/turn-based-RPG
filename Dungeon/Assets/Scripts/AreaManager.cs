using Unity.Cinemachine;
using UnityEngine;
using System.Linq;

public class AreaManager : MonoBehaviour
{
    public static AreaManager instance;
    public Area currentArea { get; private set; }

    [SerializeField] private CinemachineCamera cam;
    private CinemachineConfiner2D confiner;

    private Area returnArea;
    private Vector3 returnPosition;

    private void Awake()
    {
        instance = this;

        confiner = cam.GetComponent<CinemachineConfiner2D>();
        Area[] areas = GetComponentsInChildren<Area>(true);
        currentArea = areas.First(a => a.gameObject.activeSelf);
    }

    public void EnterArea(Area targetArea, Transform player, Vector3 spawnPosition)
    {
        currentArea.gameObject.SetActive(false);
        targetArea.gameObject.SetActive(true);
        currentArea = targetArea;

        player.position = spawnPosition;

        confiner.BoundingShape2D = targetArea.cameraBounds;
        confiner.InvalidateBoundingShapeCache();

        cam.Lens.OrthographicSize = targetArea.orthographicSize;

        cam.ForceCameraPosition(player.position, Quaternion.identity);
        cam.PreviousStateIsValid = false;
    }

    public void SaveReturnPoint(Vector2 position)
    {
        returnArea = currentArea;
        returnPosition = position;
    }

    public void ReturnToSavedPoint(Transform player) => EnterArea(returnArea, player, returnPosition);
}