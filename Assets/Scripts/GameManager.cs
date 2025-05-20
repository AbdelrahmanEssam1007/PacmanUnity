using System;
using UnityEngine;

public class GameManager : MonoBehaviour {
  public Ghost[] ghosts;
  public Pacman pacman;
  public Transform pellets;

  public int score { get; private set; }
  public int lives { get; private set; }

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

    Reset();
  }

  private void Reset() {
    foreach (Ghost ghost in ghosts) {
      ghost.gameObject.SetActive(true);
    }

    pacman.gameObject.SetActive(true);
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
    SetScore(this.score + ghost.m_GhostPoints);
  }

  public void EatPacman() {
    this.pacman.gameObject.SetActive(false);
    SetLives(this.lives - 1);
    if (this.lives > 0) {
      Invoke(nameof(Reset), 3.0f);
    }
    else {
      GameOver();
    }
  }

  public void EatPellet(Pellet pellet) {
    SetScore(this.score + pellet.points);
    pellet.gameObject.SetActive(false);
    if (!CheckPellets()) {
      NewLevel();
    }
  }

  public void EatPowerPellet(PowerPellet powerPellet) {
    EatPellet(powerPellet);
    //TODO: add ghost scared state
  }

  private bool CheckPellets() {
    foreach (Transform pellet in pellets) {
      if (pellet.gameObject.activeSelf) {
        return true;
      }
    }

    return false;
  }
}