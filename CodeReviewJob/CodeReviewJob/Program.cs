﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodeReviewJob
{
    class Program
    {
        static void Main(string[] args)
        {
            var bootstrap = new SchedulerBootstrap();
            bootstrap.Run();
        }
    }
}
