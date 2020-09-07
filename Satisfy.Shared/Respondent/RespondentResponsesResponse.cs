using System.Collections.Generic;

namespace Satisfy.Shared.Respondent
{
    public class RespondentResponsesResponse
    {
        public ResponsesList Responses { get; set; } = new ResponsesList();
        public class ResponsesList
        {
            public List<List<string>> CsvList { get; set; } = new List<List<string>>();
        }
    }
}