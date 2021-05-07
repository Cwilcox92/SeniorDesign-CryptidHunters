using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName= " New Character ", menuName= "Character Selection/Character")]
public class Player : ScriptableObject
{
     [SerializeField] private string characterName = default;
     [SerializeField] private GameObject gameplayCharacterPrefab= default;

     public string CharacterName => characterName;
     public GameObject GameplayCharacterPrefab => gameplayCharacterPrefab;






}
