using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;

public class AdsManager : MonoBehaviour, IUnityAdsInitializationListener, IUnityAdsLoadListener, IUnityAdsShowListener
{
    [SerializeField] string _gameID = "5326468";
    [SerializeField] string _adID = "Rewarded_Android";
    [SerializeField] Button _showAdButton;
    private void Start()
    {
        Advertisement.Initialize(_gameID, true, this);
        
    }

    public void OnInitializationComplete()
    {
        Advertisement.Load(_adID, this);
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
    }

    public void OnUnityAdsAdLoaded(string placementId)
    {
        Debug.Log("Ad Loaded: " + _adID);

        if (_adID.Equals(_adID))
        {
            _showAdButton.onClick.AddListener(ShowAd);
        }

    }

    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
    {
    }

    public void OnUnityAdsShowClick(string placementId)
    {
    }

    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {
        //si mi anuncio termino
        if(placementId == _adID)
        {
            if (showCompletionState.Equals(UnityAdsShowCompletionState.COMPLETED))
            {
                Debug.Log("Termino el anuncio");
            }
            else if (showCompletionState.Equals(UnityAdsShowCompletionState.SKIPPED))
            {
                Debug.Log("Skipeo el anuncio, pocas recompensas");
            }
            else
                Debug.Log("Algo fallo");
        }
    }

    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {
    }

    public void OnUnityAdsShowStart(string placementId)
    {
    }

    public void ShowAd()
    {
        Advertisement.Show(_adID, this);
    }

}
