using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using Newtonsoft.Json;

namespace Marksheet
{
    /// <summary>
    /// Summary description for Sample
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
     [System.Web.Script.Services.ScriptService]
    public class Sample : System.Web.Services.WebService
    {
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = true)]
        public string calculationOfMarksheet()
        {
            string subStr = HttpContext.Current.Request.Params["request"];
            List<SubjectModel> subjects = JsonConvert.DeserializeObject<List<SubjectModel>>(subStr);

            double totalMarks = 0;
            double minMarks = subjects[0].obtainedMarks;
            string minMarksSub = subjects[0].name;
            double maxMarks = subjects[0].obtainedMarks;
            string maxMarksSub = subjects[0].name;
            for (int i = 0; i < subjects.Count; i++)
            {
                totalMarks += subjects[i].obtainedMarks;
                if (minMarks > subjects[i].obtainedMarks)
                {
                    minMarks = subjects[i].obtainedMarks;
                    minMarksSub = subjects[i].name;
                }

                if (maxMarks < subjects[i].obtainedMarks)
                {
                    maxMarks = subjects[i].obtainedMarks;
                    maxMarksSub = subjects[i].name;
                }
            }

            double percentage = (totalMarks / (subjects.Count * 100)) * 100;

            MarksheetModel marksheetModel = new MarksheetModel();
            marksheetModel.Percentage = percentage;
            marksheetModel.MinMarks = minMarks;
            marksheetModel.MaxMarks = maxMarks;
            marksheetModel.MinSubjectMarks = minMarksSub;
            marksheetModel.MaxSubjectMarks = maxMarksSub;

            string str = JsonConvert.SerializeObject(marksheetModel);
            return str;

        }

        public class MarksheetModel
        {
            public double MinMarks { get; set; }
            public double MaxMarks { get; set; }
            public string MinSubjectMarks { get; set; }
            public string MaxSubjectMarks { get; set; }
            public double Percentage { get; set; }
        }
        public class SubjectModel
        {
            public string name { get; set; }
            public double obtainedMarks { get; set; }
        }

    }
}
