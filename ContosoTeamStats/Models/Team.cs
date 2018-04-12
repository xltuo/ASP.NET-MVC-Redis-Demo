using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.SqlServer;

namespace ContosoTeamStats.Models
{
    public class Team
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Wins { get; set; }
        public int Losses { get; set; }
        public int Ties { get; set; }

        public static void PlayGames(IEnumerable<Team> teams)
        {
            // Simple random generation of statistics.
            var r = new Random();

            foreach (var t in teams)
            {
                t.Wins = r.Next(33);
                t.Losses = r.Next(33);
                t.Ties = r.Next(0, 5);
            }
        }
    }
    public class TeamContext : DbContext
    {
        public TeamContext()
            : base("TeamContext")
        {
        }

        public DbSet<Team> Teams { get; set; }
    }
    public class TeamInitializer : CreateDatabaseIfNotExists<TeamContext>
    {
        protected override void Seed(TeamContext context)
        {
            var teams = new List<Team>();
            for (int i = 0; i < 100; i++)
            {
                teams.Add(new Team { Name = "Adventure Works Cycles" + i.ToString() });
                teams.Add(new Team { Name = "Alpine Ski House" + i.ToString() });
                teams.Add(new Team { Name = "Blue Yonder Airlines" + i.ToString() });
                teams.Add(new Team { Name = "Coho Vineyard" + i.ToString() });
                teams.Add(new Team { Name = "Contoso, Ltd." + i.ToString() });
                teams.Add(new Team { Name = "Fabrikam, Inc." + i.ToString() });
                teams.Add(new Team { Name = "Lucerne Publishing" + i.ToString() });
                teams.Add(new Team { Name = "Northwind Traders" + i.ToString() });
                teams.Add(new Team { Name = "Consolidated Messenger" + i.ToString() });
                teams.Add(new Team { Name = "Fourth Coffee" + i.ToString() });
                teams.Add(new Team { Name = "Graphic Design Institute" + i.ToString() });
                teams.Add(new Team { Name = "Nod Publishers" + i.ToString() });
            }
            Team.PlayGames(teams);
            teams.ForEach(t => context.Teams.Add(t));
            context.SaveChanges();
        }
    }

    public class TeamConfiguration : DbConfiguration
    {
        public TeamConfiguration()
        {
            SetExecutionStrategy("System.Data.SqlClient", () => new SqlAzureExecutionStrategy());
        }
    }

}