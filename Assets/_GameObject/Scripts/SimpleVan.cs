using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SimpleVan : MonoBehaviour
{
    [Header("Player Name")]
    public string playerName;
    [Range(5, 10)]
    public float speed = 5f;
    [Range(0, 45f)]
    public float angularSpeed = 1f;

    private TextMesh textMeshPlayerName;
    //CONSTANTES    
    private Color WIN_COLOR = Color.red;
    protected const string TAG_TARGET = "Target";
    protected const string TAG_PARED = "Pared";


    protected virtual void Start()
    {
        textMeshPlayerName = GetComponentInChildren<TextMesh>();
        textMeshPlayerName.text = playerName;
    }

    public abstract void Update();

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(TAG_TARGET))
        {
            GetComponent<AudioSource>().Play();
            textMeshPlayerName.text = playerName + " WINS";
            textMeshPlayerName.color = WIN_COLOR;
            textMeshPlayerName.characterSize *= 2f;
            Time.timeScale = 0;
        }
    }
}
