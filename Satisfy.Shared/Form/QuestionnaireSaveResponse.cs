namespace Satisfy.Shared.Form
{
    public class QuestionnaireSaveResponse
    {
        public QuestionnairePublish Questionnaire { get; set; } = new QuestionnairePublish();
        public class QuestionnairePublish
        {
            public bool Success { get; set; }
        }
    }
}