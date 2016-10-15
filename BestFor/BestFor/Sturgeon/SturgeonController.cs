using BestFor.Domain.Entities;
using BestFor.Controllers;
using BestFor.Dto;
using BestFor.Services.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
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
            var model = LoadAll();
            return View(model);
        }

        [HttpGet]
        public IActionResult Admin()
        {
            var model = LoadAll();
            return View(model);
        }

        [HttpGet]
        public IActionResult Team(string id)
        {
            var model = new TeamModel();
            model.TeamName = id;
            // Load scores
            using (SqlConnection con = new SqlConnection(_appSettings.Value.DatabaseConnectionString))
            {
                con.Open();
                try
                {
                    using (SqlCommand command = new SqlCommand("select id from sturgeonteams where name = @name", con))
                    {
                        command.Parameters.Add(new SqlParameter("name", model.TeamName));
                        var reader = command.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                model.TeamId = reader.GetInt32(0);
                            }
                        }
                        reader.Close();
                    }

                    using (SqlCommand command = new SqlCommand("select slot, score from sturgeonscores where team_id = @team_id", con))
                    {
                        command.Parameters.Add(new SqlParameter("team_id", model.TeamId));
                        var reader = command.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                model.SetScore(reader.GetInt32(0), reader.GetInt32(1));
                            }
                        }
                        reader.Close();
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return View(model);
        }

        [HttpPost]
        public IActionResult Team(TeamModel model)
        {
            // Load scores
            using (SqlConnection con = new SqlConnection(_appSettings.Value.DatabaseConnectionString))
            {
                con.Open();
                try
                {
                    string password = null;
                    using (SqlCommand command = new SqlCommand("select id, secret_string from sturgeonteams where name = @name", con))
                    {
                        command.Parameters.Add(new SqlParameter("name", model.TeamName));
                        var reader = command.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                model.TeamId = reader.GetInt32(0);
                                password = reader.GetString(1);
                            }
                        }
                        reader.Close();
                    }

                    if (model.Password != password)
                    {
                        model.ErrorMessage = "Invalid password";
                        return View(model);
                    }
                    

                    using (SqlCommand command = new SqlCommand(@"if exists(select * from sturgeonscores where team_Id = @teamId and slot = @slot)
begin

    update sturgeonscores set score = @score where team_Id = @teamId and slot = @slot
end
else
begin

    insert sturgeonscores(team_id, slot, score) values(@teamId, @slot, @score)
end", con))
                    {
                        var parameterTeamId = command.Parameters.Add(new SqlParameter("teamId", model.TeamId));
                        var parameterSlot = command.Parameters.Add(new SqlParameter("slot", model.TeamId));
                        var parameterScore = command.Parameters.Add(new SqlParameter("score", model.TeamId));

                        // Update scores
                        for (int i = 1; i < 11; i++)
                        {
                            parameterSlot.Value = i;
                            parameterScore.Value = model.GetScore(i);
                            command.ExecuteNonQuery();
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return RedirectToAction("Index", "Sturgeon");
        }

        private TeamsModel LoadAll()
        {
            var model = new TeamsModel();

            using (SqlConnection con = new SqlConnection(_appSettings.Value.DatabaseConnectionString))
            {
                con.Open();
                try
                {
                    // Load teams
                    using (SqlCommand command = new SqlCommand("select id, name, secret_string from sturgeonteams", con))
                    {
                        var reader = command.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                model.Teams.Add(new TeamModel()
                                {
                                    TeamName = reader["name"].ToString(),
                                    Password = reader["secret_string"].ToString(),
                                    TeamId = reader.GetInt32(0)
                                });
                            }
                        }
                        reader.Close();
                    }

                    // Load scores
                    using (SqlCommand command = new SqlCommand("select team_id, slot, score from sturgeonscores", con))
                    {
                        var reader = command.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                var team = model.Teams.First(x => x.TeamId == reader.GetInt32(0));
                                team.SetScore(reader.GetInt32(1), reader.GetInt32(2));
                            }
                        }
                        reader.Close();
                    }

                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            return model;
        }
    }
}
