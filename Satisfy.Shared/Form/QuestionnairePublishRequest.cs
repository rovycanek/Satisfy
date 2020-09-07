namespace Satisfy.Shared.Form
{
    public class QuestionnairePublishRequest
    {
        public QuestionnairePublish Questionnaire { get; set; } = new QuestionnairePublish();
        public class QuestionnairePublish
        {
            public int QuestionnaireId { get; set; }
        }
    }
}