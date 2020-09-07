namespace Satisfy.Shared.Form
{
    public class QuestionnaireIDRequest
    {
        public int RespondentID { get; set; }

        public QuestionnaireIDRequest(int id)
        {
            RespondentID = id;
        }
        public QuestionnaireIDRequest() { }
    }
}