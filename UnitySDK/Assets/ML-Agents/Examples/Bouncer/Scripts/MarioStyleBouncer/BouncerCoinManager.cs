using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAgents;

/// <summary>
/// Goes into the Agent gameObject.
/// </summary>
public class BouncerCoinManager : MonoBehaviour
{
    static BouncerCoinManager _instance;

    #region Attributes
    [System.Serializable]
    public struct Coin { public Transform _coinPositions; public bool _collected; };

    [SerializeField]
    private Coin[] _coinList;

    [SerializeField]
    private Transform _endGoal;

    private Vector3 _forwardProgressDirection;

    private Vector3 _targetCoinPos;
    private int _coinsCollected = 0;
    #endregion

    #region Getters
    static public BouncerCoinManager Instance
    {
        get { return _instance; }
    }

    public Vector3 TargetCoin
    {
        get { return _targetCoinPos; }
    }

    public Vector3 EndGoal
    {
        get { return _endGoal.localPosition; }
    }
    #endregion

    #region Initialisation
    private void Awake()
    {
        _coinsCollected = 0;
        _targetCoinPos = Vector3.zero;
        for (int i = 0; i < _coinList.Length; i++)
        {
            _coinList[i]._collected = false;
        }
        _forwardProgressDirection = _endGoal.localPosition - this.transform.localPosition;    //Forward progress direction
    }

    private void Start()
    {
        FindClosestCoin();
    }
    #endregion

    // Update is called once per frame
    void FixedUpdate()
    {
        gameObject.transform.Rotate(new Vector3(1, 0, 0), 0.5f);
    }

    /// <summary>
    /// Called from the coin that has been collected.
    /// </summary>
    /// <param name="t"></param>
    public void CoinCollected(Transform t)
    {
        ++_coinsCollected;
        for (int i = 0; i < _coinList.Length; i++)
        {
            if (_coinList[i]._coinPositions == t)
            {
                _coinList[i]._collected = true;
                _coinList[i]._coinPositions.gameObject.SetActive(false);
                FindClosestCoin();
                break;
            }
        }
    }

    #region CoinSelection
    /// <summary>
    /// Executed everytime the agent does an action.
    /// Looks for a coin that is between the goal and the agent. If it finds one it sets the target coin to that position. If not it sets the target coin to the end goal position.
    /// </summary>
    public void FindClosestCoin()
    {
        _forwardProgressDirection = _endGoal.localPosition - this.transform.localPosition;    //Forward progress direction
        float tempDistance = 100000000f;
        float smallerDistance = 100000000f;
        int i = 0;
        bool foundOne = false;
        foreach (Coin c in _coinList)
        {
            if (IsPointInBetween(c))
            {
                tempDistance = Vector3.Distance(this.transform.localPosition, c._coinPositions.localPosition);
                if (tempDistance < smallerDistance && !c._collected) { smallerDistance = tempDistance; _targetCoinPos = c._coinPositions.localPosition; foundOne = true; }
            }
            ++i;
        }

        if (!foundOne) _targetCoinPos = _endGoal.localPosition;
    }

    /// <summary>
    /// Looks for the angle between the two vectors: agent->goal | agents->coin. If it's between -90 and 90 degrees then its in front and return true.
    /// </summary>
    /// <param name="c">Coin you want to know if it's in between</param>
    /// <returns></returns>
    private bool IsPointInBetween(Coin c)
    {
        Vector2 temp2DForward = new Vector2(_forwardProgressDirection.x, _forwardProgressDirection.z);
        Vector2 temp2DToCoin = new Vector2(c._coinPositions.localPosition.x - this.transform.localPosition.x, c._coinPositions.localPosition.z - this.transform.localPosition.z);
        float angle = Vector2.Angle(temp2DForward, temp2DToCoin);
        if (angle < 90f && angle > -90) return true;
        return false;
    }
    #endregion

    /// <summary>
    /// Restart of the manager. Called from the bouncer agent when he has to restart.
    /// </summary>
    public void Restart()
    {
        _coinsCollected = 0;
        _targetCoinPos = Vector3.zero;
        for (int i = 0; i < _coinList.Length; i++)
        {
            _coinList[i]._collected = false;
            _coinList[i]._coinPositions.gameObject.SetActive(true);
            FindClosestCoin();
        }
    }

}
