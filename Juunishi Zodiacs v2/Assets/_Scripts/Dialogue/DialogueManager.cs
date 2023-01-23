using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;
using static Dialogue;
using UnityEngine.SceneManagement;

public class DialogueManager : MonoBehaviour
{
    #region Variaveis
    //Arrey com os conjuntos de dialogos que sao feitos com Scriptable Objects
    [SerializeField] ScriptableDialogue[] myDialogTree;

    //boolan que dita caso haja ramifica��o no dialogo ou nao
    [SerializeField] bool _dialogueCanChange = true;
    [SerializeField] GameObject _chosesButtons;

    //Dialogue Position 
    [SerializeField] int _dialogNumber;
    [SerializeField] int _positionInDialog;
    [SerializeField] int _dialogTreeNumber;
    
    //Encapsuladores de Informa��o dos Scriptable Objects
    string DialogToDisplay;
    string CharacterName;
    Font font;
    Color NameColor;
    Color DialogColor;
    Sprite SpriteBackground;
    Sprite Background;

    //Variaveis de Expressoes
    Sprite ExpressionsToDisplay;
    int _expressionNumber;

    //Variaveis do Fullbody
    Sprite FullBodyToDisplay;
    int _fullbodyExpressionNumber;

    //Variaveis das Positions do Fullbody
     int FullBodyPosition;
     int _fullBodyPositionsNumber;

    //Referencia ao Scriptable Object com os dialogos
    ScriptableDialogue DialogueTree;

    //questoes para ui

    string _questionToUi1;
    string _questionToUi2;
    string _questionToUi3;

    //nivel de confian�a do vilao
    [SerializeField] int _trustValue;

    public int TrustValue { get => _trustValue; set => _trustValue = value; }
    

    #endregion

    #region Awake
    private void Start()
    {
        _positionInDialog = 0;
        _dialogNumber = 0;
        _dialogTreeNumber = 0;

        UpdateOnUI();

    }

    #endregion

    #region Enums das Expressoes
    void ExpressionsSwitch()
    {
       
        switch (myDialogTree[_dialogTreeNumber].DialogueStr[_dialogNumber].myExpressions)
        {
            case CharacterDisplayExpression.Happy:

                _fullbodyExpressionNumber = (int)CharacterDisplayExpression.Happy;
                break;

            case CharacterDisplayExpression.Surprise:

                _fullbodyExpressionNumber = (int)CharacterDisplayExpression.Surprise;
                break;

            case CharacterDisplayExpression.Nervous:

                _fullbodyExpressionNumber = (int)CharacterDisplayExpression.Nervous;
                break;

            case CharacterDisplayExpression.Sad:

                _fullbodyExpressionNumber = (int)CharacterDisplayExpression.Sad;
                break;

            case CharacterDisplayExpression.Crying:

                _fullbodyExpressionNumber = (int)CharacterDisplayExpression.Crying;
                break;

            case CharacterDisplayExpression.Hangry:

                _fullbodyExpressionNumber = (int)CharacterDisplayExpression.Hangry;
                break;

            case CharacterDisplayExpression.Serious:

                _fullbodyExpressionNumber = (int)CharacterDisplayExpression.Serious;
                break;
        }

        ExpressionsToDisplay = myDialogTree[_dialogTreeNumber].DialogueStr[_dialogNumber].MyCharacter.CharacterDisplayExpressions[_expressionNumber];
    }

    void FullbodySwitch()
    {
        switch (myDialogTree[_dialogTreeNumber].DialogueStr[_dialogNumber].myFullbody)
        {
            case CharacterFullBodyExpression.Happy:

                _expressionNumber = (int)CharacterFullBodyExpression.Happy;
                break;

            case CharacterFullBodyExpression.Surprise:

                _expressionNumber = (int)CharacterFullBodyExpression.Surprise;
                break;

            case CharacterFullBodyExpression.Nervous:

                _expressionNumber = (int)CharacterFullBodyExpression.Nervous;
                break;

            case CharacterFullBodyExpression.Sad:

                _expressionNumber = (int)CharacterFullBodyExpression.Sad;
                break;

            case CharacterFullBodyExpression.Crying:

                _expressionNumber = (int)CharacterFullBodyExpression.Crying;
                break;

            case CharacterFullBodyExpression.Hangry:

                _expressionNumber = (int)CharacterFullBodyExpression.Hangry;
                break;

            case CharacterFullBodyExpression.Serious:

                _expressionNumber = (int)CharacterFullBodyExpression.Serious;
                break;
        }

        FullBodyToDisplay = myDialogTree[_dialogTreeNumber].DialogueStr[_dialogNumber].MyCharacter.FullBodyPoses[_fullbodyExpressionNumber];
    }

    void PisitonSwitch()
    {
        switch (myDialogTree[_dialogTreeNumber].DialogueStr[_dialogNumber].myPositions)
        {
            case FullBodyPositions.Position1:

                _fullBodyPositionsNumber = (int)FullBodyPositions.Position1;
                break;

            case FullBodyPositions.Position2:

                _fullBodyPositionsNumber = (int)FullBodyPositions.Position2;
                break;

            case FullBodyPositions.Position3:

                _fullBodyPositionsNumber = (int)FullBodyPositions.Position3;
                break;

            case FullBodyPositions.Position4:

                _fullBodyPositionsNumber = (int)FullBodyPositions.Position4;
                break;

            case FullBodyPositions.Position5:

                _fullBodyPositionsNumber = (int)FullBodyPositions.Position5;
                break;

          
        }

        FullBodyPosition = _fullBodyPositionsNumber; 
    }

    #endregion

    #region Mandar informa��o para o UI
    private void UpdateOnUI()
    {

        DialogueTree = myDialogTree[_dialogTreeNumber];

        //Encapsulamento de informa��o do Scriptable Dialogue e Character Scriptable Object
        CharacterName = DialogueTree.DialogueStr[_dialogNumber].MyCharacter.CharacterName;
        font = DialogueTree.DialogueStr[_dialogNumber].MyCharacter.Font;
        NameColor = DialogueTree.DialogueStr[_dialogNumber].MyCharacter.MyNameColor;
        DialogColor = DialogueTree.DialogueStr[_dialogNumber].MyCharacter.MyDialogColor;
        SpriteBackground = DialogueTree.DialogueStr[_dialogNumber].MyCharacter.BackGround;
        Background = DialogueTree.Background;
        DialogToDisplay = DialogueTree.DialogueStr[_dialogNumber].DialogueMessages[_positionInDialog]; //Referencia ao texto dos dialgos

        //encapsulamento das questoes
        _questionToUi1 = (DialogueTree.Question1);
        _questionToUi2 = (DialogueTree.Question2);
        _questionToUi3 = (DialogueTree.Question3);

        //Iforma��o do Enum das Expressoes.
        ExpressionsSwitch();


        //Atualiza��o de informa��o no UI: Nome do personagem, Fonte do texto, Cor do nome, cor do Dialogo, Background, Expressoes de texto, background do texto do personagem.
        DialogUIManager.instance.DialogOnScrene(CharacterName, font, NameColor, DialogColor, SpriteBackground, ExpressionsToDisplay, Background);


        //Atualiza��o de informa��o no UI: Introdu��o do Texto dos dialogos
        DialogUIManager.instance.PlayCoroutine(DialogToDisplay);


        //Iforma��o dos Enums dos Fullbody.
        FullbodySwitch();
        PisitonSwitch();

        //Atualiza��o de informa��o no UI: Posi��o dos FullBody e Sprite dos Fullbody nas posi��es.
        DialogUIManager.instance.CharactersOnDisplay(FullBodyToDisplay, FullBodyPosition);

        //questoes para ui
        DialogUIManager.instance.QuestionsToUi(_questionToUi1, _questionToUi2, _questionToUi3);

      
  
    }

    #endregion

    #region Escolhas e Ramifica��es

    private void ChoseNextBrench()
    {
        
       
            _dialogueCanChange = false;
            _dialogNumber = DialogueTree.DialogueStr.Length;
           
                _chosesButtons.SetActive(true);
        
    }

    public void ChoiseChangeDialogue()
    {
        ChangeDialogue();

        if (_dialogTreeNumber <= myDialogTree.Length)
        {
            _dialogNumber = 0;
            _positionInDialog = -1;
            _dialogueCanChange = true;


        }
        _chosesButtons.SetActive(false);
    }

   
    public void ChoiseChange1()
    {
        if( _dialogueCanChange == false)
        {
            _dialogTreeNumber = DialogueTree.ChoiseDialogueToChange1;
             TrustValue += DialogueTree.TrustValueToIncrese1;
        }
    }
    public void ChoiseChange2()
    {
        if (_dialogueCanChange == false)
        {
            _dialogTreeNumber = DialogueTree.ChoiseDialogueToChange2;
            TrustValue += DialogueTree.TrustValueToIncrese2;
        }
    }
    public void ChoiseChange3()
    {
        if (_dialogueCanChange == false)
        {
            _dialogTreeNumber = DialogueTree.ChoiseDialogueToChange3;
            TrustValue += DialogueTree.TrustValueToIncrese3;
        }
    }

    #endregion

    #region Controlador dos Dialogos
    public void ChangeDialogue()
    {
        if (_dialogueCanChange == true)
        {
            if(DialogUIManager.instance.typingeffectCoroutine == null)
            {
                _positionInDialog++;
            }
           

            if (_positionInDialog >= DialogueTree.DialogueStr[_dialogNumber].DialogueMessages.Length)
            {
                _positionInDialog = 0;

                _dialogNumber++;

                if (_dialogNumber >= DialogueTree.DialogueStr.Length && DialogueTree.IsEndDialogue == false)
                {
                    if (DialogueTree.ChangeBrench == false)
                    {
                        _dialogNumber = 0;

                        if (_dialogTreeNumber < myDialogTree.Length) //avan�a a posi��o nos dialogos principais
                        {
                            _dialogTreeNumber++;
                        }                      

                    }
                    else
                    {
                        ChoseNextBrench();

                    }
                }
                else if(_dialogNumber >= DialogueTree.DialogueStr.Length && DialogueTree.IsEndDialogue == true)
                {
  
                   SceneManager.LoadScene("NavigationSystem");
                                      
                }
            }

            UpdateOnUI();

        }
    }
    #endregion
}
