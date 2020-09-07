using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Satisfy.Api.Controllers;
using Satisfy.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Satisfy.Shared
{
    public class DbModel : DbContext
    {
        public DbSet<Answer> Answers { get; set; }
        public DbSet<Api.Models.Contact> Contacts { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Questionnaire> Questionnaires { get; set; }
        public DbSet<Api.Models.Respondent> Respondents { get; set; }
        public DbSet<Response> Responses { get; set; }
        public DbSet<Api.Models.User> Users { get; set; }
       

        public DbModel(DbContextOptions<DbModel> options) : base(options)
        {

        }

        public DbModel()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                @"Server = v0vtv.database.windows.net; Database = Satisfy; User Id = satisfy; Password = v0vtvSat1sfy");
        }
    }
}
