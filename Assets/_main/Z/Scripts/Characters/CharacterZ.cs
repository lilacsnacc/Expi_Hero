using System;
using UnityEngine;

public class CharacterZ : MonoBehaviour {
  public GameManager gameManager;
  public AbilityManager abilityManager;

  public Animator animator;
  public Transform cameraFollowLerpTarget;

  public CharacterController controller;
  public CameraController cameraController;

  public Action<float> onExpiChange;
  public Action onDeath;

  protected Interactable currentInteractable;

  protected Vector3 tpDestination;

  protected Vector3 moveDirection;

  protected float HealthStat = 5;
  protected float StrengthStat = 5;
  protected float SpeedStat = 5;

  protected float Expi = 0;

  protected float maxHealth;
  protected float health;

  protected bool requestedTP;

  void Awake() {
    health = maxHealth = HealthStat * 5;
    Expi = (HealthStat + StrengthStat + SpeedStat) / 3;

  }

  void FixedUpdate() {
    if (requestedTP) {
      controller.enabled = false;
      transform.position = tpDestination;
      requestedTP = false;
      controller.enabled = true;
    }
  }
  
  public void AffectHealth(float healthAmt) {
    if (healthAmt == 0) return;

    BroadcastMessage(healthAmt > 0 ? "OnHeal" : "OnDamage", healthAmt);
    
    health = Mathf.Max(Mathf.Min(health + healthAmt, maxHealth), 0);

    BroadcastMessage("OnHealthChange", GetNormalizedHealth());

    if (health <= 0 && onDeath != null) onDeath.Invoke();
  }
  
  void OnHeal(float healAmt) {
    // Debug.Log(name + ": " + healAmt + " healing!!! thanks bb <3");
  }
  void OnDamage(float damageAmt) {
    // Debug.Log(name + ": " + damageAmt + " damage!!! oof ouchie >w<;");
  }
  void OnHealthChange(float normalizedHealth) {
    // Debug.Log(name + ": " + (normalizedHealth * 100) + "% hp remaining." + (normalizedHealth <= 0 ? ".. T_T bruh I'm literally dead" : ""));
  }

  public void OnInteract() {
    gameManager?.SetPrompt();
    currentInteractable?.Interact(this);
  }

  public virtual Interactable GetInteractable() {
    return currentInteractable;
  }

  public virtual void SetInteractable(Interactable interactable = null) {
    currentInteractable = interactable;
    gameManager?.SetPrompt(interactable?.interactMessage);
  }

  public LTDescr ResetCamera(float transitionTime = 1, bool continuousFollow = true) {
    return SetCameraLerpTarget(cameraFollowLerpTarget, transitionTime, continuousFollow);
  }

  public LTDescr SetCameraLerpTarget(Transform lerpTarget, float transitionTime = 0.5f, bool continuousFollow = false) {
    return this.cameraController.SetLerpTarget(lerpTarget, transitionTime, continuousFollow);
  }

  public void TeleportTo(Vector3 tpDest) {
    transform.position = tpDestination = tpDest;
    requestedTP = true;
  }

  public Vector3 GetMoveDirection() {
    return moveDirection;
  }
  public void SetMoveDirection(Vector3 newDirection) {
    moveDirection = newDirection;
  }

  public float GetNormalizedHealth() {
    return maxHealth == 0 ? 0 : health / maxHealth;
  }

  public float GetHealthStat() {
    return HealthStat;
  }

  public void SetHealthStat(float newHealthStatValue) {
    HealthStat = newHealthStatValue;
    health = maxHealth = HealthStat * 5;
  }

  public float GetStrengthStat() {
    return StrengthStat;
  }

  public void SetStrengthStat(float newStrengthStatValue) {
    StrengthStat = newStrengthStatValue;
  }

  public float GetSpeedStat() {
    return SpeedStat;
  }

  public void SetSpeedStat(float newSpeedStatValue) {
    SpeedStat = newSpeedStatValue;
  }

  public void SetStats(float Health, float Strength, float Speed) {
    SetHealthStat(Health);
    SetStrengthStat(Strength);
    SetSpeedStat(Speed);
  }

  public void AffectStats(float Health, float Strength, float Speed) {
    SetHealthStat(HealthStat + Health);
    SetStrengthStat(StrengthStat + Strength);
    SetSpeedStat(SpeedStat + Speed);
  }

  public float GetExpi() {
    return Expi;
  }

  public void SetExpi(float expiAmt) {
    Expi = expiAmt;
  }
  
  public void AffectExpi(float expiAmt) {
    SetExpi(Expi + expiAmt);
    onExpiChange?.Invoke(expiAmt);
  }
}
