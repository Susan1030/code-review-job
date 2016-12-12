using CodeReviewJob.Dtos;
using Dapper;
using sdmap.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeReviewJob
{
    public static class DataSource
    {
        public static List<Employee> GetFrontendDevelopers(bool isLeader)
        {
            using (var connection = DbUtil.GetConnection())
            {
                return connection
                    .QueryById<Employee>(nameof(GetFrontendDevelopers), new { IsLeader = isLeader })
                    .AsList();
            }
        }

        private static bool IsDateCreated(DateTime date)
        {
            using (var connection = DbUtil.GetConnection())
            {
                return connection
                    .ExecuteScalarById<bool>(nameof(IsDateCreated), new { Date = date });
            }
        }

        private static List<CodeReviewDto> GetCodeReviewByDate(DateTime date)
        {
            using (var connection = DbUtil.GetConnection())
            {
                return connection
                    .QueryById<CodeReviewDto, Employee, Employee, CodeReviewDto>(
                        nameof(GetCodeReviewByDate), 
                        (dto, e1, e2) =>
                        {
                            dto.Reviewer = e1;
                            dto.Fixer = e2;
                            return dto;
                        }, 
                        new { Date = date })
                    .AsList();
            }
        }

        private static List<CodeReviewDto> GenerateNewCodeReviewGroups(
            IList<Employee> frontEndDevelopers, 
            DateTime date, int groupCount)
        {
            return frontEndDevelopers
                .Sample(groupCount * 2)
                .ToObservable()
                .Buffer(2)
                .Select(x => new CodeReviewDto
                {
                    Fixer = x[0], 
                    Reviewer = x[1], 
                    ReviewDate = date
                })
                .ToEnumerable()
                .ToList();
        }

        private static void SaveReviewerGroups(IEnumerable<CodeReviewDto> dto)
        {
            using (var connection = DbUtil.GetConnection())
            {
                connection.ExecuteById(nameof(SaveReviewerGroups), dto.Select(x => new
                {
                    ReviewerId = x.Reviewer.Id, 
                    FixerId = x.Fixer.Id,
                    ReviewDate = x.ReviewDate
                }));
            }
        }

        public static List<CodeReviewDto> GetOrCreateCodeReviewGroup(DateTime date)
        {
            if (IsDateCreated(date))
            {
                Console.WriteLine("Data created, fetch from DB...");
                return GetCodeReviewByDate(date);
            }
            else
            {
                Console.WriteLine("Data not found, generate new...");
                var codeReviewDto = GenerateNewCodeReviewGroups(
                    GetFrontendDevelopers(isLeader: false), 
                    date, 
                    AppConfig.ReviewerGroupCount);

                Console.WriteLine(string.Join(",", codeReviewDto
                    .SelectMany(x => new[] { x.Reviewer.Name, x.Fixer.Name })));
                SaveReviewerGroups(codeReviewDto);

                return codeReviewDto;
            }
        }
    }
}
