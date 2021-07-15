using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.AddressableAssets;
public class AsyncLoad : MonoBehaviour
{
    public AssetReference Garza;
    private void OnEnable()
    {
        Addressables.LoadAssetAsync<Sprite>(Garza).Completed += AsyncLoad_Completed;

    }

    private void AsyncLoad_Completed(UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle<Sprite> obj)
    {
        GetComponent<SpriteRenderer>().sprite = obj.Result;

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
   
}
