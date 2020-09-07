namespace Satisfy.Shared.Form
{
    public class QuestionnaireGetRequest
    {
        public QuestionnaireGet Questionnaire { get; set; } = new QuestionnaireGet();
        public class QuestionnaireGet
        {
            public int QuestionnaireId { get; set; }
        }
    }
}