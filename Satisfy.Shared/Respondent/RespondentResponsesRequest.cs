namespace Satisfy.Shared.Respondent
{
    public class RespondentResponsesRequest
    {
        public QuestionnaireResponses Questionnaire { get; set; } = new QuestionnaireResponses();
        public class QuestionnaireResponses
        {
            public int QuestionnaireId { get; set; }
        }
    }
}