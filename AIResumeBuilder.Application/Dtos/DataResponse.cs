using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIResumeBuilder.Application.Dtos
{
    public class DataResponse<T> : BaseResponse
    {
        public T? Data { get; set; }
    }
}
