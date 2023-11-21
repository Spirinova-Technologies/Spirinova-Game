//2
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

using Assets.HeroEditor.Common.CharacterScripts;

public class Unit2 : MonoBehaviourPun
{
    public int curHp;
    public int maxHp;
    public int armor;
    public int damage;
    public int counter;
    public int maxMoveDistance;
    public int maxAttackDistance;

    public SpriteRenderer spriteVisual;

    [Header("UI")]
    public Image healthFillImage;

    [Header("Sprite")]
    public Sprite leftPlayerSprite;
    public Sprite rightPlayerSprite;

    //public bool usedThisTurn; NO TURNS CHECKPOINT

    Unit unit;
    public Character character;

    void Awake()
    {
        unit = GetComponent<Unit>();
        maxHp = unit.health;
        curHp = unit.health;
        character = GetComponent<Character>();
    }

    [PunRPC]
    void Initialize (bool isMine)
    {

        if(isMine)
            PlayerController.me.units.Add(this);
        else
            GameManager.instance.GetOtherPlayer(PlayerController.me).units.Add(this);
        
        healthFillImage.fillAmount = 1.0f;

        // set sprite
        //spriteVisual.sprite = transform.position.x < 0 ? leftPlayerSprite : rightPlayerSprite;
    }

    public bool CanSelect ()
    {
        return true;
    }

    public bool CanMove (Vector3 movePos)
    {
        if(Vector3.Distance(transform.position, movePos) <= maxMoveDistance)
            return true;
        else
            return false;
    }

    public bool CanAttack (Vector3 attackPos)
    {
        Debug.Log("Test6");
        if(Vector3.Distance(transform.position, attackPos) <= maxAttackDistance)
            return true;
        else
            return false;
    }

    public void Move (Vector3 targetPos)
    {
        return;
    }

    public void Attack (Unit2 unitToAttack)
    {
        unitToAttack.photonView.RPC("TakeDamage", PlayerController.enemy.photonPlayer, damage);
    }

    public void AttackSpacebase (Spacebase spacebaseToAttack)
    {
        spacebaseToAttack.photonView.RPC("TakeDamage", PlayerController.enemy.photonPlayer, damage);
    }

    [PunRPC]
    void Heal (int damage)
    {   
        curHp += damage; // se le quito "- armor"
        photonView.RPC("UpdateHealthBar", RpcTarget.All, (float)curHp / (float)maxHp);
    }

    [PunRPC]
    void TakeDamage (int damage)
    {   
        curHp -= damage; // se le quito "- armor"

        if(curHp <= 0)
        {
            Debug.Log("Take Damage Die test");
            photonView.RPC("Die", RpcTarget.All);
        }
        else
        {
            // update the health UI
            photonView.RPC("UpdateHealthBar", RpcTarget.All, (float)curHp / (float)maxHp);
        }
    }

    [PunRPC]
    void TakeCounterDamage (int counter)
    {
        //curHp = Mathf.Max(0, counter - armor);
        curHp = Mathf.Max(0, curHp - counter);

        if(curHp <= 0)
            photonView.RPC("Die", RpcTarget.All);
        else
        {
            // update the health UI
            photonView.RPC("UpdateHealthBar", RpcTarget.All, (float)curHp / (float)maxHp);
        }
    }

    [PunRPC]
    void UpdateHealthBar (float fillAmount)
    {
        healthFillImage.fillAmount = fillAmount;
    }

    [PunRPC]
    void Die ()
    {
        if(!photonView.IsMine)
        {
            PlayerController.enemy.units.Remove(this);
        } else
        {
            this.photonView.RPC("DeadAnim", RpcTarget.All);
            PlayerController.me.units.Remove(this);
            StartCoroutine(DestroyDeadUnit(gameObject, 1.5f));
        }
        Debug.Log("Die called");
        GameManager.instance.CheckWinCondition();
    }

    [PunRPC]
    public void DeadAnim()
    {
        character.Animator.SetBool("DieBack", true);
    }

    IEnumerator DestroyDeadUnit(GameObject gameObject, float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        PhotonNetwork.Destroy(gameObject);
    }
}
