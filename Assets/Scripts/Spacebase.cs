//2
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class Spacebase : MonoBehaviourPun
{
    public bool selected;
    GameMaster gm;

    public int tileSpeed;
    public bool hasMoved;

    public float moveSpeed;

    public int playerNumber;

    List<Unit> enemiesInRange = new List<Unit>();
    List<Spacebase> spacebaseInRange = new List<Spacebase>();
    //public bool hasAttacked;

    public GameObject weaponIcon;

    public string vocation1;
    public string vocation2;

    public string spell1;
    public string spell2;
    public string spell3;

    public int minAttackRange;
    public int maxAttackRange;
    public int physicalArmor;
    public int hollyArmor;
    public int demonArmor;
    
    public int health;
    public int attackDamage;
    public int defenseDamage;
    //public int armor;

    public int maxActionPoints;
    public int actionPoints;
    public float actionPointsTimer;

    [TextArea(15,20)]
    public string abilitiesDescription;

    public DamageIcon damageIcon;
    public GameObject deathEffect;

    private Animator camAnim;
    private AudioSource source;

    public bool isKing;

    public GameObject victoryPanel;

    public Transform movePoint;
    public LayerMask Obstacle;

    public Text actionPointsText;

    public Image healthFillImage;

    // public int curHp;
    public int maxHp;
    public int armor;
    public int damage;
    public int counter;
    public int maxMoveDistance;
    public int maxAttackDistance;

    private void Start()
    {
        maxHp = health;
        source = GetComponent<AudioSource>();
        gm = FindObjectOfType<GameMaster>();
        camAnim = Camera.main.GetComponent<Animator>();

        movePoint.parent = null;

        InvokeRepeating("GetActionPoint", actionPointsTimer, actionPointsTimer);
    }

    private void Update()
    {
        if (gm.statsPanel.activeSelf == true)
        {
            if (this.Equals(gm.selectedSpacebase) && this.Equals(gm.viewedSpacebase))
            {
                gm.statsPanel.transform.position = this.transform.position;
            }
        }

        //transform.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.deltaTime);
/*
        if (Vector3.Distance(transform.position, movePoint.position) <= 0.05f && this.Equals(gm.selectedSpacebase) && actionPoints >= 1)
        {
            if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) == 1f && actionPoints >= 1)
            {
                if(!Physics2D.OverlapCircle(gm.selectedSpacebase.movePoint.position + new Vector3(Input.GetAxisRaw("Horizontal"), 0, 0), .2f, Obstacle) && actionPoints >= 1)
                {
                    gm.selectedSpacebase.movePoint.position += new Vector3(Input.GetAxisRaw("Horizontal"), 0, 0);
                    StartCoroutine(Movement());
                }
            } else if (Mathf.Abs(Input.GetAxisRaw("Vertical")) == 1f && actionPoints >= 1)
            {
                if(!Physics2D.OverlapCircle(gm.selectedSpacebase.movePoint.position + new Vector3(0, Input.GetAxisRaw("Vertical"), 0), .2f, Obstacle) && actionPoints >= 1)
                {
                    gm.selectedSpacebase.movePoint.position += new Vector3(0, Input.GetAxisRaw("Vertical"), 0);
                    StartCoroutine(Movement());
                }
            }
        }
*/
    }

    [PunRPC]
    public void setPlayerNumber(int number){
        playerNumber = number;
        healthFillImage.color = playerNumber==1 ? Color.blue : (playerNumber==2 ? Color.red : Color.yellow);
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(1))
        {
            gm.ToggleSpacebaseStatsPanel(this);
        }
    }

    private void OnMouseDown()
    {
        ResetWeaponIcons();
        
        if (selected == true)
        {
            selected = false;
            gm.selectedSpacebase = null;
            gm.ResetTiles();
        } else {
            if (/*playerNumber == gm.playerTurn && GameManager.instance.curPlayer == PlayerController.me*//*PlayerController.me.photonPlayer.IsLocal*/photonView.IsMine) //NO TURNS CHECKPOINT
            {
                if (gm.selectedSpacebase != null)
                {
                    gm.selectedSpacebase.selected = false;
                }

                source.Play();
                selected = true;
                gm.selectedSpacebase = this;

                gm.ResetTiles();
                GetEnemies();
                GetWalkableTiles();
            }
        }

        Collider2D col = Physics2D.OverlapCircle(Camera.main.ScreenToWorldPoint(Input.mousePosition), 0.15f);
        Unit unit = col.GetComponent<Unit>();
        Spacebase spacebase = col.GetComponent<Spacebase>();
        if (gm.selectedUnit != null)
        {
            if (gm.selectedUnit.spacebaseInRange.Contains(spacebase) && gm.selectedUnit.actionPoints >= 2 /*&& gm.selectedSpacebase.hasAttacked == false*/)
            {
                gm.selectedUnit.AttackSpacebase(spacebase);
            }
        }
    }

    void Attack(Unit enemy)
    {
        camAnim.SetTrigger("shake");
        
        //hasAttacked = true;

        int enemyDamage = Mathf.Max(0, attackDamage - enemy.armor);
        int myDamage = Mathf.Max(0, enemy.defenseDamage - armor);

        if (enemyDamage >= 1)
        {
            DamageIcon instance = Instantiate(damageIcon, enemy.transform.position, Quaternion.identity);
            instance.Setup(enemyDamage);
            enemy.health -= enemyDamage;
        }

        if (myDamage >= 1)
        {
            if ((Mathf.Abs(transform.position.x - enemy.transform.position.x) + Mathf.Abs(transform.position.y - enemy.transform.position.y) <= enemy.maxAttackRange) && (Mathf.Abs(transform.position.x - enemy.transform.position.x) + Mathf.Abs(transform.position.y - enemy.transform.position.y) >= enemy.minAttackRange))
            {
                DamageIcon instance = Instantiate(damageIcon, transform.position, Quaternion.identity);
                instance.Setup(myDamage);
                health -= myDamage;
                photonView.RPC("TakeCounterDamage", PlayerController.me.photonPlayer, myDamage);
            }
        }

        if (enemy.health <= 0)
        {
            Instantiate(deathEffect, enemy.transform.position, Quaternion.identity);
            //Destroy(enemy.gameObject);
            GetWalkableTiles();
            gm.RemoveStatsPanel(enemy);
            //GameManager.instance.CheckWinCondition();
            if (enemy.isKing == true)
            {
                {
                    Debug.Log("1ro. Activado");
                    GameManager.instance.photonView.RPC("WinGame", RpcTarget.All, PlayerController.me == GameManager.instance.leftPlayer ? 0 : 1);
                }
            }
        }

        if (health <= 0)
        {
            if (isKing == true)
            {
                Debug.Log("2do. Activado");
                GameManager.instance.photonView.RPC("WinGame", RpcTarget.All, PlayerController.enemy == GameManager.instance.leftPlayer ? 0 : 1);
            }
            
            Instantiate(deathEffect, transform.position, Quaternion.identity);
            gm.ResetTiles();
            gm.RemoveSpacebaseStatsPanel(this);
            //Destroy(this.gameObject);
            //GameManager.instance.CheckWinCondition();
        }

        gm.UpdateStatsPanel();
        enemy.photonView.RPC("TakeDamage", PlayerController.enemy.photonPlayer, attackDamage);
    }

    void AttackSpacebase(Spacebase spacebase)
    {
        camAnim.SetTrigger("shake");
        
        //hasAttacked = true;


        int spacebaseDamage = Mathf.Max(0, attackDamage - spacebase.armor);
        //int myDamage = Mathf.Max(0, enemy.defenseDamage - armor);

        if (spacebaseDamage >= 1)
        {
            DamageIcon instance = Instantiate(damageIcon, spacebase.transform.position, Quaternion.identity);
            instance.Setup(spacebaseDamage);
            spacebase.health -= spacebaseDamage;
        }

        if (spacebase.health <= 0)
        {
            Instantiate(deathEffect, spacebase.transform.position, Quaternion.identity);
            //Destroy(spacebase.gameObject);
            GetWalkableTiles();
            gm.RemoveSpacebaseStatsPanel(spacebase);
            //GameManager.instance.CheckWinCondition();
            
            //GameManager.instance.photonView.RPC("WinGame", RpcTarget.All, PlayerController.me == GameManager.instance.leftPlayer ? 1 : 0);
        }

        gm.UpdateStatsPanel();
        spacebase.photonView.RPC("TakeDamage", PlayerController.enemy.photonPlayer, attackDamage);
    }

    void GetWalkableTiles()
    {
        //tileSpeed = actionPoints;

        if (hasMoved == true)
        {
            return;
        }

        foreach (Tile tile in FindObjectsOfType<Tile>())
        {
            if (Mathf.Abs(transform.position.x - tile.transform.position.x) + Mathf.Abs(transform.position.y - tile.transform.position.y) <= tileSpeed)
            {
                if (tile.IsClear() == true)
                {
                    tile.Highlight();
                }
            }
        }
    }

    void GetEnemies()
    {
        enemiesInRange.Clear();
        spacebaseInRange.Clear();

        foreach (Unit unit in FindObjectsOfType<Unit>())
        {
            if ((Mathf.Abs(transform.position.x - unit.transform.position.x) + Mathf.Abs(transform.position.y - unit.transform.position.y) <= maxAttackRange) && (Mathf.Abs(transform.position.x - unit.transform.position.x) + Mathf.Abs(transform.position.y - unit.transform.position.y) >= minAttackRange))
            {
                if (/*unit.playerNumber != gm.playerTurn*/playerNumber != unit.playerNumber /*&& hasAttacked == false*/) //NO TURNS CHECKPOINT
                {
                    enemiesInRange.Add(unit);
                    unit.weaponIcon.SetActive(true);
                }
            }
        }

        foreach (Spacebase spacebase in FindObjectsOfType<Spacebase>())
        {
            if ((Mathf.Abs(transform.position.x - spacebase.transform.position.x) + Mathf.Abs(transform.position.y - spacebase.transform.position.y) <= maxAttackRange) && (Mathf.Abs(transform.position.x - spacebase.transform.position.x) + Mathf.Abs(transform.position.y - spacebase.transform.position.y) >= minAttackRange))
            {
                if (/*unit.playerNumber != gm.playerTurn*/playerNumber != spacebase.playerNumber /*&& hasAttacked == false*/) //NO TURNS CHECKPOINT
                {
                    spacebaseInRange.Add(spacebase);
                    spacebase.weaponIcon.SetActive(true);
                }
            }
        }
    }

    public void ResetWeaponIcons()
    {
        foreach (Unit unit in FindObjectsOfType<Unit>())
        {
            unit.weaponIcon.SetActive(false);
        }
    }

    public void Move(Vector2 tilePos)
    {
        gm.ResetTiles();
        StartCoroutine(StartMovement(tilePos));
    }

    IEnumerator StartMovement(Vector2 tilePos)
    {
        
        while(transform.position.x != tilePos.x)
        {
            //if(!Physics2D.OverlapCircle(movePoint.position + new Vector3(tilePos.x, transform.position.y, 0), .2f, whatStopsMovement))
            //{
                //movePoint.position += new Vector3(tilePos.x, transform.position.y, 0);
                transform.position = Vector2.MoveTowards(transform.position, new Vector2(tilePos.x, transform.position.y), moveSpeed * Time.deltaTime);
                //transform.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.deltaTime);
                yield return null;
            //}
        }

        while(transform.position.y != tilePos.y)
        {
            //if(!Physics2D.OverlapCircle(movePoint.position + new Vector3(transform.position.x, tilePos.y, 0), .2f, whatStopsMovement))
            //{
                //movePoint.position += new Vector3(transform.position.x, tilePos.y, 0);
                transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x, tilePos.y), moveSpeed * Time.deltaTime);
                //transform.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.deltaTime);
                yield return null;
            //}
        }
        
        hasMoved = true;
        ResetWeaponIcons();
        GetEnemies();
        gm.MoveSpacebaseStatsPanel(this);
    }

    IEnumerator Movement()
    {
        while(transform.position != movePoint.position)
        {
            transform.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.deltaTime);
            yield return null;
        }

        actionPoints -= 1;
        UpdateActionPointsText();
        ResetWeaponIcons();
        GetEnemies();
        gm.MoveSpacebaseStatsPanel(this);
    }

    void GetActionPoint()
    {
        if (actionPoints < maxActionPoints)
        {
            actionPoints += 1;
            UpdateActionPointsText();
        } else
        {
            return;
        }
    }

    public void UpdateActionPointsText()
    {
            actionPointsText.text = actionPoints.ToString();
    }

    [PunRPC]
    void Initialize (bool isMine)
    {

        if(isMine)
            PlayerController.me.spacebases.Add(this);
        else
            GameManager.instance.GetOtherPlayer(PlayerController.me).spacebases.Add(this);
        
        healthFillImage.fillAmount = 1.0f;

        // set sprite
        //spriteVisual.sprite = transform.position.x < 0 ? leftPlayerSprite : rightPlayerSprite;
    }

    public bool CanSelectSpacebase ()
    {
        return true;
    }

    public bool CanMoveSpacebase (Vector3 movePos)
    {
        if(Vector3.Distance(transform.position, movePos) <= maxMoveDistance)
            return true;
        else
            return false;
    }

    public void MoveSpacebase (Vector3 targetPos)
    {
        return;
    }

    [PunRPC]
    void TakeDamage (int damage)
    {
        health -= Mathf.Max(0, damage - armor);

        if(health <= 0)
            photonView.RPC("Die", RpcTarget.All);
        else
        {
            // update the health UI
            photonView.RPC("UpdateHealthBar", RpcTarget.All, (float)health / (float)maxHp);
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
            PlayerController.enemy.spacebases.Remove(this);
        else
        {
            PlayerController.me.spacebases.Remove(this);

            // check the win condition
            //GameManager.instance.CheckWinCondition();
            //if(gameObject.isKing == true)
            //if(this.isKing == true)
            //if(unit.isKing == true) "SAFE"
            //if(Unit.isKing == true)
            //if(Unit.this.isKing == true)
            //if(this.unit.isKing == true)
                //GameManager.photonView.RPC("WinGame", RpcTarget.All, PlayerController.enemy == leftPlayer ? 0 : 1);
                //GameManager.photonView.RPC("WinGame", RpcTarget.All, PlayerController.enemy == GameManager.leftPlayer ? 0 : 1);
                //GameManager.instance.photonView.RPC("WinGame", RpcTarget.All, PlayerController.enemy == GameManager.instance.leftPlayer ? 0 : 1);
                //GameManager.instance.photonView.RPC("WinGame", RpcTarget.All, PlayerController == GameManager.instance.leftPlayer ? 0 : 1);
                //GameManager.instance.photonView.RPC("WinGame", RpcTarget.All, PlayerController.me == GameManager.instance.leftPlayer ? 0 : 1);


            PhotonNetwork.Destroy(gameObject);
        }
        GameManager.instance.CheckWinCondition();
    }
}