namespace Satisfy.Shared.Form
{
    public class QuestionnairePublishResponse
    {
        public QuestionnairePublish Questionnaire { get; set; } = new QuestionnairePublish();
        public class QuestionnairePublish
        {
            public string status { get; set; }
        }
    }
}