using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spells : MonoBehaviour
{   
    // AP: Passive
    // Effect: Lifeleech. Difficulty: 1
    public static void AbsorcionDeEnergia()
    {
        
    }

    // AP: 2
    // Effect: 75% Instakill if Enemy is near water. Difficulty: No
    /*
    public static void Ahogamiento()
    {
        
    }
    */

    // AP: 4
    // Effect: Ataque normal, enemigo pierde 1 Turno. Difficulty: Done
    /*
    public void Ahorcamiento(Unit unit)
    {
        if (unit.ahorcamientoCast == false)
        {
            photonView.RPC("VictoryAnim", RpcTarget.All);
            unit.ahorcamientoCast = true;
            unit.actionPoints -= 4;
        }
    }
    */

    // AP: Passive
    // Effect: Normal hit teleports across. Difficulty: No
    /*
    public static void AlasDelInfierno()
    {
        
    }
    */
    
    // AP: 2
    // Effect: Hits Elemental for 1 Turn. Difficulty: 1
    public static void Alquimia()
    {
        
    }

    // AP: 4
    // Effect: Next attack is 3x Physical Missile. Difficulty: Done
    /*
    public void Ametralladora(Unit unit)
    {
        if (unit.ametralladoraCast == false)
        {
            photonView.RPC("VictoryAnim", RpcTarget.All);
            unit.ametralladoraCast = true;
            unit.actionPoints -= 4;
            unit.UpdateActionPointsText();
        }
    }
    */

    // AP: Passive
    // Effect: Turns Invisible when receive damage for 2 Turns. Difficulty: 1
    public static void AmoDeLasSombras()
    {
        
    }
    
    // AP: 6
    // Effect: All allied units get +1 AP. Difficulty: Done
    /*
    public void AmoDelUniverso(Unit unit)
    {
        if (unit.amoDelUniversoCast == false)
        {
            photonView.RPC("VictoryAnim", RpcTarget.All);
            unit.amoDelUniversoCast = true;
            unit.actionPoints -= 6;
            foreach (Unit units in FindObjectsOfType<Unit>())
            {
                units.actionPoints += 1;
            }
            unit.amoDelUniversoCast = false;
        }
    }
    */

    // AP: 4
    // Effect: +1 Range. Difficulty: Done
    /*
    public void ApuntarLanza(Unit unit)
    {
        if (unit.apuntarLanzaCast == false)
        {
            photonView.RPC("VictoryAnim", RpcTarget.All);
            unit.apuntarLanzaCast = true;
            unit.actionPoints -= 4;
            unit.UpdateActionPointsText();
            unit.maxAttackRange += 1;
            unit.apuntarLanzaCast = false;
        }
    }
    */

    // AP: Passive
    // Effect: Physical defense = 99. Difficulty: Done
    /*
    public static void ArmaduraDeDragon()
    {
        
    }
    */

    // AP: Passive
    // Effect: Immune to Physical, Counters Holly. Difficulty: 1
    public static void ArmaduraLegendaria()
    {
        
    }

    // AP: 4
    // Effect: +2 Attack Damage, +2 Counter & +1 Defense. Loses 1 turn. Difficulty: Done
    /*
    public void ArteMarcial(Unit unit)
    {
        if (unit.arteMarcialCast == false)
        {
            photonView.RPC("VictoryAnim", RpcTarget.All);
            unit.arteMarcialCast = true;
            unit.actionPoints -= 4;
            unit.UpdateActionPointsText();
            unit.attackDamage += 2;
            unit.physicalArmor += 1;
            unit.defenseDamage += 2;
            unit.cantMove = true;
            unit.cantAttack = true;
            StartCoroutine(ArteMarcialUncast(gm.selectedUnit, 5f));
        }
    }
    */

    // AP: Passive
    // Effect: Pega y cobra gold. Difficulty: 1
    public static void Asalto()
    {
        
    }

    // AP: 4
    // Effect: 2 SQT +1 Attack Damage UE for 2 Turns. Difficulty: 1
    public static void Asedio()
    {
        
    }

    // AP: Passive
    // Effect: 15% de probabilidad de Instakill. Difficulty: Done
    /*
    public static void Asesinato()
    {
        unit.asesinato = true; ON START
    }
    */

    // AP: 2
    // Effect: Brinca 1 Tile y Ataca. Difficulty: No
    /*
    public static void AtaqueDeSalto()
    {
        
    }
    */

    // AP: 4
    // Effect: 2 Tiles Physical UE. Difficulty: 1
    public static void AtaqueSonico()
    {
        
    }

    // AP: 4
    // Effect: Physical Explo at 2-3 SQT. Difficulty: 1
    public static void Avalancha()
    {
        
    }

    // AP: 6
    // Effect: 1-6x Demon Attack at 2-3 SQT. Difficulty: Done
    /*
    public void AventarHuevos(Unit unit)
    {
        if (unit.aventarHuevosCast == false)
        {
            photonView.RPC("VictoryAnim", RpcTarget.All);
            unit.aventarHuevosCast = true;
            unit.actionPoints -= 6;
        }
    }
    */

    // AP: 2
    // Effect: Allies in 3 SQT get +2 AP. Difficulty: Done
    /*
    public void Baile(Unit unit)
    {
        if (unit.baileCast == false)
        {
            photonView.RPC("VictoryAnim", RpcTarget.All);
            unit.baileCast = true;
            unit.actionPoints -= 4;
            alliesInAOERange.Clear();
            
            foreach (Unit unitInRange in FindObjectsOfType<Unit>())
            {
            if ((Mathf.Abs(transform.position.x - unitInRange.transform.position.x) + Mathf.Abs(transform.position.y - unitInRange.transform.position.y) <= 3))
            {
                if (unit.playerNumber == unitInRange.playerNumber)
                {
                    this.alliesInAOERange.Add(unitInRange);
                    if (this.alliesInAOERange.Contains(unitInRange))
                    {
                        gm.selectedUnit.AttackUE(unitInRange, "baile");
                    }
                }
            }
            }
            unit.baileCast = false;
        }
    }
    */

    // AP: 4
    // Effect: 2-3 Tiles Holly Missile. Difficulty: Nadie lo tiene.
    public static void BalasDePlata()
    {
        
    }

    // AP: 2
    // Effect: Mueve obstaculo “across”. Difficulty: 1
    public static void BaseEspacial()
    {
        
    }

    // AP: 4
    // Effect: +3 Range & +2 Damage for 2 Turns. Difficulty: Done
    /*
    public void Bazooka(Unit unit)
    {
        if (unit.bazookaCast == false)
        {
            photonView.RPC("VictoryAnim", RpcTarget.All);
            unit.bazookaCast = true;
            unit.actionPoints -= 4;
            unit.maxAttackRange += 3;
            unit.attackDamage += 2;
            gm.UpdateStatsPanel();
            StartCoroutine(BazookaUncast(gm.selectedUnit, 10f));
            gm.UpdateStatsPanel();
        }
    }
    */

    // AP: 2
    // Effect: El siguiente hit le hace 0 Damage. Difficulty: Bug
    public static void Bloqueo()
    {
        
    }

    // AP: 2
    // Effect: 2x Holly Attack. Difficulty: Done
    /*
    public void BolaDeNieve(Unit unit)
    {
        if (unit.bolaDeNieveCast == false)
        {
            photonView.RPC("VictoryAnim", RpcTarget.All);
            unit.bolaDeNieveCast = true;
            unit.actionPoints -= 2;
            unit.UpdateActionPointsText();
        }
    }
    */

    // AP: 4
    // Effect: Invisible Explo UE for 2 Turns. Difficulty: 1
    public static void BombaDeHumo()
    {
        
    }

    // AP: 2
    // Effect: Ally becomes Invulnerable for 1 Turn. Difficulty: 1
    public static void Bondad()
    {
        
    }

    // AP: 0
    // Effect: +1 Attack Damage, may cost. Difficulty: 1
    public static void Botin()
    {
        
    }

    // AP: Passive
    // Effect: May costs to hit. Difficulty: 1
    public static void Brutalidad()
    {
        
    }

    // AP: 6
    // Effect: Normal hit with paralyze 2 SQT UE. Difficulty: 1
    public static void CalzonChino()
    {
        
    }

    // AP: Passive
    // Effect: When moves, leaves golden tile (HP Heal). Difficulty: No
    /*
    public static void CaminoDorado()
    {
        
    }
    */

    // AP: 4
    // Effect: Turns Invisible for 2 Turns. Difficulty: 1
    public static void Camuflaje()
    {
        
    }

    // AP: 3
    // Effect: Allies in 3 SQT get +1 AP. Difficulty: Done
    /*
    public void Canto(Unit unit)
    {
        if (unit.cantoCast == false)
        {
            photonView.RPC("VictoryAnim", RpcTarget.All);
            unit.cantoCast = true;
            unit.actionPoints -= 3;
            alliesInAOERange.Clear();
            
            foreach (Unit unitInRange in FindObjectsOfType<Unit>())
            {
            if ((Mathf.Abs(transform.position.x - unitInRange.transform.position.x) + Mathf.Abs(transform.position.y - unitInRange.transform.position.y) <= 3))
            {
                if (unit.playerNumber == unitInRange.playerNumber)
                {
                    this.alliesInAOERange.Add(unitInRange);
                    if (this.alliesInAOERange.Contains(unitInRange))
                    {
                        gm.selectedUnit.AttackUE(unitInRange, "canto");
                    }
                }
            }
            }
            unit.cantoCast = false;
        }
    }
    */

    // AP: 2
    // Effect: 2x Demon Attack. Difficulty: Done
    /*
    public void Chispazo(Unit unit)
    {
        if (unit.chispazoCast == false)
        {
            photonView.RPC("VictoryAnim", RpcTarget.All);
            unit.chispazoCast = true;
            unit.actionPoints -= 2;
            unit.UpdateActionPointsText();
        }
    }
    */

    // AP: 2
    // Effect: Mueve aliado de posición “across”. Difficulty: 1
    public static void Comando()
    {
        
    }

    // AP: 6
    // Effect: Convierte el Attack Type de Allies en Demon por 2 Turns. Difficulty: 1
    public static void Conjuro()
    {
        
    }

    // AP: 6
    // Effect: Grito del Dragón a todos los Aliados por 2 turnos. Difficulty: Done
    /*
    public void Conquista(Unit unit)
    {
        if (unit.conquistaCast == false)
        {
            photonView.RPC("VictoryAnim", RpcTarget.All);
            unit.conquistaCast = true;
            unit.actionPoints -= 6;
            foreach (Unit units in FindObjectsOfType<Unit>())
            {
                units.attackDamage += 1;
                units.physicalArmor += 1;
                units.defenseDamage += 1;
            }
            gm.UpdateStatsPanel();
            StartCoroutine(ConquistaUncast(gm.selectedUnit, 10f));
            gm.UpdateStatsPanel();
            unit.conquistaCast = false;
        }
    }
    */

    // AP: 6
    // Effect: Crea Obstáculo. Difficulty: No
    /*
    public static void ConstruccionDeTeletransportador()
    {
        
    }
    */

    // AP: 6
    // Effect: 3 SQT UE Sing & enemies get paralyzed for 2 Turns. Difficulty: 1
    public static void ContarChiste()
    {
        
    }

    // AP: 4
    // Effect: Possess for 2 Turns. Difficulty: 1
    public static void ControlMental()
    {
        
    }

    // AP: 2
    // Effect: +1 Range to Allies. Difficulty: 1
    public static void CreacionDeBalasLaser()
    {
        
    }

    // AP: 4
    // Effect: +1 AP to Allies. Difficulty: 1
    public static void CreacionDeBrujula()
    {
        
    }

    // AP: 2
    // Effect: +1 Attack Damage & +1 Range for 2 Turns. Difficulty: Done
    /*
    public void CrearMuniciones(Unit unit)
    {
        if (unit.crearMunicionesCast == false)
        {
            photonView.RPC("VictoryAnim", RpcTarget.All);
            unit.crearMunicionesCast = true;
            unit.actionPoints -= 2;
            unit.UpdateActionPointsText();
            unit.maxAttackRange += 1;
            unit.attackDamage += 1;
            unit.crearMunicionesCast = false;
            gm.UpdateStatsPanel();
            StartCoroutine(CrearMunicionesUncast(gm.selectedUnit, 10f));
            gm.UpdateStatsPanel();
        }
    }
    */

    // AP: 2
    // Effect: Heals Ally 1 SQT. Difficulty: 1
    public static void Curacion()
    {
        
    }

    // AP: 4
    // Effect: 2x Heals Ally 2-3 SQT. Difficulty: 1
    public static void CuracionAvanzada()
    {
        
    }

    // AP: 6
    // Effect: Movement costs 2 AP for 2 Turns. Difficulty: 1
    public static void DanzaDeLluvia()
    {
        
    }

    // AP: 6
    // Effect: All allies get +1 Attack Damage. All enemies get -2 AP. Difficulty: Done
    /*
    public void DeclaracionDeGuerra(Unit unit)
    {
        if (unit.declaracionDeGuerraCast == false)
        {
            photonView.RPC("VictoryAnim", RpcTarget.All);
            unit.declaracionDeGuerraCast = true;
            unit.actionPoints -= 6;
            foreach (Unit units in FindObjectsOfType<Unit>())
            {
                units.attackDamage += 1;
            }
            photonView.RPC("DeclaracionDeGuerraEnemy", RpcTarget.Others);
            unit.declaracionDeGuerraCast = false;
        }
    }
    */

    // AP: 4
    // Effect: Instakill if enemy HP is less than 50%. Difficulty: Done
    /*
    public void Degollar(Unit unit)
    {
        if (unit.degollarCast == false)
        {
            photonView.RPC("VictoryAnim", RpcTarget.All);
            unit.degollarCast = true;
            unit.actionPoints -= 4;
            unit.UpdateActionPointsText();
        }
    }
    */
    
    // AP: 4
    // Effect: All enemies get -1 Defense. Difficulty: Done
    /*
    public void DemoniosEternos(Unit unit)
    {
        if (unit.demoniosEternosCast == false)
        {
            photonView.RPC("VictoryAnim", RpcTarget.All);
            unit.demoniosEternosCast = true;
            unit.actionPoints -= 6;
            photonView.RPC("DemoniosEternosEnemy", RpcTarget.Others);
            unit.demoniosEternosCast = false;
        }
    }

    [PunRPC]
    public void DemoniosEternosEnemy()
    {
        foreach (Unit units in FindObjectsOfType<Unit>())
        {
            units.physicalArmor -= 1;
            gm.UpdateStatsPanel();
        }
    }
    */

    // AP: 4
    // Effect: 50% Instakill. Difficulty: Done
    /*
    public void Desmembrar(Unit unit)
    {
        if (unit.desmembrarCast == false)
        {
            photonView.RPC("VictoryAnim", RpcTarget.All);
            unit.desmembrarCast = true;
            unit.actionPoints -= 4;
            unit.UpdateActionPointsText();
        }
    }
    */

    // AP: 6
    // Effect: All Units take Demon Damage. Difficulty: Bug
    public static void DestruccionInfinita()
    {
        
    }

    // AP: 4
    // Effect: +3 Range for 2 Turns. Difficulty: 1
    public static void Disparo()
    {
        
    }

    // AP: 4
    // Effect: 2 Tiles Explo Missile. Difficulty: 1
    public static void DisparoExplosivo()
    {
        
    }

    // AP: 4
    // Effect: Paralyze Attack. Difficulty: Done
    /*
    public void DisparoParalizador(Unit unit)
    {
        if (unit.disparoParalizadorCast == false)
        {
            photonView.RPC("VictoryAnim", RpcTarget.All);
            unit.disparoParalizadorCast = true;
            unit.actionPoints -= 4;
        }
    }
    */

    // AP: 1
    // Effect: 2x Physical Attack. Difficulty: Done
    /*
    public void DisparoRapido(Unit unit)
    {
        if (unit.disparoRapidoCast == false)
        {
            photonView.RPC("VictoryAnim", RpcTarget.All);
            unit.disparoRapidoCast = true;
            unit.actionPoints -= 1;
            unit.UpdateActionPointsText();
        }
    }
    */

    // AP: Passive
    // Effect: Demon 2x Attack. Difficulty: Done
    /*
    public static void DobleAtaqueDeDemonio()
    {
        unit.dobleAtaqueDeDemonio = true; ON START
    }
    */

    // AP: 6
    // Effect: If Health = 0 on next turn, comes back to life at 75% HP. Difficulty: Bug
    public static void DragonEterno()
    {
        
    }

    // AP: 6
    // Effect: 1.5x Attack Damage, 1/2 Defense. Can only be casted once. Difficulty: Done
    /*
    public void DragonSagrado(Unit unit)
    {
        if (unit.dragonSagradoCast == false)
        {
            photonView.RPC("VictoryAnim", RpcTarget.All);
            unit.dragonSagradoCast = true;
            unit.actionPoints -= 6;
            unit.UpdateActionPointsText();
            unit.attackDamage = (int)(Mathf.Round(unit.attackDamage * 1.5f));
            unit.physicalArmor = (int)(Mathf.Round(unit.physicalArmor / 2));
        }
    }
    */

    // AP: 2
    // Effect: +1 Defense for 2 Turns. Difficulty: Done
    /*
    public void Entrenamiento(Unit unit)
    {
        if (unit.entrenamientoCast == false)
        {
            photonView.RPC("VictoryAnim", RpcTarget.All);
            unit.entrenamientoCast = true;
            unit.actionPoints -= 2;
            unit.UpdateActionPointsText();
            unit.physicalArmor += 1;
            unit.entrenamientoCast = false;
            gm.UpdateStatsPanel();
            StartCoroutine(EntrenamientoUncast(gm.selectedUnit, 10f));
            gm.UpdateStatsPanel();
        }
    }
    */

    // AP: 4
    // Effect: 2x Defense, 75% Attack Damage. Difficulty: Done
    /*
    public void EscamasDeDragon(Unit unit)
    {
        if (unit.escamasDeDragonCast == false)
        {
            photonView.RPC("VictoryAnim", RpcTarget.All);
            unit.escamasDeDragonCast = true;
            unit.actionPoints -= 4;
            unit.UpdateActionPointsText();
            unit.attackDamage = (int)(Mathf.Round(unit.attackDamage * 0.75f));
            unit.physicalArmor = (int)(Mathf.Round(unit.physicalArmor * 2));
            unit.escamasDeDragonCast = false;
        }
    }
    */

    // AP: 6
    // Effect: +2 Defense, +1 Holly Defense & +1 Demon Defense. Difficulty: Done
    /*
    public void EscudoDeDragon(Unit unit)
    {
        if (unit.escudoDeDragonCast == false)
        {
            photonView.RPC("VictoryAnim", RpcTarget.All);
            unit.escudoDeDragonCast = true;
            unit.actionPoints -= 6;
            unit.UpdateActionPointsText();
            unit.physicalArmor += 2;
            unit.hollyArmor += 1;
            unit.demonArmor += 1;
            unit.escudoDeDragonCast = false;
        }
    }
    */

    // AP: 6
    // Effect: Guard unit at 3 SQT. Difficulty: No
    /*
    public static void EscudoGuardian()
    {
        
    }
    */

    // AP: Passive
    // Effect: 1/2 Damage from Physical. Difficulty: Done
    /*
    public static void EscudoMistico()
    {
        unit.escudoMistico = true; ON START
    }
    */

    // AP: Passive
    // Effect: If alone within 2 SQT UE, Instakills. Difficulty: Bug
    public static void EspadachinDeLaSoledad()
    {
        
    }

    // AP: Passive
    // Effect: Normal Attack hits Holly Explo. Difficulty: 1
    public static void EspadachinMistico()
    {
        
    }

    // AP: Passive
    // Effect: Normal Attack hits explo. Difficulty: 1
    public static void EspadaDeFuego()
    {
        
    }

    // AP: 4
    // Effect: Attack at +3 Attack Damage, 15% Chance instakill. Difficulty: Done
    /*
    public void EspadaDeLaMuerte(Unit unit)
    {
        if (unit.espadaDeLaMuerteCast == false)
        {
            photonView.RPC("VictoryAnim", RpcTarget.All);
            unit.espadaDeLaMuerteCast = true;
            unit.actionPoints -= 4;
        }
    }
    */

    // AP: 2
    // Effect: Attack at +1 Attack Damage, 10% Chance instakill. Difficulty: Done
    /*
    public void EspadaDelDemonio(Unit unit)
    {
        if (unit.espadaDelDemonioCast == false)
        {
            photonView.RPC("VictoryAnim", RpcTarget.All);
            unit.espadaDelDemonioCast = true;
            unit.actionPoints -= 2;
        }
    }
    */

    // AP: Passive
    // Effect: Al pegar, marca al enemigo por 2 Turnos. Aliados al pegar a enemigo marcado hacen life leech. Difficulty: 1
    public static void EspadaDeLuz()
    {
        
    }

    // AP: Passive
    // Effect: Normal hit, 25% posses for 2 Turns. Difficulty: 1
    public static void EspadaDivina()
    {
        
    }

    // AP: 4
    // Effect: Swap with Enemy at 1-2 Tiles. Difficulty: 1
    public static void Espionaje()
    {
        
    }

    // AP: Passive
    // Effect: Counters Elemental. Difficulty: Done
    /*
    public static void EspirituDeGaia()
    {
        unit.espirituDeGaia = true; ON START
    }
    */

    // AP: 4
    // Effect: Cures Ally Debuff at 1-2 SQT. Difficulty: No
    /*
    public static void Exorcismo()
    {
        
    }
    */

    // AP: 2
    // Effect: Doubles AP. Can’t attack for 2 Turns. Difficulty: Done
    /*
    public void Exploracion(Unit unit)
    {
        if (unit.exploracionCast == false)
        {
            photonView.RPC("VictoryAnim", RpcTarget.All);
            unit.exploracionCast = true;
            unit.actionPoints -= 2;
            unit.actionPoints = unit.actionPoints * 2;
            unit.cantAttack = true;
            StartCoroutine(ExploracionUncast(gm.selectedUnit, 10f));
        }
    }
    */

    // AP: 6
    // Effect: 2 Tiles Demon UE. Difficulty: Done
    /*
    public void Explosion(Unit unit)
    {
        if (unit.explosionCast == false)
        {
            photonView.RPC("VictoryAnim", RpcTarget.All);
            unit.explosionCast = true;
            unit.actionPoints -= 4;
            enemiesInAOERange.Clear();
            
            foreach (Unit unitInRange in FindObjectsOfType<Unit>())
            {
            if ((Mathf.Abs(transform.position.x - unitInRange.transform.position.x) + Mathf.Abs(transform.position.y - unitInRange.transform.position.y) <= 2))
            {
                if (unit.playerNumber != unitInRange.playerNumber)
                {
                    this.enemiesInAOERange.Add(unitInRange);
                    if (this.enemiesInAOERange.Contains(unitInRange))
                    {
                        gm.selectedUnit.AttackUE(unitInRange, "demon");
                    }
                }
            }
            }
            unit.explosionCast = false;
        }
    }
    */

    // AP: 6
    // Effect: 2 Tiles Holly UE. Difficulty: Done
    /*
    public void ExplosionDivina(Unit unit)
    {
        if (unit.explosionDivinaCast == false)
        {
            photonView.RPC("VictoryAnim", RpcTarget.All);
            unit.explosionDivinaCast = true;
            unit.actionPoints -= 6;
            enemiesInAOERange.Clear();
            
            foreach (Unit unitInRange in FindObjectsOfType<Unit>())
            {
            if ((Mathf.Abs(transform.position.x - unitInRange.transform.position.x) + Mathf.Abs(transform.position.y - unitInRange.transform.position.y) <= 3))
            {
                if (unit.playerNumber != unitInRange.playerNumber)
                {
                    this.enemiesInAOERange.Add(unitInRange);
                    if (this.enemiesInAOERange.Contains(unitInRange))
                    {
                        gm.selectedUnit.AttackUE(unitInRange, "holly");
                    }
                }
            }
            }
            unit.explosionDivinaCast = false;
        }
    }
    */

    // AP: 6
    // Effect: 3 SQT Universe UE. Difficulty: Done
    /*
    public void ExplosionEstelar(Unit unit)
    {
        if (unit.explosionEstelarCast == false)
        {
            photonView.RPC("VictoryAnim", RpcTarget.All);
            unit.explosionEstelarCast = true;
            unit.actionPoints -= 6;
            enemiesInAOERange.Clear();
            
            foreach (Unit unitInRange in FindObjectsOfType<Unit>())
            {
            if ((Mathf.Abs(transform.position.x - unitInRange.transform.position.x) + Mathf.Abs(transform.position.y - unitInRange.transform.position.y) <= 3))
            {
                if (unit.playerNumber != unitInRange.playerNumber)
                {
                    this.enemiesInAOERange.Add(unitInRange);
                    if (this.enemiesInAOERange.Contains(unitInRange))
                    {
                        gm.selectedUnit.AttackUE(unitInRange, "universe");
                    }
                }
            }
            }
            unit.explosionEstelarCast = false;
        }
    }
    */

    // AP: 6
    // Effect: All enemy units action points = 0. Can only be used once. Difficulty: Done
    /*
    public void FinDelTiempo(Unit unit)
    {
        if (unit.finDelTiempoCast == false)
        {
            photonView.RPC("VictoryAnim", RpcTarget.All);
            unit.finDelTiempoCast = true;
            unit.actionPoints -= 6;
            photonView.RPC("FinDelTiempoEnemy", RpcTarget.Others);
        }
    }

    [PunRPC]
    public void FinDelTiempoEnemy()
    {
        foreach (Unit units in FindObjectsOfType<Unit>())
        {
            units.actionPoints = 0;
        }
    }
    */

    // AP: 2
    // Effect: +1 Attack Damage for 2 Turns. Difficulty: Done
    /*
    public void Fuerza(Unit unit)
    {
        if (unit.fuerzaCast == false)
        {
            photonView.RPC("VictoryAnim", RpcTarget.All);
            unit.fuerzaCast = true;
            unit.actionPoints -= 2;
            unit.UpdateActionPointsText();
            unit.attackDamage += 1;
            unit.fuerzaCast = false;
            gm.UpdateStatsPanel();
            StartCoroutine(FuerzaUncast(gm.selectedUnit, 10f));
            gm.UpdateStatsPanel();
        }
    }
    */

    // AP: 6
    // Effect: +1 Attack Damage a Aliados a 2 SQT por 2 Turns. Difficulty: Done
    /*
    public void FuriaDeLasBestias(Unit unit)
    {
        if (unit.furiaDeLasBestiasCast == false)
        {
            photonView.RPC("VictoryAnim", RpcTarget.All);
            unit.furiaDeLasBestiasCast = true;
            unit.actionPoints -= 6;
            alliesInAOERange.Clear();
            
            foreach (Unit unitInRange in FindObjectsOfType<Unit>())
            {
            if ((Mathf.Abs(transform.position.x - unitInRange.transform.position.x) + Mathf.Abs(transform.position.y - unitInRange.transform.position.y) <= 2))
            {
                if (unit.playerNumber == unitInRange.playerNumber)
                {
                    this.alliesInAOERange.Add(unitInRange);
                    if (this.alliesInAOERange.Contains(unitInRange))
                    {
                        gm.selectedUnit.AttackUE(unitInRange, "furiaDeLasBestiasAlly");
                    }
                }
            }
            }
            unit.furiaDeLasBestiasCast = false;
        }
    }
    */

    // AP: 6
    // Effect: All enemies become paralyzed for 2 turns. Difficulty: Done
    /*
    public void FuriaEterna(Unit unit)
    {
        if (unit.furiaEternaCast == false)
        {
            photonView.RPC("VictoryAnim", RpcTarget.All);
            unit.furiaEternaCast = true;
            unit.actionPoints -= 6;
            photonView.RPC("FuriaEternaEnemy", RpcTarget.Others);
            unit.furiaEternaCast = false;
        }
    }
    */

    // AP: 4
    // Effect: +3 Attack Damage. Difficulty: Done
    /*
    public void FuriaInfernal(Unit unit)
    {
        if (unit.furiaInfernalCast == false)
        {
            photonView.RPC("VictoryAnim", RpcTarget.All);
            unit.furiaInfernalCast = true;
            unit.actionPoints -= 4;
            unit.UpdateActionPointsText();
            unit.attackDamage += 3;
            unit.furiaInfernalCast = false;
        }
    }
    */

    // AP: 4
    // Effect: +2 Attack Damage. Difficulty: Done
    /*
    public void GarraDemoniaca(Unit unit)
    {
        if (unit.garraDemoniacaCast == false)
        {
            photonView.RPC("VictoryAnim", RpcTarget.All);
            unit.garraDemoniacaCast = true;
            unit.actionPoints -= 4;
            unit.UpdateActionPointsText();
            unit.attackDamage += 2;
            unit.garraDemoniacaCast = false;
        }
    }
    */

    // AP: Passive
    // Effect: When kill enemy, gets Grito del Dragon. Difficulty: Done
    /*
    public static void GloriaEterna()
    {
        unit.gloriaEterna = true; ON START
    }
    */

    // AP: 2
    // Effect: Pega y teleports across. Difficulty: No
    /*
    public static void GolpeFantasma()
    {
        
    }
    */

    // AP: Passive
    // Effect: Every successful attack gets +1 Attack Damage. Difficulty: Done
    /*
    public static void GolpeLetal()
    {
        unit.golpeLetal = true; ON START
    }
    */

    // AP: 2
    // Effect: Pega y regresa 1 Tile. Difficulty: 1
    public static void GolpePegaso()
    {
        
    }

    // AP: Passive
    // Effect: Free Attack if in range when Brute Attacks. Difficulty: No
    /*
    public static void Golpiza()
    {
        
    }
    */

    // AP: 4
    // Effect: Paralize Attack w Explo. Difficulty: 1
    public static void GranadaAturdidora()
    {
        
    }

    // AP: 6
    // Effect: 3 Tiles Demon UE. Difficulty: Nadie lo tiene
    public static void GranExplosion()
    {
        
    }

    // AP: 4
    // Effect: +1 Defense, +1 Counter & + 1 Attack Damage. Difficulty: Done
    /*
    public void GritoDelDragon(Unit unit)
    {
        if (unit.gritoDelDragonCast == false)
        {
            photonView.RPC("VictoryAnim", RpcTarget.All);
            unit.gritoDelDragonCast = true;
            unit.actionPoints -= 4;
            unit.UpdateActionPointsText();
            unit.attackDamage += 1;
            unit.physicalArmor += 1;
            unit.defenseDamage += 1;
            unit.gritoDelDragonCast = false;
        }
    }
    */

    // AP: 6
    // Effect: 3 SQT Charm UE. Difficulty: 1
    public static void HacerOjitos()
    {
        
    }

    // AP: 4
    // Effect: Attack & Silences for 2 Turns. Difficulty: 1
    public static void HachaDeLaOscuridad()
    {
        
    }

    // AP: 1
    // Effect: Next attack at +3 Attack Damage. Difficulty: Done
    /*
    public void HachaDelDemonio(Unit unit)
    {
        if (unit.hachaDelDemonioCast == false)
        {
            photonView.RPC("VictoryAnim", RpcTarget.All);
            unit.hachaDelDemonioCast = true;
            unit.actionPoints -= 1;
            unit.UpdateActionPointsText();
        }
    }
    */

    // AP: Passive
    // Effect: Normal Attack hits explo. Difficulty: 1
    public static void HachaDeTrueno()
    {
        
    }

    // AP: 6
    // Effect: Convierte el Attack Type de Allies en Holly por 2 Turns. Difficulty: 1
    public static void Hechizo()
    {
        
    }

    // AP: 4
    // Effect: All allies get +1 Attack Damage. Puede generar Gold. Difficulty: 1
    public static void Imperio()
    {
        
    }

    // AP: 6
    // Effect: 3 Tiles Demon UE. Difficulty: Done
    /*
    public void ImplosionNuclear(Unit unit)
    {
        if (unit.implosionNuclearCast == false)
        {
            photonView.RPC("VictoryAnim", RpcTarget.All);
            unit.implosionNuclearCast = true;
            unit.actionPoints -= 6;
            enemiesInAOERange.Clear();
            
            foreach (Unit unitInRange in FindObjectsOfType<Unit>())
            {
            if ((Mathf.Abs(transform.position.x - unitInRange.transform.position.x) + Mathf.Abs(transform.position.y - unitInRange.transform.position.y) <= 3))
            {
                if (unit.playerNumber != unitInRange.playerNumber)
                {
                    this.enemiesInAOERange.Add(unitInRange);
                    if (this.enemiesInAOERange.Contains(unitInRange))
                    {
                        gm.selectedUnit.AttackUE(unitInRange, "demon");
                    }
                }
            }
            }
            unit.implosionNuclearCast = false;
        }
    }
    */

    // AP: 6
    // Effect: No unit can attack for 2 turns. Difficulty: 1
    public static void ImposicionReal()
    {
        
    }

    // AP: 6
    // Effect: 2 SQT Invisible UE for 2 Turns. Difficulty: 1
    public static void InfiltracionMasiva()
    {
        
    }

    // AP: 4
    // Effect: Swap with Ally at 1-2 Tiles. Difficulty: 1
    public static void Infiltrado()
    {
        
    }

    // AP: Passive
    // Effect: Last hit heals unit 100%. Difficulty: Done
    /*
    public static void Inmortalidad()
    {
        unit.inmortalidad = true; ON START
    }
    */

    // AP: 6
    // Effect: 2 SQT UE Invulnerable for 1 Turn & Enemy paralyze. Difficulty: 1
    public static void InvocacionDeAngeles()
    {
        
    }

    // AP: 4
    // Effect: +1 AP 1 SQT UE. Difficulty: 1
    public static void InvocacionDeVelocidad()
    {
        
    }

    // AP: 2
    // Effect: +1 Attack Damage. Loses 2 HP. Difficulty: Done
    /*
    public void Ira(Unit unit)
    {
        if (unit.iraCast == false)
        {
            photonView.RPC("VictoryAnim", RpcTarget.All);
            unit.iraCast = true;
            unit.actionPoints -= 2;
            unit.UpdateActionPointsText();
            unit.attackDamage += 1;
            unit.health -= 2;
            unit.photonView.RPC("TakeDamage", PlayerController.enemy.photonPlayer, 2);
            unit.iraCast = false;
        }
    }
    */

    // AP: 4
    // Effect: +5 Attack Damage. Difficulty: Done
    /*
    public void IraMaldita(Unit unit)
    {
        if (unit.iraMalditaCast == false)
        {
            photonView.RPC("VictoryAnim", RpcTarget.All);
            unit.iraMalditaCast = true;
            unit.actionPoints -= 4;
            unit.UpdateActionPointsText();
            unit.attackDamage += 5;
            unit.iraMalditaCast = false;
        }
    }
    */

    // AP: Passive
    // Effect: When attack, gets 1 AP. Difficulty: Done
    /*
    public static void JineteLegendario()
    {
        unit.jineteLegendario = true; ON START
    }
    */

    // AP: 8
    // Effect: + X Attack, X = lost health. Can’t move or attack for 1 turn. Difficulty: Done
    /*
    public void Karma(Unit unit)
    {
        if (unit.karmaCast == false)
        {
            photonView.RPC("VictoryAnim", RpcTarget.All);
            unit.karmaCast = true;
            unit.actionPoints -= 8;
            unit.UpdateActionPointsText();
            Unit2 unit2 = GetComponent<Unit2>();
            unit.attackDamage += (unit2.maxHp - unit2.curHp);
            unit.cantMove = true;
            unit.cantAttack = true;
            StartCoroutine(KarmaUncast(gm.selectedUnit, 5f));
        }
    }
    */

    // AP: Passive
    // Effect: 10% Chance Instakill. Difficulty: Done
    /*
    public static void Katana()
    {
        unit.katana = true; ON START
    }
    */

    // AP: Passive
    // Effect: Normal Tile hit, can't attack for 2 Turns. Difficulty: Done
    /*
    public static void LanzaDelAmor()
    {
        unit.lanzaDelAmor = true; ON START
    }
    */

    // AP: Passive
    // Effect: Normal Tile hit, 25% posses for 2 Turns. Difficulty: 1
    public static void LanzaDivina()
    {
        
    }

    // AP: Passive
    // Effect: Demon Tile hit. Difficulty: Done
    /*
    public static void LanzaEndemoniada()
    {
        unit.lanzaEndemoniada = true; ON START
    }
    */

    // AP: Passive
    // Effect: Normal Tile hit, 1/2 Attack Damage for 2 Turns. Difficulty: Done
    /*
    public static void LanzaGloriosa()
    {
        unit.lanzaGloriosa = true; ON START
    }
    */

    // AP: 4
    // Effect: +2 Range & Demon Attack w Explo for 2 Turns. Difficulty: 1
    public static void Lanzagranadas()
    {
        
    }

    // AP: Passive
    // Effect: 25% chance to leave enemy at 1HP. Difficulty: Done
    /*
    public static void LanzaInfernal()
    {
        unit.lanzaInfernal = true; ON START
    }
    */

    // AP: 4
    // Effect: 1/3 SQT Demon Missile. Difficulty: No
    /*
    public static void Lanzallamas()
    {
        
    }
    */

    // AP: 4
    // Effect: +1 Range for 2 Turns. Difficulty: 1
    public static void Lanzamiento()
    {
        
    }

    // AP: Passive
    // Effect: Silence Tile hit. Difficulty: 1
    public static void LanzaOscura()
    {
        
    }

    // AP: Passive
    // Effect: 30% de probabilidad de Instakill. Difficulty: Done
    /*
    public static void Letalidad()
    {
        unit.letalidad = true; ON START
    }
    */

    // AP: 4
    // Effect: Heals All Allies. Difficulty: 1
    public static void LevantarALosMuertos()
    {
        
    }

    // AP: 6
    // Effect: Swaps with allied Unit. Unit gets 2x Attack for 2 Turns. Difficulty: 1
    public static void LeVoyADecirAMisPapas()
    {
        
    }

    // AP: 4
    // Effect: Teleports Ally next to General. 3 SQT. Difficulty: No
    /*
    public static void LeyYOrden()
    {
        
    }
    */

    // AP: 6
    // Effect: All allies double their AP. Can only be casted once. Difficulty: Done
    /*
    public void Liberacion(Unit unit)
    {
        if (unit.liberacionCast == false)
        {
            photonView.RPC("VictoryAnim", RpcTarget.All);
            unit.liberacionCast = true;
            unit.actionPoints -= 6;
            foreach (Unit units in FindObjectsOfType<Unit>())
            {
                units.actionPoints = units.actionPoints * 2;
            }
        }
    }
    */

    // AP: Passive
    // Effect: When attacks, can get gold. Difficulty: 1
    public static void LiderazgoLegendario()
    {
        
    }

    // AP: 6
    // Effect: 3 SQT UE 2x Attack Damage & enemies get 1/2 Attack Damage for 2 Turns. Difficulty: Done
    /*
    public void Lloriqueo(Unit unit)
    {
        if (unit.lloriqueoCast == false)
        {
            photonView.RPC("VictoryAnim", RpcTarget.All);
            unit.lloriqueoCast = true;
            unit.actionPoints -= 6;
            enemiesInAOERange.Clear();
            alliesInAOERange.Clear();
            
            foreach (Unit unitInRange in FindObjectsOfType<Unit>())
            {
            if ((Mathf.Abs(transform.position.x - unitInRange.transform.position.x) + Mathf.Abs(transform.position.y - unitInRange.transform.position.y) <= 3))
            {
                if (unit.playerNumber != unitInRange.playerNumber)
                {
                    this.enemiesInAOERange.Add(unitInRange);
                    if (this.enemiesInAOERange.Contains(unitInRange))
                    {
                        gm.selectedUnit.AttackUE(unitInRange, "lloriqueoEnemy");
                    }
                }

                if (unit.playerNumber == unitInRange.playerNumber)
                {
                    this.alliesInAOERange.Add(unitInRange);
                    if (this.alliesInAOERange.Contains(unitInRange))
                    {
                        gm.selectedUnit.AttackUE(unitInRange, "lloriqueoAlly");
                    }
                }
            }
            }
            unit.lloriqueoCast = false;
        }
    }
    */
    
    // AP: 6
    // Effect: Nadie puede atacar por 2 Turnos. Difficulty: 1
    public static void Maldicion()
    {
        
    }

    // AP: 4
    // Effect: +1 Range for 3(15s) Turns. Difficulty: Done
    /*
    public void ManejoDeArmas(Unit unit)
    {
        if (unit.manejoDeArmasCast == false)
        {
            photonView.RPC("VictoryAnim", RpcTarget.All);
            unit.manejoDeArmasCast = true;
            unit.actionPoints -= 4;
            unit.UpdateActionPointsText();
            unit.maxAttackRange += 1;
            unit.manejoDeArmasCast = false;
            gm.UpdateStatsPanel();
            StartCoroutine(ManejoDeArmasUncast(gm.selectedUnit, 15f));
            gm.UpdateStatsPanel();
        }
    }
    */

    // AP: 6
    // Effect: Viaje Astral + Salto al Vacio. Difficulty: 1
    public static void ManifestacionDelphi()
    {
        
    }

    // AP: Passive
    // Effect: +1AP when Ally Attacks. Difficulty: No
    /*
    public static void Marabunta()
    {
        
    }
    */

    // AP: 4
    // Effect: +1 Attack Damage, +1 Counter & +1 Defense. Can’t move or attack for 1 turn. Difficulty: Done
    /*
    public void Meditacion(Unit unit)
    {
        if (unit.meditacionCast == false)
        {
            photonView.RPC("VictoryAnim", RpcTarget.All);
            unit.meditacionCast = true;
            unit.actionPoints -= 4;
            unit.UpdateActionPointsText();
            unit.attackDamage += 1;
            unit.physicalArmor += 1;
            unit.defenseDamage += 1;
            unit.cantMove = true;
            unit.cantAttack = true;
            StartCoroutine(MeditacionUncast(gm.selectedUnit, 5f));
        }
    }
    */

    // AP: 6
    // Effect: 2 SQT +AP to Allies & Invisibles for 2 Turns. Difficulty: 1
    public static void MensajeSecreto()
    {
        
    }
    
    // AP: 2
    // Effect: +1 Attack Damage. Difficulty: Done
    /*
    public void Motivacion(Unit unit)
    {
        if (unit.motivacionCast == false)
        {
            photonView.RPC("VictoryAnim", RpcTarget.All);
            unit.motivacionCast = true;
            unit.actionPoints -= 2;
            unit.UpdateActionPointsText();
            unit.attackDamage += 1;
            unit.motivacionCast = false;
        }
    }
    */

    // AP: Passive
    // Effect: Turns Invisible when hit. Difficulty: 1
    public static void Ninjitsu()
    {
        
    }

    // AP: 6
    // Effect: All Allies Invisible for 2 Turns. Difficulty: Bug
    public static void NocheDeMisterio()
    {
        
    }

    // AP: 4
    // Effect: Becomes Invulnerable for 2 Turns. Difficulty: Bug
    public static void Odisea()
    {
        
    }

    // AP: 2
    // Effect: Heals Ally 2 SQT UE. Difficulty: 1
    public static void Oracion()
    {
        
    }

    // AP: 2
    // Effect: Heals Ally 3 SQT UE. Difficulty: 1
    public static void OracionADistancia()
    {
        
    }

    // AP: 4
    // Effect: 3 Tiles Heal & +2 AP UE. Difficulty: Bug
    public static void OracionAncestral()
    {
        
    }

    // AP: 6
    // Effect: Grito del Dragón a Aliados. Difficulty: 1
    public static void OrdenReal()
    {
        
    }

    // AP: 6
    // Effect: All Allies Turn Invisible for 2 Turns. Difficulty: Bug
    public static void OscuridadTotal()
    {
        
    }

    // AP: Passive
    // Effect: Normal attacks give -2AP to enemy. Difficulty: Done
    /*
    public static void PaletaPegosteosa()
    {
        unit.paletaPegosteosa = true; ON START
    }
    */

    // AP: 6
    // Effect: Explo Obstacle Wall at 2 SQT. Difficulty: No
    /*
    public static void ParedDeHielo()
    {
        
    }
    */

    // AP: Passive
    // Effect: Physical Defense, Demon Defense & Holly Defense = 99. Difficulty: Done
    /*
    public static void PompisDeBebe()
    {
        unit.pompisDeBebe = true; ON START
    }
    */

    // AP: 4
    // Effect: Creates Portal at 1 SQT. Difficulty: No
    /*
    public static void PortalAtemporal()
    {
        
    }
    */

    // AP: 4
    // Effect: +1 Defense & +1 Counter. Difficulty: Done
    /*
    public void PosicionDeDefensa(Unit unit)
    {
        if (unit.posicionDeDefensaCast == false)
        {
            photonView.RPC("VictoryAnim", RpcTarget.All);
            unit.posicionDeDefensaCast = true;
            unit.actionPoints -= 4;
            unit.UpdateActionPointsText();
            unit.physicalArmor += 1;
            unit.defenseDamage += 1;
            unit.posicionDeDefensaCast = false;
        }
    }
    */

    // AP: 4
    // Effect: Grants Invulnerable to Ally for 2 Turns. Difficulty: No
    /*
    public static void Presagio()
    {
        
    }
    */

    // AP: 2
    // Effect: Guard Ally at 2 SQT for 2 Turns. Difficulty: No
    /*
    public static void Proteccion()
    {
        
    }
    */

    // AP: 6
    // Effect: Guard UE at 2 SQT. Difficulty: No
    /*
    public static void ProteccionMasiva()
    {
        
    }
    */

    // AP: 4
    // Effect: Hits Demon in a straight line 3 SQT. Difficulty: No
    /*
    public static void RayoDeFuego()
    {
        
    }
    */

    // AP: 6
    // Effect: Hits Elemental in a straight line 3 SQT. Difficulty: No
    /*
    public static void RayoElemental()
    {
        
    }
    */

    // AP: 4
    // Effect: Hits Universe in a straight line 3 SQT. Difficulty: No
    /*
    public static void RayoLunar()
    {
        
    }
    */

    // AP: 4
    // Effect: Hits Universe 1/3 SQT. Difficulty: No
    /*
    public static void RayoSolar()
    {
        
    }
    */

    // AP: 4
    // Effect: Next attack is Explo Physical UE. Difficulty: No
    /*
    public static void RecargarArmas()
    {
        
    }
    */

    // AP: 4
    // Effect: Heals 100% all Allies. Can only be used once. Difficulty: 1
    public static void Renacimiento()
    {
        
    }

    // AP: 2
    // Effect: Cura Obstaculo. Difficulty: No
    /*
    public static void ReparacionDeMaquinas()
    {
        
    }
    */

    // AP: 4
    // Effect: +1 Def. Difficulty: 1
    public static void Resistencia()
    {
        
    }

    // AP: 6
    // Effect: 4x Universe Attack at 85% Attack Damage. Unit has 1 HP remaining. Difficulty: Bug
    public static void SacrificioUniversal()
    {
        
    }

    // AP: 4
    // Effect: Teleports & Universe 2 SQT UE. Difficulty: 1
    public static void SaltoAlVacio()
    {
        
    }

    // AP: 6
    // Effect: Gives Inmortalidad to all Allies. Can only be used once. Difficulty: Bug
    public static void SalvacionEterna()
    {
        
    }

    // AP: 6
    // Effect: Charm & -1 Attack Damage 2 Tiles UE for 2 Turns. Difficulty: 1
    public static void Serenata()
    {
        
    }

    // AP: 2
    // Effect: Enemy -1 Attack Damage Debuff at 1-2 SQT. Difficulty: Done
    /*
    public void Sermon(Unit unit)
    {
        if (unit.sermonCast == false)
        {
            photonView.RPC("VictoryAnim", RpcTarget.All);
            unit.sermonCast = true;
            unit.actionPoints -= 2;
        }
    }
    */

    // AP: 4
    // Effect: +1 AP. Can’t move or attack for 1 Turn. Difficulty: Done
    /*
    public void Sigilo(Unit unit)
    {
        if (unit.sigiloCast == false)
        {
            photonView.RPC("VictoryAnim", RpcTarget.All);
            unit.sigiloCast = true;
            unit.actionPoints += 1;
            unit.cantMove = true;
            unit.cantAttack = true;
            StartCoroutine(SigiloUncast(gm.selectedUnit, 5f));
        }
    }
    */

    // AP: Passive
    // Effect: +2AP when Ally Attacks. Difficulty: No
    /*
    public static void SoldadoRazo()
    {
        
    }
    */

    // AP: 4
    // Effect: 1/2 Attack Damage 2 Tiles UE for 2 Turns. Difficulty: 1
    public static void SonidoAturdidor()
    {
        
    }

    // AP: 6
    // Effect: Allies at 2 SQT get +2AP, Cant Attack for 2 Turns. Difficulty: Bug
    public static void SonidoDeAve()
    {
        
    }

    // AP: 6
    // Effect: Allies at 2 SQT get +2 AP, Allies & Enemies can’t Move for 1 Turn. Difficulty: Done
    /*
    public void SonidoDeTigre(Unit unit)
    {
        if (unit.sonidoDeTigreCast == false)
        {
            photonView.RPC("VictoryAnim", RpcTarget.All);
            unit.sonidoDeTigreCast = true;
            unit.actionPoints -= 6;
            enemiesInAOERange.Clear();
            alliesInAOERange.Clear();
            
            foreach (Unit unitInRange in FindObjectsOfType<Unit>())
            {
            if ((Mathf.Abs(transform.position.x - unitInRange.transform.position.x) + Mathf.Abs(transform.position.y - unitInRange.transform.position.y) <= 2))
            {
                if (unit.playerNumber != unitInRange.playerNumber)
                {
                    this.enemiesInAOERange.Add(unitInRange);
                    if (this.enemiesInAOERange.Contains(unitInRange))
                    {
                        gm.selectedUnit.AttackUE(unitInRange, "sonidoDeTigreEnemy");
                    }
                }

                if (unit.playerNumber == unitInRange.playerNumber)
                {
                    this.alliesInAOERange.Add(unitInRange);
                    if (this.alliesInAOERange.Contains(unitInRange))
                    {
                        gm.selectedUnit.AttackUE(unitInRange, "sonidoDeTigreAlly");
                    }
                }
            }
            }
            unit.sonidoDeTigreCast = false;
        }
    }
    */

    // AP: 6
    // Effect: Nadie se puede mover por 2 Turnos. Difficulty: Done
    /*
    public void Terremoto(Unit unit)
    {
        if (unit.terremotoCast == false)
        {
            photonView.RPC("VictoryAnim", RpcTarget.All);
            unit.terremotoCast = true;
            unit.actionPoints -= 6;
            foreach (Unit units in FindObjectsOfType<Unit>())
            {
                units.cantMove = true;
                StartCoroutine(TerremotoUncast(units, 10f));
            }
            photonView.RPC("TerremotoEnemy", RpcTarget.Others);
            unit.terremotoCast = false;
        }
    }
    */

    // AP: 6
    // Effect: Normal hit with Charm 2 SQT UE. Difficulty: 1
    public static void TeVoyAMearWey()
    {
        
    }

    // AP: 4
    // Effect: 3x Universe Attack. Difficulty: Done
    /*
    public void Trifuerza(Unit unit)
    {
        if (unit.trifuerzaCast == false)
        {
            photonView.RPC("VictoryAnim", RpcTarget.All);
            unit.trifuerzaCast = true;
            unit.actionPoints -= 4;
        }
    }
    */

    // AP: 4
    // Effect: 2 Tiles Holly UE. Difficulty: Done
    /*
    public void Trueno(Unit unit)
    {
        if (unit.truenoCast == false)
        {
            photonView.RPC("VictoryAnim", RpcTarget.All);
            unit.truenoCast = true;
            unit.actionPoints -= 4;
            enemiesInAOERange.Clear();
            
            foreach (Unit unitInRange in FindObjectsOfType<Unit>())
            {
            if ((Mathf.Abs(transform.position.x - unitInRange.transform.position.x) + Mathf.Abs(transform.position.y - unitInRange.transform.position.y) <= 2))
            {
                if (unit.playerNumber != unitInRange.playerNumber)
                {
                    this.enemiesInAOERange.Add(unitInRange);
                    if (this.enemiesInAOERange.Contains(unitInRange))
                    {
                        gm.selectedUnit.AttackUE(unitInRange, "holly");
                    }
                }
            }
            }
            unit.truenoCast = false;
        }
    }
    */

    // AP: 4
    // Effect: Teleports & 2 SQT UE Heal. Difficulty: 1
    public static void ViajeAstral()
    {
        
    }

    // AP: 6
    // Effect: Grants Invulnerable to all Allies for 2 Turns. Costs. Difficulty: 1
    public static void VisitaAlOraculo()
    {
        
    }
}
