using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogUIManager : MonoBehaviour
{
    #region Variaveis

    public static DialogUIManager instance;

    [Header("Dialogue")]
    [SerializeField] private Text _charDialog;
    [SerializeField] private Text _charName;
    [SerializeField] private Image _diaBackgroundUI;
    [SerializeField] private Image _characterUI;
    [SerializeField] private Image _backgroundUI;
    [SerializeField] private Image _backgroundNameUI;

    [SerializeField] private Image[] _charactersPositions;

    [SerializeField] DialogueManager _diaManager;

    float _textVelocity = 0.05f;

    public IEnumerator typingeffectCoroutine;

    [Header("Questions")]
    [SerializeField] Text _questionText1;
    [SerializeField] Text _questionText2;
    [SerializeField] Text _questionText3;

    string _currentMensage;

    #endregion

    #region Propriedades
    public Image[] CharactersPositions { get => _charactersPositions; set => _charactersPositions = value; }

    #endregion

    #region Awake com Sigletone
    private void Awake()
    {
        
        //singletone
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            
        } 
    }
    #endregion

    #region Informação para UI
    //Passa a informação do dialogo para o UI
    public void DialogOnScrene(string characterName, Font font, Color NameColor, Color DialogColor, Sprite diaBackground, Sprite character, Sprite background, Sprite backgroundname)
    {

        _charName.text = characterName; //Passa o nome do Personagem para o UI
        _diaBackgroundUI.sprite = diaBackground;
        _characterUI.sprite = character;
        _charDialog.font = font; 
        _charName.font = font;
        _backgroundUI.sprite = background;
        _backgroundNameUI.sprite = backgroundname;

        _charDialog.color = DialogColor;
        _charName.color = NameColor;
    }

    public void CharactersOnDisplay(Sprite characterSprite, int characterPositionOnDisplay)
    {
        
        CharactersPositions[characterPositionOnDisplay].gameObject.SetActive(true);
        CharactersPositions[characterPositionOnDisplay].sprite = characterSprite;
    }

    #endregion

    #region Exposição do Dialogo
    public void PlayCoroutine(string dialogToDisplay)
    {

        if (typingeffectCoroutine != null)
        {
            EndCoroutine(dialogToDisplay);
            Debug.Log("oi");
        }
        else
        {
            typingeffectCoroutine = Typing(dialogToDisplay);
            StartCoroutine(typingeffectCoroutine);
        }

    }
    IEnumerator Typing(string dialog)
    {
        Debug.Log("o123123");
        _charDialog.text = "";
        _currentMensage = dialog;

        foreach (char letra in dialog.ToCharArray())
        {
            _charDialog.text += letra;
            yield return new WaitForSeconds(_textVelocity);
        }
        typingeffectCoroutine = null;

    }


    public void EndCoroutine(string dialogue)
    {
        StopCoroutine(typingeffectCoroutine);

        _charDialog.text = _currentMensage;

        typingeffectCoroutine = null;
        //dar display do texto completo
    }

    public void NextDialogueButton()
    {
        _diaManager.ChangeDialogue();
    }

    #endregion

    #region Encolhas das ramificações
    public void NextDialgueOnChoise1()
    {
        _diaManager.ChoiseChange1();
        _diaManager.ChoiseChangeDialogue();

        // _diaManager.TrustValue += _diaManager.DialogueTree1.TrustValueToIncrese1;

    }
    public void NextDialgueOnChoise2()
    {
        _diaManager.ChoiseChange2();
        _diaManager.ChoiseChangeDialogue();

        // _diaManager.TrustValue += _diaManager.DialogueTree1.TrustValueToIncrese2;
    }
    public void NextDialgueOnChoise3()
    {
        _diaManager.ChoiseChange3();
        _diaManager.ChoiseChangeDialogue();

        // _diaManager.TrustValue += _diaManager.DialogueTree1.TrustValueToIncrese3;
    }

    public void QuestionsToUi(string question1, string question2, string question3)
    {
        _questionText1.text = question1;
        _questionText2.text = question2;
        _questionText3.text = question3;
    }

    #endregion

  
}
