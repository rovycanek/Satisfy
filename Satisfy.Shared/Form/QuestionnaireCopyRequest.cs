namespace Satisfy.Shared.Form
{
    public class QuestionnaireCopyRequest
    {
        public QuestionnaireCopy Questionnaire { get; set; } = new QuestionnaireCopy();
        public class QuestionnaireCopy
        {
            public int QuestionnaireId { get; set; }
        }
    }
}