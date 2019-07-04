using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleHandler : MonoBehaviour {

	private System.Random randomNumber = new System.Random();
	public static BattleHandler main;
	public Battle currentBattle;
	public int enemyHealth;
	public List<StatusEffect> enemyAfflications;

	public GameObject battleUI, beginPanel, actionPanel, enemyPositionPanel, enemyObj;
	public AudioClip battleStart, enemyMove;

	public int enemyPos;
	public float turnTime;

	void Awake () {
		if(main == null){
			main = this;
		} else {
			Destroy(this.gameObject);
		}
	}

	void Start(){
		beginPanel = battleUI.transform.Find("BeginPanel").gameObject;
		battleUI.SetActive(false);
	}
	


	// Update is called once per frame
	void Update () {
		
	}

	public void startNewBattle(Battle battle){
		currentBattle = battle;
		enemyHealth = battle.enemyHealth;
		DisplayBattleInformation.main.currentBattle = battle;
		battleUI.SetActive(true);
		beginPanel.transform.Find("BattleIntroPanel").GetChild(0).GetComponent<Text>().text = battle.introText;
		DisplayBattleInformation.main.updateAllBattleDisplays();
	}

	public void closeIntroWindow(){
		beginPanel.SetActive(false);
		SFXManager.main.playSoundOnce(battleStart);
		DisplayBattleInformation.main.addBattleLog(currentBattle.enemyName + " approaches!");
		StartCoroutine(playTrapPhase());
	}

	IEnumerator playTrapPhase(){
		for(int pos = 4; pos > -1; pos--){
			DisplayBattleInformation.main.updateAllBattleDisplays();
			yield return new WaitForSeconds(turnTime);
			enemyObj.transform.position = enemyPositionPanel.transform.GetChild(pos).transform.position;
			DisplayBattleInformation.main.addBattleLog(currentBattle.enemyName + " moves closer.");
			yield return new WaitForSeconds(turnTime / 2);
			applyTrapEffect(pos);
			yield return new WaitForSeconds(turnTime);
			if(enemyHealth <= 0) StartCoroutine(applyWinState());
		}
		playerTurnPhase();
	}		


	public void playerTurnPhase(){
		foreach(Transform child in actionPanel.transform){
			child.gameObject.SetActive(true);
		}
	}

	public void applyTrapEffect(int index){
		if(GameInformation.main.builtTraps.Count - 1 >= index){
			bool multiDmg = false;
			Trap trap = GameInformation.main.builtTraps[index];
			DisplayBattleInformation.main.addBattleLog(currentBattle.enemyName + " encountered " + trap.trapName + "!");
			float baseDmg = trap.baseDamage;

			foreach(StatusEffect effect in enemyAfflications){
				if(enemyAfflications.Contains(trap.effectThatMultplies)){
					baseDmg = baseDmg * trap.multiplier;
					multiDmg = true;
				}
			}

			int hitResult = randomNumber.Next(0, 100);
			if(baseDmg > 0f){
				if(hitResult <= trap.chanceToHit)
				{
					enemyHealth -= (int)Mathf.Round(baseDmg);
					if(multiDmg)
					{
						DisplayBattleInformation.main.addBattleLog("Extra damage!");
					}

					DisplayBattleInformation.main.addBattleLog(currentBattle.enemyName + " took " + (int)Mathf.Round(baseDmg) + " damage from " + trap.trapName);

					if(trap.applyEffect != StatusEffect.None)
					{
						int effectResult = randomNumber.Next(0, 100);
						if(effectResult <= trap.applyChance && !enemyAfflications.Contains(trap.applyEffect)) {
							enemyAfflications.Add(trap.applyEffect);
							DisplayBattleInformation.main.addBattleLog(trap.trapName + " inflicted " + trap.applyEffect + " on " + currentBattle.enemyName);
						}
					}

				} 
				else
				{
					DisplayBattleInformation.main.addBattleLog(currentBattle.enemyName + " evaded " + trap.trapName);
				}
			}


			SFXManager.main.playSoundOnce(trap.useSound);
		}
	}

	public void playerAttack(){
		int atkVal = GameInformation.main.playerStats.Find(x => x.name == StatNames.Strength).currentValue;
		enemyHealth -= atkVal;
		DisplayBattleInformation.main.addBattleLog("You struck " + currentBattle.enemyName + " for " + atkVal.ToString());
		DisplayBattleInformation.main.updateAllBattleDisplays();
		if(enemyHealth <= 0) StartCoroutine(applyWinState());
	}

	public void fleeBattle(){
		DisplayBattleInformation.main.addBattleLog("Cannot flee!");
	}

	public void negotiate(){
		DisplayBattleInformation.main.addBattleLog(currentBattle.enemyName + " is not interested!");
	}

	public void hide(){
		DisplayBattleInformation.main.addBattleLog("Nowhere to hide!");
	}

	IEnumerator applyWinState(){
		DisplayBattleInformation.main.addBattleLog("You killed " + currentBattle.enemyName);
		yield return new WaitForSeconds(turnTime);
		endBattle();
	}

	public void endBattle(){
		battleUI.SetActive(false);
	}

}
