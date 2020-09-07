namespace Satisfy.Shared.Respondent
{
    public class RespondentResponseRequest
    {
        public int QuestionnaireId { get; set; }
        public int QuestionId { get; set; }
        public string TextOfRespondent { get; set; }
        public int RespondentId { get; set; }
        public int AnswerId { get; set; }

        public RespondentResponseRequest(int questionnaireId, int questionId, string textOfRespondent, int respondentId, int answerId)
        {
            this.QuestionnaireId = questionnaireId;
            this.QuestionId = questionId;
            this.TextOfRespondent = textOfRespondent;
            this.RespondentId = respondentId;
            this.AnswerId = answerId;
        }
        public RespondentResponseRequest() { }

    }
}