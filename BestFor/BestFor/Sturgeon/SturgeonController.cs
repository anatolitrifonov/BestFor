using BestFor.Domain.Entities;
using BestFor.Controllers;
using BestFor.Dto;
using BestFor.Services.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using BestFor.Common;
using System.Data.SqlClient;

namespace BestFor.Sturgeon
{
    /// <summary>
    /// Allows user to vote for answers and answer descriptions.
    /// 
    /// This filter will parse the culture from the URL and set it into Viewbag.
    /// Controller has to inherit BaseApiController in order for filter to work correctly.
    /// </summary>
    [ServiceFilter(typeof(LanguageActionFilter))]
    public class SturgeonController : BaseApiController
    {
        private readonly IOptions<AppSettings> _appSettings;

        public SturgeonController(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var model = new TeamsModel();

            using (SqlConnection con = new SqlConnection(_appSettings.Value.DatabaseConnectionString))
            {
                con.Open();
                try
                {
                    using (SqlCommand command = new SqlCommand("select * from sturgeonteams", con))
                    {
                        var reader = command.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                model.Teams.Add(new TeamModel()
                                {
                                    TeamName = reader["name"].ToString(),
                                    Password = reader["secret_string"].ToString()
                                });
                            }
                        }
                    }
                }
                catch(Exception ex)
                {
                    throw ex;
                }
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Team(string id)
        {
            var model = new TeamModel();
            model.TeamName = id;

            return View(model);
        }

    }
}
