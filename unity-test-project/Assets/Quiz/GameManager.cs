using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using GraphQL;

public class GameManager : MonoBehaviour
{
    private static List<Question> unansweredQuestions;
    private Question currentQuestion;

    [SerializeField]
    private string graphQLEndpoint;
    private GraphQLClient graphQLClient;

    [SerializeField]
    private Text factText;

    [SerializeField]
    private Text trueAnswerText;

    [SerializeField]
    private Text falseAnswerText;

    [SerializeField]
    private Animator animator;

    [SerializeField]
    private float timeBetweenQuestions = 1f;

    string QuestionsQuery = @"query { questions { fact isTrue } }";

    void Start()
    {
        Debug.Log("Starting!");
        if (unansweredQuestions == null || unansweredQuestions.Count == 0)
        {
            if (graphQLClient == null)
            {
                graphQLClient = new GraphQLClient(graphQLEndpoint);
            }
            graphQLClient.Query(QuestionsQuery, callback: QuestionsCallback);
            return;
        }
        SetCurrentQuestion();
    }

    void QuestionsCallback(GraphQLResponse response)
    {
        if (response.Exception != null)
        {
            factText.text = "Failed to get questions from GraphQL server: " + response.Exception;
            return;
        }

        unansweredQuestions = response.GetList<Question>("questions");
        SetCurrentQuestion();
    }

    void SetCurrentQuestion()
    {
        int randomQuestionIndex = Random.Range(0, unansweredQuestions.Count);
        currentQuestion = unansweredQuestions[randomQuestionIndex];

        factText.text = currentQuestion.fact;

        if (currentQuestion.isTrue)
        {
            trueAnswerText.text = "CORRECT";
            falseAnswerText.text = "WRONG";
        }
        else
        {
            trueAnswerText.text = "WRONG";
            falseAnswerText.text = "CORRECT";
        }
    }

    IEnumerator TransitionToNextQuestion()
    {
        unansweredQuestions.Remove(currentQuestion);
        yield return new WaitForSeconds(timeBetweenQuestions);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void UserSelectTrue()
    {
        animator.SetTrigger("True");

        if (currentQuestion.isTrue)
        {
            Debug.Log("CORRECT!");
        }
        else
        {
            Debug.Log("WRONG!");
        }

        StartCoroutine(TransitionToNextQuestion());
    }
    public void UserSelectFalse()
    {
        animator.SetTrigger("False");

        if (currentQuestion.isTrue)
        {
            Debug.Log("WRONG!");
        }
        else
        {
            Debug.Log("CORRECT!");
        }
        StartCoroutine(TransitionToNextQuestion());
    }
}
