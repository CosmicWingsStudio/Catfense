using UnityEngine;

public class FxSpawner : MonoBehaviour
{
    public static FxSpawner Instance;

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;
    }

    [SerializeField] private GameObject PurchaseEffect;
    [SerializeField] private GameObject BuildingEffect;
    [SerializeField] private GameObject DeathEffect;
    [SerializeField] private GameObject UpgradeEffect;
    [SerializeField] private GameObject PlaceSlotEffect;

    public void SpawnPurchaseEffect(Vector3 position)
    {
        GameObject efx = Instantiate(PurchaseEffect);
        efx.transform.position = position;
    }

    public void SpawnBuildingEffect(Vector3 position)
    {
        GameObject efx = Instantiate(BuildingEffect);
        efx.transform.position = position;
    }

    public void SpawnDeathEffect(Vector3 position)
    {
        GameObject efx = Instantiate(DeathEffect);
        efx.transform.position = position;
    }

    public void SpawnUpgradeEffect(Vector3 position)
    {
        GameObject efx = Instantiate(UpgradeEffect);
        efx.transform.position = position;
    }
    public void SpawnPlaceSlotEffect(Vector3 position)
    {
        var efxPos = position;
        efxPos.y -= 0.9f;
        GameObject efx = Instantiate(PlaceSlotEffect);
        efx.transform.position = efxPos;
    }
}
