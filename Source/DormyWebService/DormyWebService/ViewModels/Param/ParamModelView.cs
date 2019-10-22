using System;

namespace DormyWebService.ViewModels.Param
{
    public class ParamModelView
    {
        public int ParamId { get; set; }
        public int ParamTypeId { get; set; }

        public string Name { get; set; }

        public int Value { get; set; }

        public string TextValue { get; set; }

        public DateTime TimeValue { get; set; }
    }
}