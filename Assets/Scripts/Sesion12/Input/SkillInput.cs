using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class SkillInput : MonoBehaviour
{
    public delegate void FNotifySkillInput(int SkillIndexInput);
    public static event FNotifySkillInput OnSkillInputPress;

    public Image cooldownBackground;
    public int AbilityIndex;


    private void OnEnable()
    {
        GetComponent<Button>().onClick.AddListener(() => { SkillPress(); });
        PlayerAbility.OnActivateAbility += PlayerAbility_OnActivateAbility;
    }

    private void PlayerAbility_OnActivateAbility(int NotifyAbilityIndex, PlayerAbility playerAbilityReference)
    {
        if (NotifyAbilityIndex != AbilityIndex) return;
        cooldownBackground.fillAmount = 1f;
        cooldownBackground.DOFillAmount(0, playerAbilityReference.cooldownTime);
    }

    private void OnDisable()
    {
        PlayerAbility.OnActivateAbility -= PlayerAbility_OnActivateAbility;

    }
     

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void SkillPress()
    {
        OnSkillInputPress?.Invoke(AbilityIndex);//callback
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            SkillPress();
        }
    }
}
