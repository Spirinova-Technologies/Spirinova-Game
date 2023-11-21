using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

using Assets.HeroEditor.Common.CharacterScripts;

public class Unit : MonoBehaviourPun, IPunObservable
{
    public bool selected;
    GameMaster gm;

    public int tileSpeed;
    public bool hasMoved;

    public float moveSpeed;

    public int playerNumber;

    List<Unit> enemiesInRange = new List<Unit>();
    public List<Spacebase> spacebaseInRange = new List<Spacebase>();
    List<Unit> alliesInAOERange = new List<Unit>();
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
    public int armor;

    public int maxActionPoints;
    public int actionPoints;
    public float actionPointsTimer;
    
    [TextArea(15,20)]
    public string abilitiesDescription;

    public DamageIcon damageIcon;
    public GameObject taunt;
    public GameObject deathEffect;

    // ANIMATIONS
    public GameObject batsCloud; // Fin del Tiempo & Chirrin Chin Chin Animation
    public GameObject bigSplash; // Danza de Lluvia Debuff Animation
    public GameObject blood;
    public GameObject brokenHeart; // Charm Animation
    public GameObject enemyDeathSkull;
    public GameObject pickupDiamond2; // Paralyze Animation
    public GameObject pickupHeart; // Armor Buff
    public GameObject pickupStar; // Inmortalidad & Renacimiento Animation 1/2
    public GameObject sparksHitBSphere; // Lanza Infernal Animation 1/2
    public GameObject wanderingSpirits;
    public GameObject wwExplosionC; // Universe UE Animation
    public GameObject darkMagicAuraA; // Attack Debuff Animation
    public GameObject fireExplosion; // Espada de Fuego & Disparo Explosivo Animation
    public GameObject fireShield; // Attack Buff Animation
    public GameObject hitElectricAGround;
    public GameObject hitElectricCAir; // Trueno UE Animation
    public GameObject hitLightCAir; // Lanza Infernal Animation 2/2
    public GameObject hitSmokePuff;
    public GameObject magicAuraBRunic; // AP Gain Animation
    public GameObject magicAuraDRunic; // Armor Debuff Animation
    public GameObject resurrectionLightCircle; // Grito del Dragon, Inmortalidad & Renacimiento 2/2 Animation
    public GameObject skullExplosion; // Letalidad, Degollar, Desmembrar & Asesinato Animation
    public GameObject auraBubbleC; // Curacion & Curacion Avanzada Animation
    public GameObject bubblesWhirl; // Danza de Lluvia Animation
    public GameObject disruptiveForce; // AP Loss Animation
    public GameObject drillAirHit; // Terremoto Animation
    public GameObject hitBOrange; // Demon Hit Animation
    public GameObject magicHit; // Holly UE Animation
    public GameObject electricityBall;
    public GameObject explosionBSmokeText;
    public GameObject fireworkTrailsGravity; // Taunt Animation
    public GameObject magicalSource;
    public GameObject magicPoof; // Alquimia & Espiritu de GaiaAnimation
    public GameObject magicCircleF; // Furia Eterna Animation
    public GameObject magicCircleH;
    public GameObject magicCircleI;
    public GameObject magicCircleJ;
    public GameObject magicCircleM;
    public GameObject magicCircleN; // Range Buff Animation
    public GameObject magicCircleO; // Liberacion Animation
    public GameObject magicCircleP;
    public GameObject impact01; // Ataque Sonico UE Animation
    public GameObject impact02; // Implosion Nuclear Animation
    public GameObject impact03;
    public GameObject blueFire;
    public GameObject brilliantLoop;
    public GameObject bugholeCircle;
    public GameObject devilAwait;
    public GameObject ghostFire;
    public GameObject recoverHold;
    public GameObject roseFire; // Explosion Estelar Animation
    public GameObject twistSorsery;
    public GameObject unityKeeper;

    private Animator camAnim;
    private AudioSource source;

    public bool isKing;

    public GameObject victoryPanel;

    public Transform movePoint;
    public LayerMask Obstacle;

    public Text actionPointsText;

    public bool motivacionCast;
    public bool manejoDeArmasCast;
    public bool gritoDelDragonCast;
    public bool dragonSagradoCast;
    public bool amoDelUniversoCast;
    public bool liberacionCast;
    public bool iraMalditaCast;
    public bool escudoDeDragonCast;
    public bool escamasDeDragonCast;
    public bool posicionDeDefensaCast;
    public bool apuntarLanzaCast;
    public bool furiaInfernalCast;
    public bool crearMunicionesCast;
    public bool garraDemoniacaCast;
    public bool fuerzaCast;
    public bool entrenamientoCast;
    public bool iraCast;
    public bool declaracionDeGuerraCast;
    public bool finDelTiempoCast;
    public bool conquistaCast;
    public bool degollarCast;
    public bool ametralladoraCast;
    public bool desmembrarCast;
    public bool odiseaCast;
    public bool hachaDelDemonioCast;
    public bool demoniosEternosCast;
    public bool destruccionInfinitaCast;
    public bool salvacionEternaCast;
    public bool oracionAncestralCast;
    public bool sigiloCast;
    public bool disparoParalizadorCast;
    public bool meditacionCast;
    public bool karmaCast;
    public bool oscuridadTotalCast;
    public bool disparoRapidoCast;
    public bool dragonEternoCast;
    public bool furiaEternaCast;
    public bool trifuerzaCast;
    public bool arteMarcialCast;
    public bool sacrificioUniversalCast;
    public bool explosionEstelarCast;
    public bool implosionNuclearCast;
    public bool terremotoCast;
    public bool chispazoCast;
    public bool bolaDeNieveCast;
    public bool lloriqueoCast;
    public bool aventarHuevosCast;
    public bool espadaDeLaMuerteCast;
    public bool explosionCast;
    public bool sermonCast;
    public bool ahorcamientoCast;
    public bool baileCast;
    public bool cantoCast;
    public bool truenoCast;
    public bool explosionDivinaCast;
    public bool espadaDelDemonioCast;
    public bool bloqueoCast;
    public bool exploracionCast;
    public bool imposicionRealCast;
    public bool bazookaCast;
    public bool sonidoDeTigreCast;
    public bool maldicionCast;
    public bool sonidoDeAveCast;
    public bool danzaDeLluviaCast;
    public bool furiaDeLasBestiasCast;
    public bool disparoCast;
    public bool lanzamientoCast;
    public bool hacerOjitosCast;
    public bool contarChisteCast;
    public bool calzonChinoCast;
    public bool teVoyAMearWeyCast;
    public bool disparoExplosivoCast;
    public bool invocacionDeVelocidadCast;
    public bool curacionCast;
    public bool curacionAvanzadaCast;
    public bool creacionDeBalasLaserCast;
    public bool alquimiaCast;
    public bool sonidoAturdidorCast;
    public bool ataqueSonicoCast;
    public bool avalanchaCast;
    public bool lanzagranadasCast;
    public bool creacionDeBrujulaCast;
    public bool ordenRealCast;
    public bool asedioCast;
    public bool serenataCast;
    public bool granadaAturdidoraCast;
    public bool renacimientoCast;
    public bool invocacionDeAngelesCast;
    public bool resistenciaCast;
    public bool ordenDeLaEmperatrizCast;
    public bool hacerleComoGatitoCast;

    public bool spellCastOnAlly;

    // Passives
    public bool dobleAtaqueDeDemonio;
    public bool jineteLegendario;
    public bool gloriaEterna;
    public bool lanzaInfernal;
    public bool lanzaEndemoniada;
    public bool inmortalidad;
    public bool letalidad;
    public bool lanzaGloriosa;
    public bool golpeLetal;
    public bool asesinato;
    public bool katana;
    public bool paletaPegosteosa;
    public bool escudoMistico;
    public bool lanzaDelAmor;
    public bool espirituDeGaia;
    public bool espadachinDeLaSoledad;
    public bool hachaDeTrueno;
    public bool espadaDeFuego;
    public bool absorcionDeEnergia;
    public bool absorcionDeEnergiaDebuff;
    public bool espadaDeLuz;
    public bool espadaDeLuzDebuff;
    public bool armaduraLegendaria;
    public bool perfumeDeRosas;
    public bool chirrinChinChin;

    // CantMove & CantAttack
    public bool cantMove;
    public bool cantAttack;

    public Animator animation;
    public Character character;
    

    private void Start()
    {
        source = GetComponent<AudioSource>();
        gm = FindObjectOfType<GameMaster>();
        camAnim = Camera.main.GetComponent<Animator>();
        character = GetComponent<Character>();

        

        movePoint.parent = null;
        
        motivacionCast = false;
        manejoDeArmasCast = false;
        gritoDelDragonCast = false;
        dragonSagradoCast = false;
        amoDelUniversoCast = false;
        liberacionCast = false;
        iraMalditaCast = false;
        escudoDeDragonCast = false;
        escamasDeDragonCast = false;
        posicionDeDefensaCast = false;
        apuntarLanzaCast = false;
        furiaInfernalCast = false;
        crearMunicionesCast = false;
        garraDemoniacaCast = false;
        fuerzaCast = false;
        entrenamientoCast = false;
        iraCast = false;
        declaracionDeGuerraCast = false;
        finDelTiempoCast = false;
        conquistaCast = false;
        degollarCast = false;
        ametralladoraCast = false;
        desmembrarCast = false;
        odiseaCast = false;
        hachaDelDemonioCast = false;
        demoniosEternosCast = false;
        destruccionInfinitaCast = false;
        salvacionEternaCast = false;
        oracionAncestralCast = false;
        sigiloCast = false;
        disparoParalizadorCast = false;
        meditacionCast = false;
        karmaCast = false;
        oscuridadTotalCast = false;
        disparoRapidoCast = false;
        dragonEternoCast = false;
        furiaEternaCast = false;
        trifuerzaCast = false;
        arteMarcialCast = false;
        sacrificioUniversalCast = false;
        explosionEstelarCast = false;
        implosionNuclearCast = false;
        terremotoCast = false;
        chispazoCast = false;
        bolaDeNieveCast = false;
        lloriqueoCast = false;
        aventarHuevosCast = false;
        espadaDeLaMuerteCast = false;
        explosionCast = false;
        sermonCast = false;
        ahorcamientoCast = false;
        baileCast = false;
        cantoCast = false;
        truenoCast = false;
        explosionDivinaCast = false;
        espadaDelDemonioCast = false;
        bloqueoCast = false;
        exploracionCast = false;
        imposicionRealCast = false;
        bazookaCast = false;
        sonidoDeTigreCast = false;
        maldicionCast = false;
        sonidoDeAveCast = false;
        danzaDeLluviaCast = false;
        furiaDeLasBestiasCast = false;
        disparoCast = false;
        lanzamientoCast = false;
        hacerOjitosCast = false;
        contarChisteCast = false;
        calzonChinoCast = false;
        teVoyAMearWeyCast = false;
        disparoExplosivoCast = false;
        invocacionDeVelocidadCast = false;
        curacionCast = false;
        curacionAvanzadaCast = false;
        creacionDeBalasLaserCast = false;
        alquimiaCast = false;
        sonidoAturdidorCast = false;
        ataqueSonicoCast = false;
        avalanchaCast = false;
        lanzagranadasCast = false;
        creacionDeBrujulaCast = false;
        ordenRealCast = false;
        asedioCast = false;
        serenataCast = false;
        granadaAturdidoraCast = false;
        renacimientoCast = false;
        invocacionDeAngelesCast = false;
        resistenciaCast = false;
        ordenDeLaEmperatrizCast = false;
        hacerleComoGatitoCast = false;

        cantMove = false;
        cantAttack = false;

        animation = GetComponent<Animator>();
        
        InvokeRepeating("GetActionPoint", actionPointsTimer, actionPointsTimer);
    }

    [PunRPC]
    public void setPlayerNumber(int number){
        playerNumber = number;
        GetComponent<Unit2>().healthFillImage.color = playerNumber==1 ? Color.blue : (playerNumber==2 ? Color.red : Color.yellow);
    }

    private void Update()
    {
        if (gm.statsPanel.activeSelf == true)
        {
            if (this.Equals(gm.selectedUnit) && this.Equals(gm.viewedUnit))
            {
                gm.statsPanel.transform.position = this.transform.position;
            }
        }

        //transform.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, movePoint.position) <= 0.05f && this.Equals(gm.selectedUnit) && actionPoints >= 1 && cantMove == false)
        {
            if (gm.selectedUnit.danzaDeLluviaCast == true)
            {
                if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) == 1f && actionPoints >= 2)
                {
                if(!Physics2D.OverlapCircle(gm.selectedUnit.movePoint.position + new Vector3(Input.GetAxisRaw("Horizontal"), 0, 0), .2f, Obstacle) && actionPoints >= 2)
                {
                    gm.selectedUnit.movePoint.position += new Vector3(Input.GetAxisRaw("Horizontal"), 0, 0);
                    StartCoroutine(DanzaDeLluviaMovement());
                    // Jinete Legendario Passive
                    if (gm.selectedUnit.jineteLegendario == true)
                    {
                        gm.selectedUnit.photonView.RPC("JineteLegendarioAnimation", RpcTarget.All);
                        gm.selectedUnit.attackDamage += 1;
                        StartCoroutine(JineteLegendarioUncast(gm.selectedUnit, 10f));
                    }
                }
                } else if (Mathf.Abs(Input.GetAxisRaw("Vertical")) == 1f && actionPoints >= 2)
                {
                if(!Physics2D.OverlapCircle(gm.selectedUnit.movePoint.position + new Vector3(0, Input.GetAxisRaw("Vertical"), 0), .2f, Obstacle) && actionPoints >= 2)
                {
                    gm.selectedUnit.movePoint.position += new Vector3(0, Input.GetAxisRaw("Vertical"), 0);
                    StartCoroutine(DanzaDeLluviaMovement());
                    // Jinete Legendario Passive
                    if (gm.selectedUnit.jineteLegendario == true)
                    {
                        gm.selectedUnit.photonView.RPC("JineteLegendarioAnimation", RpcTarget.All);
                        gm.selectedUnit.attackDamage += 1;
                        StartCoroutine(JineteLegendarioUncast(gm.selectedUnit, 10f));
                    }
                }
                }   
            } else if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) == 1f && actionPoints >= 1)
            {
                if(!Physics2D.OverlapCircle(gm.selectedUnit.movePoint.position + new Vector3(Input.GetAxisRaw("Horizontal"), 0, 0), .2f, Obstacle) && actionPoints >= 1)
                {
                    gm.selectedUnit.movePoint.position += new Vector3(Input.GetAxisRaw("Horizontal"), 0, 0);
                    actionPoints -= 1;
                    StartCoroutine(Movement());
                    // Jinete Legendario Passive
                    if (gm.selectedUnit.jineteLegendario == true)
                    {
                        gm.selectedUnit.photonView.RPC("JineteLegendarioAnimation", RpcTarget.All);
                        gm.selectedUnit.attackDamage += 1;
                        StartCoroutine(JineteLegendarioUncast(gm.selectedUnit, 10f));
                    }
                }
            } else if (Mathf.Abs(Input.GetAxisRaw("Vertical")) == 1f && actionPoints >= 1)
            {
                if(!Physics2D.OverlapCircle(gm.selectedUnit.movePoint.position + new Vector3(0, Input.GetAxisRaw("Vertical"), 0), .2f, Obstacle) && actionPoints >= 1)
                {
                    gm.selectedUnit.movePoint.position += new Vector3(0, Input.GetAxisRaw("Vertical"), 0);
                    actionPoints -= 1;
                    StartCoroutine(Movement());
                    // Jinete Legendario Passive
                    if (gm.selectedUnit.jineteLegendario == true)
                    {
                        gm.selectedUnit.photonView.RPC("JineteLegendarioAnimation", RpcTarget.All);
                        gm.selectedUnit.attackDamage += 1;
                        StartCoroutine(JineteLegendarioUncast(gm.selectedUnit, 10f));
                    }
                }
            }
        }
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(1))
        {
            gm.ToggleStatsPanel(this);
        }
    }

    private void OnMouseDown()
    {
        ResetWeaponIcons();
        
        if (selected == true)
        {
            selected = false;
            gm.selectedUnit = null;
            gm.ResetTiles();
        } else {
            if (photonView.IsMine)
            {        
                if (gm.selectedUnit != null)
                {
                    gm.selectedUnit.selected = false;
                }
/*
                if (gm.selectedUnit.spellCastOnAlly == true)
                {
                    if (gm.selectedUnit.salvacionEternaCast == true) // Salvacion Eterna
                    {
                        photonView.RPC("VictoryAnim", RpcTarget.All);
                        this.inmortalidad = true;
                        gm.selectedUnit.spellCastOnAlly = false;
                        gm.selectedUnit.salvacionEternaCast = false;
                        return;
                    }
                }
*/
                source.Play();
                selected = true;
                gm.selectedUnit = this;

                int tauntChance = Random.Range(1,1001);
                if (tauntChance == 1)
                {
                    photonView.RPC("Taunt", RpcTarget.All);
                }

                gm.ResetTiles();
                GetEnemies();
                GetWalkableTiles();
            }
        }

        Collider2D col = Physics2D.OverlapCircle(Camera.main.ScreenToWorldPoint(Input.mousePosition), 0.15f);
        Unit unit = col.GetComponent<Unit>();
        Unit2 unit2 = col.GetComponent<Unit2>();
        Spacebase spacebase = col.GetComponent<Spacebase>();
        if (gm.selectedUnit != null)
        {
            if (gm.selectedUnit.enemiesInRange.Contains(unit) && gm.selectedUnit.actionPoints >= 2)
            {           
                if (gm.selectedUnit.degollarCast == true) // Degollar attack
                {
                    gm.selectedUnit.degollarCast = false;
                    gm.selectedUnit.photonView.RPC("SlashAnim", RpcTarget.All);
                    gm.selectedUnit.DegollarAttack(unit2, unit);
                } else if (gm.selectedUnit.desmembrarCast == true) // Desmembrar attack
                {
                    int desmembrarChance = Random.Range(1, 3);
                    if (desmembrarChance == 1) // 1 = Desmembrar, 2 = Failed
                    {
                        gm.selectedUnit.desmembrarCast = false;
                        gm.selectedUnit.photonView.RPC("SlashAnim", RpcTarget.All);
                        gm.selectedUnit.DesmembrarAttack(unit2, unit);
                    } else
                    {
                        gm.selectedUnit.desmembrarCast = false;
                        gm.selectedUnit.photonView.RPC("SlashAnim", RpcTarget.All);
                        gm.selectedUnit.Attack(unit);
                    }
                } else if (gm.selectedUnit.dobleAtaqueDeDemonio == true) 
                {
                    gm.selectedUnit.photonView.RPC("SlashAnim", RpcTarget.All);
                    gm.selectedUnit.DobleAtaqueDeDemonioAttack(unit);
                } else if (gm.selectedUnit.ametralladoraCast == true)
                {
                    gm.selectedUnit.AmetralladoraAttack(unit);
                }
                else
                {
                    gm.selectedUnit.photonView.RPC("JabAnim", RpcTarget.All);
                    gm.selectedUnit.Attack(unit);
                }
            }
        }

        if (gm.selectedUnit != null)
        {
            if (gm.selectedUnit.spacebaseInRange.Contains(spacebase) && gm.selectedUnit.actionPoints >= 2/*&& gm.selectedUnit.hasAttacked == false*/)
            {
                gm.selectedUnit.photonView.RPC("JabAnim", RpcTarget.All);
                gm.selectedUnit.AttackSpacebase(spacebase);
            }
        }
    }

    void Attack(Unit enemy)
    {
        if (gm.selectedUnit.cantAttack == true)
        {
            return;
        }
        
        camAnim.SetTrigger("shake");

        gm.selectedUnit.actionPoints -= 2;
        UpdateActionPointsText();

        int enemyDamage = Mathf.Max(0, attackDamage - enemy.physicalArmor);
        int myDamage = Mathf.Max(0, enemy.defenseDamage - physicalArmor);

        if (enemy.espirituDeGaia == true || enemy.perfumeDeRosas == true) // Espiritu de Gaia or Perfume de Rosas
        {
            gm.selectedUnit.photonView.RPC("EspirituDeGaiaAnimation", RpcTarget.All);
            myDamage = Mathf.Max(0, 4 * enemy.defenseDamage - physicalArmor - demonArmor - hollyArmor);
        } else if (enemy.armaduraLegendaria == true) // Armadura Legendaria
        {
            myDamage = Mathf.Max(0, enemy.defenseDamage - hollyArmor);
        }

        if (gm.selectedUnit.disparoExplosivoCast == true) // Disparo Explosivo
        {
            enemy.photonView.RPC("DisparoExplosivoDebuff", RpcTarget.All);
            gm.selectedUnit.disparoExplosivoCast = false;
        } else if (gm.selectedUnit.granadaAturdidoraCast == true) // Granada Aturdidora
        {
            enemy.photonView.RPC("GranadaAturdidoraDebuff", RpcTarget.All);
            gm.selectedUnit.granadaAturdidoraCast = false;
        } else if (gm.selectedUnit.avalanchaCast == true) // Avalancha
        {
            enemy.photonView.RPC("AvalanchaDebuff", RpcTarget.All);
            gm.selectedUnit.avalanchaCast = false;
        } else if (gm.selectedUnit.lanzagranadasCast == true) // Lanza Granadas
        {
            enemy.photonView.RPC("LanzagranadasDebuff", RpcTarget.All);
            gm.selectedUnit.lanzagranadasCast = false;
        }
        
        else if (gm.selectedUnit.alquimiaCast == true) // Alquimia
        {
            enemy.photonView.RPC("AlquimiaAnimation", RpcTarget.All);
            // enemyDamage = (attackDamage) + Mathf.Max(0, attackDamage - enemy.physicalArmor) + Mathf.Max(0, attackDamage - enemy.demonArmor) + Mathf.Max(0, attackDamage - enemy.hollyArmor);
            enemyDamage = (attackDamage) + (attackDamage - enemy.physicalArmor) + (attackDamage - enemy.demonArmor) + (attackDamage - enemy.hollyArmor);
            gm.selectedUnit.alquimiaCast = false;
        }
        
        if (gm.selectedUnit.lanzaEndemoniada == true) // Lanza Endemoniada Passive
        {
            enemy.photonView.RPC("DemonHitAnimation", RpcTarget.All);
            enemyDamage = Mathf.Max(0, attackDamage - enemy.demonArmor);
        } else if (gm.selectedUnit.hachaDelDemonioCast == true) // Hacha del Demonio
        {
            hachaDelDemonioCast = false;
            enemyDamage += 3;
        } else if (gm.selectedUnit.disparoParalizadorCast == true) // Disparo Paralizador
        {
            enemy.photonView.RPC("DisparoParalizadorDebuff", RpcTarget.All);
            gm.selectedUnit.disparoParalizadorCast = false;
        } else if (gm.selectedUnit.disparoRapidoCast == true) // Disparo Rapido
        {
            enemyDamage = Mathf.Max(0, 2 * attackDamage - enemy.physicalArmor);
            gm.selectedUnit.disparoRapidoCast = false;
        } else if (gm.selectedUnit.chispazoCast == true) // Chispazo
        {
            enemy.photonView.RPC("DemonHitAnimation", RpcTarget.All);
            enemyDamage = Mathf.Max(0, 2 * attackDamage - enemy.demonArmor);
            gm.selectedUnit.chispazoCast = false;
        } else if (gm.selectedUnit.bolaDeNieveCast == true) // Bola de Nieve
        {
            enemy.photonView.RPC("HollyHitAnimation", RpcTarget.All);
            enemyDamage = Mathf.Max(0, 2 * attackDamage - enemy.hollyArmor);
            gm.selectedUnit.bolaDeNieveCast = false;
        } else if (gm.selectedUnit.trifuerzaCast == true) // Trifuerza
        {
            enemy.photonView.RPC("TrifuerzaAnimation", RpcTarget.All);
            enemyDamage = (3 * attackDamage);
            gm.selectedUnit.trifuerzaCast = false;
        } else if (gm.selectedUnit.sacrificioUniversalCast == true) // Sacrificio Universal
        {
            enemyDamage = (4 * attackDamage);
            gm.selectedUnit.sacrificioUniversalCast = false;
            gm.selectedUnit.health = 1;
            gm.selectedUnit.photonView.RPC("TakeDamage", RpcTarget.All, gm.selectedUnit.health - 1);
        } else if (gm.selectedUnit.aventarHuevosCast == true) // Aventar Huevos
        {
            int aventarHuevosAmount = Random.Range(1, 7);
            enemy.photonView.RPC("AventarHuevosAnimation", RpcTarget.All);
            enemyDamage = (aventarHuevosAmount * Mathf.Max(0, attackDamage - enemy.demonArmor));
            gm.selectedUnit.aventarHuevosCast = false;
        } else if (gm.selectedUnit.espadaDeLaMuerteCast == true) // Espada de la Muerte
        {
            int espadaDeLaMuerteChance = Random.Range(1, 101);
            if (espadaDeLaMuerteChance <= 15)
            {
                enemy.photonView.RPC("EspadaDeLaMuerteAnimation", RpcTarget.All);
                enemyDamage = enemy.health;
            } else
            {
                enemyDamage = Mathf.Max(0, gm.selectedUnit.attackDamage + 3 - enemy.physicalArmor);
            }
            gm.selectedUnit.espadaDeLaMuerteCast = false;
        } else if (gm.selectedUnit.espadaDelDemonioCast == true) // Espada del Demonio
        {
            int espadaDelDemonioChance = Random.Range(1, 11);
            if (espadaDelDemonioChance == 1)
            {
                enemy.photonView.RPC("EspadaDelDemonioAnimation", RpcTarget.All);
                enemyDamage = enemy.health;
            } else
            {
                enemyDamage = Mathf.Max(0, gm.selectedUnit.attackDamage + 1 - enemy.physicalArmor);
            }
            gm.selectedUnit.espadaDelDemonioCast = false; 
        } else if (gm.selectedUnit.sermonCast == true) // Sermon
        {
            enemy.photonView.RPC("SermonDebuff", RpcTarget.All);
            gm.selectedUnit.sermonCast = false;
        } else if (gm.selectedUnit.ahorcamientoCast == true) // Ahorcamiento
        {
            enemy.photonView.RPC("AhorcamientoDebuff", RpcTarget.All);
            gm.selectedUnit.ahorcamientoCast = false;
        } else if (gm.selectedUnit.calzonChinoCast == true) // Calzon Chino
        {
            enemy.photonView.RPC("CalzonChinoDebuff", RpcTarget.All);
            gm.selectedUnit.calzonChinoCast = false;
        } else if (gm.selectedUnit.teVoyAMearWeyCast == true) // Te Voy a Mear Wey
        {
            enemy.photonView.RPC("TeVoyAMearWeyDebuff", RpcTarget.All);
            gm.selectedUnit.teVoyAMearWeyCast = false;
        }

        if (gm.selectedUnit.chirrinChinChin == true) // Chirrin Chin Chin Passive
        {
            if (enemy.cantAttack == true || enemy.cantMove == true) 
            {
                enemy.photonView.RPC("ChirrinChinChinAnimation", RpcTarget.All);
                enemyDamage = enemy.health - 1;
            }
        } else if (gm.selectedUnit.lanzaInfernal == true) // Lanza Infernal Passive
        {
            int lanzaInfernalChance = Random.Range(1, 5);
            if (lanzaInfernalChance == 1) // 1 = Lanza Infernal, 2-4 = Failed
            {
                enemy.photonView.RPC("LanzaInfernalAnimation", RpcTarget.All);
                enemyDamage = enemy.health - 1;
            }
        } else if (gm.selectedUnit.letalidad == true) // Letalidad Passive
        {
            int letalidadChance = Random.Range(1, 11);
            if (letalidadChance <= 3) // 1-3 = Letalidad, 4-10 = Failed
            {
                enemy.photonView.RPC("LetalidadAnimation", RpcTarget.All);
                enemyDamage = enemy.health;
            }
        } else if (gm.selectedUnit.asesinato == true) // Asesinato Passive
        {
            int asesinatoChance = Random.Range(1, 101);
            if (asesinatoChance <= 15) // 1-15 = Asesinato, 16-100 = Failed
            {
                enemy.photonView.RPC("AsesinatoAnimation", RpcTarget.All);
                enemyDamage = enemy.health;
            }
        } else if (gm.selectedUnit.katana == true) // Katana Passive
        {
            int katanaChance = Random.Range(1, 11);
            if (katanaChance == 1) // 1 = Katana, 2-10 = Failed
            {
                enemy.photonView.RPC("KatanaAnimation", RpcTarget.All);
                enemyDamage = enemy.health;
            }
        } else if (gm.selectedUnit.lanzaGloriosa == true) // Lanza Gloriosa Passive
        {
            enemy.attackDamage = (int)(enemy.attackDamage / 2);
            enemy.photonView.RPC("LanzaGloriosaDebuff", PlayerController.enemy.photonPlayer);
            gm.UpdateStatsPanel();
        } else if (gm.selectedUnit.paletaPegosteosa == true) // Paleta Pegosteosa Passive
        {
            enemy.photonView.RPC("PaletaPegosteosaDebuff", RpcTarget.All);
        } else if (gm.selectedUnit.hachaDeTrueno == true) // Hacha de Trueno Passive
        {
            enemy.photonView.RPC("HachaDeTruenoDebuff", RpcTarget.All);
        } else if (gm.selectedUnit.espadaDeFuego == true) // Espada de Fuego Passive
        {
            enemy.photonView.RPC("EspadaDeFuegoDebuff", RpcTarget.All);
        } else if (gm.selectedUnit.espadaDeLuz == true) // Espada de Luz Passive
        {
            enemy.photonView.RPC("EspadaDeLuzDebuff", RpcTarget.All);
        } 
        
        if (gm.selectedUnit.lanzaDelAmor == true) // Lanza del Amor Passive
        {
            enemy.photonView.RPC("LanzaDelAmorDebuff", PlayerController.enemy.photonPlayer);
        }

        if (gm.selectedUnit.espadachinDeLaSoledad == true) // Espadachin de la Soledad Passive
        {
            gm.selectedUnit.alliesInAOERange.Clear();
            foreach (Unit unitInRange in FindObjectsOfType<Unit>())
            {
            if ((Mathf.Abs(transform.position.x - unitInRange.transform.position.x) + Mathf.Abs(transform.position.y - unitInRange.transform.position.y) <= 3))
            {
                if (gm.selectedUnit.playerNumber == unitInRange.playerNumber)
                {
                    gm.selectedUnit.alliesInAOERange.Add(unitInRange);
                    if (gm.selectedUnit.alliesInAOERange.Count == 1)
                    {
                        enemyDamage = enemy.health;
                    }
                }
            }
            }
        }


        if (enemy.odiseaCast == true) // Odisea
        {
            enemyDamage = 0;
        }

        if (enemy.escudoMistico == true)
        {
            enemyDamage = (int)(enemyDamage / 2);
        }

        if (enemy.bloqueoCast == true) // Bloqueo
        {
            enemyDamage = 0;
            enemy.bloqueoCast = false;
        }

        if (gm.selectedUnit.absorcionDeEnergia == true) // Absorcion de Energia
        {
            gm.selectedUnit.photonView.RPC("AbsorcionDeEnergiaAnimation", RpcTarget.All);
            gm.selectedUnit.health += enemyDamage;
            gm.selectedUnit.photonView.RPC("Heal", RpcTarget.All, enemyDamage);
        }

        if (enemy.espadaDeLuzDebuff == true) // Espada de Luz Debuff
        {
            gm.selectedUnit.health += enemyDamage;
            gm.selectedUnit.photonView.RPC("Heal", RpcTarget.All, enemyDamage);
        }

        if (enemyDamage >= 1)
        {
            enemy.photonView.RPC("HitAnim", RpcTarget.All);
            DamageIcon instance = Instantiate(damageIcon, enemy.transform.position, Quaternion.identity);
            instance.Setup(enemyDamage);
            enemy.health -= enemyDamage;

            if (gm.selectedUnit.golpeLetal == true) // Golpe Letal Passive
            {
                gm.selectedUnit.photonView.RPC("GolpeLetalAnimation", RpcTarget.All);
                gm.selectedUnit.attackDamage += 1;
            }
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
            if (enemy.inmortalidad == true) // Inmortalidad Passive
            {
                enemy.photonView.RPC("InmortalidadAnimation", RpcTarget.All);
                enemy.inmortalidad = false;
                Unit2 unit2 = enemy.GetComponent<Unit2>();
                enemy.health = unit2.maxHp;
                unit2.healthFillImage.fillAmount = 1.0f;
                gm.UpdateStatsPanel();
                return;
            }
            
            // Gloria Eterna Passive
            if (gm.selectedUnit.gloriaEterna == true)
            {
                gm.selectedUnit.photonView.RPC("GloriaEternaAnimation", RpcTarget.All);
                gm.selectedUnit.photonView.RPC("VictoryAnim", RpcTarget.All);
                gm.selectedUnit.attackDamage += 1;
                gm.selectedUnit.physicalArmor += 1;
                gm.selectedUnit.defenseDamage += 1;
            }

            Instantiate(deathEffect, enemy.transform.position, Quaternion.identity);
            GetWalkableTiles();
            gm.RemoveStatsPanel(enemy);
        }

        if (health <= 0)
        {   
            if (this.inmortalidad == true) // Inmortalidad Passive
            {
                this.photonView.RPC("InmortalidadAnimation", RpcTarget.All);
                this.inmortalidad = false;
                Unit2 unit2 = this.GetComponent<Unit2>();
                this.health = unit2.maxHp;
                unit2.healthFillImage.fillAmount = 1.0f;
                gm.UpdateStatsPanel();
                return;
            }
            
            Instantiate(deathEffect, transform.position, Quaternion.identity);
            gm.ResetTiles();
            gm.RemoveStatsPanel(this);
        }

        gm.UpdateStatsPanel();
        // enemy.photonView.RPC("TakeDamage", PlayerController.enemy.photonPlayer, enemyDamage); // Se cambio de attackDamage a enemyDamage
        enemy.photonView.RPC("TakeDamage", RpcTarget.All, enemyDamage); // Se cambio de attackDamage a enemyDamage
    }

    public void HealUE(Unit enemy, string type)
    {
        if (gm.selectedUnit.cantAttack == true)
        {
            return;
        }

        int enemyDamage = 0;
        
        camAnim.SetTrigger("shake");

        if (type == "curacion")
        {
            enemy.photonView.RPC("CuracionAnimation", RpcTarget.All);
            enemyDamage = attackDamage;
        } else if (type == "curacion avanzada")
        {
            enemy.photonView.RPC("CuracionAvanzadaAnimation", RpcTarget.All);
            enemyDamage = 2 * attackDamage;
        } 

        if (enemyDamage >= 1)
        {
            enemy.health += enemyDamage;
        }

        Unit2 enemy2 = GetComponent<Unit2>();

        if (enemy.health >= enemy2.maxHp)
        {
            enemy.health = enemy2.maxHp;
        }

        gm.UpdateStatsPanel();
        enemy.photonView.RPC("Heal", PlayerController.enemy.photonPlayer, enemyDamage); // Se cambio de attackDamage a enemyDamage
    }

    public void AttackUE(Unit enemy, string type)
    {
        if (gm.selectedUnit.cantAttack == true)
        {
            return;
        }

        int enemyDamage = 0;
        
        camAnim.SetTrigger("shake");

        if (type == "universe")
        {
            enemy.photonView.RPC("UniverseUEAnimation", RpcTarget.All);
            enemyDamage = attackDamage;
        } else if (type == "ataque sonico")
        {
            enemy.photonView.RPC("AtaqueSonicoAnimation", RpcTarget.All);
            enemyDamage = Mathf.Max(0, attackDamage - enemy.physicalArmor);
        } else if (type == "demon")
        {
            enemy.photonView.RPC("DemonUEAnimation", RpcTarget.All);
            enemyDamage = Mathf.Max(0, attackDamage - enemy.demonArmor);
        } else if (type == "espada de fuego")
        {
            enemy.photonView.RPC("EspadaDeFuegoAnimation", RpcTarget.All);
            enemyDamage = Mathf.Max(0, attackDamage - enemy.demonArmor);
        } else if (type == "disparo explosivo")
        {
            enemy.photonView.RPC("DisparoExplosivoAnimation", RpcTarget.All);
            enemyDamage = Mathf.Max(0, attackDamage - enemy.demonArmor);
        } else if (type == "holly")
        {
            enemy.photonView.RPC("HollyUEAnimation", RpcTarget.All);
            enemyDamage = Mathf.Max(0, attackDamage - enemy.hollyArmor);
        } else if (type == "trueno")
        {
            enemy.photonView.RPC("TruenoUEAnimation", RpcTarget.All);
            enemyDamage = Mathf.Max(0, attackDamage - enemy.hollyArmor);
        } else if (type == "lloriqueoEnemy")
        {
            enemy.photonView.RPC("LloriqueoEnemy", RpcTarget.All);
        } else if (type == "lloriqueoAlly")
        {
            enemy.photonView.RPC("LloriqueoAlly", RpcTarget.All);
        } else if (type == "baile")
        {
            enemy.photonView.RPC("BaileAnimation", RpcTarget.All);
            enemy.actionPoints += 2;
        } else if (type == "canto")
        {
            enemy.photonView.RPC("CantoAnimation", RpcTarget.All);
            enemy.actionPoints += 1;
        } else if (type == "sonidoDeTigreEnemy")
        {
            enemy.photonView.RPC("SonidoDeTigreEnemy", RpcTarget.All);
        } else if (type == "sonidoDeTigreAlly")
        {
            enemy.photonView.RPC("SonidoDeTigreAlly", RpcTarget.All);
        } else if (type == "sonidoDeAveAlly")
        {
            enemy.photonView.RPC("SonidoDeAveAlly", RpcTarget.All);
        } else if (type == "furiaDeLasBestiasAlly")
        {
            enemy.photonView.RPC("FuriaDeLasBestiasAlly", RpcTarget.All);
        } else if (type == "hacerOjitosEnemy")
        {
            enemy.photonView.RPC("HacerOjitosEnemy", RpcTarget.All);
        } else if (type == "contarChisteEnemy")
        {
            enemy.photonView.RPC("ContarChisteEnemy", RpcTarget.All);
        } else if (type == "contarChisteAlly")
        {
            enemy.photonView.RPC("ContarChisteAlly", RpcTarget.All);
        } else if (type == "calzonChinoEnemy")
        {
            enemy.photonView.RPC("CalzonChinoEnemy", RpcTarget.All);
        } else if (type == "teVoyAMearWeyEnemy")
        {
            enemy.photonView.RPC("TeVoyAMearWeyEnemy", RpcTarget.All);
        } else if (type == "sonido aturdidor")
        {
            enemy.photonView.RPC("SonidoAturdidorEnemy", RpcTarget.All);
        } else if (type == "serenataEnemy")
        {
            enemy.photonView.RPC("SerenataEnemy", RpcTarget.All);
        } else if (type == "granada aturdidora")
        {
            enemy.photonView.RPC("GranadaAturdidoraEnemy", RpcTarget.All);
        } else if (type == "invocacionDeAngelesEnemy")
        {
            enemy.photonView.RPC("InvocacionDeAngelesEnemy", RpcTarget.All);
        } else if (type == "invocacionDeAngelesAlly")
        {
            enemy.photonView.RPC("InvocacionDeAngelesAlly", RpcTarget.All);
        } else if (type == "ordenDeLaEmperatrizAlly")
        {
            enemy.photonView.RPC("OrdenDeLaEmperatrizAlly", RpcTarget.All);
        } else if (type == "hacerleComoGatitoEnemy")
        {
            enemy.photonView.RPC("HacerleComoGatitoEnemy", RpcTarget.All);
        }

        if (enemy.odiseaCast == true) // Odisea
        {
            enemyDamage = 0;
        }

        if (gm.selectedUnit.absorcionDeEnergia == true) // Absorcion de Energia
        {
            gm.selectedUnit.photonView.RPC("AbsorcionDeEnergiaAnimation", RpcTarget.All);
            gm.selectedUnit.health += enemyDamage;
            gm.selectedUnit.photonView.RPC("Heal", RpcTarget.All, enemyDamage);
        }

        if (enemy.espadaDeLuzDebuff == true) // Espada de Luz Debuff
        {
            gm.selectedUnit.health += enemyDamage;
            gm.selectedUnit.photonView.RPC("Heal", RpcTarget.All, enemyDamage);
        }

        if (enemyDamage >= 1)
        {
            enemy.photonView.RPC("HitAnim", RpcTarget.All);
            DamageIcon instance = Instantiate(damageIcon, enemy.transform.position, Quaternion.identity);
            instance.Setup(enemyDamage);
            enemy.health -= enemyDamage;
        }

        if (enemy.health <= 0)
        {
            if (enemy.inmortalidad == true) // Inmortalidad Passive
            {
                enemy.photonView.RPC("InmortalidadAnimation", RpcTarget.All);
                enemy.inmortalidad = false;
                Unit2 unit2 = enemy.GetComponent<Unit2>();
                enemy.health = unit2.maxHp;
                unit2.healthFillImage.fillAmount = 1.0f;
                gm.UpdateStatsPanel();
                return;
            }

            Instantiate(deathEffect, enemy.transform.position, Quaternion.identity);
            GetWalkableTiles();
            gm.RemoveStatsPanel(enemy);
        }

        gm.UpdateStatsPanel();
        // enemy.photonView.RPC("TakeDamage", PlayerController.enemy.photonPlayer, enemyDamage); // Se cambio de attackDamage a enemyDamage
        enemy.photonView.RPC("TakeDamage", RpcTarget.All, enemyDamage); // Se cambio de attackDamage a enemyDamage
    }

    void DegollarAttack(Unit2 enemy2, Unit enemy)
    {
        if (gm.selectedUnit.cantAttack == true)
        {
            return;
        }
        
        camAnim.SetTrigger("shake");

        gm.selectedUnit.actionPoints -= 2;
        UpdateActionPointsText();

        int enemy2Damage = Mathf.Max(0, attackDamage - enemy.physicalArmor);

        if (enemy.odiseaCast == true) // Odisea
        {
            enemy2Damage = 0;
        }

        if (enemy.escudoMistico == true)
        {
            enemy2Damage = (int)(enemy2Damage / 2);
        }

        if (enemy.bloqueoCast == true) // Bloqueo
        {
            enemy2Damage = 0;
            enemy.bloqueoCast = false;
        }

        if (gm.selectedUnit.absorcionDeEnergia == true) // Absorcion de Energia
        {
            gm.selectedUnit.photonView.RPC("AbsorcionDeEnergiaAnimation", RpcTarget.All);
            gm.selectedUnit.health += enemy2Damage;
            gm.selectedUnit.photonView.RPC("Heal", RpcTarget.All, enemy2Damage);
        }

        if (enemy.espadaDeLuzDebuff == true) // Espada de Luz Debuff
        {
            gm.selectedUnit.health += enemy2Damage;
            gm.selectedUnit.photonView.RPC("Heal", RpcTarget.All, enemy2Damage);
        }

        if (enemy2.curHp <= (int)(enemy2.maxHp /2) && enemy2Damage >= 1)
        {
            if (enemy.inmortalidad == true) // Inmortalidad Passive
            {
                enemy.photonView.RPC("InmortalidadAnimation", RpcTarget.All);
                enemy.inmortalidad = false;
                enemy.health = enemy2.maxHp;
                enemy2.healthFillImage.fillAmount = 1.0f;
                gm.UpdateStatsPanel();
                return;
            }
            
            Instantiate(skullExplosion, enemy2.transform.position, Quaternion.identity);
            Instantiate(deathEffect, enemy2.transform.position, Quaternion.identity);
            GetWalkableTiles();
            enemy2.photonView.RPC("Die", RpcTarget.All);
            //gm.RemoveStatsPanel(enemy2); POSIBLE BUG A FUTURO.
        } else if (enemy2Damage >= 1)
        {
            DamageIcon instance = Instantiate(damageIcon, enemy2.transform.position, Quaternion.identity);
            instance.Setup(enemy2Damage);
            enemy.health -= enemy2Damage;
            // enemy2.photonView.RPC("TakeDamage", PlayerController.enemy.photonPlayer, enemy2Damage);
            enemy2.photonView.RPC("TakeDamage", RpcTarget.All, enemy2Damage);
        }
        
        gm.UpdateStatsPanel();
    }

    void DesmembrarAttack(Unit2 enemy2, Unit enemy)
    {
        if (gm.selectedUnit.cantAttack == true)
        {
            return;
        }
        
        camAnim.SetTrigger("shake");

        gm.selectedUnit.actionPoints -= 2;
        UpdateActionPointsText();

        if (enemy.odiseaCast == true) // Odisea
        {
            return;
        } else
        {
            if (enemy.inmortalidad == true) // Inmortalidad Passive
            {
                enemy.photonView.RPC("InmortalidadAnimation", RpcTarget.All);
                enemy.inmortalidad = false;
                enemy.health = enemy2.maxHp;
                enemy2.healthFillImage.fillAmount = 1.0f;
                gm.UpdateStatsPanel();
                return;
            }
            
            Instantiate(skullExplosion, enemy.transform.position, Quaternion.identity);
            Instantiate(deathEffect, enemy.transform.position, Quaternion.identity);
            GetWalkableTiles();
            enemy2.photonView.RPC("Die", RpcTarget.All);
            gm.UpdateStatsPanel();
        }
    }

    void DobleAtaqueDeDemonioAttack(Unit enemy)
    {
        if (gm.selectedUnit.cantAttack == true)
        {
            return;
        }

        camAnim.SetTrigger("shake");

        gm.selectedUnit.actionPoints -= 3;
        UpdateActionPointsText();

        int enemyDamage = Mathf.Max(0, (2 * attackDamage) - enemy.demonArmor);
        int myDamage = Mathf.Max(0, enemy.defenseDamage - physicalArmor);

        if (enemy.espirituDeGaia == true || enemy.perfumeDeRosas == true)
        {
            gm.selectedUnit.photonView.RPC("EspirituDeGaiaAnimation", RpcTarget.All);
            myDamage = Mathf.Max(0, 4 * enemy.defenseDamage - physicalArmor - demonArmor - hollyArmor);
        } else if (enemy.armaduraLegendaria == true) // Armadura Legendaria
        {
            myDamage = Mathf.Max(0, enemy.defenseDamage - hollyArmor);
        }

        if (enemy.odiseaCast == true) // Odisea
        {
            enemyDamage = 0;
        }

        if (enemy.escudoMistico == true)
        {
            enemyDamage = (int)(enemyDamage / 2);
        }

        if (enemy.bloqueoCast == true) // Bloqueo
        {
            enemyDamage = 0;
            enemy.bloqueoCast = false;
        }

        if (gm.selectedUnit.absorcionDeEnergia == true) // Absorcion de Energia
        {
            gm.selectedUnit.photonView.RPC("AbsorcionDeEnergiaAnimation", RpcTarget.All);
            gm.selectedUnit.health += enemyDamage;
            gm.selectedUnit.photonView.RPC("Heal", RpcTarget.All, enemyDamage);
        }

        if (enemy.espadaDeLuzDebuff == true) // Espada de Luz Debuff
        {
            gm.selectedUnit.health += enemyDamage;
            gm.selectedUnit.photonView.RPC("Heal", RpcTarget.All, enemyDamage);
        }

        if (enemyDamage >= 1)
        {
            enemy.photonView.RPC("HitAnim", RpcTarget.All);
            Instantiate(hitBOrange, enemy.transform.position, Quaternion.identity);
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
            if (enemy.inmortalidad == true) // Inmortalidad Passive
            {
                enemy.photonView.RPC("InmortalidadAnimation", RpcTarget.All);
                enemy.inmortalidad = false;
                Unit2 unit2 = enemy.GetComponent<Unit2>();
                enemy.health = unit2.maxHp;
                unit2.healthFillImage.fillAmount = 1.0f;
                gm.UpdateStatsPanel();
                return;
            }
            
            Instantiate(deathEffect, enemy.transform.position, Quaternion.identity);
            GetWalkableTiles();
            gm.RemoveStatsPanel(enemy);
        }

        if (health <= 0)
        {
            if (this.inmortalidad == true) // Inmortalidad Passive
            {
                this.photonView.RPC("InmortalidadAnimation", RpcTarget.All);
                this.inmortalidad = false;
                Unit2 unit2 = this.GetComponent<Unit2>();
                this.health = unit2.maxHp;
                unit2.healthFillImage.fillAmount = 1.0f;
                gm.UpdateStatsPanel();
                return;
            }

            Instantiate(deathEffect, transform.position, Quaternion.identity);
            gm.ResetTiles();
            gm.RemoveStatsPanel(this);
            //Destroy(this.gameObject);
            //GameManager.instance.CheckWinCondition();
        }

        gm.UpdateStatsPanel();
        // enemy.photonView.RPC("TakeDamage", PlayerController.enemy.photonPlayer, attackDamage);
        enemy.photonView.RPC("TakeDamage", RpcTarget.All, attackDamage);
    }

    void AmetralladoraAttack(Unit enemy)
    {
        if (gm.selectedUnit.cantAttack == true)
        {
            return;
        }

        camAnim.SetTrigger("shake");

        gm.selectedUnit.ametralladoraCast = false;
        gm.selectedUnit.actionPoints -= 2;
        UpdateActionPointsText();

        int enemyDamage = Mathf.Max(0, (3 * attackDamage) - enemy.physicalArmor);
        int myDamage = Mathf.Max(0, enemy.defenseDamage - physicalArmor);

        if (enemy.espirituDeGaia == true || enemy.perfumeDeRosas == true)
        {
            gm.selectedUnit.photonView.RPC("EspirituDeGaiaAnimation", RpcTarget.All);
            myDamage = Mathf.Max(0, 4 * enemy.defenseDamage - physicalArmor - demonArmor - hollyArmor);
        } else if (enemy.armaduraLegendaria == true) // Armadura Legendaria
        {
            myDamage = Mathf.Max(0, enemy.defenseDamage - hollyArmor);
        }

        if (enemy.odiseaCast == true) // Odisea
        {
            enemyDamage = 0;
        }

        if (enemy.escudoMistico == true)
        {
            enemyDamage = (int)(enemyDamage / 2);
        }

        if (enemy.bloqueoCast == true) // Bloqueo
        {
            enemyDamage = 0;
            enemy.bloqueoCast = false;
        }

        if (gm.selectedUnit.absorcionDeEnergia == true) // Absorcion de Energia
        {
            gm.selectedUnit.photonView.RPC("AbsorcionDeEnergiaAnimation", RpcTarget.All);
            gm.selectedUnit.health += enemyDamage;
            gm.selectedUnit.photonView.RPC("Heal", RpcTarget.All, enemyDamage);
        }

        if (enemy.espadaDeLuzDebuff == true) // Espada de Luz Debuff
        {
            gm.selectedUnit.health += enemyDamage;
            gm.selectedUnit.photonView.RPC("Heal", RpcTarget.All, enemyDamage);
        }

        if (enemyDamage >= 1)
        {
            enemy.photonView.RPC("HitAnim", RpcTarget.All);
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
            if (enemy.inmortalidad == true) // Inmortalidad Passive
            {
                enemy.photonView.RPC("InmortalidadAnimation", RpcTarget.All);
                enemy.inmortalidad = false;
                Unit2 unit2 = enemy.GetComponent<Unit2>();
                enemy.health = unit2.maxHp;
                unit2.healthFillImage.fillAmount = 1.0f;
                gm.UpdateStatsPanel();
                return;
            }
            
            Instantiate(deathEffect, enemy.transform.position, Quaternion.identity);
            GetWalkableTiles();
            gm.RemoveStatsPanel(enemy);
        }

        if (health <= 0)
        {
            if (this.inmortalidad == true) // Inmortalidad Passive
            {
                this.photonView.RPC("InmortalidadAnimation", RpcTarget.All);
                this.inmortalidad = false;
                Unit2 unit2 = this.GetComponent<Unit2>();
                this.health = unit2.maxHp;
                unit2.healthFillImage.fillAmount = 1.0f;
                gm.UpdateStatsPanel();
                return;
            }
            
            Instantiate(deathEffect, transform.position, Quaternion.identity);
            gm.ResetTiles();
            gm.RemoveStatsPanel(this);
            //Destroy(this.gameObject);
            //GameManager.instance.CheckWinCondition();
        }

        gm.UpdateStatsPanel();
        // enemy.photonView.RPC("TakeDamage", PlayerController.enemy.photonPlayer, attackDamage);
        enemy.photonView.RPC("TakeDamage", RpcTarget.All, attackDamage);
    }

    public void AttackSpacebase(Spacebase spacebase)
    {
        if (gm.selectedUnit.cantAttack == true)
        {
            return;
        }
        
        camAnim.SetTrigger("shake");

        gm.selectedUnit.actionPoints -= 2;
        UpdateActionPointsText();

        
        //hasAttacked = true;

        int spacebaseDamage = Mathf.Max(0, attackDamage - spacebase.physicalArmor);
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
            //Destroy(enemy.gameObject);
            GetWalkableTiles();
            gm.RemoveSpacebaseStatsPanel(spacebase);
            //GameManager.instance.CheckWinCondition();
            
            //GameManager.instance.photonView.RPC("WinGame", RpcTarget.All, PlayerController.me == GameManager.instance.leftPlayer ? 0 : 1);
        }

        gm.UpdateStatsPanel();
        spacebase.photonView.RPC("TakeDamage", PlayerController.enemy.photonPlayer, attackDamage);
        // spacebase.photonView.RPC("TakeDamage", RpcTarget.All, attackDamage);
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
                if (playerNumber != unit.playerNumber)
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

        foreach (Spacebase spacebase in FindObjectsOfType<Spacebase>())
        {
            spacebase.weaponIcon.SetActive(false);
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
                transform.position = Vector2.MoveTowards(transform.position, new Vector2(tilePos.x, transform.position.y), moveSpeed * Time.deltaTime);
                yield return null;
        }

        while(transform.position.y != tilePos.y)
        {
                transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x, tilePos.y), moveSpeed * Time.deltaTime);
                yield return null;
        }
        
        hasMoved = true;
        ResetWeaponIcons();
        GetEnemies();
        gm.MoveStatsPanel(this);
    }

    IEnumerator Movement()
    {
        photonView.RPC("RunAnim", RpcTarget.All);
        while(transform.position != movePoint.position)
        {
            transform.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.deltaTime);
            yield return null;
        }

        
        UpdateActionPointsText();
        ResetWeaponIcons();
        GetEnemies();
        gm.MoveStatsPanel(this);
        photonView.RPC("RunAnimOff", RpcTarget.All);
    }

    IEnumerator DanzaDeLluviaMovement()
    {
        photonView.RPC("RunAnim", RpcTarget.All);
        while(transform.position != movePoint.position)
        {
            transform.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.deltaTime);
            yield return null;
        }

        actionPoints -= 2;
        UpdateActionPointsText();
        ResetWeaponIcons();
        GetEnemies();
        gm.MoveStatsPanel(this);
        photonView.RPC("RunAnimOff", RpcTarget.All);
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

    [PunRPC]
    public void UpdateActionPointsText()
    {
        foreach (Unit unit in FindObjectsOfType<Unit>())
        {
            actionPointsText.text = actionPoints.ToString();
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(health);
            stream.SendNext(actionPoints);
            stream.SendNext(attackDamage);
            stream.SendNext(maxAttackRange);
            stream.SendNext(physicalArmor);
            stream.SendNext(hollyArmor);
            stream.SendNext(demonArmor);
            stream.SendNext(defenseDamage);
            UpdateActionPointsText();
            gm.UpdateStatsPanel();
        } 
        else if (stream.IsReading)
        {
            health = (int)stream.ReceiveNext();
            actionPoints = (int)stream.ReceiveNext();
            attackDamage = (int)stream.ReceiveNext();
            maxAttackRange = (int)stream.ReceiveNext();
            physicalArmor = (int)stream.ReceiveNext();
            hollyArmor = (int)stream.ReceiveNext();
            demonArmor = (int)stream.ReceiveNext();
            defenseDamage = (int)stream.ReceiveNext();
            UpdateActionPointsText();
            gm.UpdateStatsPanel();
        }
    }

    IEnumerator JineteLegendarioUncast(Unit unit, float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        unit.attackDamage = Mathf.Max(0, unit.attackDamage - 1);
    }

    [PunRPC]
    public void VictoryAnim()
    {
        character.Animator.SetTrigger("Victory Trigger");
    }

    [PunRPC]
    public void RunAnim()
    {
        character.Animator.SetBool("Run", true);
    }

    [PunRPC]
    public void RunAnimOff()
    {
        character.Animator.SetBool("Run", false);
    }

    [PunRPC]
    public void LanzaGloriosaDebuff()
    {
        Instantiate(darkMagicAuraA, this.transform.position, Quaternion.identity);
        this.attackDamage = (int)(this.attackDamage / 2);
    }

    [PunRPC]
    public void DisparoParalizadorDebuff()
    {
        Instantiate(pickupDiamond2, this.transform.position, Quaternion.identity);
        this.cantMove = true;
        StartCoroutine(DisparoParalizadorUncast(this, 10f));
    }

    IEnumerator DisparoParalizadorUncast(Unit unit, float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        unit.cantMove = false;
    }

    [PunRPC]
    public void PaletaPegosteosaDebuff()
    {
        Instantiate(disruptiveForce, this.transform.position, Quaternion.identity);
        this.actionPoints -= 2;
        if (this.actionPoints <= 0)
        {
            this.actionPoints = 0;
        }
    }

    IEnumerator FuriaEternaUncast(Unit unit, float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        unit.cantMove = false;
    }

    [PunRPC]
    public void LanzaDelAmorDebuff()
    {
        Instantiate(brokenHeart, this.transform.position, Quaternion.identity);
        this.cantAttack = true;
        StartCoroutine(LanzaDelAmorUncast(this, 10f));
    }

    IEnumerator LanzaDelAmorUncast(Unit unit, float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        unit.cantAttack = false;
    }

    [PunRPC]
    public void LloriqueoEnemy()
    {
        Instantiate(darkMagicAuraA, this.transform.position, Quaternion.identity);
        this.attackDamage = (int)(this.attackDamage / 2);
        StartCoroutine(LloriqueoUncastEnemy(this, 10f));
    }

    [PunRPC]
    public void LloriqueoAlly()
    {
        Instantiate(fireShield, this.transform.position, Quaternion.identity);
        this.attackDamage = (int)(this.attackDamage * 2);
        StartCoroutine(LloriqueoUncastAlly(this, 10f));
    }

    IEnumerator LloriqueoUncastEnemy(Unit unit, float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        this.attackDamage = (int)(this.attackDamage * 2);
    }

    IEnumerator LloriqueoUncastAlly(Unit unit, float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        this.attackDamage = (int)(this.attackDamage / 2);
    }

    [PunRPC]
    public void SermonDebuff()
    {
        Instantiate(darkMagicAuraA, this.transform.position, Quaternion.identity);
        this.attackDamage = Mathf.Max(0, this.attackDamage - 1);
    }

    [PunRPC]
    public void AhorcamientoDebuff()
    {
        Instantiate(pickupDiamond2, this.transform.position, Quaternion.identity);
        Instantiate(brokenHeart, this.transform.position, Quaternion.identity);
        this.cantMove = true;
        this.cantAttack = true;
        StartCoroutine(AhorcamientoUncast(this, 5f));
    }

    IEnumerator AhorcamientoUncast(Unit unit, float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        this.cantMove = false;
        this.cantAttack = false;
    }

    [PunRPC]
    public void ImposicionRealEnemy()
    {
        foreach (Unit units in FindObjectsOfType<Unit>())
        {
            this.cantAttack = true;
            StartCoroutine(ImposicionRealUncast(this, 10f));
        }
    }

    IEnumerator ImposicionRealUncast(Unit unit, float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        this.cantAttack = false;
    }

    [PunRPC]
    public void SonidoDeTigreEnemy()
    {
        Instantiate(pickupDiamond2, this.transform.position, Quaternion.identity);
        this.cantMove = true;
        StartCoroutine(SonidoDeTigreUncastEnemy(this, 5f));
    }

    [PunRPC]
    public void SonidoDeTigreAlly()
    {
        Instantiate(magicAuraBRunic, this.transform.position, Quaternion.identity);
        this.actionPoints += 2;
        this.cantMove = true;
        StartCoroutine(SonidoDeTigreUncastAlly(this, 5f));
    }

    IEnumerator SonidoDeTigreUncastEnemy(Unit unit, float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        this.cantMove = false;
    }

    IEnumerator SonidoDeTigreUncastAlly(Unit unit, float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        this.cantMove = false;
    }

    [PunRPC]
    public void MaldicionEnemy()
    {
        foreach (Unit units in FindObjectsOfType<Unit>())
            {
                units.photonView.RPC("MaldicionAnimation", RpcTarget.All);
                units.cantAttack = true;
                StartCoroutine(MaldicionUncast(units, 5f));
            }
    }

    [PunRPC]
    public void MaldicionAnimation()
    {
        Instantiate(brokenHeart, this.transform.position, Quaternion.identity);
    }

    IEnumerator MaldicionUncast(Unit unit, float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        unit.cantAttack = false;
    }

    [PunRPC]
    public void SonidoDeAveAlly()
    {
        this.actionPoints += 2;
        this.cantAttack = true;
        StartCoroutine(SonidoDeAveUncastAlly(this, 10f));
    }

    IEnumerator SonidoDeAveUncastAlly(Unit unit, float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        this.cantAttack = false;
    }

    IEnumerator DanzaDeLluviaUncast(Unit unit, float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        this.danzaDeLluviaCast = false;
    }

    [PunRPC]
    public void FuriaDeLasBestiasAlly()
    {
        Instantiate(fireShield, this.transform.position, Quaternion.identity);
        this.attackDamage += 1;
        StartCoroutine(FuriaDeLasBestiasUncastAlly(this, 10f));
    }

    IEnumerator FuriaDeLasBestiasUncastAlly(Unit unit, float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        this.attackDamage = Mathf.Max(0, this.attackDamage - 1);
    }

    [PunRPC]
    public void HacerOjitosEnemy()
    {
        Instantiate(brokenHeart, this.transform.position, Quaternion.identity);
        this.cantAttack = true;
        StartCoroutine(HacerOjitosUncastEnemy(this, 10f));
    }

    IEnumerator HacerOjitosUncastEnemy(Unit unit, float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        this.cantAttack = false;
    }

    [PunRPC]
    public void ContarChisteEnemy()
    {
        Instantiate(pickupDiamond2, this.transform.position, Quaternion.identity);
        this.cantMove = true;
        StartCoroutine(ContarChisteUncastEnemy(this, 10f));
    }

    [PunRPC]
    public void ContarChisteAlly()
    {
        Instantiate(magicAuraBRunic, this.transform.position, Quaternion.identity);
        this.actionPoints += 1;
    }

    IEnumerator ContarChisteUncastEnemy(Unit unit, float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        this.cantMove = false;
    }

    [PunRPC]
    public void CalzonChinoDebuff()
    {
        this.alliesInAOERange.Clear();
        foreach (Unit unitInRange in FindObjectsOfType<Unit>())
            {
            if ((Mathf.Abs(transform.position.x - unitInRange.transform.position.x) + Mathf.Abs(transform.position.y - unitInRange.transform.position.y) <= 2))
            {
                if (this.playerNumber == unitInRange.playerNumber)
                {
                    this.alliesInAOERange.Add(unitInRange);
                    if (this.alliesInAOERange.Contains(unitInRange))
                    {
                        gm.selectedUnit.AttackUE(unitInRange, "calzonChinoEnemy");
                    }
                }
            }
            }
    }

    [PunRPC]
    public void CalzonChinoEnemy()
    {
        Instantiate(pickupDiamond2, this.transform.position, Quaternion.identity);
        this.cantMove = true;
        StartCoroutine(CalzonChinoUncastEnemy(this, 10f));
    }

    IEnumerator CalzonChinoUncastEnemy(Unit unit, float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        this.cantMove = false;
    }

    [PunRPC]
    public void TeVoyAMearWeyDebuff()
    {
        this.alliesInAOERange.Clear();
        foreach (Unit unitInRange in FindObjectsOfType<Unit>())
            {
            if ((Mathf.Abs(transform.position.x - unitInRange.transform.position.x) + Mathf.Abs(transform.position.y - unitInRange.transform.position.y) <= 2))
            {
                if (this.playerNumber == unitInRange.playerNumber)
                {
                    this.alliesInAOERange.Add(unitInRange);
                    if (this.alliesInAOERange.Contains(unitInRange))
                    {
                        gm.selectedUnit.AttackUE(unitInRange, "teVoyAMearWeyEnemy");
                    }
                }
            }
            }
    }

    [PunRPC]
    public void TeVoyAMearWeyEnemy()
    {
        Instantiate(brokenHeart, this.transform.position, Quaternion.identity);
        this.cantAttack = true;
        StartCoroutine(TeVoyAMearWeyUncastEnemy(this, 10f));
    }

    IEnumerator TeVoyAMearWeyUncastEnemy(Unit unit, float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        this.cantAttack = false;
    }

    [PunRPC]
    public void HachaDeTruenoDebuff()
    {
        this.alliesInAOERange.Clear();
        foreach (Unit unitInRange in FindObjectsOfType<Unit>())
            {
            if ((Mathf.Abs(transform.position.x - unitInRange.transform.position.x) + Mathf.Abs(transform.position.y - unitInRange.transform.position.y) <= 1))
            {
                if (this.playerNumber == unitInRange.playerNumber)
                {
                    this.alliesInAOERange.Add(unitInRange);
                    if (this.alliesInAOERange.Contains(unitInRange))
                    {
                        gm.selectedUnit.AttackUE(unitInRange, "trueno");
                    }
                }
            }
            }
    }

    [PunRPC]
    public void SonidoAturdidorEnemy()
    {
        Instantiate(darkMagicAuraA, this.transform.position, Quaternion.identity);
        this.attackDamage = (int)(this.attackDamage / 2);
        StartCoroutine(SonidoAturdidorUncastEnemy(this, 10f));
    }

    IEnumerator SonidoAturdidorUncastEnemy(Unit unit, float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        this.attackDamage = (int)(this.attackDamage * 2);
    }

    [PunRPC]
    public void EspadaDeFuegoDebuff()
    {
        this.alliesInAOERange.Clear();
        foreach (Unit unitInRange in FindObjectsOfType<Unit>())
            {
            if ((Mathf.Abs(transform.position.x - unitInRange.transform.position.x) + Mathf.Abs(transform.position.y - unitInRange.transform.position.y) <= 1))
            {
                if (this.playerNumber == unitInRange.playerNumber)
                {
                    this.alliesInAOERange.Add(unitInRange);
                    if (this.alliesInAOERange.Contains(unitInRange))
                    {
                        gm.selectedUnit.AttackUE(unitInRange, "espada de fuego");
                    }
                }
            }
            }
    }

    [PunRPC]
    public void DisparoExplosivoDebuff()
    {
        this.alliesInAOERange.Clear();
        foreach (Unit unitInRange in FindObjectsOfType<Unit>())
            {
            if ((Mathf.Abs(transform.position.x - unitInRange.transform.position.x) + Mathf.Abs(transform.position.y - unitInRange.transform.position.y) <= 1))
            {
                if (this.playerNumber == unitInRange.playerNumber)
                {
                    this.alliesInAOERange.Add(unitInRange);
                    if (this.alliesInAOERange.Contains(unitInRange))
                    {
                        gm.selectedUnit.AttackUE(unitInRange, "disparo explosivo");
                    }
                }
            }
            }
    }

    [PunRPC]
    public void AvalanchaDebuff()
    {
        this.alliesInAOERange.Clear();
        foreach (Unit unitInRange in FindObjectsOfType<Unit>())
            {
            if ((Mathf.Abs(transform.position.x - unitInRange.transform.position.x) + Mathf.Abs(transform.position.y - unitInRange.transform.position.y) <= 2))
            {
                if (this.playerNumber == unitInRange.playerNumber)
                {
                    this.alliesInAOERange.Add(unitInRange);
                    if (this.alliesInAOERange.Contains(unitInRange))
                    {
                        gm.selectedUnit.AttackUE(unitInRange, "holly");
                    }
                }
            }
            }
    }

    [PunRPC]
    public void LanzagranadasDebuff()
    {
        this.alliesInAOERange.Clear();
        foreach (Unit unitInRange in FindObjectsOfType<Unit>())
            {
            if ((Mathf.Abs(transform.position.x - unitInRange.transform.position.x) + Mathf.Abs(transform.position.y - unitInRange.transform.position.y) <= 2))
            {
                if (this.playerNumber == unitInRange.playerNumber)
                {
                    this.alliesInAOERange.Add(unitInRange);
                    if (this.alliesInAOERange.Contains(unitInRange))
                    {
                        gm.selectedUnit.AttackUE(unitInRange, "demon");
                    }
                }
            }
            }
    }

    [PunRPC]
    public void AsedioEnemy()
    {
        this.attackDamage = Mathf.Max(0, this.attackDamage - 1);
    }

    [PunRPC]
    public void AsedioAlly()
    {
        this.attackDamage += 1;
    }

    [PunRPC]
    public void SerenataEnemy()
    {
        Instantiate(brokenHeart, this.transform.position, Quaternion.identity);
        Instantiate(darkMagicAuraA, this.transform.position, Quaternion.identity);
        this.cantAttack = true;
        this.attackDamage = Mathf.Max(0, this.attackDamage - 1);
        StartCoroutine(SerenataUncastEnemy(this, 10f));
    }

    IEnumerator SerenataUncastEnemy(Unit unit, float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        this.cantAttack = false;
        this.attackDamage += 1;
    }

    [PunRPC]
    public void GranadaAturdidoraDebuff()
    {
        this.alliesInAOERange.Clear();
        foreach (Unit unitInRange in FindObjectsOfType<Unit>())
            {
            if ((Mathf.Abs(transform.position.x - unitInRange.transform.position.x) + Mathf.Abs(transform.position.y - unitInRange.transform.position.y) <= 1))
            {
                if (this.playerNumber == unitInRange.playerNumber)
                {
                    this.alliesInAOERange.Add(unitInRange);
                    if (this.alliesInAOERange.Contains(unitInRange))
                    {
                        gm.selectedUnit.AttackUE(unitInRange, "ataque sonico");
                        gm.selectedUnit.AttackUE(unitInRange, "granada aturdidora");
                    }
                }
            }
            }
    }

    [PunRPC]
    public void GranadaAturdidoraEnemy()
    {
        Instantiate(pickupDiamond2, this.transform.position, Quaternion.identity);
        this.cantMove = true;
        StartCoroutine(GranadaAturdidoraUncastEnemy(this, 10f));
    }

    IEnumerator GranadaAturdidoraUncastEnemy(Unit unit, float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        this.cantMove = false;
    }

    [PunRPC]
    public void EspadaDeLuzDebuff()
    {
        this.absorcionDeEnergiaDebuff = true;
        StartCoroutine(EspadaDeLuzUncast(this, 5f));
    }

    IEnumerator EspadaDeLuzUncast(Unit unit, float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        this.absorcionDeEnergiaDebuff = false;
    }

    [PunRPC]
    public void InvocacionDeAngelesEnemy()
    {
        Instantiate(pickupDiamond2, this.transform.position, Quaternion.identity);
        this.cantMove = true;
        StartCoroutine(InvocacionDeAngelesUncastEnemy(this, 5f));
    }

    [PunRPC]
    public void InvocacionDeAngelesAlly()
    {
        Instantiate(magicAuraBRunic, this.transform.position, Quaternion.identity);
        this.actionPoints += 1;
    }

    IEnumerator InvocacionDeAngelesUncastEnemy(Unit unit, float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        this.cantMove = false;
    }

    [PunRPC]
    public void OrdenDeLaEmperatrizAlly()
    {
        Instantiate(fireShield, this.transform.position, Quaternion.identity);
        Instantiate(pickupDiamond2, this.transform.position, Quaternion.identity);
        Instantiate(brokenHeart, this.transform.position, Quaternion.identity);
        Unit2 unit2 = this.GetComponent<Unit2>();
        this.attackDamage += (unit2.maxHp - unit2.curHp);
        this.cantMove = true;
        this.cantAttack = true;
        StartCoroutine(OrdenDeLaEmperatrizUncast(this, 5f));
    }

    IEnumerator OrdenDeLaEmperatrizUncast(Unit unit, float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        this.cantMove = false;
        this.cantAttack = false;
    }

    [PunRPC]
    public void HacerleComoGatitoEnemy()
    {
        Instantiate(pickupDiamond2, this.transform.position, Quaternion.identity);
        Instantiate(brokenHeart, this.transform.position, Quaternion.identity);
        this.cantAttack = true;
        this.cantMove = true;
        StartCoroutine(HacerleComoGatitoUncastEnemy(this, 10f));
    }

    IEnumerator HacerleComoGatitoUncastEnemy(Unit unit, float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        this.cantMove = false;
        this.cantAttack = false;
    }

    [PunRPC]
    void Taunt ()
    {   
        this.taunt.SetActive(true);
        Instantiate(fireworkTrailsGravity, this.transform.position, Quaternion.identity);
        StartCoroutine(DisableTaunt(this, 3f));
    }

    IEnumerator DisableTaunt(Unit unit, float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        unit.taunt.SetActive(false);
    }

    [PunRPC]
    void AventarHuevosAnimation ()
    {   
        Instantiate(hitBOrange, this.transform.position, Quaternion.identity);
    }

    [PunRPC]
    void GloriaEternaAnimation ()
    {   
        Instantiate(fireShield, this.transform.position, Quaternion.identity);
        Instantiate(pickupHeart, this.transform.position, Quaternion.identity);
    }

    [PunRPC]
    void JineteLegendarioAnimation ()
    {   
        Instantiate(fireShield, this.transform.position, Quaternion.identity);
    }

    [PunRPC]
    void InmortalidadAnimation ()
    {   
        Instantiate(pickupStar, this.transform.position, Quaternion.identity);
        Instantiate(resurrectionLightCircle, this.transform.position, Quaternion.identity);
    }

    [PunRPC]
    void UniverseUEAnimation ()
    {   
        Instantiate(wwExplosionC, this.transform.position, Quaternion.identity);
    }

    [PunRPC]
    void TruenoUEAnimation ()
    {   
        Instantiate(hitElectricCAir, this.transform.position, Quaternion.identity);
    }

    [PunRPC]
    void DemonHitAnimation ()
    {   
        Instantiate(hitBOrange, this.transform.position, Quaternion.identity);
    }

    [PunRPC]
    void LanzaInfernalAnimation ()
    {   
        Instantiate(sparksHitBSphere, this.transform.position, Quaternion.identity);
        Instantiate(hitLightCAir, this.transform.position, Quaternion.identity);
    }

    [PunRPC]
    void LetalidadAnimation ()
    {   
        Instantiate(skullExplosion, this.transform.position, Quaternion.identity);
    }

    [PunRPC]
    void CantoAnimation ()
    {   
        Instantiate(magicAuraBRunic, this.transform.position, Quaternion.identity);
    }

    [PunRPC]
    void AtaqueSonicoAnimation ()
    {   
        Instantiate(impact01, this.transform.position, Quaternion.identity);
    }

    [PunRPC]
    void EspadaDeFuegoAnimation ()
    {   
        Instantiate(fireExplosion, this.transform.position, Quaternion.identity);
    }

    [PunRPC]
    void EspadaDeLaMuerteAnimation ()
    {   
        Instantiate(skullExplosion, this.transform.position, Quaternion.identity);
    }

    [PunRPC]
    void AlquimiaAnimation ()
    {   
        Instantiate(magicPoof, this.transform.position, Quaternion.identity);
    }

    [PunRPC]
    void BaileAnimation ()
    {   
        Instantiate(magicAuraBRunic, this.transform.position, Quaternion.identity);
    }

    [PunRPC]
    void CuracionAnimation ()
    {   
        Instantiate(auraBubbleC, this.transform.position, Quaternion.identity);
    }

    [PunRPC]
    void CuracionAvanzadaAnimation ()
    {   
        Instantiate(auraBubbleC, this.transform.position, Quaternion.identity);
    }

    [PunRPC]
    void DesmembrarAnimation ()
    {   
        Instantiate(skullExplosion, this.transform.position, Quaternion.identity);
    }

    [PunRPC]
    void DisparoExplosivoAnimation ()
    {   
        Instantiate(fireExplosion, this.transform.position, Quaternion.identity);
    }

    [PunRPC]
    void TrifuerzaAnimation ()
    {   
        Instantiate(wwExplosionC, this.transform.position, Quaternion.identity);
    }

    [PunRPC]
    void EspirituDeGaiaAnimation ()
    {   
        Instantiate(magicPoof, this.transform.position, Quaternion.identity);
    }

    [PunRPC]
    void DemonUEAnimation ()
    {   
        Instantiate(impact02, this.transform.position, Quaternion.identity);
    }

    [PunRPC]
    public void GolpeLetalAnimation()
    {
        Instantiate(fireShield, this.transform.position, Quaternion.identity);
    }

    [PunRPC]
    public void AbsorcionDeEnergiaAnimation()
    {
        Instantiate(auraBubbleC, this.transform.position, Quaternion.identity);
    }

    [PunRPC]
    void AsesinatoAnimation ()
    {   
        Instantiate(skullExplosion, this.transform.position, Quaternion.identity);
    }

    [PunRPC]
    void EspadaDelDemonioAnimation ()
    {   
        Instantiate(skullExplosion, this.transform.position, Quaternion.identity);
    }

    [PunRPC]
    void HollyUEAnimation ()
    {   
        Instantiate(magicHit, this.transform.position, Quaternion.identity);
    }

    [PunRPC]
    void HollyHitAnimation ()
    {   
        Instantiate(magicHit, this.transform.position, Quaternion.identity);
    }

    [PunRPC]
    void KatanaAnimation ()
    {   
        Instantiate(skullExplosion, this.transform.position, Quaternion.identity);
    }

    [PunRPC]
    void ChirrinChinChinAnimation ()
    {   
        Instantiate(batsCloud, this.transform.position, Quaternion.identity);
    }

    [PunRPC]
    public void SlashAnim()
    {
        character.Animator.SetTrigger("Slash");
    }

    [PunRPC]
    public void JabAnim()
    {
        character.Animator.SetTrigger("Jab");
    }

    [PunRPC]
    public void HitAnim()
    {
        character.Animator.SetTrigger("Hit");
    }
}
