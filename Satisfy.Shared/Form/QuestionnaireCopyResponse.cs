namespace Satisfy.Shared.Form
{
    public class QuestionnaireCopyResponse
    {
        public QuestionnaireCopy Questionnaire { get; set; } = new QuestionnaireCopy();
        public class QuestionnaireCopy
        {
            public bool Success { get; set; }
        }
    }
}