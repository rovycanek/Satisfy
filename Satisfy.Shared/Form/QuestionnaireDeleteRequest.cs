namespace Satisfy.Shared.Form
{
    public class QuestionnaireDeleteRequest
    {
        public QuestionnaireGet Questionnaire { get; set; } = new QuestionnaireGet();
        public class QuestionnaireGet
        {
            public int QuestionnaireId { get; set; }
        }
    }
}