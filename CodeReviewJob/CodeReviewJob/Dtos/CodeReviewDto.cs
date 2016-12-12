using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeReviewJob.Dtos
{
    public class CodeReviewDto
    {
        public Employee Reviewer { get; set; }

        public Employee Fixer { get; set; }

        public DateTime ReviewDate { get; set; }
    }
}
