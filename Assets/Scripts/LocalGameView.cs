using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Networking;
using System.Text;
using UnityEngine.UI;

public class LocalGameView : MonoBehaviour
{
    public InputField PlayerName;
    public InputField MatchName;

    private string _matchName;
    private float _timeToUpdate;
    private bool _isConnected;
    private const int _maxMatches = 10;
    private List<NetworkBroadcastResult> _matches = new List<NetworkBroadcastResult>();
    private List<GameObject> _currentMatches = new List<GameObject>();
    private NetworkBroadcastResult[] _currentMatchesData = new NetworkBroadcastResult[_maxMatches];

    public Transform ServerListPanel;
    public GameObject MatchButtonTemplate;
    public float DiscoveryUpdatePeriod = 0.5f;

    private void Start ()
    {
        MyNetworkManager.Discovery.Initialize();
        MyNetworkManager.Discovery.StartAsClient();

        for (int i = 0; i < _maxMatches; i++)
        {
            var buttonObject = Instantiate(MatchButtonTemplate, ServerListPanel);
            _currentMatches.Add(buttonObject);
            Debug.Log("has a game");
            var button = buttonObject.GetComponent<Button>();

            int index = i;
            button.onClick.AddListener(() => OnMatchClicked(index));
        }

        MyNetworkManager.onServerConnect += onServerConnect;
    }

    private void onServerConnect(NetworkConnection conn)
    {
        // client connected!
        Debug.Log("We have company");
    }



    private void Update()
    {
        if (!_isConnected)
        {
            _timeToUpdate -= Time.deltaTime;
            if (_timeToUpdate < 0)
            {
                RefreshMatches();

                _timeToUpdate = DiscoveryUpdatePeriod;
            }
        }
    }
    public void CreateMatch()
    {
        MyNetworkManager.Discovery.StopBroadcast();

        MyNetworkManager.Discovery.broadcastData = MatchName.text;
        MyNetworkManager.Discovery.StartAsServer();

        MyNetworkManager.singleton.StartHost();

    }

    private void OnMatchClicked(int index)
    {
        var matchData = _currentMatchesData[index];

        MyNetworkManager.singleton.networkAddress = matchData.serverAddress;
        MyNetworkManager.singleton.StartClient();

        MyNetworkManager.Discovery.StopBroadcast();
        _isConnected = true;

    }

    private void RefreshMatches()
    {
        // filter matches
        _matches.Clear();
        foreach (var match in MyNetworkManager.Discovery.broadcastsReceived.Values)
        {
            if (_matches.Any(item => EqualsArray(item.broadcastData, match.broadcastData)))
            {
                continue;
            }

            //Debug.Log(match.serverAddress);

            _matches.Add(match);
        }

        int i = 0;
        foreach (var match in _matches)
        {
            if (i >= 10)
            {
                break;
            }

            string matchName = Encoding.Unicode.GetString(match.broadcastData);
            //Debug.Log(matchName);

            _currentMatchesData[i] = match;

            _currentMatches[i].SetActive(true);
            _currentMatches[i].GetComponentInChildren<Text>().text =  matchName;
            i++;
        }
        for (; i < _currentMatches.Count; i++)
        {
            _currentMatches[i].SetActive(false);
        }
    }

    private bool EqualsArray(byte[] v1, byte[] v2)
    {
        if(v1.Length != v2.Length)
        {
            return false;
        }

        for(int i = 0; i < v1.Length; i++)
        {
            if (v1[i] != v2[i])
            {
                return false;
            }

        }
        return true;
    }
}
