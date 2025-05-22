using System;
using UnityEngine;

public class GameManager : MonoBehaviour {
  public Ghost[] ghosts;
  public Pacman pacman;
  public Transform pellets;

  public int score { get; private set; }
  public int lives { get; private set; }

  public int ghostMultiplier { get; private set; } = 1;

  private void Start() {
    InitGame();
  }

  private void Update() {
    if (this.lives <= 0 && Input.GetKeyDown(KeyCode.Return)) {
      InitGame();
    }
  }

  private void InitGame() {
    SetScore(0);
    SetLives(3);
    NewLevel();
  }

  private void NewLevel() {
    foreach (Transform pellet in pellets) {
      pellet.gameObject.SetActive(true);
    }

    ResetState();
  }

  private void ResetState() {
    ResetGhostMultiplier();
    foreach (Ghost ghost in ghosts) {
      ghost.ResetState();
    }

    pacman.ResetState();
  }

  private void GameOver() {
    foreach (Ghost ghost in ghosts) {
      ghost.gameObject.SetActive(false);
    }

    pacman.gameObject.SetActive(false);
  }

  private void SetScore(int newScore) {
    score = newScore;
  }

  private void SetLives(int newLives) {
    lives = newLives;
  }

  public void EatGhost(Ghost ghost) {
    SetScore(this.score + (ghost.m_GhostPoints * ghostMultiplier));
    ghostMultiplier++;
  }

  public void EatPacman() {
    this.pacman.gameObject.SetActive(false);
    SetLives(this.lives - 1);
    if (this.lives > 0) {
      Invoke(nameof(ResetState), 3.0f);
    }
    else {
      GameOver();
    }
  }

  public void EatPellet(Pellet pellet) {
    SetScore(this.score + pellet.points);
    pellet.gameObject.SetActive(false);
    if (!CheckPellets()) {
      this.pacman.gameObject.SetActive(false);
      Invoke(nameof(NewLevel), 3.0f);
    }
  }

  public void EatPowerPellet(PowerPellet powerPellet) {
    EatPellet(powerPellet);
    CancelInvoke();
    Invoke(nameof(ResetGhostMultiplier), powerPellet.duration);
  }

  private bool CheckPellets() {
    foreach (Transform pellet in pellets) {
      if (pellet.gameObject.activeSelf) {
        return true;
      }
    }

    return false;
  }

  private void ResetGhostMultiplier() {
    this.ghostMultiplier = 1;
  }
  
}