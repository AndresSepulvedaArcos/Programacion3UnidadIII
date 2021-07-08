using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class DotWeenMove : MonoBehaviour
{
    Vector3 v=Vector3.zero;
    public Ease moveEase;
    // Start is called before the first frame update
    void Start()
    {
        transform.DOMove(transform.position + Vector3.down * 5, 1).OnComplete(()=> {
            transform.DOMove(transform.position + (Vector3)Random.insideUnitCircle * 5f, 1f).SetLoops(-1,LoopType.Yoyo).SetEase(moveEase) ;
        });
      

        //   DOTween.To(Getter, Setter, 3, 2f).OnUpdate(CallBack);
        DOTween.To(() => { return v; }, (x) => { v = x; },new Vector3(2,3,4),2f).OnUpdate(()=> { Debug.Log(v); }) ;
    }
 
    

}
