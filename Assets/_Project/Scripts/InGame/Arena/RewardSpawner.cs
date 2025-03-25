using UnityEngine;

public class RewardSpawner
{
    public RewardSpawner(Transform moneyTextPosition, Reward rewardPrefab, Transform canvasObject)
    {
        _rewardPrefab = rewardPrefab;
        _moneyTextPosition = moneyTextPosition;
        _canvasObject = canvasObject;
    }

    private Transform _moneyTextPosition;
    private Reward _rewardPrefab;
    private Animator _animator;
    [Zenject.Inject] private WalletHandler _walletHanler;
    private Transform _canvasObject;

    public void SpawnReward(Vector3 rewardSpawnPosition)
    {
        var rewardObject = GameObject.Instantiate(_rewardPrefab, _canvasObject);
        rewardObject.RewardObj.Initialize(_walletHanler, _moneyTextPosition, Random.Range(5, 25));
        rewardObject.RewardObj.ChangeColor(Random.Range(0,5));
        Vector3 newPos = new(rewardSpawnPosition.x + Random.Range(-3, 3), rewardSpawnPosition.y + Random.Range(-3,3), 0);
        rewardObject.transform.position = newPos;
    }
}
