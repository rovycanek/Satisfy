namespace Satisfy.Shared.Form
{
    public class QuestionnaireDeleteResponse
    {
        public QuestionnaireDelete Questionnaire { get; set; } = new QuestionnaireDelete();
        public class QuestionnaireDelete
        {
            public bool Success { get; set; }
        }
    }
}