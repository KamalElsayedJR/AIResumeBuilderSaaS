using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIResumeBuilder.Application.Dtos.Resume
{
    public class UpdateResumeDto 
    {
        [DataType(DataType.Text)]
        public string? Title { get; set; }
        [DataType(DataType.Text)]
        public string? Summray { get; set; }
    }
}
